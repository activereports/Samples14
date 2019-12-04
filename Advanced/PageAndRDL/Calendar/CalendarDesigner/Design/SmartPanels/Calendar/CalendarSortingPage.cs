using System.Windows.Forms;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Design.DdrDesigner.Services.UI.DesignerActionUI;
using GrapeCity.ActiveReports.Calendar.Design.Designers;
using System.Diagnostics;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Calendar
{
	/// <summary>
	/// Defines sorting page for calendar chart wizard.
	/// </summary>
	internal class CalendarSortingPage : CalendarPageBase
	{
		public CalendarSortingPage(CalendarDesigner designer)
			: base(designer)
		{
			InitializeControls();
		}

		private void InitializeControls()
		{
			Debug.Assert(Designer.ReportItem.CustomData != null, "CustomData should be initialized.");
			Debug.Assert(Designer.ReportItem.CustomData.DataRowGroupings.Count == 1, "Only one data grouping is allowed.");
			SortByCollection sorting = Designer.ReportItem.CustomData.DataRowGroupings[0].Sorting;

			using (new SuspendLayoutTransaction(this))
			{
				Control sortingEditor =
					DesignerActionContainersFactory.CreateSortingEditor(ServiceProvider, sorting, Designer.ReportItem);
				using (new SuspendLayoutTransaction(sortingEditor))
				{
					sortingEditor.Dock = DockStyle.Fill;
				}
				Controls.Add(sortingEditor);
			}
		}
	}
}
