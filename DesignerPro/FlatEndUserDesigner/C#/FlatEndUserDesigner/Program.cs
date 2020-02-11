using GrapeCity.ActiveReports.Design.Advanced;
using System;
using System.Windows.Forms;

namespace GrapeCity.ActiveReports.Samples.FlatUserDesigner
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var designerForm = new DesignerForm
			{
				ExportViewerFactory = new ExportViewerFactory(),
				SessionSettingsStorage = new SessionSettingsStorage()
			};

			Application.Run(designerForm);
		}
	}
}
