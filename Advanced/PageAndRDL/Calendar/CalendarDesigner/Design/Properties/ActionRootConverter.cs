using System;
using System.ComponentModel;
using System.Globalization;

using Action = GrapeCity.ActiveReports.PageReportModel.Action;

namespace GrapeCity.ActiveReports.Calendar.Design.Properties
{
	/// <summary>
	/// ActionRootConverter
	/// </summary>
	////[DoNotObfuscateType]
	internal sealed class ActionRootConverter : TypeConverter
	{
		private const string EmptyString = "\"\"";

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			Action action = value as Action;
			if (action != null)
			{//Determine which is set
				if (action.BookmarkLink != null &&
					action.BookmarkLink.Expression != null &&
					action.BookmarkLink.Expression != EmptyString)
				{
					return "Jump to bookmark";
				}
				if (action.Hyperlink != null &&
					action.Hyperlink.Expression != null &&
					action.Hyperlink.Expression != EmptyString)
				{
					return "Jump to URL";
				}
				if (action.Drillthrough != null &&
					!action.Drillthrough.ReportName.IsEmptyString)
				{
					return "Jump to Report";
				}
				return "None";
			}
			return "(Action)";
		}
	}
}
