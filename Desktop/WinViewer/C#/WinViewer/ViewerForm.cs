using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.ActiveReports.ReportsCore.Configuration;
using GrapeCity.ActiveReports.Viewer.Helper;
using GrapeCity.ActiveReports.Viewer.Win.Internal.Export;
using GrapeCity.ActiveReports.Win.Export;

namespace GrapeCity.ActiveReports.Viewer.Win
{
	internal partial class ViewerForm : Form
	{
		private ExportForm _exportForm;
		private ExportForm.ReportType? _reportType;
		private string _openFileName;

		public ViewerForm()
		{
			InitializeComponent();
			Icon = Properties.Resource.App;
			SetReportName(null);
		}

		/// <summary>
		/// Loads the document.
		/// </summary>
		/// <param name="file">The file.</param>
		public void LoadDocument(FileInfo file)
		{
			try
			{
				_reportType = ViewerHelper.DetermineReportType(file);
				bool isRdf = ViewerHelper.IsRdf((file));
				var reportServerUri = !isRdf ? ViewerHelper.GetReportServerUri(file) : string.Empty;
				
				if (!string.IsNullOrEmpty(reportServerUri))
				{

					throw new NotSupportedException("Server report is not supported");
				}
				else
					viewer.LoadDocument(file.FullName);

				_openFileName = file.FullName;
				exportToolStripMenuItem.Enabled = true;
				saveAsRDFToolStripMenuItem.Enabled = true;
				SetReportName(openFileDialog.FileName);
			}
			catch (Exception ex)
			{
				viewer.HandleError(ex);
			}
		}

		private void OpenMenuItemHandler(object sender, EventArgs e)
		{
			openFileDialog.FileName = string.Empty;
			if (openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				LoadDocument(new FileInfo(openFileDialog.FileName));
			}
		}

		private void CloseMenuItemHandler(object sender, EventArgs e)
		{
			Close();
		}

		private void SetReportName(string reportName)
		{
			Text = string.IsNullOrEmpty(reportName) ? string.Format( Properties.Resource.DefaultTitleFormat, Properties.Resource.ViewerFormTitle) : string.Format(Properties.Resource.TitleFormat, Properties.Resource.ViewerFormTitle, Path.GetFileName(reportName));
		}

		private void AboutMenuItemHandler(object sender, EventArgs e)
		{
			const string showAboutBoxMethodName = "ShowAboutBox";
			var attributes = viewer.GetType().GetCustomAttributes(typeof(LicenseProviderAttribute), false);
			if (attributes.Length != 1) return;
			var attr = (LicenseProviderAttribute)attributes[0];
			var provider = ((LicenseProvider)Activator.CreateInstance(attr.LicenseProvider));

			var methodInfo = provider.GetType().GetMethod(showAboutBoxMethodName, BindingFlags.NonPublic | BindingFlags.Instance);

			if (methodInfo != null)
				methodInfo.Invoke(provider, new object[] { viewer.GetType() });
		}

		private void ExportMenuItemHandler(object sender, EventArgs e)
		{
			if (_exportForm == null)
			{
				_exportForm = new ExportForm(ConfigurationHelper.GetConfigFlag(ConfigurationHelper.UsePdfExportFilterKey) == true);
			}

			var reportType = _reportType ?? DetermineOpenedReportType();

			_exportForm.Show(this, new ExportViewer(viewer), reportType);
		}

		private ExportForm.ReportType DetermineOpenedReportType()
		{
			if (viewer.Document != null)
				return ExportForm.ReportType.Section;
			if (viewer.IsFplDocumentOpened())
				return ExportForm.ReportType.PageFpl;
			return ExportForm.ReportType.PageCpl;
		}

		private void SaveAsRDFItemHandler(object sender, EventArgs e)
		{
			var saveFileDialog = new SaveFileDialog()
			{
				FileName = Path.GetFileNameWithoutExtension(_openFileName) + ".rdf",
				Filter = "*.rdf | *.rdf"
			};

			if (saveFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			string saveFileName = saveFileDialog.FileName;
			

			if (_reportType.Value != ExportForm.ReportType.Section )
			{
				var outputDirectory = new DirectoryInfo(Path.GetDirectoryName(saveFileName));
				var outputProvider = new ActiveReports.Rendering.IO.FileStreamProvider(outputDirectory, Path.GetFileNameWithoutExtension(saveFileName));
				outputProvider.OverwriteOutputFile = true;
				var renderingExtension = new Export.Rdf.RdfRenderingExtension();
				var settings = new Export.Rdf.Settings();
				viewer.Render(renderingExtension, outputProvider, settings);
			}
			else
			{
				using (var stream = new FileStream(Path.Combine(saveFileDialog.FileName), FileMode.Create))
					viewer.Document.Save(stream, Document.Section.RdfFormat.ARNet);
			}
		}
	}
}
