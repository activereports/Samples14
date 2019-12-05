using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using GrapeCity.ActiveReports.Calendar.Components.Calendar;
using GrapeCity.ActiveReports.Calendar.Design.Properties;
using GrapeCity.ActiveReports.Design.DdrDesigner.Editors.ColorEditor;
using GrapeCity.ActiveReports.Design.DdrDesigner.Tools;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;

using ToolsNamespace = GrapeCity.ActiveReports.Calendar.Design.Tools;

namespace GrapeCity.ActiveReports.Calendar.Design
{
	//[DoNotObfuscateType]
	[TypeConverter(typeof(Converter))]
	internal sealed class DayHeadersStyle : IComparable
	{
		public DayHeadersStyle(ExpressionInfo dayHeadersBackColor, DesignLineStyle dayHeadersBorderStyle, DesignTextStyle dayHeadersFontStyle)
		{
			_dayHeadersBorderStyle = dayHeadersBorderStyle;
			_dayHeadersBackColor = dayHeadersBackColor;
			_dayHeadersFontStyle = dayHeadersFontStyle;
		}

		/// <summary>
		/// Gets the <see cref="DesignLineStyle"/> represented a header border color.
		/// </summary>
		[TypeConverter(typeof(DesignLineStyle.Converter))]
		internal DesignLineStyle DayHeadersBorder
		{
			get
			{
				return _dayHeadersBorderStyle;
			}
			set
			{
				_dayHeadersBorderStyle = value;
			}
		}
		private DesignLineStyle _dayHeadersBorderStyle;

		/// <summary>
		/// Gets the <see cref="DesignLineStyle"/> represented a day header back color.
		/// </summary>
		[TypeConverter(typeof(ColorExpressionInfoConverter))]
		[Editor(typeof(ColorTypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo DayHeadersBackColor
		{
			get
			{
				return _dayHeadersBackColor;
			}
			set
			{
				_dayHeadersBackColor = value;
			}
		}
		private ExpressionInfo _dayHeadersBackColor;

		/// <summary>
		/// Gets the <see cref="DesignTextStyle"/> represented a day header font style.
		/// </summary>
		[TypeConverter(typeof(DesignTextStyle.Converter))]
		internal DesignTextStyle DayHeadersFont
		{
			get
			{
				return _dayHeadersFontStyle;
			}
			set
			{
				_dayHeadersFontStyle = value;
			}
		}
		private DesignTextStyle _dayHeadersFontStyle;

		internal sealed class Converter : RootExpandableConverter
		{
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return false;
			}

			public override bool CanConvertTo(ITypeDescriptorContext context, Type sourceType)
			{
				return false;
			}

			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value is string)
				{
					string originalString = ((string)value).Trim();
					string[] styleList = GetValuesFromList(originalString, culture);

					string colorString = String.Empty;
					if (styleList.Length > 0)
						colorString = styleList[0];
					string separator = culture.TextInfo.ListSeparator + " ";
					string bordersStyleString = String.Join(separator, styleList, 1, 3);
					string fontStyleString = String.Join(separator, styleList, 4, 6);
					return new DayHeadersStyle(
						(ExpressionInfo)new ColorExpressionInfoConverter().ConvertFrom(context, culture, colorString),
						(DesignLineStyle)new DesignLineStyle.Converter().ConvertFrom(context, culture, bordersStyleString),
						(DesignTextStyle)new DesignTextStyle.Converter().ConvertFrom(context, culture, fontStyleString)
						);
				}
				return base.ConvertFrom(context, culture, value);
			}

			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					return string.Empty;
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				PropertyDescriptor[] properties = new PropertyDescriptor[3];
				properties[0] = new ShadowProperty(
					"DayHeadersBackColor",
					TypeDescriptor.CreateProperty(
					typeof(DayHeadersStyle),
					DayHeadersBackColorName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(typeof(ColorExpressionInfoConverter)),
					new DefaultValueAttribute((object)ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertColorToString(CalendarData.DefaultDayHeadersBackcolor)))));

				properties[1] = new ShadowProperty(
					"DayHeadersBorderStyle",
					TypeDescriptor.CreateProperty(
					typeof(DayHeadersStyle),
					DayHeadersBorderName,
					typeof(DesignLineStyle),
					new TypeConverterAttribute(typeof(DesignLineStyle.Converter)),
					new DefaultValueAttribute(typeof(DesignLineStyle), string.Format("{0}, {1}, {2}",
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertColorToString(CalendarData.DefaultDayHeadersBorderColor)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertLengthToString(CalendarData.DefaultDayHeadersBorderWidth)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultDayHeadersBorderStyle))))));

				properties[2] = new ShadowProperty(
					"DayHeadersFontStyle",
					TypeDescriptor.CreateProperty(
					typeof(DayHeadersStyle),
					DayHeadersFontStyleName,
					typeof(DesignTextStyle),
					new TypeConverterAttribute(typeof(DesignTextStyle.Converter)),
					new DefaultValueAttribute(typeof(DesignTextStyle), string.Format("{0}, {1}, {2}, {3}, {4}, {5}",
						ExpressionInfo.FromString(CalendarData.DefaultDayHeadersFontFamily),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertLengthToString(CalendarData.DefaultDayHeadersFontSize)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultDayHeadersFontStyle)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultDayHeadersFontWeight)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultDayHeadersFontDecoration)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertColorToString(CalendarData.DefaultDayHeadersFontColor))))));
				PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(properties);
				return pdc.Sort(PropertySortOrder);
			}

			public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
			{
				ExpressionInfo color = (ExpressionInfo)propertyValues[DayHeadersBackColorName];
				DesignLineStyle borders = (DesignLineStyle)propertyValues[DayHeadersBorderName];
				DesignTextStyle font = (DesignTextStyle)propertyValues[DayHeadersFontStyleName];
				return new DayHeadersStyle(color, borders, font);
			}
		}

		#region Comparison stuff

		///<summary>
		///Compares the current instance with another object of the same type.
		///</summary>
		///<returns>
		///A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than obj. Zero This instance is equal to obj. Greater than zero This instance is greater than obj. 
		///</returns>
		///<param name="obj">An object to compare with this instance. </param>
		///<exception cref="T:System.ArgumentException">obj is not the same type as this instance. </exception><filterpriority>2</filterpriority>
		public int CompareTo(object obj)
		{
			if (!(obj is DayHeadersStyle)) { return -1; }
			return CompareTo((DayHeadersStyle)obj);
		}

		/// <summary>
		/// Compares this object to another <see cref="DesignLineStyle"/>.
		/// </summary>
		public int CompareTo(DayHeadersStyle other)
		{
			int delta = DayHeadersBackColor == null ? -1 : DayHeadersBackColor.CompareTo(other.DayHeadersBackColor);
			if (delta == 0)
			{
				delta = DayHeadersBorder.CompareTo(other.DayHeadersBorder);
			}
			if (delta == 0)
			{
				delta = DayHeadersFont.CompareTo(other.DayHeadersFont);
			}
			return delta;
		}

		// ReSharper disable CSharpWarnings::CS0659
		///<summary>
		///Indicates whether this instance and a specified object are equal.
		///</summary>
		///<returns>
		///true if obj and this instance are the same type and represent the same value; otherwise, false.
		///</returns>
		///<param name="obj">Another object to compare to. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			return CompareTo(obj) == 0;
		}
		// ReSharper restore CSharpWarnings::CS0659

		#endregion

		// day header style property names
		internal const string DayHeadersBorderName = "DayHeadersBorder"; // NOTE: the name is just a sopy of real property name
		internal const string DayHeadersBackColorName = "DayHeadersBackColor"; // NOTE: the name is just a sopy of real property name
		internal const string DayHeadersFontStyleName = "DayHeadersFont"; // NOTE: the name is just a sopy of real property name

		private static readonly string[] PropertySortOrder = new[] { DayHeadersBackColorName, DayHeadersBorderName, DayHeadersFontStyleName };

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
