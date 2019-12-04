using System;
using System.Windows.Forms;
using System.IO;

namespace GrapeCity.ActiveReports.Samples.OleDbDataSource
{
	partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		// Loads and shows the report.
		private void MainForm_Load(object sender, EventArgs e)
		{
			var rptPath = new FileInfo(@"..\..\OleDBReport.rdlx");
			var pageReport = new PageReport(rptPath);
			reportPreview.LoadDocument(pageReport.Document);
		}
	}
}
