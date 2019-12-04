using GrapeCity.ActiveReports.Calendar.Design.Designers;
using System.Windows.Forms;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Calendar
{
	/// <summary>
	/// Defines general page for calendar chart wizard.
	/// </summary>
	internal sealed class CalendarGeneralPage : CalendarPageBase
	{
		public CalendarGeneralPage(CalendarDesigner designer)
			: base(designer)
		{
			InitializeControls();
		}

		private void InitializeControls()
		{
			using (new SuspendLayoutTransaction(this, true))
			{
				//
				// nameEditor
				//
				Control reportItemNameEditor = CreateReportItemNameEditor(Designer.ReportItem);
				reportItemNameEditor.Dock = DockStyle.Fill;
				Controls.Add(reportItemNameEditor);
			}
		}
	}
}
