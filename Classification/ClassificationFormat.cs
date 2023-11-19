using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Windows.Media;


namespace SFXCodeCompletion.Classification
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationTypes.Function)]
    [Name(nameof(FunctionClassificationFormatDefinition))]
    //this should be visible to the end user
    [UserVisible(true)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class FunctionClassificationFormatDefinition : ClassificationFormatDefinition
    {
        public FunctionClassificationFormatDefinition()
        {
            DisplayName = "GLSL Function"; //human readable version of the name
            ForegroundColor = Color.FromRgb(0xff, 0xaf, 0x5f);
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationTypes.Keyword)]
    [Name(nameof(KeywordClassificationFormatDefinition))]
    //this should be visible to the end user
    [UserVisible(true)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class KeywordClassificationFormatDefinition : ClassificationFormatDefinition
    {
        public KeywordClassificationFormatDefinition()
        {
            DisplayName = "GLSL Keyword"; //human readable version of the name
            ForegroundColor = Color.FromRgb(0x5f, 0xaf, 0xff);
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationTypes.Variable)]
    [Name(nameof(VariableClassificationFormatDefinition))]
    //this should be visible to the end user
    [UserVisible(true)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class VariableClassificationFormatDefinition : ClassificationFormatDefinition
    {
        public VariableClassificationFormatDefinition()
        {
            DisplayName = "GLSL Variable"; //human readable version of the name
            ForegroundColor = Color.FromRgb(0x5f, 0xff, 0xff);
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationTypes.Command)]
    [Name(nameof(CommandClassificationFormatDefinition))]
    //this should be visible to the end user
    [UserVisible(true)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class CommandClassificationFormatDefinition : ClassificationFormatDefinition
    {
        public CommandClassificationFormatDefinition()
        {
            DisplayName = "SFX Command"; //human readable version of the name
            ForegroundColor = Color.FromRgb(0xf5, 0xa1, 0x0c);
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationTypes.GlslSectionName)]
    [Name(nameof(GlslSectionNameClassificationFormatDefinition))]
    //this should be visible to the end user
    [UserVisible(true)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class GlslSectionNameClassificationFormatDefinition : ClassificationFormatDefinition
    {
        public GlslSectionNameClassificationFormatDefinition()
        {
            DisplayName = "SFX GLSL Section Name"; //human readable version of the name
            ForegroundColor = Color.FromRgb(0xff, 0x14, 0x3f);
        }
    }


    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationTypes.MetaSectionName)]
    [Name(nameof(MetaSectionNameClassificationFormatDefinition))]
    //this should be visible to the end user
    [UserVisible(true)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class MetaSectionNameClassificationFormatDefinition : ClassificationFormatDefinition
    {
        public MetaSectionNameClassificationFormatDefinition()
        {
            DisplayName = "SFX Meta Section Name"; //human readable version of the name
            ForegroundColor = Color.FromRgb(0xaf, 0xa4, 0x3f);
        }
    }
}


