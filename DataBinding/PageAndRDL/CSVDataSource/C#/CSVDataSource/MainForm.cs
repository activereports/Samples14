using System;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace GrapeCity.ActiveReports.Samples.CSVDataSource
{
	public partial class MainForm : Form
	{
		readonly string _settingForNoHeaderFixed;
		readonly string _settingForHeaderExistsFixed;
		public MainForm()
		{
			InitializeComponent();
			var _resource = new ResourceManager(GetType());
			_settingForNoHeaderFixed = _resource.GetString("NoHeaderFixed");
			_settingForHeaderExistsFixed = _resource.GetString("HeaderExistsFixed");
		}

		// Loads and shows the report.
		private void btnCSV_Click(object sender, EventArgs e)
		{
			const string settingForNoHeaderDelimited = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued";

			var connectionString = string.Empty;
			if (rbtnNoHeaderComma.Checked)
				connectionString = string.Format(@"Path={0};Encoding={1};TextQualifier="";ColumnsSeparator=,;RowsSeparator=\r\n;Columns={2}",
										Properties.Resources.PathToFileNoHeaderComma, Properties.Resources.CSVEncoding, settingForNoHeaderDelimited);
			else if (rbtnHeaderTab.Checked)
				connectionString = string.Format(@"Path={0};Encoding={1};TextQualifier="";ColumnsSeparator=	;RowsSeparator=\r\n;HasHeaders=True",
					  Properties.Resources.PathToFileHeaderTab, Properties.Resources.CSVEncoding);
			else if (rbtnHeader.Checked)
				connectionString = string.Format(@"Path={0};Encoding={1};Columns={2};HasHeaders=True",
										Properties.Resources.PathToFileHeader, Properties.Resources.CSVEncoding, _settingForHeaderExistsFixed);
			else if (rbtnNoHeader.Checked)
				connectionString = string.Format(@"Path={0};Encoding={1};Columns={2}",
										Properties.Resources.PathToFileNoHeader, Properties.Resources.CSVEncoding, _settingForNoHeaderFixed);

			var report = new PageReport(new FileInfo(@"..\..\Reports\StockList.rdlx"));
			var connectionProps = report.Report.DataSources[0].ConnectionProperties;
			connectionProps.DataProvider = "CSV";
			connectionProps.ConnectString = connectionString;
			arvMain.ReportViewer.LoadDocument(report.Document);
		}
	}
}
