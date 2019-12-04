using System.ComponentModel;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Calendar.Design.Designers;
using GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Controls;
using GrapeCity.ActiveReports.Calendar.Design.Tools;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Calendar
{
	/// <summary>
	/// Represents the page which allows to specify the style of weekend.
	/// </summary>
	internal sealed class CalendarWeekendAppearancePage : CalendarPageBase
	{
		public CalendarWeekendAppearancePage(CalendarDesigner designer)
			: base(designer)
		{
			InitializeControls();
		}

		private void InitializeControls()
		{
			using (new SuspendLayoutTransaction(this, true))
			{
				PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(Designer.ReportItem);
				PropertyDescriptor weekendDescriptor = pdc[CalendarDesigner.WeekendPropertyName];
				object weekendObject = weekendDescriptor.GetValue(Designer.ReportItem);
				PropertyDescriptorCollection weekendConverterProperties = weekendDescriptor.Converter.GetProperties(weekendObject);
				//
				// fontEditor
				//
				DesignTextStyleEditor fontEditor = new DesignTextStyleEditor(ServiceProvider,
					weekendObject, Designer.ReportItem, weekendDescriptor, weekendConverterProperties[WeekendStyle.WeekendFontStylePropertyName]);
				Controls.Add(fontEditor);
				//
				// borderEditor
				//
				DesignLineStyleEditor borderEditor = new DesignLineStyleEditor(ServiceProvider,
					weekendObject, Designer.ReportItem, weekendDescriptor, weekendConverterProperties[WeekendStyle.WeekendBorderStylePropertyName]);
				Controls.Add(borderEditor);
				//
				// backcolorEditor
				//
				TypedValueEditor backcolorEditor = new TypedValueEditor(ServiceProvider,
					Resources.CalendarSmartPanelBackgroundFillColorLabel, ComponentProperty.Create(weekendConverterProperties[WeekendStyle.WeekendBackcolorPropertyName], weekendObject), Designer.ReportItem, weekendDescriptor);
				backcolorEditor.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, backcolorEditor.ControlInfo, 1);
				Controls.Add(backcolorEditor);
				//
				// formatLabel
				//
				ControlGroupHeadingLabel formatLabel = new ControlGroupHeadingLabel(ServiceProvider);
				formatLabel.Text = Resources.CalendarSmartPanelFormattingLabel;
				formatLabel.Dock = DockStyle.Top;
				Controls.Add(formatLabel);
				//
				// textAlignEditor
				//
				TypedValueEditor textAlignEditor = new TypedValueEditor(ServiceProvider,
					Resources.CalendarSmartPanelTextAlignLabel, ComponentProperty.Create(weekendConverterProperties[WeekendStyle.WeekendTextAlignPropertyName], weekendObject), Designer.ReportItem, weekendDescriptor);
				textAlignEditor.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, textAlignEditor.ControlInfo, 1);
				Controls.Add(textAlignEditor);
				//
				// formatEditor
				//
				TypedValueEditor verticalAlignEditor = new TypedValueEditor(ServiceProvider,
					Resources.CalendarSmartPanelVerticalAlignLabel, ComponentProperty.Create(weekendConverterProperties[WeekendStyle.WeekendVerticalAlignPropertyName], weekendObject), Designer.ReportItem, weekendDescriptor);
				verticalAlignEditor.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, verticalAlignEditor.ControlInfo, 1);
				Controls.Add(verticalAlignEditor);
			}
		}
	}
}
