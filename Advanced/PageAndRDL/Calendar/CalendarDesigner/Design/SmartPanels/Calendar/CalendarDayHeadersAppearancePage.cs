using System.ComponentModel;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Calendar.Design.Designers;
using GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Controls;
using GrapeCity.ActiveReports.Calendar.Design.Tools;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Calendar
{
	internal sealed class CalendarDayHeadersAppearancePage : CalendarPageBase
	{
		public CalendarDayHeadersAppearancePage(CalendarDesigner designer)
			: base(designer)
		{
			InitializeControls();
		}

		private void InitializeControls()
		{
			using (new SuspendLayoutTransaction(this, true))
			{
				PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(Designer.ReportItem);
				PropertyDescriptor dayHeadersDescriptor = pdc[CalendarDesigner.DayHeadersPropertyName];
				object dayHeadersObject = dayHeadersDescriptor.GetValue(Designer.ReportItem);
				PropertyDescriptorCollection dayHeadersConverterProperties = dayHeadersDescriptor.Converter.GetProperties(dayHeadersObject);
				//
				// fontEditor
				//				
				DesignTextStyleEditor fontEditor = new DesignTextStyleEditor(ServiceProvider,
					dayHeadersObject, Designer.ReportItem, dayHeadersDescriptor, dayHeadersConverterProperties[DayHeadersStyle.DayHeadersFontStyleName]);
				Controls.Add(fontEditor);
				//
				// borderEditor
				//
				DesignLineStyleEditor borderEditor = new DesignLineStyleEditor(ServiceProvider,
					dayHeadersObject, Designer.ReportItem, dayHeadersDescriptor, dayHeadersConverterProperties[DayHeadersStyle.DayHeadersBorderName]);
				Controls.Add(borderEditor);
				//
				// backcolorEditor
				//
				TypedValueEditor backcolorEditor = new TypedValueEditor(ServiceProvider,
					Resources.CalendarSmartPanelBackgroundFillColorLabel, ComponentProperty.Create(dayHeadersConverterProperties[DayHeadersStyle.DayHeadersBackColorName], dayHeadersObject), Designer.ReportItem, dayHeadersDescriptor);
				backcolorEditor.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, backcolorEditor.ControlInfo, 1);
				Controls.Add(backcolorEditor);
			}
		}
	}
}
