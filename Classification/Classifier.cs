﻿using Microsoft.Build.Framework;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
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
      bool glsl = textBuffer.ContentType.IsOfType("glsl");
      return textBuffer.Properties.GetOrCreateSingletonProperty(() => new Classifier(textBuffer, parser, glsl)); //per buffer classifier
    }

    protected readonly SyntaxColorParser parser;
  }

  internal class Classifier : IClassifier
  {
    internal Classifier(ITextBuffer textBuffer, SyntaxColorParser parser, bool inGlsl)
    {
      this.startInGlsl = inGlsl;
      if (textBuffer is null)
      {
        throw new ArgumentNullException(nameof(textBuffer));
      }

      if (parser is null)
      {
        throw new ArgumentNullException(nameof(parser));
      }

      var observableSnapshot = Observable.Return(textBuffer.CurrentSnapshot).Concat(
          Observable.FromEventPattern<TextContentChangedEventArgs>(h => textBuffer.Changed += h, h => textBuffer.Changed -= h)
          .Select(e => e.EventArgs.After));

      parser.Changed += _ => UpdateSpans();

      void UpdateSpans()
      {
        var snapshotSpan = new SnapshotSpan(textBuffer.CurrentSnapshot, 0, textBuffer.CurrentSnapshot.Length);
        var spans = parser.CalculateSpans(snapshotSpan, startInGlsl);

        this.spans = spans;
        ClassificationChanged?.Invoke(this, new ClassificationChangedEventArgs(snapshotSpan));
      }

      observableSnapshot
          .Throttle(TimeSpan.FromSeconds(0.3f))
          .Subscribe(_ => UpdateSpans());
    }

    public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

    public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan inputSpan)
    {
      var output = new List<ClassificationSpan>();
      var currentSpans = spans; // if UpdateSpans runs during execution we want to avoid any exceptions
      if (0 == currentSpans.Count) return output;
      var translatedInput = inputSpan.TranslateTo(currentSpans[0].Span.Snapshot, SpanTrackingMode.EdgeInclusive);

      foreach (var span in currentSpans)
      {
        if (translatedInput.OverlapsWith(span.Span))
        {
          output.Add(span);
        }
      }
      return output;
    }

    private bool startInGlsl = false;
    private IList<ClassificationSpan> spans = new List<ClassificationSpan>();
  }
}
