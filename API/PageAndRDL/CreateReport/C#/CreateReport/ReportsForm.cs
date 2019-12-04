using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml;
namespace GrapeCity.ActiveReports.Samples.CreateReport
{
	public partial class ReportsForm : Form
	{
		public ReportsForm()
		{
			InitializeComponent();
		}

		private void ReportsForm_Load(object sender, EventArgs e)
		{
			// Load a layout into a page report object.
			PageReport report = LayoutBuilder.BuildReportLayout();
			// Add a data source to a page report object.
			report = LayoutBuilder.AddDataSetDataSource(report);
			// Load a page report object into a stream.
			MemoryStream reportStream = LayoutBuilder.LoadReportToStream(report);
			reportStream.Position = 0;
			// Load the stream to the Viewer.
			viewer1.LoadDocument(reportStream, GrapeCity.Viewer.Common.DocumentFormat.Rdlx);

			report.Dispose();
			reportStream.Dispose();
		}
	}
}
