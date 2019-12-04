using System;
using System.Windows.Forms;

namespace GrapeCity.ActiveReports.Samples.Layers
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメインエントリポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new LayersForm());
		}
	}
}
