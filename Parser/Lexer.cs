using Microsoft.VisualStudio.Package;
using Newtonsoft.Json.Linq;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SFXCodeCompletion.Parser
{
    public class TokenList
    {
        string buffer;

        public TokenList(string ibuffer) { buffer = ibuffer; starts = new List<int>(); lengths = new List<int>(); types = new List<SfxTokenType>(); }

        public void Add(int start, int length, SfxTokenType type)
        {
            if (length <= 0) return;

            starts.Add(start);
            lengths.Add(length);
            types.Add(type);
        }

        public int Count() { return lengths.Count; }

        public List<int> starts;
        public List<int> lengths;
        public List<SfxTokenType> types;
    };

    public class Lexer
    {
        public Lexer()
        {
        }

        enum State
        {
            LineStart,
            TokenStart,
            Identifier,
            Number,
            Comment,
            BlockComment,
            String,
            MaybeSection
        };

        static public bool IsOperator(char c)
        {
            return "~;,+*()[]{}<>=&$!%?:|^\\#$".Contains(c);
        }

        private static int EndOfLine(string input, int index)
        {
            while (index < input.Length && input[index] != '\n')
                index++;
            return index;
        }

        private static bool TrimCheck(string check, string input, ref int index)
        {
            while (index < input.Length && input[index] != '\n')
            {
                if (char.IsWhiteSpace(input[index]))
                    index++;
                else
                    break;
            }

            if ((input.Length - index >= check.Length && input.Substring(index, check.Length) == check))
            {
                index += check.Length;
                return true;
            }
            return false;
        }
        private static bool IsLineBegin(string input, int index)
        {
            while (index > 0)
            {
                char last = input[index - 1];
                if (last == '\n') return true;
                if (!char.IsWhiteSpace(last)) return false;
                index--;
            }
            return (index == 0);
        }

        public TokenList Tokenize(string input)
        {
            var tokens = new TokenList(input);
            int index = 0;
            int length = 0;
            bool inGlsl = false;
            while (index < input.Length)
            {
                char currentChar = input[index];

                switch (currentChar)
                {
                    case '-':
                        int sectionEndIndex = index + 1;
                        if (IsLineBegin(input, index) && sectionEndIndex < input.Length && input[sectionEndIndex] == '-')
                        {
                            sectionEndIndex++;
                            if (TrimCheck("glsl", input, ref sectionEndIndex) && TrimCheck(":", input, ref sectionEndIndex))
                                inGlsl = true;
                            sectionEndIndex = EndOfLine(input, sectionEndIndex);
                            tokens.Add(index, sectionEndIndex - index, inGlsl ? SfxTokenType.GlslSection : SfxTokenType.MetaSection);
                            index = sectionEndIndex;
                        }
                        else
                        {
                            tokens.Add(index, 1, SfxTokenType.Operator);
                            index++;
                        }
                        break;
                    case '#':
                        if (IsLineBegin(input, index))
                        {
                            // Preprocessor Directive
                            int directiveEndIndex = index + 1;
                            while (directiveEndIndex < input.Length && !char.IsWhiteSpace(input[directiveEndIndex]))
                            {
                                directiveEndIndex++;
                            }

                            tokens.Add(index, directiveEndIndex - index, SfxTokenType.Preprocessor);
                            index = directiveEndIndex;
                        }
                        else
                        {
                            tokens.Add(index, 1, SfxTokenType.Operator);
                            index++;
                        }
                        break;
                    case char c when IsOperator(c):
                        tokens.Add(index, 1, SfxTokenType.Operator);
                        index++;
                        break;
                    case '"':
                        // Quoted String
                        int endIndex = input.IndexOf('"', index + 1);
                        length = endIndex - index;
                        if (endIndex == -1)
                        {
                            // Unterminated quoted string
                            endIndex = input.Length - 1;
                            length = input.Length - index;
                        }
                        tokens.Add(index, length, SfxTokenType.QuotedString);
                        index = endIndex + 1;
                        break;
                    case '.':
                        // Check if dot is part of a number or an operator
                        if (!IsPartOfNumber(tokens, index, input))
                        {
                            tokens.Add(index, 1, SfxTokenType.Operator);
                            index++;
                            break;
                        }
                        else
                            goto Parse_number;
                    case char c when char.IsDigit(c):
                    Parse_number:
                        // Number (integer or floating-point) with exponent notation
                        int numEndIndex = index;
                        bool hasDot = false;

                        while (numEndIndex < input.Length && (char.IsDigit(input[numEndIndex]) || (input[numEndIndex] == '.' && !hasDot) || input[numEndIndex] == 'e' || input[numEndIndex] == 'E' || (input[numEndIndex] == '-' && numEndIndex > 0 && (input[numEndIndex - 1] == 'e' || input[numEndIndex - 1] == 'E'))))
                        {
                            if (input[numEndIndex] == '.')
                            {
                                hasDot = true;
                            }

                            numEndIndex++;
                        }

                        tokens.Add(index, numEndIndex - index, SfxTokenType.Number);
                        index = numEndIndex;
                        break;

                    case '/':
                        // Check for comments or block comments
                        if (index + 1 < input.Length && input[index + 1] == '/')
                        {
                            // Line Comment
                            int lineCommentEnd = input.IndexOf('\n', index);
                            length = lineCommentEnd - index;
                            if (lineCommentEnd == -1)
                            {
                                // Unterminated block comment
                                lineCommentEnd = input.Length - 1;
                                length = input.Length - index;
                            }
                            index = lineCommentEnd + 1;
                            tokens.Add(index, length, SfxTokenType.Comment);
                        }
                        else if (index + 1 < input.Length && input[index + 1] == '*')
                        {
                            // Block Comment
                            int blockCommentEnd = input.IndexOf("*/", index + 2);
                            length = blockCommentEnd - index;
                            if (blockCommentEnd == -1)
                            {
                                // Unterminated block comment
                                blockCommentEnd = input.Length - 2;
                                length = input.Length - index;
                            }
                            index = blockCommentEnd + 2;
                            tokens.Add(index, length, SfxTokenType.Comment);
                        }
                        else
                        {
                            // Division operator
                            tokens.Add(index, 1, SfxTokenType.Operator);
                            index++;
                        }
                        break;

                    case char c when char.IsLetter(c) || c == '_':
                        // Identifier
                        int identifierEndIndex = index;
                        while (identifierEndIndex < input.Length && (char.IsLetterOrDigit(input[identifierEndIndex]) || input[identifierEndIndex] == '_'))
                        {
                            identifierEndIndex++;
                        }
                        length = identifierEndIndex - index;
                        tokens.Add(index, length, inGlsl ? KnownTokens.GetKnownTokenType(input.Substring(index, length)) : KnownTokens.GetCommandType(input.Substring(index, length)));
                        index = identifierEndIndex;
                        break;

                    case char c when char.IsWhiteSpace(c):
                        // Skip whitespaces
                        index++;
                        break;

                }
            }



            return tokens;
        }

        private static bool IsPartOfNumber(TokenList tokens, int currentIndex, string input)
        {
            // Check if the dot is part of a number by looking ahead in the input string
            int nextIndex = currentIndex + 1;
            return (nextIndex < input.Length && char.IsDigit(input[nextIndex]));

        }

    }
}
