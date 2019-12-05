using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using GrapeCity.ActiveReports.Calendar.Components.Calendar;
using GrapeCity.ActiveReports.Calendar.Design.Properties;
using GrapeCity.ActiveReports.Design.DdrDesigner.Editors.ColorEditor;
using GrapeCity.ActiveReports.Design.DdrDesigner.Tools;
using GrapeCity.ActiveReports.Rdl;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;

using ToolsNamespace = GrapeCity.ActiveReports.Calendar.Design.Tools;


namespace GrapeCity.ActiveReports.Calendar.Design
{
	//[DoNotObfuscateType]
	[TypeConverter(typeof(Converter))]
	internal sealed class MonthTitleStyle : IComparable
	{
		public MonthTitleStyle(ExpressionInfo monthTitleBackcolor, DesignLineStyle monthTitleBorderStyle, DesignTextStyle monthTitleFontStyle, ExpressionInfo monthTitleTextAlign, ExpressionInfo monthTitleFormat)
		{
			_monthTitleBackcolor = monthTitleBackcolor;
			_monthTitleBorderStyle = monthTitleBorderStyle;
			_monthTitleFontStyle = monthTitleFontStyle;
			_monthTitleTextAlign = monthTitleTextAlign;
			_monthTitleFormat = monthTitleFormat;
		}

		/// <summary>
		/// Gets or sets <see cref="ExpressionInfo"/> represents a backcolor of month title.
		/// </summary>
		[TypeConverter(typeof(ColorExpressionInfoConverter))]
		[Editor(typeof(ColorTypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo MonthTitleBackcolor
		{
			get
			{
				return _monthTitleBackcolor;
			}
			set
			{
				_monthTitleBackcolor = value;
			}
		}
		private ExpressionInfo _monthTitleBackcolor;

		/// <summary>
		/// Gets or sets the <see cref="DesignLineStyle"/> represented a month title border style.
		/// </summary>
		[TypeConverter(typeof(DesignLineStyle.Converter))]
		internal DesignLineStyle MonthTitleBorder
		{
			get
			{
				return _monthTitleBorderStyle;
			}
			set
			{
				_monthTitleBorderStyle = value;
			}
		}
		private DesignLineStyle _monthTitleBorderStyle;

		/// <summary>
		/// Gets or sets the <see cref="DesignTextStyle"/> represented a month title font style.
		/// </summary>
		[TypeConverter(typeof(DesignTextStyle.Converter))]
		internal DesignTextStyle MonthTitleFont
		{
			get
			{
				return _monthTitleFontStyle;
			}
			set
			{
				_monthTitleFontStyle = value;
			}
		}
		private DesignTextStyle _monthTitleFontStyle;


		/// <summary>
		/// Gets or sets the <see cref="ExpressionInfo"/> represented a month title text align.
		/// </summary>
		[ExpressionBaseType(typeof(TextAlign))]
		[TypeConverter(typeof(EnumExpressionInfoConverter))]
		[Editor(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo MonthTitleTextAlign
		{
			get
			{
				return _monthTitleTextAlign;
			}
			set
			{
				_monthTitleTextAlign = value;
			}
		}
		private ExpressionInfo _monthTitleTextAlign;

		/// <summary>
		/// Gets or sets <see cref="ExpressionInfo"/> represents a month title format.
		/// </summary>
		[TypeConverter(typeof(StringExpressionInfoConverter))]
		[Editor(typeof(FormatUITypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo MonthTitleFormat
		{
			get
			{
				return _monthTitleFormat;
			}
			set
			{
				_monthTitleFormat = value;
			}
		}
		private ExpressionInfo _monthTitleFormat;

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
					string textAlignString = String.Empty;
					if (styleList.Length > 10)
						textAlignString = styleList[10];
					string titleFormatString = String.Empty;
					if (styleList.Length > 11)
						titleFormatString = styleList[11];
					return new MonthTitleStyle(
						(ExpressionInfo)new ColorExpressionInfoConverter().ConvertFrom(context, culture, colorString),
						(DesignLineStyle)new DesignLineStyle.Converter().ConvertFrom(context, culture, bordersStyleString),
						(DesignTextStyle)new DesignTextStyle.Converter().ConvertFrom(context, culture, fontStyleString),
						ExpressionInfo.FromString(textAlignString),
						ExpressionInfo.FromString(titleFormatString));
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
				PropertyDescriptor[] properties = new PropertyDescriptor[5];
				// backcolor
				properties[0] = new ShadowProperty(
					"MonthTitleBackcolor",
					TypeDescriptor.CreateProperty(
					typeof(MonthTitleStyle),
					MonthTitleBackcolorPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(typeof(ColorExpressionInfoConverter)),
					new DefaultValueAttribute((object)ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertColorToString(CalendarData.DefaultMonthTitleBackcolor)))));
				// border style
				properties[1] = new ShadowProperty(
					"MonthTitleBorderStyle",
					TypeDescriptor.CreateProperty(
					typeof(MonthTitleStyle),
					MonthTitleBorderStylePropertyName,
					typeof(DesignLineStyle),
					new TypeConverterAttribute(typeof(DesignLineStyle.Converter)),
					new DefaultValueAttribute(typeof(DesignLineStyle), string.Format("{0}, {1}, {2}",
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertColorToString(CalendarData.DefaultMonthTitleBorderColor)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertLengthToString(CalendarData.DefaultMonthTitleBorderWidth)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultMonthTitleBorderStyle))))));
				// font style
				properties[2] = new ShadowProperty(
					"MonthTitleFontStyle",
					TypeDescriptor.CreateProperty(
					typeof(MonthTitleStyle),
					MonthTitleFontStylePropertyName,
					typeof(DesignTextStyle),
					new TypeConverterAttribute(typeof(DesignTextStyle.Converter)),
					new DefaultValueAttribute(typeof(DesignTextStyle), string.Format("{0}, {1}, {2}, {3}, {4}, {5}",
						ExpressionInfo.FromString(CalendarData.DefaultMonthTitleFontFamily),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertLengthToString(CalendarData.DefaultMonthTitleFontSize)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultMonthTitleFontStyle)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultMonthTitleFontWeight)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultMonthTitleFontDecoration)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertColorToString(CalendarData.DefaultMonthTitleFontColor))))));
				// text align
				properties[3] = new ShadowProperty(
					"MonthTitleTextAlign",
					TypeDescriptor.CreateProperty(
					typeof(MonthTitleStyle),
					MonthTitleTextAlignPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(typeof(EnumExpressionInfoConverter)),
					new DefaultValueAttribute((object)ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultMonthTitleTextAlign)))));
				// format
				properties[4] = new ShadowProperty(
					"MonthTitleFormat",
					TypeDescriptor.CreateProperty(
					typeof(MonthTitleStyle),
					MonthTitleFormatPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(typeof(StringExpressionInfoConverter)),
					new DefaultValueAttribute((object)ExpressionInfo.FromString(CalendarData.DefaultMonthTitleFormat))));
				PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(properties);
				return pdc.Sort(PropertySortOrder);
			}

			public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
			{
				ExpressionInfo backColor = (ExpressionInfo)propertyValues[MonthTitleBackcolorPropertyName];
				DesignLineStyle borderStyle = (DesignLineStyle)propertyValues[MonthTitleBorderStylePropertyName];
				DesignTextStyle fontStyle = (DesignTextStyle)propertyValues[MonthTitleFontStylePropertyName];
				ExpressionInfo textAlign = (ExpressionInfo)propertyValues[MonthTitleTextAlignPropertyName];
				ExpressionInfo format = (ExpressionInfo)propertyValues[MonthTitleFormatPropertyName];
				return new MonthTitleStyle(backColor, borderStyle, fontStyle, textAlign, format);
			}
		}

		#region Comparison stuff

		///<summary>
		///Compares the current instance with another object of the same type.
		///</summary>
		///
		///<returns>
		///A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than obj. Zero This instance is equal to obj. Greater than zero This instance is greater than obj. 
		///</returns>
		///
		///<param name="obj">An object to compare with this instance. </param>
		///<exception cref="T:System.ArgumentException">obj is not the same type as this instance. </exception><filterpriority>2</filterpriority>
		public int CompareTo(object obj)
		{
			if (!(obj is MonthTitleStyle)) { return -1; }
			return CompareTo((MonthTitleStyle)obj);
		}

		/// <summary>
		/// Compares this object to another <see cref="DesignLineStyle"/>.
		/// </summary>
		public int CompareTo(MonthTitleStyle other)
		{
			int delta = MonthTitleBackcolor == null ? -1 : MonthTitleBackcolor.CompareTo(other.MonthTitleBackcolor);
			if (delta == 0)
			{
				delta = MonthTitleBorder.CompareTo(other.MonthTitleBorder);
			}
			if (delta == 0)
			{
				delta = MonthTitleFont.CompareTo(other.MonthTitleFont);
			}
			if (delta == 0)
			{
				delta = MonthTitleTextAlign.CompareTo(other.MonthTitleTextAlign);
			}
			if (delta == 0)
			{
				delta = MonthTitleFormat.CompareTo(other.MonthTitleFormat);
			}
			return delta;
		}

		///<summary>
		///Indicates whether this instance and a specified object are equal.
		///</summary>
		///
		///<returns>
		///true if obj and this instance are the same type and represent the same value; otherwise, false.
		///</returns>
		///
		///<param name="obj">Another object to compare to. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			return CompareTo(obj) == 0;
		}

		#endregion

		// month title style prop names
		internal const string MonthTitleBackcolorPropertyName = "MonthTitleBackcolor"; // NOTE: the name is just a sopy of real property name
		internal const string MonthTitleBorderStylePropertyName = "MonthTitleBorder"; // NOTE: the name is just a sopy of real property name
		internal const string MonthTitleFontStylePropertyName = "MonthTitleFont"; // NOTE: the name is just a sopy of real property name
		internal const string MonthTitleTextAlignPropertyName = "MonthTitleTextAlign"; // NOTE: the name is just a sopy of real property name
		internal const string MonthTitleFormatPropertyName = "MonthTitleFormat"; // NOTE: the name is just a sopy of real property name

		private static readonly string[] PropertySortOrder =
			new[] { MonthTitleBackcolorPropertyName, MonthTitleBorderStylePropertyName, MonthTitleFontStylePropertyName, MonthTitleTextAlignPropertyName, MonthTitleFormatPropertyName };

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
