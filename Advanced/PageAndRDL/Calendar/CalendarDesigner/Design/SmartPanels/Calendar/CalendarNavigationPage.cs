using System;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Calendar.Design.Designers;
using GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Controls;
using GrapeCity.ActiveReports.Rdl.Tools;
using Action = GrapeCity.ActiveReports.PageReportModel.Action;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Calendar
{
	/// <summary>
	/// Defines navigation page for calendar chart wizard.
	/// </summary>
	internal sealed class CalendarNavigationPage : CalendarPageBase
	{
		private Action _action;

		public CalendarNavigationPage(CalendarDesigner designer)
			: base(designer)
		{
			InitializeControls();
			VisibleChanged += CalendarNavigationPage_VisibleChanged;
		}

		void CalendarNavigationPage_VisibleChanged(object sender, EventArgs e)
		{
			Designer.BookmarkLink = _action.BookmarkLink;
			Designer.Hyperlink = _action.Hyperlink;
			Designer.Drillthrough = _action.Drillthrough;
		}

		private void InitializeControls()
		{
			_action = new Action();
			_action.BookmarkLink = Designer.BookmarkLink;
			_action.Hyperlink = Designer.Hyperlink;
			_action.Drillthrough.ReportName = Designer.Drillthrough.ReportName;
			_action.Drillthrough.Parameters.AddRange(Designer.Drillthrough.Parameters);

			using (new SuspendLayoutTransaction(this, true))
			{
				Control editor = CreateActionEditor(Designer.ReportItem, _action);
				editor.Dock = DockStyle.Top;
				Controls.Add(editor);

				Controls.Add(new TypedValueEditor(ServiceProvider,
					Resources.CalendarSmartPanelDocumentMapLabel, Designer.ReportItem, ReportItemDesignerBase.DocumentMapLabelPropertyName));

				Controls.Add(new TypedValueEditor(ServiceProvider,
					Resources.CalendarSmartPanelBookmarkIDLabel, Designer.ReportItem, ReportItemDesignerBase.BookmarkIDPropertyName));
			}
		}
	}
}
