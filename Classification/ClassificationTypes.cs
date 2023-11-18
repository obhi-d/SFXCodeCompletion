using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace SFXCodeCompletion.Classification
{
    internal class ClassificationTypes
    {
        public const string Function = nameof(glslFunction);
        public const string Keyword = nameof(glslKeyword);
        public const string Variable = nameof(glslVariable);
        public const string Command = nameof(sfxCommand);
        public const string SectionName = nameof(sfxSectionName);


        [Export]
        [Name(Function)]
        [BaseDefinition(PredefinedClassificationTypeNames.Identifier)]
        private static readonly ClassificationTypeDefinition glslFunction;

        [Export]
        [Name(Keyword)]
        [BaseDefinition(PredefinedClassificationTypeNames.Keyword)]
        private static readonly ClassificationTypeDefinition glslKeyword;

        [Export]
        [Name(Variable)]
        [BaseDefinition(PredefinedClassificationTypeNames.Identifier)]
        private static readonly ClassificationTypeDefinition glslVariable;

        [Export]
        [Name(Command)]
        [BaseDefinition(PredefinedClassificationTypeNames.Identifier)]
        private static readonly ClassificationTypeDefinition sfxCommand;

        [Export]
        [Name(SectionName)]
        [BaseDefinition(PredefinedClassificationTypeNames.MarkupNode)]
        private static readonly ClassificationTypeDefinition sfxSectionName;
    }
}
