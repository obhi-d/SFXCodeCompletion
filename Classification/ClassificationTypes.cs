





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
      [BaseDefinition(PredefinedClassificationTypeNames.Identifier)]
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
      [BaseDefinition(PredefinedClassificationTypeNames.Identifier)]
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
      [BaseDefinition(PredefinedClassificationTypeNames.Identifier)]
      private static readonly ClassificationTypeDefinition typeDefGlslNumber;


      public const string GlsPreprocessor = nameof(typeDefGlsPreprocessor);
      [Export]
      [Name(GlsPreprocessor)]
      [BaseDefinition(PredefinedClassificationTypeNames.Identifier)]
      private static readonly ClassificationTypeDefinition typeDefGlsPreprocessor;


      public const string Operator = nameof(typeDefOperator);
      [Export]
      [Name(Operator)]
      [BaseDefinition(PredefinedClassificationTypeNames.Identifier)]
      private static readonly ClassificationTypeDefinition typeDefOperator;


      public const string String = nameof(typeDefString);
      [Export]
      [Name(String)]
      [BaseDefinition(PredefinedClassificationTypeNames.Identifier)]
      private static readonly ClassificationTypeDefinition typeDefString;


      public const string Comment = nameof(typeDefComment);
      [Export]
      [Name(Comment)]
      [BaseDefinition(PredefinedClassificationTypeNames.Identifier)]
      private static readonly ClassificationTypeDefinition typeDefComment;


      public const string Command = nameof(typeDefCommand);
      [Export]
      [Name(Command)]
      [BaseDefinition(PredefinedClassificationTypeNames.Identifier)]
      private static readonly ClassificationTypeDefinition typeDefCommand;


      public const string MetaSectionName = nameof(typeDefMetaSectionName);
      [Export]
      [Name(MetaSectionName)]
      [BaseDefinition(PredefinedClassificationTypeNames.Identifier)]
      private static readonly ClassificationTypeDefinition typeDefMetaSectionName;


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
			DisplayName = "SFX: GLSL Section Name"; //human readable version of the name
			ForegroundColor = Color.FromRgb(255, 17, 85);
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
			DisplayName = "SFX: GLSL Builtin Function"; //human readable version of the name
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
			DisplayName = "SFX: GLSL Builtin Keyword"; //human readable version of the name
			ForegroundColor = Color.FromRgb(218, 203, 169);
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
			DisplayName = "SFX: GLSL Builtin Variable"; //human readable version of the name
			ForegroundColor = Color.FromRgb(203, 201, 236);
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
			DisplayName = "SFX: GLSL Builtin Type"; //human readable version of the name
			ForegroundColor = Color.FromRgb(234, 193, 214);
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
			DisplayName = "SFX: GLSL User Function"; //human readable version of the name
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
			DisplayName = "SFX: GLSL Identifier"; //human readable version of the name
			ForegroundColor = Color.FromRgb(240, 194, 184);
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
			DisplayName = "SFX: GLSL Number"; //human readable version of the name
			ForegroundColor = Color.FromRgb(247, 216, 124);
		}
	}



	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = ClassificationTypes.GlsPreprocessor)]
	[Name(nameof(GlsPreprocessorFormatDef))]
	//this should be visible to the end user
	[UserVisible(true)]
	//set the priority to be after the default classifiers
	[Order(Before = Priority.Default)]
	internal sealed class GlsPreprocessorFormatDef : ClassificationFormatDefinition
	{
		public GlsPreprocessorFormatDef()
		{
			DisplayName = "SFX: GLSL Preprocessor"; //human readable version of the name
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
			DisplayName = "SFX: Operator"; //human readable version of the name
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
			DisplayName = "SFX: String"; //human readable version of the name
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
			DisplayName = "SFX: Comment"; //human readable version of the name
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
			DisplayName = "SFX: Command"; //human readable version of the name
			ForegroundColor = Color.FromRgb(255, 160, 108);
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
			DisplayName = "SFX: Meta Section Name"; //human readable version of the name
			ForegroundColor = Color.FromRgb(255, 96, 55);
		}
	}



}
