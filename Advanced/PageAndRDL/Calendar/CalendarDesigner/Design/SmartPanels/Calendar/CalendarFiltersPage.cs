using GrapeCity.ActiveReports.Calendar.Design.Designers;
using GrapeCity.ActiveReports.Design.DdrDesigner.Designers;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Calendar
{
	/// <summary>
	/// Defines filters page for calendar wizard.
	/// </summary>
	internal sealed class CalendarFiltersPage : FiltersPageBase
	{
		public CalendarFiltersPage(CalendarDesigner designer)
			: base(designer.ReportItem.Site, new CustomDataFiltersPropertyProvider(designer.ReportItem))
		{
		}
	}
}
