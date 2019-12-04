using System.ComponentModel;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Calendar.Design.Designers;
using GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Controls;
using GrapeCity.ActiveReports.Calendar.Design.Tools;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Calendar
{
	/// <summary>
	/// Represents the page which allows to specify the style of appontments.
	/// </summary>
	internal sealed class CalendarEventAppearancePage : CalendarPageBase
	{
		public CalendarEventAppearancePage(CalendarDesigner designer) : base(designer)
		{
			InitializeControls();
		}
		private void InitializeControls()
		{
			using (new SuspendLayoutTransaction(this, true))
			{
				PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(Designer.ReportItem);
				PropertyDescriptor appointmentDescriptor = pdc[CalendarDesigner.EventPropertyName];
				object appointmentObject = appointmentDescriptor.GetValue(Designer.ReportItem);
				PropertyDescriptorCollection appointmentConverterProperties = appointmentDescriptor.Converter.GetProperties(appointmentObject);
				//
				// formatEditor
				//
				TypedValueEditor formatEditor = new TypedValueEditor(ServiceProvider,
					Resources.CalendarSmartPanelFormatLabel, ComponentProperty.Create(appointmentConverterProperties[AppointmentStyle.EventFormatPropertyName], appointmentObject), Designer.ReportItem, appointmentDescriptor);
				formatEditor.Margin = System.Windows.Forms.Padding.Empty;
				Controls.Add(formatEditor);
				//
				// textAlignEditor
				//
				TypedValueEditor textAlignEditor = new TypedValueEditor(ServiceProvider,
					Resources.CalendarSmartPanelTextAlignLabel, ComponentProperty.Create(appointmentConverterProperties[AppointmentStyle.EventTextAlignPropertyName], appointmentObject), Designer.ReportItem, appointmentDescriptor);
				textAlignEditor.Margin = System.Windows.Forms.Padding.Empty;
				Controls.Add(textAlignEditor);
				//
				// fontEditor
				//
				DesignTextStyleEditor fontEditor = new DesignTextStyleEditor(ServiceProvider,
					appointmentObject, Designer.ReportItem, appointmentDescriptor, appointmentConverterProperties[AppointmentStyle.EventFontPropertyName]);
				fontEditor.Margin = System.Windows.Forms.Padding.Empty;
				Controls.Add(fontEditor);
				//
				// backgroundLabel
				//
				ControlGroupHeadingLabel backgroundLabel = new ControlGroupHeadingLabel(ServiceProvider);
				backgroundLabel.Text = Resources.CalendarSmartPanelBackgroundLabel;
				backgroundLabel.Dock = DockStyle.Top;
				Controls.Add(backgroundLabel);
				//
				// backgroundColorEditor
				//
				TypedValueEditor backcolorEditor = new TypedValueEditor(ServiceProvider,
					Resources.CalendarSmartPanelFillColorLabel, ComponentProperty.Create(appointmentConverterProperties[AppointmentStyle.EventBackcolorPropertyName], appointmentObject), Designer.ReportItem, appointmentDescriptor);
				backcolorEditor.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, backcolorEditor.ControlInfo, 1);
				Controls.Add(backcolorEditor);
				//
				// borderColorEditor
				//
				TypedValueEditor borderColorEditor = new TypedValueEditor(ServiceProvider,
					Resources.CalendarSmartPanelBorderColorLabel, ComponentProperty.Create(appointmentConverterProperties[AppointmentStyle.EventBorderColorPropertyName], appointmentObject), Designer.ReportItem, appointmentDescriptor);
				borderColorEditor.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, backcolorEditor.ControlInfo, 1);
				Controls.Add(borderColorEditor);
				//
				// Image properties for the appointment
				// 
				DesignImageStyleEditor imageEditor = new DesignImageStyleEditor(ServiceProvider,
					appointmentObject, Designer.ReportItem, appointmentDescriptor, appointmentConverterProperties[AppointmentStyle.EventImagePropertyName]);
				imageEditor.Margin = System.Windows.Forms.Padding.Empty;
				Controls.Add(imageEditor);
			}
		}
	}
}
