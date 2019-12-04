using System;
using System.Windows.Forms;
using System.Xml;
using GrapeCity.ActiveReports.Data;
using GrapeCity.ActiveReports.SectionReportModel;

namespace GrapeCity.ActiveReports.Samples.CrossSectionControls
{
	public partial class ViewerForm : Form
	{
		enum ReportType
		{
			CrossSectionControls,
			RepeatToFill,
			PrintAtBottom
		}

		private delegate void LoadReportInvoker(ReportType reportType);

		public ViewerForm()
		{
			InitializeComponent();
		}

		private void ViewerForm_Load(object sender, EventArgs e)
		{
			// Allow the Form to open while the report is rendering.
			BeginInvoke(new LoadReportInvoker(LoadReport), ReportType.CrossSectionControls);
			BeginInvoke(new LoadReportInvoker(LoadReport), ReportType.PrintAtBottom);
			BeginInvoke(new LoadReportInvoker(LoadReport), ReportType.RepeatToFill);
		}

		void LoadReport(ReportType reportType)
		{
			// Instantiate a new Invoice report
			var report = new SectionReport();
			report.LoadLayout(XmlReader.Create(Properties.Resources.Invoice));

			report.MaxPages = 10;

			// Set the connection string to the sample database.
			((OleDBDataSource)report.DataSource).ConnectionString = Properties.Resources.ConnectionString;			
			switch (reportType)
			{
				case ReportType.CrossSectionControls:
					cscViewer.LoadDocument(report);
					break;
				case ReportType.RepeatToFill:
					((Detail) report.Sections["Detail"]).RepeatToFill = true;
					repeatToFillViewer.LoadDocument(report);
					break;
				case ReportType.PrintAtBottom:
					((GroupFooter) report.Sections["customerGroupFooter"]).PrintAtBottom = true;
					printAtBottomViewer.LoadDocument(report);
					break;
			}
		}
	}
}
