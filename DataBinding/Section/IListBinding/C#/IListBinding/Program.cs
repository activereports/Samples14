using System;
using System.Windows.Forms;

namespace GrapeCity.ActiveReports.Samples.IListBinding
{
	class Program
	{
		[STAThread]
		public static void Main()
		{
			Application.EnableVisualStyles();
			Application.Run(new BindIListToDataGridSample());
		}
	}
}
