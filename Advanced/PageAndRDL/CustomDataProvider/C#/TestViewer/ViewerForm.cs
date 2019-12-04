using System;
using System.Windows.Forms;

namespace GrapeCity.ActiveReports.Samples.TestViewer
{
	public partial class ViewerForm : Form
	{
		public ViewerForm()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			viewer1.LoadDocument("../../DemoReport.rdlx");
		}
	}
}
