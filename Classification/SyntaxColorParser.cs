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
    private Parser.Lexer plexer = null;
    public SyntaxColorParser(IClassificationTypeRegistryService classificationTypeRegistry)
    {
      if (classificationTypeRegistry is null)
      {
        throw new System.ArgumentNullException(nameof(classificationTypeRegistry));
      }

      plexer = new Parser.Lexer(classificationTypeRegistry);
    }

    public delegate void ChangedEventHandler(object sender);
    public event ChangedEventHandler Changed;

    public IList<ClassificationSpan> CalculateSpans(SnapshotSpan snapshotSpan, bool startInGlsl)
    {
      var output = new List<ClassificationSpan>();
      var text = snapshotSpan.GetText();
      return plexer.Tokenize(snapshotSpan.Snapshot, startInGlsl);
    }

  }
}
