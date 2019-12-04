using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Export.Pdf.Page;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.IO;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Rendering.IO;
using GrapeCity.ActiveReports.Samples.Export.Rendering.Pdf;
using GrapeCity.ActiveReports.Samples.Export.Rendering.Properties;

namespace GrapeCity.ActiveReports.Samples.Export.Rendering
{
	public sealed partial class MainForm : Form
	{
		private const string ReportsPath = @"..\..\..\..\Reports\";

		private DateTime _startTimeout = DateTime.Now;

		private readonly IRenderingExtension _arPdf = new PdfRenderingExtension();
		private readonly IRenderingExtension _sharpPdf = new PdfSharpRenderingExtension();

		public MainForm()
		{
			InitializeComponent();

			arViewer.RefreshReport += (_, __) =>
				{
					_startTimeout = DateTime.Now;
				};
			arViewer.LoadCompleted += (_, __) =>
				{
					BeginInvoke(new MethodInvoker(() =>
						{
							try
							{
								arTime.Text = Math.Ceiling((DateTime.Now - _startTimeout).TotalMilliseconds).ToString() + "ms";
							}
							catch (Exception ex)
							{
								MessageBox.Show(this, string.Format(Resources.ErrorMessage, ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}));
				};
		}

		protected override void OnLoad(EventArgs e)
		{
			splitContainer1.Panel1MinSize = 190;
			splitContainer1.Panel2MinSize = 400;
			splitContainer3.Panel1MinSize = 250;
			splitContainer3.Panel2MinSize = 250;
			base.OnLoad(e);
			SetPropertyGrid();
			foreach (var reportFile in Directory.GetFiles(ReportsPath, "*.rdlx"))
				reports.Items.Add(Path.GetFileName(reportFile));
		}

		private void reports_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetPropertyGrid();
			ButtonsEnable();
		}

		private void exports_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetPropertyGrid();
			ButtonsEnable();
		}

		private void SetPropertyGrid()
		{
			if (!radioButton1.Checked && !radioButton2.Checked)
				return;

			var export = radioButton1.Checked ? _arPdf : _sharpPdf;
			if (export != null && reports.SelectedItem != null)
				propertyGrid.SelectedObject = ((IConfigurable)export).GetSupportedSettings(reports.SelectedItem != null ? IsFpl((string)reports.SelectedItem) : false);
		}

		private void ButtonsEnable()
		{
			if ((radioButton1.Checked || radioButton2.Checked) && reports.SelectedItem != null)
			{
				exportButton.Enabled = true;
				saveAsButton.Enabled = true;
			}
		}


		private bool IsFpl(string reportFile)
		{
			using (var pageReport = new PageReport(new FileInfo(Path.Combine(ReportsPath, reportFile))))
			{
				var report = pageReport.Report;
				if (report == null || report.Body == null)
					return false;
				var items = report.Body.ReportItems;
				return items != null && items.Count == 1 && items[0] is FixedPage;
			}
		}

		private void exportButton_Click(object sender, EventArgs e)
		{
			_startTimeout = DateTime.Now;
			var reportPath = Path.Combine(ReportsPath, (string)reports.SelectedItem);
			arViewer.LoadDocument(new PageReport(new FileInfo(reportPath)).Document);
			RenderPdf(new MemoryStreamProvider(), streams =>
				{
					BeginInvoke(new MethodInvoker(() =>
						{
							try
							{
								pdfTime.Text = Math.Ceiling((DateTime.Now - _startTimeout).TotalMilliseconds).ToString()+"ms";
								pdfViewer.Document = PdfiumViewer.PdfDocument.Load(streams.GetPrimaryStream().OpenStream());
							}
							catch (Exception ex)
							{
								MessageBox.Show(this, string.Format(Resources.ErrorMessage, ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}));
				});
		}

		private void saveAsButton_Click(object sender, EventArgs e)
		{
			if (saveAsPdf.ShowDialog(this) != DialogResult.OK || string.IsNullOrEmpty(saveAsPdf.FileName))
				return;
			var pdfPath = saveAsPdf.FileName;
			RenderPdf(new FileStreamProvider(pdfPath), _ =>
				{
					try
					{
						Process.Start(pdfPath);
					}
					catch (Exception ex)
					{
						MessageBox.Show(this, string.Format(Resources.ErrorMessage, ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				});
		}

		private void RenderPdf(StreamProvider streams, Action<StreamProvider> postAction)
		{
			var reportPath = Path.Combine(ReportsPath, (string)reports.SelectedItem);
			var pdfSettings = (ISettings)propertyGrid.SelectedObject;
			var export = radioButton1.Checked ? _arPdf : _sharpPdf;

			Cursor = Cursors.WaitCursor;
			Enabled = false;
			var thread = new Thread(_ =>
				{
					try
					{
						using (var report = new PageReport(new FileInfo(reportPath)))
							report.Document.Render(export, streams, pdfSettings.GetSettings());
						postAction(streams);
					}
					catch (Exception ex)
					{
						BeginInvoke(
							new MethodInvoker(() =>
								{
									MessageBox.Show(this, string.Format(Resources.ErrorMessage, ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
								}));
					}
					finally
					{
						BeginInvoke(new MethodInvoker(() => { Enabled = true; }));
						BeginInvoke(new MethodInvoker(() => { Cursor = Cursors.Default; }));
					}
				});
			thread.Start();
		}
	}
}
