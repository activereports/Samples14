using GrapeCity.ActiveReports.Design.Advanced;
using GrapeCity.ActiveReports.Viewer.Win.Internal.Export;
using GrapeCity.ActiveReports.Win.Export;

namespace GrapeCity.ActiveReports.Samples.FlatUserDesigner
{
	internal class ExportViewerFactory : IExportViewerFactory
	{
		public IExportViewer CreateExportViewer(Viewer.Win.Viewer viewer)
		{
			return new ExportViewer(viewer);
		}
	}
}
