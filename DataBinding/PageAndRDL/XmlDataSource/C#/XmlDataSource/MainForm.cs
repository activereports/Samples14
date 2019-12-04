using System;
using System.IO;
using System.Windows.Forms;

namespace GrapeCity.ActiveReports.Samples.XmlDataSource
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
			var dataSourceName = args.DataSet.Query.DataSourceName;
			var source = new DataLayer();
			if (dataSourceName == "BandedListDS")
			{
				data = source.CreateReader();
			}
			else if (dataSourceName == "CountrySalesDS")
			{
				data = source.CreateDocument();
			}

			args.Data = data;
		}

		// Loads and shows the report.
		private void MainForm_Load(object sender, EventArgs e)
		{
			var rptPath = new FileInfo(@"..\..\BandedListXML.rdlx");
			var definition = new PageReport(rptPath);
			definition.Document.LocateDataSource += OnLocateDataSource;
			reportPreview.ReportViewer.LoadDocument(definition.Document);
		}
	}
}
