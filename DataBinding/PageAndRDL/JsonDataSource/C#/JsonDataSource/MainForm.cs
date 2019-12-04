using System;
using System.IO;
using System.Windows.Forms;

namespace GrapeCity.ActiveReports.Samples.JsonDataSource
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		// The handler of <see cref="PageDocument.LocateDataSource"/> that returns appropriate data for a report.
		private void OnLocateDataSource(object sender, LocateDataSourceEventArgs args)
		{
			object data = null;
			var dataSourceName = args.DataSet.Name;
			var source = new DataLayer();
			if (dataSourceName == "DataSet1")
			{
				data = source.CreateData();
			}

			args.Data = data;
		}

		// Loads and shows the report.
		private void MainForm_Load(object sender, EventArgs e)
		{
			var rptPath = new FileInfo(@"..\..\testReport.rdlx");
			var definition = new PageReport(rptPath);
			definition.Document.LocateDataSource += OnLocateDataSource;
			reportPreview.ReportViewer.LoadDocument(definition.Document);
		}
	}
}
