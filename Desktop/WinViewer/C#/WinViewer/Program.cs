using System;
using System.IO;
using System.Windows.Forms;

namespace GrapeCity.ActiveReports.Viewer.Win
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var applicationForm = new ViewerForm();

			if (args.Length > 0 && !string.IsNullOrEmpty(args[0]))
			{
				var file = new FileInfo(args[0]);
				if (file.Exists)
				{
					applicationForm.LoadDocument(file);
				}
			}


			Application.Run(applicationForm);
		}
	}
}
