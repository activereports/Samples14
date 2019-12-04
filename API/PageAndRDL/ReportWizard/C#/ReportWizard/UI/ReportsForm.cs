using System;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace GrapeCity.ActiveReports.Samples.ReportWizard.UI
{
	public partial class ReportsForm : Form
	{
		private readonly PageReport _reportDefinition;

		public ReportsForm(PageReport reportDefinition)
		{
			_reportDefinition = reportDefinition;
			InitializeComponent();
		}

		private static Stream SaveReportToStream(PageReport def)
		{
			MemoryStream stream = new MemoryStream();
			using (XmlWriter writer = XmlWriter.Create(stream))
			{
				def.Save(writer);
			}
			stream.Position = 0;
			return stream;
		}

		private void ReportsForm_Load(object sender, EventArgs e)
		{
			using (Stream stream = SaveReportToStream(_reportDefinition))
			{
				arvMain.LoadDocument(stream, GrapeCity.Viewer.Common.DocumentFormat.Rdlx);

			}
		}
	}
}
