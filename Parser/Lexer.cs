using Newtonsoft.Json.Linq;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFXCodeCompletion.Parser
{

    public class Lexer
    {
        private static readonly Parser<string> NumberWithTrailingDigit = from number in Parse.Number
                                                                         from trailingDot in Parse.Char('.')
                                                                         select number + trailingDot;

        private static readonly Parser<string> ParserNumber = Parse.DecimalInvariant.Or(NumberWithTrailingDigit);
        private static readonly Parser<string> ParserComment = new CommentParser().AnyComment;
        private static readonly Parser<string> ParserString = from start in Parse.Char('"')
                                                              from text in Parse.CharExcept("\"\r\n").Many().Text()
                                                              from end in Parse.Char('"').Optional()
                                                              select start + text + end;
        private static readonly Parser<string> ParserPreprocessor = from _ in Parse.Char('#')
                                                                    from rest in Parse.LetterOrDigit.Many().Text()
                                                                    select rest;
        private static readonly Parser<string> ParserSection =
            from term in Parse.String("--")
            from content in Parse.CharExcept("\r\n").Many().Text()
            select term + content;

        private static readonly Parser<string> ParserIdentifier = Parse.Identifier(Parse.Char(KnownTokens.IsIdentifierStartChar, "Identifier start"),
                                                                            Parse.Char(KnownTokens.IsIdentifierChar, "Identifier character"));

        private static readonly Parser<char> ParserOperator = Parse.Chars("~.;,+-*/()[]{}<>=&$!%?:|^\\");

        private readonly Parser<IEnumerable<SfxToken>> tokenParser;

        public Lexer()
        {
            var section = ParserSection.Select(value => new SfxToken(SfxTokenType.Section, value));
            var comment = ParserComment.Select(value => new SfxToken(SfxTokenType.Comment, value));
            var quotedString = ParserString.Select(value => new SfxToken(SfxTokenType.QuotedString, value));
            var preprocessor = ParserPreprocessor.Select(value => new SfxToken(SfxTokenType.Preprocessor, value));
            var number = ParserNumber.Select(value => new SfxToken(SfxTokenType.Number, value));
            var identifier = ParserIdentifier.Select(value => new SfxToken(KnownTokens.GetKnownTokenType(value), value));
            var op = ParserOperator.Select(value => new SfxToken(SfxTokenType.Operator, value.ToString()));
            var token = section.Or(comment).Or(preprocessor).Or(quotedString).Or(number).Or(identifier).Or(op);
            tokenParser = token.Positioned().Token().XMany();
        }

        public IEnumerable<SfxToken> Tokenize(string text)
        {
            if (0 == text.Trim().Length) yield break;
            var tokens = tokenParser.TryParse(text);
            if (tokens.WasSuccessful)
            {
                foreach (var token in tokens.Value)
                {
                    yield return token;
                }
            }
        }
    }
}
