using GrapeCity.ActiveReports.Calendar.Design.Designers;
using System.Windows.Forms;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Calendar
{
	/// <summary>
	/// Represents the page which allows to specify the calendar appearance options.
	/// </summary>
	internal sealed class CalendarAppearancePage : CalendarPageBase
	{
		public CalendarAppearancePage(CalendarDesigner designer)
			: base(designer)
		{
			InitializeControls();
		}

		private void InitializeControls()
		{
			using (new SuspendLayoutTransaction(this, true))
			{
				Control childPanel =
					CreateSmartPanelControl(CalendarSmartPanelBuilder.CreateCalendarAppearanceChildPage(Designer));
				childPanel.Dock = DockStyle.Fill;
				Controls.Add(childPanel);
			}
		}
	}
}
