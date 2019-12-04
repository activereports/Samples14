using System.Windows.Forms;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Design.DdrDesigner.Services.UI.DesignerActionUI;
using GrapeCity.ActiveReports.Calendar.Design.Designers;
using System.Diagnostics;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Calendar
{
	/// <summary>
	/// Represents the detail grouping editor for calendar RI.
	/// </summary>
	internal sealed class CalendarDetailGroupPage : CalendarPageBase
	{
		public CalendarDetailGroupPage(CalendarDesigner designer) : base(designer)
		{
			InitializeComponents();
		}
		private void InitializeComponents()
		{
			Debug.Assert(Designer.ReportItem.CustomData != null, "CustomData should be initialized.");
			Debug.Assert(Designer.ReportItem.CustomData.DataRowGroupings.Count == 1, "Only one data grouping is allowed.");
			Grouping grouping = Designer.ReportItem.CustomData.DataRowGroupings[0].Grouping;

			using (new SuspendLayoutTransaction(this, true))
			{
				//
				// groupingEditor
				//
				Control _groupsEditor = DesignerActionContainersFactory.CreateGroupsEditor(ServiceProvider, grouping, Designer.ReportItem);
				_groupsEditor.Dock = DockStyle.Fill;
				if (_groupsEditor is DesignerActionContainersFactory.IGroupsEditor)
					((DesignerActionContainersFactory.IGroupsEditor)_groupsEditor).SetGridHeight(100);
				Controls.Add(_groupsEditor);
			}
		}
	}
}
