using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Newtonsoft.Json.Linq;
using SFXCodeCompletion.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFXCodeCompletion.Classification
{
    internal class SyntaxColorParser
    {
        public SyntaxColorParser(IClassificationTypeRegistryService classificationTypeRegistry)
        {
            if (classificationTypeRegistry is null)
            {
                throw new System.ArgumentNullException(nameof(classificationTypeRegistry));
            }

            Comment = classificationTypeRegistry.GetClassificationType(PredefinedClassificationTypeNames.Comment);
            Identifier = classificationTypeRegistry.GetClassificationType(PredefinedClassificationTypeNames.Identifier);
            Number = classificationTypeRegistry.GetClassificationType(PredefinedClassificationTypeNames.Number);
            Operator = classificationTypeRegistry.GetClassificationType(PredefinedClassificationTypeNames.Operator);
            QuotedString = classificationTypeRegistry.GetClassificationType(PredefinedClassificationTypeNames.String);
            PreprocessorKeyword = classificationTypeRegistry.GetClassificationType(PredefinedClassificationTypeNames.PreprocessorKeyword);

            Function = classificationTypeRegistry.GetClassificationType(ClassificationTypes.Function);
            Keyword = classificationTypeRegistry.GetClassificationType(ClassificationTypes.Keyword);
            Command = classificationTypeRegistry.GetClassificationType(ClassificationTypes.Command);
            GlslSectionName = classificationTypeRegistry.GetClassificationType(ClassificationTypes.GlslSectionName);
            MetaSectionName = classificationTypeRegistry.GetClassificationType(ClassificationTypes.MetaSectionName);
            Variable = classificationTypeRegistry.GetClassificationType(ClassificationTypes.Variable);
            parser = new Parser.Lexer();
        }

        public delegate void ChangedEventHandler(object sender);
        public event ChangedEventHandler Changed;

        public IList<ClassificationSpan> CalculateSpans(SnapshotSpan snapshotSpan)
        {
            var output = new List<ClassificationSpan>();
            var text = snapshotSpan.GetText();
            var tokens = parser.Tokenize(text);
            for (int t = 0; t < tokens.Count(); ++t)
            {
                var lineSpan = new SnapshotSpan(snapshotSpan.Snapshot, tokens.starts[t], tokens.lengths[t]);
                output.Add(new ClassificationSpan(lineSpan, Convert(tokens.types[t])));
            }
            return output;
        }

        private readonly Parser.Lexer parser;
        private readonly Dictionary<string, IClassificationType> userKeywords = new Dictionary<string, IClassificationType>();

        private IClassificationType Comment { get; }
        private IClassificationType Identifier { get; }
        private IClassificationType Number { get; }
        private IClassificationType Operator { get; }
        private IClassificationType QuotedString { get; }
        private IClassificationType PreprocessorKeyword { get; }
        private IClassificationType Function { get; }
        private IClassificationType Keyword { get; }
        private IClassificationType Command { get; }
        private IClassificationType ParameterName { get; }
        private IClassificationType ParameterValue { get; }
        private IClassificationType GlslSectionName { get; }
        private IClassificationType MetaSectionName { get; }
        private IClassificationType Variable { get; }

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
                case SfxTokenType.GlslSection: return GlslSectionName;
                case SfxTokenType.MetaSection: return MetaSectionName;
                case SfxTokenType.QuotedString: return QuotedString;
                case SfxTokenType.Command: return Command;
                default:
                    return Identifier;
            }
        }
    }
}
