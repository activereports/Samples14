using System.ComponentModel.Design;
using System.Diagnostics;
using GrapeCity.ActiveReports.Calendar.Design.Designers;

namespace GrapeCity.ActiveReports.Calendar.Rendering
{
	/// <summary>
	/// DesignerImageLocatorService
	/// </summary>
	public sealed class DesignerImageLocatorService : ImageLocatorService
	{
		public DesignerImageLocatorService(CalendarDesigner calendarDesigner)
		{
			IDesignerHost host = calendarDesigner.ReportItem.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (host == null)
			{
				Debug.Fail("Can get IDesignerHost for calendar report item");
				return;
			}
			_parentPageReport = host.RootComponent as PageReport;

			InitializeServices();
		}
	}
}
