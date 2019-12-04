using System;
using System.Windows.Forms;

namespace GrapeCity.ActiveReports.Samples.TableOfContents
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ReportsForm());
		}
	}
}
