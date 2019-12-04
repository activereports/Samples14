using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using GrapeCity.ActiveReports.Calendar.Design.Tools;
using GrapeCity.ActiveReports.Design.DdrDesigner.Actions;

using DesignerActionMethodItem = GrapeCity.ActiveReports.Design.DdrDesigner.Actions.DesignerActionMethodItem;

namespace GrapeCity.ActiveReports.Calendar.Design.Properties
{
	/// <summary>
	/// Displays the Action smart panel page for the calendar control.
	/// </summary>
	//[DoNotObfuscateType]
	internal sealed class ActionUITypeEditor : UITypeEditor
	{
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (context == null)
				return base.EditValue(context, provider, value);
			if (context.Instance == null || !(context.Instance is IComponent))
				return base.EditValue(context, provider, value);

			ICollection paramsOut;

			CommandID commandId = new CommandID(
				new Guid(Utils.ReportsUICommandsGroupGuidString),
				Utils.cmdidEditAction);

			DesignerActionMethodItem.ExecuteCommand(context.Instance as IComponent, commandId, new[] { 0 }, out paramsOut);

			return null;
		}
	}
}
