﻿using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;
using SFXCodeCompletion.Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SFXCodeCompletion.CodeCompletion
{
  [Export(typeof(IVsTextViewCreationListener))]
  [ContentType("sfx")]
  [ContentType("glsl")]
  [TextViewRole(PredefinedTextViewRoles.Interactive)]
  internal sealed class VsTextViewCreationListener : IVsTextViewCreationListener
  {
    [Import]
    private readonly IVsEditorAdaptersFactoryService AdaptersFactory = null;

    [Import]
    private readonly ICompletionBroker CompletionBroker = null;

    public void VsTextViewCreated(IVsTextView textViewAdapter)
    {
      IWpfTextView view = AdaptersFactory.GetWpfTextView(textViewAdapter);

      Debug.Assert(view != null);
      view.Properties.GetOrCreateSingletonProperty(
               () => new CommandFilter(textViewAdapter, view, CompletionBroker));
    }
  }

  internal sealed class CommandFilter : IOleCommandTarget
  {
    private ICompletionSession _currentSession;
    private IOleCommandTarget _next;
    public CommandFilter(IVsTextView adapter, IWpfTextView textView, ICompletionBroker broker)
    {
      _currentSession = null;

      TextView = textView;
      Broker = broker;

      adapter.AddCommandFilter(this, out _next);
    }

    public IWpfTextView TextView { get; private set; }
    public ICompletionBroker Broker { get; private set; }
    
    private char GetTypeChar(IntPtr pvaIn)
    {
      return (char)(ushort)Marshal.GetObjectForNativeVariant(pvaIn);
    }

    public int QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
    {
      if (VSConstants.VSStd2K == pguidCmdGroup)
      {
        switch ((VSConstants.VSStd2KCmdID)prgCmds[0].cmdID)
        {
          case VSConstants.VSStd2KCmdID.AUTOCOMPLETE:
          case VSConstants.VSStd2KCmdID.COMPLETEWORD:
          case VSConstants.VSStd2KCmdID.COMMENTBLOCK:
          case VSConstants.VSStd2KCmdID.UNCOMMENTBLOCK:
          case VSConstants.VSStd2KCmdID.COMMENT_BLOCK:
          case VSConstants.VSStd2KCmdID.UNCOMMENT_BLOCK:
            prgCmds[0].cmdf = (uint)OLECMDF.OLECMDF_ENABLED | (uint)OLECMDF.OLECMDF_SUPPORTED;
            return VSConstants.S_OK;
        }
      }
      ThreadHelper.ThrowIfNotOnUIThread();
      return _next.QueryStatus(pguidCmdGroup, cCmds, prgCmds, pCmdText);
    }

    public int Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
    {
      bool handled = false;
      int hresult = VSConstants.S_OK;

      // 1. Pre-process
      if (pguidCmdGroup == VSConstants.VSStd2K)
      {
        switch ((VSConstants.VSStd2KCmdID)nCmdID)
        {
          case VSConstants.VSStd2KCmdID.AUTOCOMPLETE:
          case VSConstants.VSStd2KCmdID.COMPLETEWORD:
            handled = StartSession();
            break;
          case VSConstants.VSStd2KCmdID.RETURN:
            handled = Complete(false);
            break;
          case VSConstants.VSStd2KCmdID.TAB:
            handled = Complete(true);
            break;
          case VSConstants.VSStd2KCmdID.CANCEL:
            Cancel();
            break;
          case VSConstants.VSStd2KCmdID.COMMENTBLOCK:
          case VSConstants.VSStd2KCmdID.COMMENT_BLOCK:
            Comment(TextView);
            handled = true;
            break;
          case VSConstants.VSStd2KCmdID.UNCOMMENTBLOCK:
          case VSConstants.VSStd2KCmdID.UNCOMMENT_BLOCK:
            UnComment(TextView);
            handled = true;
            break;
        }
      }
      ThreadHelper.ThrowIfNotOnUIThread();
      if (!handled) 
        hresult = _next.Exec(pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);

      if (ErrorHandler.Succeeded(hresult))
      {
        if (pguidCmdGroup == VSConstants.VSStd2K)
        {
          switch ((VSConstants.VSStd2KCmdID)nCmdID)
          {
            case VSConstants.VSStd2KCmdID.TYPECHAR:
              var c = GetTypeChar(pvaIn);
              if (!KnownTokens.IsIdentifierChar(c))
                Cancel();
              else if (_currentSession != null)
                Filter();
              else if (KnownTokens.IsIdentifierStartChar(c))
                StartSession();
              break;
            case VSConstants.VSStd2KCmdID.BACKSPACE:
              Filter();
              break;
          }
        }
      }

      return hresult;
    }

    private static void Comment(ITextView textView)
    {
      var buffer = textView.TextBuffer;
      var selection = textView.Selection;
      var startLine = selection.Start.Position.GetContainingLine().LineNumber;
      var endLine = selection.End.Position.GetContainingLine().LineNumber;
      var edit = buffer.CreateEdit();
      for (int i = startLine; i <= endLine; ++i)
      {
        var line = buffer.CurrentSnapshot.GetLineFromLineNumber(i);
        edit.Insert(line.Start, "//");
      }
      edit.Apply();
    }

    private static void UnComment(ITextView textView)
    {
      var buffer = textView.TextBuffer;
      var selection = textView.Selection;
      var startLine = selection.Start.Position.GetContainingLine().LineNumber;
      var endLine = selection.End.Position.GetContainingLine().LineNumber;
      var edit = buffer.CreateEdit();
      for (int i = startLine; i <= endLine; ++i)
      {
        var line = buffer.CurrentSnapshot.GetLineFromLineNumber(i);
        var text = line.GetText();
        var index = text.IndexOf("//");
        if (-1 != index) edit.Delete(line.Start + index, 2);
      }
      edit.Apply();
    }

    /// <summary>
    /// Narrow down the list of options as the user types input
    /// </summary>
    private void Filter()
    {
      if (_currentSession == null)
        return;

      _currentSession.SelectedCompletionSet.SelectBestMatch();
      _currentSession.SelectedCompletionSet.Recalculate();
    }

    /// <summary>
    /// Cancel the auto-complete session, and leave the text unmodified
    /// </summary>
    private void Cancel()
    {
      if (null != _currentSession)
      {
        _currentSession.Dismiss();
      }

    }

    /// <summary>
    /// Auto-complete text using the specified token
    /// </summary>
    private bool Complete(bool force)
    {
      if (_currentSession == null)
        return false;

      if (!_currentSession.SelectedCompletionSet.SelectionStatus.IsSelected && !force)
      {
        _currentSession.Dismiss();
        return false;
      }
      else
      {
        _currentSession.Commit();
        return true;
      }
    }

    /// <summary>
    /// Display list of potential tokens
    /// </summary>
    private bool StartSession()
    {
      if (_currentSession != null) return false;

      SnapshotPoint caret = TextView.Caret.Position.BufferPosition;
      ITextSnapshot snapshot = caret.Snapshot;

      if (!Broker.IsCompletionActive(TextView))
      {
        _currentSession = Broker.CreateCompletionSession(TextView, snapshot.CreateTrackingPoint(caret, PointTrackingMode.Positive), true);
      }
      else
      {
        _currentSession = Broker.GetSessions(TextView)[0];
      }
      if (_currentSession is null) return false;

      _currentSession.Dismissed += (sender, args) => _currentSession = null;
      if (_currentSession.IsStarted)
        return false;
      _currentSession.Start();
      return true;
    }
  }
}
