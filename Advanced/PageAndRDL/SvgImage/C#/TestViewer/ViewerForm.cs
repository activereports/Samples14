using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Document;
using System;
using System.IO;
using System.Windows.Forms;

namespace TestViewer
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

			var reportFile = new FileInfo(@"..\..\..\..\Report\Svg.rdlx");
			var report = new PageReport(reportFile);
			var document = new PageDocument(report);

			viewer1.LoadDocument(document);
		}
	}
}
