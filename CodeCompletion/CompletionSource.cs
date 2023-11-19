using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using SFXCodeCompletion.Classification;
using SFXCodeCompletion.Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SFXCodeCompletion.CodeCompletion
{
    [Export(typeof(ICompletionSourceProvider))]
    [ContentType("sfx")]
    [Name("sfxCompletion")]
    internal class CompletionSourceProvider : ICompletionSourceProvider
    {
        [ImportingConstructor]
        public CompletionSourceProvider(IClassifierAggregatorService classifierAggregatorService, IGlyphService glyphService)
        {
            if (glyphService is null)
            {
                throw new ArgumentNullException(nameof(glyphService));
            }

            this.classifierAggregatorService = classifierAggregatorService ?? throw new ArgumentNullException(nameof(classifierAggregatorService));
            identifier = glyphService.GetGlyph(StandardGlyphGroup.GlyphGroupVariable, StandardGlyphItem.GlyphItemFriend);

            var keyword = glyphService.GetGlyph(StandardGlyphGroup.GlyphKeyword, StandardGlyphItem.GlyphItemPublic);
            var function = glyphService.GetGlyph(StandardGlyphGroup.GlyphGroupMethod, StandardGlyphItem.GlyphItemPublic);
            var variable = glyphService.GetGlyph(StandardGlyphGroup.GlyphGroupVariable, StandardGlyphItem.GlyphItemPublic);
            var command = glyphService.GetGlyph(StandardGlyphGroup.GlyphGroupIntrinsic, StandardGlyphItem.GlyphItemPublic);
            ImageSource ConvertReservedType(SfxTokenType type)
            {
                switch (type)
                {
                    case SfxTokenType.Keyword: return keyword;
                    case SfxTokenType.Function: return function;
                    case SfxTokenType.Variable: return variable;
                    case SfxTokenType.Command: return command;
                    default: return identifier;
                }
            }
            foreach (var var in KnownTokens.ReservedWords)
            {
                staticCompletions.Add(CompletionSource.NewCompletion(var.Key, ConvertReservedType(var.Value)));
            }
            staticCompletions.Sort((a, b) => a.DisplayText.CompareTo(b.DisplayText));
        }

        public ICompletionSource TryCreateCompletionSource(ITextBuffer textBuffer)
        {
            var classifier = classifierAggregatorService.GetClassifier(textBuffer);
            return new CompletionSource(textBuffer, staticCompletions, identifier, classifier);
        }

        private readonly IClassifierAggregatorService classifierAggregatorService;
        private readonly ImageSource identifier;
        private readonly List<Completion> staticCompletions = new List<Completion>();
    }


    internal class CompletionSource : ICompletionSource
    {
        private readonly ITextBuffer currentBuffer;
        private bool _disposed = false;
        private readonly IEnumerable<Completion> staticCompletions;
        private readonly Func<SnapshotSpan, IEnumerable<string>> queryIdentifiers;
        private readonly ImageSource imgIdentifier;

        public CompletionSource(ITextBuffer buffer, IEnumerable<Completion> staticCompletions, ImageSource identifier, IClassifier classifier)
        {
            currentBuffer = buffer;
            this.staticCompletions = staticCompletions;
            imgIdentifier = identifier;

            queryIdentifiers = (snapshotSpan) =>
            {
                var tokens = classifier.GetClassificationSpans(snapshotSpan);
                //only those tokens that are identifiers and do not overlap the input position because we do not want to add char that started session to completions
                var filtered = from token in tokens
                               where (token.ClassificationType.IsOfType(ClassificationTypes.GlslIdentifier) || token.ClassificationType.IsOfType(ClassificationTypes.GlslUserFunction))
                                && !token.Span.Contains(snapshotSpan.End - 1)
                               let text = token.Span.GetText()
                               orderby text
                               select text;
                return filtered.Distinct();
            };
        }

        public void AugmentCompletionSession(ICompletionSession session, IList<CompletionSet> completionSets)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(CompletionSource));

            var snapshot = currentBuffer.CurrentSnapshot;
            var triggerPoint = (SnapshotPoint)session.GetTriggerPoint(snapshot);

            if (triggerPoint == default) return;

            var completions = new List<Completion>();
            var startToPosition = new Span(0, triggerPoint.Position);
            foreach (var identifier in queryIdentifiers.Invoke(new SnapshotSpan(snapshot, startToPosition)))
            {
                completions.Add(NewCompletion(identifier, imgIdentifier));
            }
            completions.AddRange(staticCompletions);

            var start = NonIdentifierPositionBefore(triggerPoint);
            var applicableTo = snapshot.CreateTrackingSpan(new SnapshotSpan(start, triggerPoint), SpanTrackingMode.EdgeInclusive);
            completionSets.Add(new CompletionSet("All", "All", applicableTo, completions, Enumerable.Empty<Completion>()));
        }

        public static Completion NewCompletion(string text, ImageSource image)
        {
            return new Completion(text, text, null, image, null);
        }

        public void Dispose()
        {
            _disposed = true;
        }

        private static SnapshotPoint NonIdentifierPositionBefore(SnapshotPoint triggerPoint)
        {
            var line = triggerPoint.GetContainingLine();

            SnapshotPoint start = triggerPoint;

            while (start > line.Start && KnownTokens.IsIdentifierChar((start - 1).GetChar()))
            {
                start -= 1;
            }

            return start;
        }
    }

}
