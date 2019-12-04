using System.ComponentModel;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Calendar.Design.Designers;
using GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Controls;
using GrapeCity.ActiveReports.Calendar.Design.Tools;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Calendar
{
	/// <summary>
	/// Represents the page which allows to specify the style of month title.
	/// </summary>
	internal sealed class CalendarMonthAppearancePage : CalendarPageBase
	{
		public CalendarMonthAppearancePage(CalendarDesigner designer) : base(designer)
		{
			InitializeControls();
		}

		private void InitializeControls()
		{
			using (new SuspendLayoutTransaction(this, true))
			{
				PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(Designer.ReportItem);
				PropertyDescriptor monthTitleDescriptor = pdc[CalendarDesigner.MonthTitlePropertyName];
				object monthTitleObject = monthTitleDescriptor.GetValue(Designer.ReportItem);
				PropertyDescriptorCollection monthTitleConverterProperties = monthTitleDescriptor.Converter.GetProperties(monthTitleObject);
				//
				// fontEditor
				//
				DesignTextStyleEditor fontEditor = new DesignTextStyleEditor(ServiceProvider,
					monthTitleObject, Designer.ReportItem, monthTitleDescriptor, monthTitleConverterProperties[MonthTitleStyle.MonthTitleFontStylePropertyName]);
				Controls.Add(fontEditor);
				//
				// borderEditor
				//				
				DesignLineStyleEditor borderEditor = new DesignLineStyleEditor(ServiceProvider,
					monthTitleObject, Designer.ReportItem, monthTitleDescriptor, monthTitleConverterProperties[MonthTitleStyle.MonthTitleBorderStylePropertyName]);
				Controls.Add(borderEditor);
				//
				// backcolorEditor
				//			
				TypedValueEditor backcolorEditor = new TypedValueEditor(ServiceProvider,
					Resources.CalendarSmartPanelBackgroundFillColorLabel, ComponentProperty.Create(monthTitleConverterProperties[MonthTitleStyle.MonthTitleBackcolorPropertyName], monthTitleObject), Designer.ReportItem, monthTitleDescriptor);
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
					Resources.CalendarSmartPanelTextAlignLabel, ComponentProperty.Create(monthTitleConverterProperties[MonthTitleStyle.MonthTitleTextAlignPropertyName], monthTitleObject), Designer.ReportItem, monthTitleDescriptor);
				textAlignEditor.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, textAlignEditor.ControlInfo, 1);
				Controls.Add(textAlignEditor);
				//
				// formatEditor
				//
				TypedValueEditor formatEditor = new TypedValueEditor(ServiceProvider,
					Resources.CalendarSmartPanelFormatLabel, ComponentProperty.Create(monthTitleConverterProperties[MonthTitleStyle.MonthTitleFormatPropertyName], monthTitleObject), Designer.ReportItem, monthTitleDescriptor);
				formatEditor.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, formatEditor.ControlInfo, 1);
				Controls.Add(formatEditor);
			}
		}
	}
}
