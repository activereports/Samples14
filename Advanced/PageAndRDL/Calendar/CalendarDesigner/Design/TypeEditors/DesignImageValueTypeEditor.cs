using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using GrapeCity.ActiveReports.Calendar.Design.Designers;
using GrapeCity.ActiveReports.Calendar.Design.Properties;
using GrapeCity.ActiveReports.Design.DdrDesigner.UITypeEditors.ImageValueEditors;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;

namespace GrapeCity.ActiveReports.Calendar.Design.TypeEditors
{
	internal sealed class DesignImageValueTypeEditor : ImageValueTypeEditorBase
	{
		protected override bool ImageSourceIsExternal(ITypeDescriptorContext context, IServiceProvider provider)
		{
			var designImage = GetInstance(context);
			return designImage != null && designImage.Value.ImageSource == ExpressionInfo.FromString(ImageSource.External.ToString());
		}

		private static DesignImage? GetInstance(ITypeDescriptorContext context)
		{
			if (context.Instance is DesignImage)
				return (DesignImage)context.Instance;

			var host = (IDesignerHost)context.Container;
			var component = context.Instance as IComponent;
			var designer = component != null ? host.GetDesigner(component) as CalendarDesigner : null;
			return designer != null ? designer.EventStyle.AppointmentImage : (DesignImage?)null;
		}
	}
}
