using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Export;
using System.IO;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Export.Word.Section;
using System;

namespace GrapeCity.ActiveReports.Samples.WordExport
{
	public partial class MainForm : Form
	{
		private const string _path = @"../../../../Reports/";

		public MainForm()
		{
			InitializeComponent();
			var files = Directory.GetFiles(_path);

			foreach (var file in files)
			{
				reports.Items.Add(Path.GetFileName(file));
			}

			exports.Items.Add(new ExportWrapper(new RtfExport(), Properties.Resources.ARExport));
			exports.Items.Add(new ExportWrapper(new HtmlToOpenXmlExport(), Properties.Resources.HtmlToOpenXmlExport));
			exports.Items.Add(new ExportWrapper(new MariGoldExport(), Properties.Resources.MariGoldExport));
		}

		private void reports_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			var filePath = Path.Combine(_path, (string)reports.SelectedItem);
			viewer.LoadDocument(filePath);
			EnableSaveButton();
		}

		private void saveAsButton_Click(object sender, System.EventArgs e)
		{
			var exportType = ((ExportWrapper)exports.SelectedItem).GetExportType();
			var saveFileDialog = new SaveFileDialog()
			{
				FileName = Path.GetFileNameWithoutExtension((string)reports.SelectedItem) + "." + exportType,
				Filter = exportType + " files| *." + exportType
			};

			if (saveFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			var fileName = saveFileDialog.FileName;

			var docExport = (ExportWrapper)exports.SelectedItem;
			docExport.Export(viewer.Document, fileName);
			System.Diagnostics.Process.Start(fileName);
		}

		/// <summary>
		/// Wrapper for export.
		/// </summary>
		private sealed class ExportWrapper
		{
			private IDocumentExport _docExp;
			private string _name;
			private const string _docxExportType = "docx";
			private const string _docExportType = "doc";

			public ExportWrapper(IDocumentExport docExp, string name)
			{
				_docExp = docExp;
				_name = name;
			}

			public override string ToString()
			{
				return _name;
			}

			public string GetExportType()
			{
				return _docExp is RtfExport ? _docExportType : _docxExportType;
			}

			public void Export(SectionDocument document, string destFilePath)
			{
				try
				{
					_docExp.Export(document, destFilePath, null);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void exports_SelectedIndexChanged(object sender, EventArgs e)
		{
			EnableSaveButton();
		}

		private void EnableSaveButton()
		{
			if (!saveAsButton.Enabled && exports.SelectedItem != null && reports.SelectedItem != null)
			{
				saveAsButton.Enabled = true;
			}
		}
	}
}
