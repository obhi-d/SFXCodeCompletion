using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Utilities;
using System;
using System.ComponentModel.Composition;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace SFXCodeCompletion
{
  /// <summary>
  /// This is the class that implements the package exposed by this assembly.
  /// </summary>
  /// <remarks>
  /// <para>
  /// The minimum requirement for a class to be considered a valid package for Visual Studio
  /// is to implement the IVsPackage interface and register itself with the shell.
  /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
  /// to do it: it derives from the Package class that provides the implementation of the
  /// IVsPackage interface and uses the registration attributes defined in the framework to
  /// register itself and its components with the shell. These attributes tell the pkgdef creation
  /// utility what data to put into .pkgdef file.
  /// </para>
  /// <para>
  /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
  /// </para>
  /// </remarks>
  [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
  [Guid(SFXCodeCompletionPackage.PackageGuidString)]
  public sealed class SFXCodeCompletionPackage : AsyncPackage
  {
    [Export]
    [FileExtension(".sfx")]
    [ContentType("sfx")]
    internal FileExtensionToContentTypeDefinition gfxFileExtensionDefinition = null;

    [Export]
    [FileExtension(".glsl")]
    [ContentType("glsl")]
    internal FileExtensionToContentTypeDefinition glslFileExtensionDefinition = null;

    [Export]
    [FileExtension(".vert")]
    [ContentType("glsl")]
    internal FileExtensionToContentTypeDefinition vertFileExtensionDefinition = null;

    [Export]
    [FileExtension(".frag")]
    [ContentType("glsl")]
    internal FileExtensionToContentTypeDefinition fragFileExtensionDefinition = null;

    [Export]
    [FileExtension(".tesc")]
    [ContentType("glsl")]
    internal FileExtensionToContentTypeDefinition tescFileExtensionDefinition = null;

    [Export]
    [FileExtension(".tese")]
    [ContentType("glsl")]
    internal FileExtensionToContentTypeDefinition teseFileExtensionDefinition = null;

    [Export]
    [FileExtension(".geom")]
    [ContentType("glsl")]
    internal FileExtensionToContentTypeDefinition geomFileExtensionDefinition = null;

    [Export]
    [FileExtension(".comp")]
    [ContentType("glsl")]
    internal FileExtensionToContentTypeDefinition compFileExtensionDefinition = null;

    [Export]
    [FileExtension(".rgen")]
    [ContentType("glsl")]
    internal FileExtensionToContentTypeDefinition rgenFileExtensionDefinition = null;

    [Export]
    [FileExtension(".rint")]
    [ContentType("glsl")]
    internal FileExtensionToContentTypeDefinition rintFileExtensionDefinition = null;

    [Export]
    [FileExtension(".rahit")]
    [ContentType("glsl")]
    internal FileExtensionToContentTypeDefinition rahitFileExtensionDefinition = null;

    [Export]
    [FileExtension(".rchit")]
    [ContentType("glsl")]
    internal FileExtensionToContentTypeDefinition rchitFileExtensionDefinition = null;

    [Export]
    [FileExtension(".rmiss")]
    [ContentType("glsl")]
    internal FileExtensionToContentTypeDefinition rmissFileExtensionDefinition = null;

    [Export]
    [FileExtension(".rcall")]
    [ContentType("glsl")]
    internal FileExtensionToContentTypeDefinition rcallFileExtensionDefinition = null;

    //
    [Export]
    [Name("sfx")]
    [BaseDefinition("code")]
    internal static ContentTypeDefinition gfxContentType = null;

    [Export]
    [Name("glsl")]
    [BaseDefinition("code")]
    internal static ContentTypeDefinition glslContentType = null;

    /// <summary>
    /// SFXCodeCompletionPackage GUID string.
    /// </summary>
    public const string PackageGuidString = "46407b9f-eb92-4bd2-bc5b-9f59a7a17494";

    #region Package Members

    /// <summary>
    /// Initialization of the package; this method is called right after the package is sited, so this is the place
    /// where you can put all the initialization code that rely on services provided by VisualStudio.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
    /// <param name="progress">A provider for progress updates.</param>
    /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
    protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
    {
      // When initialized asynchronously, the current thread may be a background thread at this point.
      // Do any initialization that requires the UI thread after switching to the UI thread.
      await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
    }

    #endregion
  }
}
