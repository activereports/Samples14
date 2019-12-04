using GrapeCity.ActiveReports;
using System;
using System.IO;
using System.Windows.Forms;

namespace ODataDataSource
{
	/// <summary>
	/// Json Provider clinet, which get data via OData.
	/// </summary>
	public partial class MainForm: Form
    {
		private PageReport _pageReport;
		private const string _path = @"..\..\..\..\Reports\testReport.rdlx";
		private Service _service = Service.None;

		public MainForm()
        {
            InitializeComponent();
			var rptPath = new FileInfo(_path);
			_pageReport = new PageReport(rptPath);
			_pageReport.Document.LocateDataSource += OnLocateDataSource;
			comboBox1.SelectedIndex = 0;
        }

		// The handler of <see cref="PageDocument.LocateDataSource"/> that returns appropriate data for a report.
		private void OnLocateDataSource(object sender, LocateDataSourceEventArgs args)
		{
			object data = null;
			var dataSourceName = args.DataSet.Query.DataSourceName;
			if (dataSourceName == "DataSource1")
			{
				try
				{
					data = DataLayer.CreateData(_service);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}
			args.Data = data;
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((int)_service == comboBox1.SelectedIndex)
				return;

			_service = (Service)Enum.Parse(typeof(Service), comboBox1.SelectedIndex.ToString());
			reportPreview.LoadDocument(_pageReport.Document);
		}
	}
}
