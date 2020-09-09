using System;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Export;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Viewer.Win.Internal.Export;
using GrapeCity.ActiveReports.Extensibility.Rendering.IO;

namespace GrapeCity.ActiveReports.Win.Export
{
	internal class ExportViewer : IExportViewer
	{
		private readonly GrapeCity.ActiveReports.Viewer.Win.Viewer _viewer;

		public ExportViewer(GrapeCity.ActiveReports.Viewer.Win.Viewer viewer)
		{
			_viewer = viewer;
		}

		public void Export(IDocumentExportEx export, FileInfo file)
		{
			_viewer.Export(export, file);
		}

		public void Export(IDocumentExportEx export, FileStream stream)
		{
			_viewer.Export(export, stream);
		}

		public void Render(IRenderingExtension export, StreamProvider streamProvider, NameValueCollection settings)
		{
			_viewer.Render(export, streamProvider, settings);
		}

		public SectionDocument Document
		{
			get { return _viewer.Document; }
		}

		public bool CanExport
		{
			get { return _viewer.CanExport; }
		}

		public IWin32Window Owner
		{
			get { return _viewer; }
		}

		public void HandleError(Exception exception)
		{
			_viewer.BeginInvoke(new MethodInvoker(() => _viewer.HandleError(exception)));
		}
	}
}
