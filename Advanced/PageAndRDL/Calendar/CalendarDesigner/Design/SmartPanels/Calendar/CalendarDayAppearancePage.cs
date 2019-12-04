using System.ComponentModel;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Calendar.Design.Designers;
using GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Controls;
using GrapeCity.ActiveReports.Calendar.Design.Tools;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Calendar
{
	/// <summary>
	/// Represents the page which allows to specify the style of general day.
	/// </summary>
	internal sealed class CalendarDayAppearancePage : CalendarPageBase
	{
		public CalendarDayAppearancePage(CalendarDesigner designer)
			: base(designer)
		{
			InitializeControls();
		}

		private void InitializeControls()
		{
			using (new SuspendLayoutTransaction(this, true))
			{
				PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(Designer.ReportItem);
				PropertyDescriptor dayDescriptor = pdc[CalendarDesigner.DayPropertyName];
				object dayObject = dayDescriptor.GetValue(Designer.ReportItem);
				PropertyDescriptorCollection dayConverterProperties = dayDescriptor.Converter.GetProperties(dayObject);
				//
				// fontEditor
				//
				DesignTextStyleEditor fontEditor = new DesignTextStyleEditor(ServiceProvider,
					dayObject, Designer.ReportItem, dayDescriptor, dayConverterProperties[DayStyle.DayFontStylePropertyName]);
				Controls.Add(fontEditor);
				//
				// borderEditor
				//
				DesignLineStyleEditor borderEditor = new DesignLineStyleEditor(ServiceProvider,
					dayObject, Designer.ReportItem, dayDescriptor, dayConverterProperties[DayStyle.DayBorderStylePropertyName]);
				Controls.Add(borderEditor);
				//
				// backcolorEditor
				//
				TypedValueEditor backcolorEditor = new TypedValueEditor(ServiceProvider,
					Resources.CalendarSmartPanelBackgroundFillColorLabel, ComponentProperty.Create(dayConverterProperties[DayStyle.DayBackcolorPropertyName], dayObject), Designer.ReportItem, dayDescriptor);
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
					Resources.CalendarSmartPanelTextAlignLabel, ComponentProperty.Create(dayConverterProperties[DayStyle.DayTextAlignPropertyName], dayObject), Designer.ReportItem, dayDescriptor);
				textAlignEditor.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, textAlignEditor.ControlInfo, 1);
				Controls.Add(textAlignEditor);
				//
				// formatEditor
				//
				TypedValueEditor verticalAlignEditor = new TypedValueEditor(ServiceProvider,
					Resources.CalendarSmartPanelVerticalAlignLabel, ComponentProperty.Create(dayConverterProperties[DayStyle.DayVerticalAlignPropertyName], dayObject), Designer.ReportItem, dayDescriptor);
				verticalAlignEditor.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, verticalAlignEditor.ControlInfo, 1);
				Controls.Add(verticalAlignEditor);
			}
		}
	}
}
