using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Newtonsoft.Json.Linq;
using SFXCodeCompletion.Classification;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SFXCodeCompletion.Parser
{
  public enum SfxTokenType
  {
    None,
    Command,
    GlslSection,
    MetaSection,
    Type,
    Function,
    Identifier,
    Keyword,
    Number,
    Operator,
    Preprocessor,
    Variable,
    QuotedString,
    UserFunction,
    Comment
  }

  public class Lexer
  {

    private IClassificationType Comment { get; }
    private IClassificationType Identifier { get; }
    private IClassificationType Number { get; }
    private IClassificationType Operator { get; }
    private IClassificationType QuotedString { get; }
    private IClassificationType PreprocessorKeyword { get; }
    private IClassificationType Function { get; }
    private IClassificationType Keyword { get; }
    private IClassificationType Command { get; }
    private IClassificationType GlslSection { get; }
    private IClassificationType MetaSection { get; }
    private IClassificationType Variable { get; }
    private IClassificationType UserFunction { get; }
    private IClassificationType BuiltinType { get; }

    private IClassificationType Convert(SfxTokenType token)
    {
      switch (token)
      {
        case SfxTokenType.Comment: return Comment;
        case SfxTokenType.Function: return Function;
        case SfxTokenType.Keyword: return Keyword;
        case SfxTokenType.Number: return Number;
        case SfxTokenType.Operator: return Operator;
        case SfxTokenType.Preprocessor: return PreprocessorKeyword;
        case SfxTokenType.Variable: return Variable;
        case SfxTokenType.Identifier: return Identifier;
        case SfxTokenType.GlslSection: return GlslSection;
        case SfxTokenType.MetaSection: return MetaSection;
        case SfxTokenType.QuotedString: return QuotedString;
        case SfxTokenType.Command: return Command;
        case SfxTokenType.Type: return BuiltinType;
        case SfxTokenType.UserFunction: return UserFunction;
        default:
          return Identifier;
      }
    }

    public Lexer(IClassificationTypeRegistryService classificationTypeRegistry)
    {
      Comment = classificationTypeRegistry.GetClassificationType(ClassificationTypes.Comment);
      Identifier = classificationTypeRegistry.GetClassificationType(ClassificationTypes.GlslIdentifier);
      Number = classificationTypeRegistry.GetClassificationType(ClassificationTypes.GlslNumber);
      Operator = classificationTypeRegistry.GetClassificationType(ClassificationTypes.Operator);
      QuotedString = classificationTypeRegistry.GetClassificationType(ClassificationTypes.String);
      PreprocessorKeyword = classificationTypeRegistry.GetClassificationType(ClassificationTypes.GlsPreprocessor);

      Function = classificationTypeRegistry.GetClassificationType(ClassificationTypes.GlslFunction);
      Keyword = classificationTypeRegistry.GetClassificationType(ClassificationTypes.GlslKeyword);
      Command = classificationTypeRegistry.GetClassificationType(ClassificationTypes.Command);
      GlslSection = classificationTypeRegistry.GetClassificationType(ClassificationTypes.GlslSectionName);
      MetaSection = classificationTypeRegistry.GetClassificationType(ClassificationTypes.MetaSectionName);
      Variable = classificationTypeRegistry.GetClassificationType(ClassificationTypes.GlslVariable);
      BuiltinType = classificationTypeRegistry.GetClassificationType(ClassificationTypes.GlslBuiltinType);
      UserFunction = classificationTypeRegistry.GetClassificationType(ClassificationTypes.GlslUserFunction);

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

    List<ClassificationSpan> classificationSpans = null;
    ITextSnapshot currentSnapshot = null;
    SfxTokenType lastToken = SfxTokenType.None;

    public void Add(int start, int length, SfxTokenType type)
    {
      lastToken = type;
      classificationSpans.Add(new ClassificationSpan(new SnapshotSpan(currentSnapshot, start, length), Convert(type)));
    }

    public List<ClassificationSpan> Tokenize(ITextSnapshot snapshot, bool startInGlsl)
    {
      classificationSpans = new List<ClassificationSpan>();
      currentSnapshot = snapshot;
      string input = snapshot.GetText();
      int index = 0;
      int length = 0;
      bool inGlsl = startInGlsl;
      lastToken = SfxTokenType.None;

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
              Add(index, sectionEndIndex - index, inGlsl ? SfxTokenType.GlslSection : SfxTokenType.MetaSection);
              index = sectionEndIndex;
            }
            else
            {
              Add(index, 1, SfxTokenType.Operator);
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

              Add(index, directiveEndIndex - index, SfxTokenType.Preprocessor);
              index = directiveEndIndex;
            }
            else
            {
              Add(index, 1, SfxTokenType.Operator);
              index++;
            }
            break;
          case '(':
            if (lastToken == SfxTokenType.Identifier && inGlsl)
            {
              var last = classificationSpans.Last();
              classificationSpans[classificationSpans.Count - 1] = new ClassificationSpan(last.Span, Convert(SfxTokenType.UserFunction));
            }
            Add(index, 1, SfxTokenType.Operator);
            index++;
            break;
          case char c when IsOperator(c):
            Add(index, 1, SfxTokenType.Operator);
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
            Add(index, length, SfxTokenType.QuotedString);
            index = endIndex + 1;
            break;
          case '.':
            // Check if dot is part of a number or an operator
            if (!IsPartOfNumber(index, input))
            {
              Add(index, 1, SfxTokenType.Operator);
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
            bool hex = false;

            while (numEndIndex < input.Length && (char.IsDigit(input[numEndIndex]) ||
                (input[numEndIndex] == '.' && !hasDot) ||
                input[numEndIndex] == 'e' ||
                input[numEndIndex] == 'E' ||
                (input[numEndIndex] == 'x' || input[numEndIndex] == 'X' && numEndIndex > 0 && input[numEndIndex - 1] == '0') ||
                (input[numEndIndex] == '-' && numEndIndex > 0 && (input[numEndIndex - 1] == 'e' || input[numEndIndex - 1] == 'E'))) ||
                (hex && "ABCDEFabcdef".Contains(input[numEndIndex]))
                )

            {
              if (input[numEndIndex] == '.')
                hasDot = true;
              if (input[numEndIndex] == 'x')
                hex = true;
              numEndIndex++;
            }

            Add(index, numEndIndex - index, SfxTokenType.Number);
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
              Add(index, length, SfxTokenType.Comment);
              index = lineCommentEnd + 1;
            }
            else if (index + 1 < input.Length && input[index + 1] == '*')
            {
              // Block Comment
              int blockCommentEnd = input.IndexOf("*/", index + 2);
              length = (blockCommentEnd + 2) - index;
              if (blockCommentEnd == -1)
              {
                // Unterminated block comment
                blockCommentEnd = input.Length - 2;
                length = input.Length - index;
              }
              Add(index, length, SfxTokenType.Comment);
              index = blockCommentEnd + 2;
            }
            else
            {
              // Division operator
              Add(index, 1, SfxTokenType.Operator);
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
            Add(index, length, inGlsl ? KnownTokens.GetKnownTokenType(input.Substring(index, length)) : KnownTokens.GetCommandType(input.Substring(index, length)));
            index = identifierEndIndex;
            break;

          case char c when char.IsWhiteSpace(c):
            // Skip whitespaces
            index++;
            break;
          default:
            // Skip 
            index++;
            break;
        }
      }

      return classificationSpans;
    }

    private static bool IsPartOfNumber(int currentIndex, string input)
    {
      // Check if the dot is part of a number by looking ahead in the input string
      int nextIndex = currentIndex + 1;
      return (nextIndex < input.Length && char.IsDigit(input[nextIndex]));

    }

  }
}
