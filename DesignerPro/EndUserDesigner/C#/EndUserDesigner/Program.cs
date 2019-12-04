using System;
using System.Windows.Forms;

namespace GrapeCity.ActiveReports.Designer.Win
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new DesignerForm());
		}
	}
}
