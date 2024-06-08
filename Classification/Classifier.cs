using EnvDTE;
using Microsoft.Build.Framework;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using Sprache;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;

namespace SFXCodeCompletion.Classification
{

  [Export(typeof(IClassifierProvider))]
  [ContentType("sfx")]
  [ContentType("glsl")]
  [TagType(typeof(ClassificationTag))]
  internal class ClassifierProvider : IClassifierProvider
  {
    [ImportingConstructor]
    public ClassifierProvider(IClassificationTypeRegistryService classificationTypeRegistry)
    {
      if (classificationTypeRegistry is null)
      {
        throw new System.ArgumentNullException(nameof(classificationTypeRegistry));
      }
      parser = new SyntaxColorParser(classificationTypeRegistry);
    }

    public IClassifier GetClassifier(ITextBuffer textBuffer)
    {
      return textBuffer.Properties.GetOrCreateSingletonProperty(() => new Classifier(parser)); //per buffer classifier
    }

    protected readonly SyntaxColorParser parser;
  }

  internal class Classifier : IClassifier
  {
    internal Classifier(SyntaxColorParser parser)
    {
      this.parser = parser;
    }

    public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;


    public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan inputSpan)
    {
      bool startInGlsl = inputSpan.Snapshot.ContentType.IsOfType("glsl");
      return parser.CalculateSpans(inputSpan, startInGlsl);
    }

    private SyntaxColorParser parser = null;
  }
}
