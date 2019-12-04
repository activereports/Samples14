using System;
using System.Collections.Generic;
using System.Drawing;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Design.DdrDesigner.ReportViewerWinForms.UI.Controls;

namespace GrapeCity.ActiveReports.Calendar.Tools
{
	/// <summary>
	/// Represents utility methods to use in user controls for smart panels.
	/// </summary>
	internal static class Utils
	{

		/// <summary>
		/// Updates the margin of control by an indentation given in inches.
		/// </summary>
		/// <param name="margin">margin to update</param>
		/// <param name="serviceProvider">provider to create a label</param>
		/// <param name="indent">indent</param>
		[Obsolete("Don't use this method instead use another one with ControlInfo")]
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
		/// Parses the <see cref="Length"/> from a specified string.
		/// </summary>
		/// <param name="lengthString">the length string to parse</param>
		/// <param name="defaultLength">the default length to return if nothing to parse</param>
		public static Length ParseLength(string lengthString, Length defaultLength)
		{
			Length length = defaultLength;
			if (!string.IsNullOrEmpty(lengthString))
			{
				Length lengthValue = lengthString;
				if (lengthValue.IsValid)
					length = lengthValue;
			}
			return length;
		}

		/// <summary>
		/// Parses the <see cref="Enum"/> type from specified string. 
		/// </summary>
		/// <typeparam name="T">Enum type to use in parsing</typeparam>
		/// <param name="valueString">the enum value to parse</param>
		/// <param name="defaultValue">the default value to return if nothing to pase</param>
		public static T Parse<T>(string valueString, T defaultValue) where T : struct
		{
			T value = defaultValue;
			if (!string.IsNullOrEmpty(valueString) && Enum.IsDefined(typeof(T), value))
			{
				value = (T)Enum.Parse(typeof(T), valueString, true);
			}
			return value;
		}


		/// <summary>
		/// Converts a value type value to string constant.
		/// </summary>
		public static string ConvertToString<T>(T value) where T : struct
		{
			return value.ToString();
		}


		/// <summary>
		/// Performs the stable sort on the generic list.
		/// </summary>
		/// <typeparam name="T">The type of the elementes contained in the list.</typeparam>
		/// <param name="list">The list to sort.</param>
		/// <param name="comparison">The method to compare the instances of the list type.</param>
		public static void InsertionSort<T>(IList<T> list, Comparison<T> comparison)
		{
			if (list == null)
				throw new ArgumentNullException("list");
			if (comparison == null)
				throw new ArgumentNullException("comparison");

			int count = list.Count;
			for (int j = 1; j < count; j++)
			{
				T key = list[j];
				int i = j - 1;
				for (; i >= 0 && comparison(list[i], key) > 0; i--)
				{
					list[i + 1] = list[i];
				}
				list[i + 1] = key;
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

		const float threshold = .999f;

		static internal bool ApproxGreaterOrEquals(float op1, float op2)
		{
			return op1 - op2 >= -threshold;
		}

	}
}
