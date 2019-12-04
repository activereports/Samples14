using System.ComponentModel;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Calendar.Design.Designers;
using GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Controls;
using GrapeCity.ActiveReports.Calendar.Design.Tools;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Calendar
{
	/// <summary>
	/// Represents the page which allows to specify the style of filler day.
	/// </summary>
	internal sealed class CalendarFillerDayAppearancePage : CalendarPageBase
	{
		public CalendarFillerDayAppearancePage(CalendarDesigner designer)
			: base(designer)
		{
			InitializeControls();
		}

		private void InitializeControls()
		{
			using (new SuspendLayoutTransaction(this, true))
			{
				PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(Designer.ReportItem);
				PropertyDescriptor fillerDayDescriptor = pdc[CalendarDesigner.FillerDayPropertyName];
				object fillerDayObject = fillerDayDescriptor.GetValue(Designer.ReportItem);
				PropertyDescriptorCollection fillerDayConverterProperties = fillerDayDescriptor.Converter.GetProperties(fillerDayObject);
				//
				// fontEditor
				//
				DesignTextStyleEditor fontEditor = new DesignTextStyleEditor(ServiceProvider,
					fillerDayObject, Designer.ReportItem, fillerDayDescriptor, fillerDayConverterProperties[FillerDayStyle.FillerDayFontStylePropertyName]);
				Controls.Add(fontEditor);
				//
				// borderEditor
				//
				DesignLineStyleEditor borderEditor = new DesignLineStyleEditor(ServiceProvider,
					fillerDayObject, Designer.ReportItem, fillerDayDescriptor, fillerDayConverterProperties[FillerDayStyle.FillerDayBorderStylePropertyName]);
				Controls.Add(borderEditor);
				//
				// backcolorEditor
				//
				TypedValueEditor backcolorEditor = new TypedValueEditor(ServiceProvider,
					Resources.CalendarSmartPanelBackgroundFillColorLabel, ComponentProperty.Create(fillerDayConverterProperties[FillerDayStyle.FillerDayBackcolorPropertyName], fillerDayObject), Designer.ReportItem, fillerDayDescriptor);
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
					Resources.CalendarSmartPanelTextAlignLabel, ComponentProperty.Create(fillerDayConverterProperties[FillerDayStyle.FillerDayTextAlignPropertyName], fillerDayObject), Designer.ReportItem, fillerDayDescriptor);
				textAlignEditor.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, textAlignEditor.ControlInfo, 1);
				Controls.Add(textAlignEditor);
				//
				// formatEditor
				//
				TypedValueEditor verticalAlignEditor = new TypedValueEditor(ServiceProvider,
					Resources.CalendarSmartPanelVerticalAlignLabel, ComponentProperty.Create(fillerDayConverterProperties[FillerDayStyle.FillerDayVerticalAlignPropertyName], fillerDayObject), Designer.ReportItem, fillerDayDescriptor);
				verticalAlignEditor.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, verticalAlignEditor.ControlInfo, 1);
				Controls.Add(verticalAlignEditor);
			}
		}
	}
}
