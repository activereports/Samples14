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
	internal sealed class DayStyle : IComparable
	{
		public DayStyle(ExpressionInfo dayBackcolor, DesignLineStyle borderStyle, DesignTextStyle fontStyle, ExpressionInfo dayTextAlign, ExpressionInfo dayVerticalAlign)
		{
			_dayBackcolor = dayBackcolor;
			_borderStyle = borderStyle;
			_fontStyle = fontStyle;
			_dayTextAlign = dayTextAlign;
			_dayVerticalAlign = dayVerticalAlign;
		}

		/// <summary>
		/// Gets or sets <see cref="ExpressionInfo"/> represents a backcolor of day.
		/// </summary>
		[TypeConverter(typeof(ColorExpressionInfoConverter))]
		[Editor(typeof(ColorTypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo DayBackcolor
		{
			get
			{
				return _dayBackcolor;
			}
			set
			{
				_dayBackcolor = value;
			}
		}
		private ExpressionInfo _dayBackcolor;

		/// <summary>
		/// Gets or sets the <see cref="DesignLineStyle"/> represented a day border style.
		/// </summary>
		[TypeConverter(typeof(DesignLineStyle.Converter))]
		internal DesignLineStyle BorderStyle
		{
			get
			{
				return _borderStyle;
			}
			set
			{
				_borderStyle = value;
			}
		}
		private DesignLineStyle _borderStyle;

		/// <summary>
		/// Gets or sets the <see cref="DesignTextStyle"/> represented a day font style.
		/// </summary>
		[TypeConverter(typeof(DesignTextStyle.Converter))]
		internal DesignTextStyle FontStyle
		{
			get
			{
				return _fontStyle;
			}
			set
			{
				_fontStyle = value;
			}
		}
		private DesignTextStyle _fontStyle;

		/// <summary>
		/// Gets or sets the <see cref="ExpressionInfo"/> represented a day text align.
		/// </summary>
		[ExpressionBaseType(typeof(TextAlign))]
		[TypeConverter(typeof(EnumExpressionInfoConverter))]
		[Editor(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo DayTextAlign
		{
			get
			{
				return _dayTextAlign;
			}
			set
			{
				_dayTextAlign = value;
			}
		}
		private ExpressionInfo _dayTextAlign;

		/// <summary>
		/// Gets or sets <see cref="ExpressionInfo"/> represents a day vertical align.
		/// </summary>
		[ExpressionBaseType(typeof(VerticalAlign))]
		[TypeConverter(typeof(EnumExpressionInfoConverter))]
		[Editor(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo DayVerticalAlign
		{
			get
			{
				return _dayVerticalAlign;
			}
			set
			{
				_dayVerticalAlign = value;
			}
		}
		private ExpressionInfo _dayVerticalAlign;

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
					string verticalAlignString = String.Empty;
					if (styleList.Length > 11)
						verticalAlignString = styleList[11];
					return new DayStyle(
						(ExpressionInfo)new ColorExpressionInfoConverter().ConvertFrom(context, culture, colorString),
						(DesignLineStyle)new DesignLineStyle.Converter().ConvertFrom(context, culture, bordersStyleString),
						(DesignTextStyle)new DesignTextStyle.Converter().ConvertFrom(context, culture, fontStyleString),
						ExpressionInfo.FromString(textAlignString),
						ExpressionInfo.FromString(verticalAlignString));
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
					"DayBackcolor",
					TypeDescriptor.CreateProperty(
					typeof(DayStyle),
					DayBackcolorPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(typeof(ColorExpressionInfoConverter)),
					new DefaultValueAttribute((object)ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertColorToString(CalendarData.DefaultDayBackcolor)))));
				// border style
				properties[1] = new ShadowProperty(
					"DayBorderStyle",
					TypeDescriptor.CreateProperty(
					typeof(DayStyle),
					DayBorderStylePropertyName,
					typeof(DesignLineStyle),
					new TypeConverterAttribute(typeof(DesignLineStyle.Converter)),
					new DefaultValueAttribute(typeof(DesignLineStyle), string.Format("{0}, {1}, {2}",
					ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertColorToString(CalendarData.DefaultDayBorderColor)),
					ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertLengthToString(CalendarData.DefaultDayBorderWidth)),
					ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultDayBorderStyle))))));
				// font style
				properties[2] = new ShadowProperty(
					"DayFontStyle",
					TypeDescriptor.CreateProperty(
					typeof(DayStyle),
					DayFontStylePropertyName,
					typeof(DesignTextStyle),
					new TypeConverterAttribute(typeof(DesignTextStyle.Converter)),
					new DefaultValueAttribute(typeof(DesignTextStyle), string.Format("{0}, {1}, {2}, {3}, {4}, {5}",
					ExpressionInfo.FromString(CalendarData.DefaultDayFontFamily),
					ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertLengthToString(CalendarData.DefaultDayFontSize)),
					ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultDayFontStyle)),
					ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultDayFontWeight)),
					ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultDayFontDecoration)),
					ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertColorToString(CalendarData.DefaultDayFontColor))))));
				// text align
				properties[3] = new ShadowProperty(
					"DayTextAlign",
					TypeDescriptor.CreateProperty(
					typeof(DayStyle),
					DayTextAlignPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(typeof(EnumExpressionInfoConverter)),
					new DefaultValueAttribute((object)ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultDayTextAlign)))));
				// vertical align
				properties[4] = new ShadowProperty(
					"DayVerticalAlign",
					TypeDescriptor.CreateProperty(
					typeof(DayStyle),
					DayVerticalAlignPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(typeof(EnumExpressionInfoConverter)),
					new DefaultValueAttribute((object)ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultDayVerticalAlign)))));
				PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(properties);
				return pdc;
			}

			public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
			{
				ExpressionInfo backColor = (ExpressionInfo)propertyValues[DayBackcolorPropertyName];
				DesignLineStyle borderStyle = (DesignLineStyle)propertyValues[DayBorderStylePropertyName];
				DesignTextStyle fontStyle = (DesignTextStyle)propertyValues[DayFontStylePropertyName];
				ExpressionInfo textAlign = (ExpressionInfo)propertyValues[DayTextAlignPropertyName];
				ExpressionInfo verticalAlign = (ExpressionInfo)propertyValues[DayVerticalAlignPropertyName];
				return new DayStyle(backColor, borderStyle, fontStyle, textAlign, verticalAlign);
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
			if (!(obj is DayStyle)) { return -1; }
			return CompareTo((DayStyle)obj);
		}

		/// <summary>
		/// Compares this object to another <see cref="DesignLineStyle"/>.
		/// </summary>
		public int CompareTo(DayStyle other)
		{
			int delta = DayBackcolor == null ? -1 : DayBackcolor.CompareTo(other.DayBackcolor);
			if (delta == 0)
			{
				delta = BorderStyle.CompareTo(other.BorderStyle);
			}
			if (delta == 0)
			{
				delta = FontStyle.CompareTo(other.FontStyle);
			}
			if (delta == 0)
			{
				delta = DayTextAlign == null ? -1 : DayTextAlign.CompareTo(other.DayTextAlign);
			}
			if (delta == 0)
			{
				delta = DayVerticalAlign == null ? -1 : DayVerticalAlign.CompareTo(other.DayVerticalAlign);
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

		// day style prop names
		internal const string DayBackcolorPropertyName = "DayBackcolor"; // NOTE: the name is just a sopy of real property name
		internal const string DayBorderStylePropertyName = "BorderStyle"; // NOTE: the name is just a sopy of real property name
		internal const string DayFontStylePropertyName = "FontStyle"; // NOTE: the name is just a sopy of real property name
		internal const string DayTextAlignPropertyName = "DayTextAlign"; // NOTE: the name is just a sopy of real property name
		internal const string DayVerticalAlignPropertyName = "DayVerticalAlign"; // NOTE: the name is just a sopy of real property name

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}

}
