using System.Windows.Forms.Layout;
using GrapeCity.ActiveReports.Calendar.Design.Designers;
using GrapeCity.ActiveReports.Design.DdrDesigner.Services.UI.DesignerActionUI;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Calendar
{
	/// <summary>
	/// Defines base page for calendar wizard.
	/// </summary>
	internal class CalendarPageBase : DesignerActionPage
	{
		private readonly CalendarDesigner _designer;

		protected CalendarPageBase(CalendarDesigner designer)
			: base(designer.ReportItem.Site)
		{
			_designer = designer;
		}

		public CalendarDesigner Designer
		{
			get { return _designer; }
		}

		public override LayoutEngine LayoutEngine
		{
			get { return VerticalFlowLayoutEngine.Instance; }
		}
	}
}
