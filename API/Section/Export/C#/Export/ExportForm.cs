using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Export;
using System;
using System.IO;
using System.Windows.Forms;

namespace Export
{
	public partial class ExportForm : Form
	{

		private const string _pdf = "pdf";
		private const string _xlsx = "xlsx";
		private const string _mht = "mht";
		private const string _rtf = "rtf";
		private const string _csv = "csv";

		public ExportForm()
		{
			InitializeComponent();
		}

		private void ExportForm_Load(object sender, EventArgs e)
		{
			reportsNames.SelectedIndex = 0;
			exportTypes.SelectedIndex = 0;
		}

		private void exportButton_Click(object sender, EventArgs e)
		{
			var reportName = (string)reportsNames.SelectedItem;
			var exportType = (string)exportTypes.SelectedItem;
			IDocumentExportEx documentExportEx = null;

			var report = new SectionReport();
			System.Xml.XmlTextReader xtr = new System.Xml.XmlTextReader(@"..\..\..\..\Reports\" + reportName);
			report.LoadLayout(xtr);
			report.Run();

			var exporTypeLower = exportType.ToLower();
			switch (exporTypeLower)
			{
				case _pdf:
					documentExportEx = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
					break;
				case _xlsx:
					documentExportEx = new GrapeCity.ActiveReports.Export.Excel.Section.XlsExport(){ FileFormat = GrapeCity.ActiveReports.Export.Excel.Section.FileFormat.Xlsx};
					break;
				case _rtf:
					documentExportEx = new GrapeCity.ActiveReports.Export.Word.Section.RtfExport();
					break;
				case _mht:
					documentExportEx = new GrapeCity.ActiveReports.Export.Html.Section.HtmlExport();
					break;
				case _csv:
					documentExportEx = new GrapeCity.ActiveReports.Export.Xml.Section.TextExport();
					break;
			}

			var fileName = Path.GetFileNameWithoutExtension(reportName);
			var saveFileDialog = new SaveFileDialog()
			{
				FileName = fileName + GetSaveDialogExtension(exporTypeLower),
				Filter = GetSaveDialogFilter(exporTypeLower)
			};


			if (saveFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			exportButton.Enabled = false;
			using (var stream = new FileStream(Path.Combine(saveFileDialog.FileName), FileMode.Create))
				documentExportEx.Export(report.Document, stream);
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
