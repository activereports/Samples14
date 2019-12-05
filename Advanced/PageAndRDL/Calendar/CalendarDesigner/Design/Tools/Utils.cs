using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using GrapeCity.ActiveReports.Design;
using GrapeCity.ActiveReports.Design.DdrDesigner.Tools;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Design.DdrDesigner.ReportViewerWinForms.UI;
using GrapeCity.ActiveReports.Design.DdrDesigner.ReportViewerWinForms.UI.Controls;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;
using GrapeCity.ActiveReports.Calendar.Design.Designers;
using System.Diagnostics;

namespace GrapeCity.ActiveReports.Calendar.Design.Tools
{
	/// <summary>
	/// Represents utility methods to use in user controls for smart panels.
	/// </summary>
	internal static class Utils
	{
		// common cmd IDs
		public const string VSStandardMenuGroupGuidString = "5efc7975-14bc-11cf-9b2b-00aa00573819";
		public const string ReportsUICommandsGroupGuidString = Guids.ReportsUICommandsGroupGuidString;
		public const int cmdidCut = 0x0010;//16
		public const int cmdidDelete = 0x0011;//17
		public const int cmdidPaste = 0x001A;//26
		public const int cmdidEditCalendarData = 0x016A;//362
		public const int cmdidEditAction = 0xF020;//61472


		/// <summary>
		/// Searches for the innermost data region contained the specified item
		/// </summary>
		public static DataRegion GetParentDataRegion(IReportComponent reportComponent)
		{
			if (reportComponent == null)
			{
				Debug.Assert(false, "Invalid report item");
				return null;
			}
			if (reportComponent is DataRegion)
			{
				return reportComponent as DataRegion;
			}
			IReportComponentContainer parent = reportComponent.Parent;
			while (parent != null)
			{
				if (parent is DataRegion)
				{
					return parent as DataRegion;
				}
				parent = parent.Parent;
			}
			Trace.TraceInformation("Failed to find a data region containing item");
			return null;
		}

		/// <summary>
		/// Returns a <see cref="Report"/> from the serviceProvider.
		/// </summary>
		/// <param name="serviceProvider"></param>
		/// <returns>The <see cref="Report"/>, or null if one is not found.</returns>
		public static Report GetReportFromServiceProvider(IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
			{
				Trace.TraceWarning("Unable to get a report. The ServiceProvider is null.");
				return null;
			}

			IDesignerHost host = serviceProvider as IDesignerHost ?? serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;

			if (host == null)
			{
				Trace.TraceWarning("Unabled to get a report. Expected to be able to get a designer host");
				return null;
			}
			PageReport reportDef = host.RootComponent as PageReport;
			if (reportDef == null)
			{
				Trace.TraceInformation("Unabled to get a report. Unable to get locate a report.");
				return null;
			}
			return reportDef.Report;
		}

		/// <summary>
		/// Checks if the specified string identifier consists from only of alphanumeric and underscore characters
		/// </summary>
		static public bool IsValidCLRIdentifier(string identifier)
		{
			if (string.IsNullOrEmpty(identifier))
			{
				return false;
			}
			if (char.IsDigit(identifier, 0) || identifier[0] == '_') { return false; } // see CR20139
			for (int i = 0; i < identifier.Length; ++i)
			{
				if (!char.IsLetterOrDigit(identifier, i) && identifier[i] != '_')
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns a valid field reference value expression based on the supplied field name.
		/// </summary>
		internal static string GenerateFieldValueExpression(string fieldName)
		{//CR 20626: When a field name that did not start with an alpha character ("2004") there were invalid expressions generated.
			if (IsValidCLRIdentifier(fieldName))
				return string.Format("Fields!{0}.Value", fieldName);
			return string.Format("Fields.Item(\"{0}\").Value", fieldName);
		}

		private static StringCollection GetFieldsForCustomReportItemDataSet(CustomReportItem customReportItem, IServiceProvider serviceProvider)
		{
			StringCollection fields = new StringCollection();
			Report report = GetReportFromServiceProvider(serviceProvider);
			if (report == null)
			{
				Trace.TraceWarning("Unabled to get fields for custom report item. Unable to get a report from the service provider");
				return fields;
			}
			Debug.Assert(customReportItem.CustomData != null, "CustomData can not be null.");
			string dataSetName = customReportItem.CustomData.DataSetName;
			foreach (DataSet dataSet in report.DataSets)
			{
				if (dataSet.Name == dataSetName)
				{
					foreach (Field field in dataSet.Fields)
					{
						fields.Add(string.Format("={0}", GenerateFieldValueExpression(field.Name)));
					}
					break;
				}
			}
			return fields;
		}

		/// <summary>
		/// Updates the margin of control by an indentation given in inches.
		/// </summary>
		/// <param name="margin">margin to update</param>
		/// <param name="controlInfo">control info to resolve indentation size</param>
		/// <param name="indent">indent</param>
		public static System.Windows.Forms.Padding UpdateMarginByIndent(System.Windows.Forms.Padding margin, ControlInfo controlInfo, int indent)
		{
			int indentSize = controlInfo.ControlSpacingInfo.Indention;
			margin.Left = indent * indentSize;
			return margin;
		}

		/// <summary>
		/// Updates the margin of control by an indentation given in inches.
		/// </summary>
		/// <param name="margin">margin to update</param>
		/// <param name="serviceProvider">provider to create a label</param>
		/// <param name="indent">indent</param>
		public static System.Windows.Forms.Padding UpdateMarginByIndent(System.Windows.Forms.Padding margin, IServiceProvider serviceProvider, int indent)
		{
			using (LabelEx labelControl = new LabelEx(serviceProvider))
			{
				int indentSize = labelControl.ControlInfo.ControlSpacingInfo.Indention;
				margin.Left = indent * indentSize;
			}
			return margin;
		}


		/// <summary>
		/// Parses the <see cref="Color"/> from a specified string.
		/// </summary>
		/// <param name="colorString">the color string to parse</param>
		/// <param name="defaultColor">the default color to return if nothing to parse</param>
		public static Color ParseColor(string colorString, Color defaultColor)
		{
			Color color = defaultColor;
			if (!string.IsNullOrEmpty(colorString))
			{
				color = ColorTranslator.FromHtml(colorString.Trim());
			}
			return color;
		}


		/// <summary>
		/// Converts a <see cref="Color"/> to a string constant.
		/// </summary>
		/// <param name="color">the color to convert</param>
		public static string ConvertColorToString(Color color)
		{
			//Case 38605: "The default appearance should be set as follows"
			//KnownColor enumeration contains entries with same ARGB values but different names
			//for example KnownColor.White and KnownColor.ButtonHighlight and the 
			//html value of KnownColor.White is "White" html value of KnownColor.ButtonHighlight is ""
			//We cannot distinguish such colors by it's argb values.
			// hash the known name of colors, they are not parsed from ARGB values
			if (_colors == null)
			{
				KnownColor[] colors = (KnownColor[])Enum.GetValues(typeof(KnownColor));
				_colors = new Dictionary<int, Color>(colors.Length);
				foreach (KnownColor knownColor in colors)
				{
					Color namedColor = Color.FromKnownColor(knownColor);
					if (!string.IsNullOrEmpty(ColorTranslator.ToHtml(namedColor)))
					{
						_colors[namedColor.ToArgb()] = namedColor;
					}
				}
			}

			// restore known name from hashed ones
			Color colorArgb = Color.FromArgb(color.A, color.R, color.G, color.B);
			if (_colors.ContainsKey(colorArgb.ToArgb()))
				colorArgb = _colors[colorArgb.ToArgb()];

			// return the color using HTML based syntax
			return ColorTranslator.ToHtml(colorArgb);
		}
		private static Dictionary<int, Color> _colors;

		/// <summary>
		/// Converts a <see cref="Length"/> value to string constant.
		/// </summary>
		public static string ConvertLengthToString(Length length)
		{
			return length.ToString(CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Converts a value type value to string constant.
		/// </summary>
		public static string ConvertToString<T>(T value) where T : struct
		{
			return value.ToString();
		}


		public static void CheckIfExpressionIsBad(ExpressionInfo expression, List<ValidationEntry> entries, ValidationContext validationContext, string propertyName, IReportComponent ownerComponent)
		{
			// RULE: Globals!PageNumber & Globals!TotalPages can't be used outside pageheader or pagefooter
			if (ownerComponent != null && expression != null)
			{
				if (!IsHostInType(ownerComponent, typeof(PageHeaderFooter)))
				{
					if (expression.Expression.Contains("Globals!PageNumber") || expression.Expression.Contains("Globals!TotalPages"))
					{
						ReportItem ownerItem = ownerComponent as ReportItem;
						if (ownerItem != null)
						{
							string message = string.Format(Resources.InvalidUsingPageNumberReference, new object[] { propertyName, ownerItem.GetType().Name, ownerItem.Name });
							entries.Add(new ValidationEntry(Severity.Error, message, new InvalidOperationException(message), ownerComponent));
						}
					}
				}
			}
		}

		/// <summary>
		/// Walks up the site tree to see if the controls is hosted in the specified type.
		/// </summary>
		/// <param name="parent">The current component being checked.</param>
		/// <param name="type">The type to look for.</param>
		/// <returns>True if the component is hosted in the specified type</returns>
		internal static bool IsHostInType(IReportComponent parent, Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			if (parent == null)
				return false;

			if (parent.GetType() == type)
				return true;

			IReportComponent component = parent.Parent;

			return component != null && IsHostInType(component, type);
		}

		/// <summary>
		/// Ensure that value size is less or equal to maxSize. If maxSize is empty, no restriction is applied.
		/// </summary>
		/// <param name="calendarDesigner"> </param>
		/// <param name="value"></param>
		/// <param name="maxSize"></param>
		/// <returns></returns>
		internal static DesignSize RestrictAtMaxSize(CalendarDesigner calendarDesigner, DesignSize value, DesignSize maxSize)
		{
			if (maxSize != DesignSize.Empty && (value.Width > maxSize.Width || value.Height > maxSize.Height))
			{
				value = new DesignSize(
					value.Width > maxSize.Width && maxSize.Width.ToTwips() > 0 ? maxSize.Width : value.Width,
					value.Height > maxSize.Height && maxSize.Height.ToTwips() > 0 ? maxSize.Height : value.Height);
			}

			calendarDesigner.ResizeParents(value);
			return value;
		}

		/// <summary>
		/// Ensure that value size is greater or equal to minSize.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="minSize"></param>
		/// <returns></returns>
		internal static DesignSize RestrictAtMinSize(DesignSize value, DesignSize minSize)
		{
			if (value.Width < minSize.Width || value.Height < minSize.Height)
			{
				value = new DesignSize(
					value.Width < minSize.Width ? minSize.Width : value.Width,
					value.Height < minSize.Height ? minSize.Height : value.Height);
			}
			return value;
		}

		/// <summary>
		/// Defines is need to paint fixed size.
		/// </summary>
		public static bool NeedsFixedSizePainted(CustomReportItem item, DesignSize fixedSize, Rectangle bounds, ConversionService conversionService)
		{
			return IsFixedSizeApplicable(item) && (conversionService.ToDisplayWidth(fixedSize.Width) > bounds.Width || conversionService.ToDisplayHeight(fixedSize.Height) > bounds.Height);
		}

		/// <summary>
		/// Defines is fixed size applicable for provided report item.
		/// </summary>
		/// <param name="reportItem"></param>
		/// <returns>True if fixed size is apllicable for provided report item.</returns>
		public static bool IsFixedSizeApplicable(CustomReportItem reportItem)
		{
			return reportItem != null && reportItem.Parent != null && string.Equals(reportItem.Type, "Calendar") &&
				   GetParentDataRegion(reportItem.Parent) is FixedPage;
		}

		/// <summary>
		/// Defines is the item is in FPL report.
		/// </summary>
		/// <param name="item"></param>
		/// <returns>True if provided item is in FPL report</returns>
		public static bool IsFplReport(IComponent item)
		{
			PageReport pageReport;
			Report report;
			if (item == null || (pageReport = GetReportDefinition(item)) == null ||
				(report = pageReport.Report) == null || report.Body == null)
			{
				return false;
			}

			var items = report.Body.ReportItems;
			return items != null && items.Count == 1 && items[0] is FixedPage;
		}

		/// <summary>
		/// Returns report definition of the provided component
		/// </summary>
		public static PageReport GetReportDefinition(IComponent component)
		{
			IDesignerHost host;
			PageReport pageReport;
			if (component == null || component.Site == null ||
				(host = component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost) == null ||
				(pageReport = host.RootComponent as PageReport) == null)
			{
				Trace.TraceWarning("Invalid or unsited component");
				return null;
			}
			return pageReport;
		}
	}
}
