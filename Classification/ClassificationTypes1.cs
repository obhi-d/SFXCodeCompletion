





using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.StandardClassification;
using System.Windows.Media;

namespace SFXCodeCompletion.Classification
{
    internal class ClassificationTypes
    {

      public const string GlslSectionName = nameof(typeDefGlslSectionName);
      [Export]
      [Name(GlslSectionName)]
      [BaseDefinition(PredefinedClassificationTypeNames.MarkupAttribute)]
      private static readonly ClassificationTypeDefinition typeDefGlslSectionName;


      public const string GlslFunction = nameof(typeDefGlslFunction);
      [Export]
      [Name(GlslFunction)]
      [BaseDefinition(PredefinedClassificationTypeNames.Identifier)]
      private static readonly ClassificationTypeDefinition typeDefGlslFunction;


      public const string GlslKeyword = nameof(typeDefGlslKeyword);
      [Export]
      [Name(GlslKeyword)]
      [BaseDefinition(PredefinedClassificationTypeNames.Identifier)]
      private static readonly ClassificationTypeDefinition typeDefGlslKeyword;


      public const string GlslVariable = nameof(typeDefGlslVariable);
      [Export]
      [Name(GlslVariable)]
      [BaseDefinition(PredefinedClassificationTypeNames.Identifier)]
      private static readonly ClassificationTypeDefinition typeDefGlslVariable;


      public const string GlslBuiltinType = nameof(typeDefGlslBuiltinType);
      [Export]
      [Name(GlslBuiltinType)]
      [BaseDefinition(PredefinedClassificationTypeNames.Type)]
      private static readonly ClassificationTypeDefinition typeDefGlslBuiltinType;


      public const string GlslUserFunction = nameof(typeDefGlslUserFunction);
      [Export]
      [Name(GlslUserFunction)]
      [BaseDefinition(PredefinedClassificationTypeNames.Identifier)]
      private static readonly ClassificationTypeDefinition typeDefGlslUserFunction;


      public const string GlslIdentifier = nameof(typeDefGlslIdentifier);
      [Export]
      [Name(GlslIdentifier)]
      [BaseDefinition(PredefinedClassificationTypeNames.Identifier)]
      private static readonly ClassificationTypeDefinition typeDefGlslIdentifier;


      public const string GlslNumber = nameof(typeDefGlslNumber);
      [Export]
      [Name(GlslNumber)]
      [BaseDefinition(PredefinedClassificationTypeNames.Number)]
      private static readonly ClassificationTypeDefinition typeDefGlslNumber;


      public const string GlslPreprocessor = nameof(typeDefGlslPreprocessor);
      [Export]
      [Name(GlslPreprocessor)]
      [BaseDefinition(PredefinedClassificationTypeNames.PreprocessorKeyword)]
      private static readonly ClassificationTypeDefinition typeDefGlslPreprocessor;


      public const string Operator = nameof(typeDefOperator);
      [Export]
      [Name(Operator)]
      [BaseDefinition(PredefinedClassificationTypeNames.Operator)]
      private static readonly ClassificationTypeDefinition typeDefOperator;


      public const string String = nameof(typeDefString);
      [Export]
      [Name(String)]
      [BaseDefinition(PredefinedClassificationTypeNames.String)]
      private static readonly ClassificationTypeDefinition typeDefString;


      public const string Comment = nameof(typeDefComment);
      [Export]
      [Name(Comment)]
      [BaseDefinition(PredefinedClassificationTypeNames.Comment)]
      private static readonly ClassificationTypeDefinition typeDefComment;


      public const string Command = nameof(typeDefCommand);
      [Export]
      [Name(Command)]
      [BaseDefinition(PredefinedClassificationTypeNames.MarkupNode)]
      private static readonly ClassificationTypeDefinition typeDefCommand;


      public const string CommandParam = nameof(typeDefCommandParam);
      [Export]
      [Name(CommandParam)]
      [BaseDefinition(PredefinedClassificationTypeNames.MarkupAttribute)]
      private static readonly ClassificationTypeDefinition typeDefCommandParam;


      public const string MetaSectionName = nameof(typeDefMetaSectionName);
      [Export]
      [Name(MetaSectionName)]
      [BaseDefinition(PredefinedClassificationTypeNames.MarkupAttribute)]
      private static readonly ClassificationTypeDefinition typeDefMetaSectionName;


      public const string NewLine = nameof(typeDefNewLine);
      [Export]
      [Name(NewLine)]
      [BaseDefinition(PredefinedClassificationTypeNames.WhiteSpace)]
      private static readonly ClassificationTypeDefinition typeDefNewLine;


    }


	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = ClassificationTypes.GlslSectionName)]
	[Name(nameof(GlslSectionNameFormatDef))]
	//this should be visible to the end user
	[UserVisible(true)]
	//set the priority to be after the default classifiers
	[Order(Before = Priority.Default)]
	internal sealed class GlslSectionNameFormatDef : ClassificationFormatDefinition
	{
		public GlslSectionNameFormatDef()
		{
			DisplayName = "GLSL: GLSL Section Name"; //human readable version of the name
			ForegroundColor = Color.FromRgb(84, 207, 149);
		}
	}


	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = ClassificationTypes.GlslFunction)]
	[Name(nameof(GlslFunctionFormatDef))]
	//this should be visible to the end user
	[UserVisible(true)]
	//set the priority to be after the default classifiers
	[Order(Before = Priority.Default)]
	internal sealed class GlslFunctionFormatDef : ClassificationFormatDefinition
	{
		public GlslFunctionFormatDef()
		{
			DisplayName = "GLSL: GLSL Builtin Function"; //human readable version of the name
			ForegroundColor = Color.FromRgb(240, 194, 184);
		}
	}


	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = ClassificationTypes.GlslKeyword)]
	[Name(nameof(GlslKeywordFormatDef))]
	//this should be visible to the end user
	[UserVisible(true)]
	//set the priority to be after the default classifiers
	[Order(Before = Priority.Default)]
	internal sealed class GlslKeywordFormatDef : ClassificationFormatDefinition
	{
		public GlslKeywordFormatDef()
		{
			DisplayName = "GLSL: GLSL Builtin Keyword"; //human readable version of the name
			ForegroundColor = Color.FromRgb(255, 72, 75);
		}
	}


	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = ClassificationTypes.GlslVariable)]
	[Name(nameof(GlslVariableFormatDef))]
	//this should be visible to the end user
	[UserVisible(true)]
	//set the priority to be after the default classifiers
	[Order(Before = Priority.Default)]
	internal sealed class GlslVariableFormatDef : ClassificationFormatDefinition
	{
		public GlslVariableFormatDef()
		{
			DisplayName = "GLSL: GLSL Builtin Variable"; //human readable version of the name
			ForegroundColor = Color.FromRgb(252, 188, 188);
		}
	}


	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = ClassificationTypes.GlslBuiltinType)]
	[Name(nameof(GlslBuiltinTypeFormatDef))]
	//this should be visible to the end user
	[UserVisible(true)]
	//set the priority to be after the default classifiers
	[Order(Before = Priority.Default)]
	internal sealed class GlslBuiltinTypeFormatDef : ClassificationFormatDefinition
	{
		public GlslBuiltinTypeFormatDef()
		{
			DisplayName = "GLSL: GLSL Builtin Type"; //human readable version of the name
			ForegroundColor = Color.FromRgb(183, 197, 244);
		}
	}


	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = ClassificationTypes.GlslUserFunction)]
	[Name(nameof(GlslUserFunctionFormatDef))]
	//this should be visible to the end user
	[UserVisible(true)]
	//set the priority to be after the default classifiers
	[Order(Before = Priority.Default)]
	internal sealed class GlslUserFunctionFormatDef : ClassificationFormatDefinition
	{
		public GlslUserFunctionFormatDef()
		{
			DisplayName = "GLSL: GLSL User Function"; //human readable version of the name
			ForegroundColor = Color.FromRgb(185, 212, 182);
		}
	}


	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = ClassificationTypes.GlslIdentifier)]
	[Name(nameof(GlslIdentifierFormatDef))]
	//this should be visible to the end user
	[UserVisible(true)]
	//set the priority to be after the default classifiers
	[Order(Before = Priority.Default)]
	internal sealed class GlslIdentifierFormatDef : ClassificationFormatDefinition
	{
		public GlslIdentifierFormatDef()
		{
			DisplayName = "GLSL: GLSL Identifier"; //human readable version of the name
			ForegroundColor = Color.FromRgb(212, 212, 212);
		}
	}


	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = ClassificationTypes.GlslNumber)]
	[Name(nameof(GlslNumberFormatDef))]
	//this should be visible to the end user
	[UserVisible(true)]
	//set the priority to be after the default classifiers
	[Order(Before = Priority.Default)]
	internal sealed class GlslNumberFormatDef : ClassificationFormatDefinition
	{
		public GlslNumberFormatDef()
		{
			DisplayName = "GLSL: GLSL Number"; //human readable version of the name
			ForegroundColor = Color.FromRgb(247, 216, 124);
		}
	}


	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = ClassificationTypes.GlslPreprocessor)]
	[Name(nameof(GlslPreprocessorFormatDef))]
	//this should be visible to the end user
	[UserVisible(true)]
	//set the priority to be after the default classifiers
	[Order(Before = Priority.Default)]
	internal sealed class GlslPreprocessorFormatDef : ClassificationFormatDefinition
	{
		public GlslPreprocessorFormatDef()
		{
			DisplayName = "GLSL: GLSL Preprocessor"; //human readable version of the name
			ForegroundColor = Color.FromRgb(92, 110, 116);
		}
	}


	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = ClassificationTypes.Operator)]
	[Name(nameof(OperatorFormatDef))]
	//this should be visible to the end user
	[UserVisible(true)]
	//set the priority to be after the default classifiers
	[Order(Before = Priority.Default)]
	internal sealed class OperatorFormatDef : ClassificationFormatDefinition
	{
		public OperatorFormatDef()
		{
			DisplayName = "GLSL: Operator"; //human readable version of the name
			ForegroundColor = Color.FromRgb(212, 212, 212);
		}
	}


	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = ClassificationTypes.String)]
	[Name(nameof(StringFormatDef))]
	//this should be visible to the end user
	[UserVisible(true)]
	//set the priority to be after the default classifiers
	[Order(Before = Priority.Default)]
	internal sealed class StringFormatDef : ClassificationFormatDefinition
	{
		public StringFormatDef()
		{
			DisplayName = "GLSL: String"; //human readable version of the name
			ForegroundColor = Color.FromRgb(160, 215, 211);
		}
	}


	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = ClassificationTypes.Comment)]
	[Name(nameof(CommentFormatDef))]
	//this should be visible to the end user
	[UserVisible(true)]
	//set the priority to be after the default classifiers
	[Order(Before = Priority.Default)]
	internal sealed class CommentFormatDef : ClassificationFormatDefinition
	{
		public CommentFormatDef()
		{
			DisplayName = "GLSL: Comment"; //human readable version of the name
			ForegroundColor = Color.FromRgb(128, 128, 128);
		}
	}


	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = ClassificationTypes.Command)]
	[Name(nameof(CommandFormatDef))]
	//this should be visible to the end user
	[UserVisible(true)]
	//set the priority to be after the default classifiers
	[Order(Before = Priority.Default)]
	internal sealed class CommandFormatDef : ClassificationFormatDefinition
	{
		public CommandFormatDef()
		{
			DisplayName = "GLSL: Command"; //human readable version of the name
			ForegroundColor = Color.FromRgb(255, 160, 108);
		}
	}


	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = ClassificationTypes.CommandParam)]
	[Name(nameof(CommandParamFormatDef))]
	//this should be visible to the end user
	[UserVisible(true)]
	//set the priority to be after the default classifiers
	[Order(Before = Priority.Default)]
	internal sealed class CommandParamFormatDef : ClassificationFormatDefinition
	{
		public CommandParamFormatDef()
		{
			DisplayName = "GLSL: CommandParam"; //human readable version of the name
			ForegroundColor = Color.FromRgb(245, 158, 180);
		}
	}


	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = ClassificationTypes.MetaSectionName)]
	[Name(nameof(MetaSectionNameFormatDef))]
	//this should be visible to the end user
	[UserVisible(true)]
	//set the priority to be after the default classifiers
	[Order(Before = Priority.Default)]
	internal sealed class MetaSectionNameFormatDef : ClassificationFormatDefinition
	{
		public MetaSectionNameFormatDef()
		{
			DisplayName = "GLSL: Meta Section Name"; //human readable version of the name
			ForegroundColor = Color.FromRgb(255, 96, 55);
		}
	}


	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = ClassificationTypes.NewLine)]
	[Name(nameof(NewLineFormatDef))]
	//this should be visible to the end user
	[UserVisible(false)]
	//set the priority to be after the default classifiers
	[Order(Before = Priority.Default)]
	internal sealed class NewLineFormatDef : ClassificationFormatDefinition
	{
		public NewLineFormatDef()
		{
			DisplayName = "GLSL: NewLine"; //human readable version of the name
			ForegroundColor = Color.FromRgb(0, 0, 0);
		}
	}


}
