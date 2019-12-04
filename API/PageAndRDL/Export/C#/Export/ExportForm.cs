using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Rendering.IO;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Export
{
	public partial class ExportForm : Form
	{
		private const string _json = "json";
		private const string _pdf = "pdf";
		private const string _xlsx = "xlsx";
		private const string _docx = "docx";
		private const string _csv = "csv";
		private const string _mht = "mht";

		public ExportForm()
		{
			InitializeComponent();
			FillReportsNames();
		}

		private void ExportForm_Load(object sender, EventArgs e)
		{
			reportsNames.SelectedIndex = 0;
			exportTypes.SelectedIndex = 0;
		}

		private void FillReportsNames()
		{
			var directoryInfo = new DirectoryInfo("../../../../Reports/");
			foreach (var file in directoryInfo.GetFiles())
			{
				reportsNames.Items.Add(file.Name);
			}
		}

		private void exportButton_Click(object sender, EventArgs e)
		{
			var reportName = (string)reportsNames.SelectedItem;
			var exportType = (string)exportTypes.SelectedItem;
			IRenderingExtension renderingExtension = null;
			NameValueCollection settings = null;
			var exporTypeLower = exportType.ToLower();
			switch (exporTypeLower)
			{
				case _pdf:
					renderingExtension = new GrapeCity.ActiveReports.Export.Pdf.Page.PdfRenderingExtension();
					settings = new GrapeCity.ActiveReports.Export.Pdf.Page.Settings();
					break;
				case _xlsx:
					renderingExtension = new GrapeCity.ActiveReports.Export.Excel.Page.ExcelRenderingExtension();
					ISettings excelSettings = new GrapeCity.ActiveReports.Export.Excel.Page.ExcelRenderingExtensionSettings()
					{
						FileFormat = GrapeCity.ActiveReports.Export.Excel.Page.FileFormat.Xlsx
					};
					settings = excelSettings.GetSettings();
					break;
				case _csv:
					settings = new GrapeCity.ActiveReports.Export.Text.Page.CsvRenderingExtension.Settings()
					{
						ColumnsDelimiter = ",",
						RowsDelimiter = "\r\n",
						QuotationSymbol = '"',
						Encoding = Encoding.UTF8
					};
					renderingExtension = new GrapeCity.ActiveReports.Export.Text.Page.CsvRenderingExtension();
					break;
				case _docx:
					renderingExtension = new GrapeCity.ActiveReports.Export.Word.Page.WordRenderingExtension();
					settings = new GrapeCity.ActiveReports.Export.Word.Page.Settings() { FileFormat = GrapeCity.ActiveReports.Export.Word.Page.FileFormat.OOXML };

					break;
				case _mht:
					renderingExtension = new GrapeCity.ActiveReports.Export.Html.Page.HtmlRenderingExtension();
					settings = new GrapeCity.ActiveReports.Export.Html.Page.Settings(){ MhtOutput = true, OutputTOC = true, Fragment = false };

					break;
				case _json:
					settings = new GrapeCity.ActiveReports.Export.Text.Page.JsonRenderingExtension.Settings() { Formatted = true };
					renderingExtension = new GrapeCity.ActiveReports.Export.Text.Page.JsonRenderingExtension();
					break;
			}

			var report = new PageReport(new FileInfo(@"..\..\..\..\Reports\" + reportName));
			var fileName =  Path.GetFileNameWithoutExtension(reportName);
			var saveFileDialog = new SaveFileDialog()
			{
				FileName = fileName + GetSaveDialogExtension(exporTypeLower),
				Filter = GetSaveDialogFilter(exporTypeLower)
			};

			if (saveFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			var outputDirectory = new DirectoryInfo(Path.GetDirectoryName(saveFileDialog.FileName));
			var outputProvider = new FileStreamProvider(outputDirectory, Path.GetFileNameWithoutExtension(saveFileDialog.FileName));

			outputProvider.OverwriteOutputFile = true;

			exportButton.Enabled = false;
			report.Document.Render(renderingExtension, outputProvider, settings);
			exportButton.Enabled = true;
		}

		private string GetSaveDialogFilter(string exportType)
		{
			return exportType + " files | *." + exportType;
		}

		private string GetSaveDialogExtension(string exportType)
		{
			return "." + exportType;
		}
	}
}
