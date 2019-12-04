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
	internal sealed class WeekendStyle : IComparable
	{
		public WeekendStyle(ExpressionInfo weekendBackcolor, DesignLineStyle weekendBorder, DesignTextStyle weekendFont, ExpressionInfo weekendTextAlign, ExpressionInfo weekendVerticalAlign)
		{
			_weekendBackcolor = weekendBackcolor;
			_weekendBorder = weekendBorder;
			_weekendFont = weekendFont;
			_weekendTextAlign = weekendTextAlign;
			_weekendVerticalAlign = weekendVerticalAlign;
		}

		/// <summary>
		/// Gets or sets <see cref="ExpressionInfo"/> represents a backcolor of weekend day.
		/// </summary>
		[TypeConverter(typeof(ColorExpressionInfoConverter))]
		[Editor(typeof(ColorTypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo WeekendBackcolor
		{
			get
			{
				return _weekendBackcolor;
			}
			set
			{
				_weekendBackcolor = value;
			}
		}
		private ExpressionInfo _weekendBackcolor;

		/// <summary>
		/// Gets or sets the <see cref="DesignLineStyle"/> represented a weekend border style.
		/// </summary>
		[TypeConverter(typeof(DesignLineStyle.Converter))]
		internal DesignLineStyle WeekendBorder
		{
			get
			{
				return _weekendBorder;
			}
			set
			{
				_weekendBorder = value;
			}
		}
		private DesignLineStyle _weekendBorder;

		/// <summary>
		/// Gets or sets the <see cref="DesignTextStyle"/> represented a weekend day font style.
		/// </summary>
		[TypeConverter(typeof(DesignTextStyle.Converter))]
		internal DesignTextStyle WeekendFont
		{
			get
			{
				return _weekendFont;
			}
			set
			{
				_weekendFont = value;
			}
		}
		private DesignTextStyle _weekendFont;

		/// <summary>
		/// Gets or sets the <see cref="ExpressionInfo"/> represented a weekend text align.
		/// </summary>
		[ExpressionBaseType(typeof(TextAlign))]
		[TypeConverter(typeof(EnumExpressionInfoConverter))]
		[Editor(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo WeekendTextAlign
		{
			get
			{
				return _weekendTextAlign;
			}
			set
			{
				_weekendTextAlign = value;
			}
		}
		private ExpressionInfo _weekendTextAlign;

		/// <summary>
		/// Gets or sets <see cref="ExpressionInfo"/> represents a weekend vertical align.
		/// </summary>
		[ExpressionBaseType(typeof(VerticalAlign))]
		[TypeConverter(typeof(EnumExpressionInfoConverter))]
		[Editor(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo WeekendVerticalAlign
		{
			get
			{
				return _weekendVerticalAlign;
			}
			set
			{
				_weekendVerticalAlign = value;
			}
		}
		private ExpressionInfo _weekendVerticalAlign;

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
					return new WeekendStyle(
						(ExpressionInfo)new ColorExpressionInfoConverter().ConvertFrom(context, culture, colorString),
						(DesignLineStyle)new NullableColorExpressionInfoConverter().ConvertFrom(context, culture, bordersStyleString),
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
					"WeekendBackcolor",
					TypeDescriptor.CreateProperty(
					typeof(WeekendStyle),
					WeekendBackcolorPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(typeof(ColorExpressionInfoConverter)),
					new DefaultValueAttribute((object)ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertColorToString(CalendarData.DefaultWeekendBackcolor)))));
				// border style
				properties[1] = new ShadowProperty(
					"WeekendBorderStyle",
					TypeDescriptor.CreateProperty(
					typeof(WeekendStyle),
					WeekendBorderStylePropertyName,
					typeof(DesignLineStyle),
					new TypeConverterAttribute(typeof(DesignLineStyle.Converter)),
					new DefaultValueAttribute(typeof(DesignLineStyle), string.Format("{0}, {1}, {2}",
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertColorToString(CalendarData.DefaultWeekendBorderColor)),
						EvaluatorService.EmptyExpression,
						EvaluatorService.EmptyExpression))));
				// font style
				properties[2] = new ShadowProperty(
					"WeekendFontStyle",
					TypeDescriptor.CreateProperty(
					typeof(WeekendStyle),
					WeekendFontStylePropertyName,
					typeof(DesignTextStyle),
					new TypeConverterAttribute(typeof(DesignTextStyle.Converter)),
					new DefaultValueAttribute(typeof(DesignTextStyle), string.Format("{0}, {1}, {2}, {3}, {4}, {5}",
						EvaluatorService.EmptyExpression,
						EvaluatorService.EmptyExpression,
						EvaluatorService.EmptyExpression,
						EvaluatorService.EmptyExpression,
						EvaluatorService.EmptyExpression,
						EvaluatorService.EmptyExpression))));
				// text align
				properties[3] = new ShadowProperty(
					"WeekendTextAlign",
					TypeDescriptor.CreateProperty(
					typeof(WeekendStyle),
					WeekendTextAlignPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(typeof(EnumExpressionInfoConverter)),
					new DefaultValueAttribute((object)EvaluatorService.EmptyExpression)));
				// vertical align
				properties[4] = new ShadowProperty(
					"WeekendVerticalAlign",
					TypeDescriptor.CreateProperty(
					typeof(WeekendStyle),
					WeekendVerticalAlignPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(typeof(EnumExpressionInfoConverter)),
					new DefaultValueAttribute((object)EvaluatorService.EmptyExpression)));
				PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(properties);
				return pdc.Sort(PropertySortOrder);
			}

			public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
			{
				ExpressionInfo backColor = (ExpressionInfo)propertyValues[WeekendBackcolorPropertyName];
				DesignLineStyle borderStyle = (DesignLineStyle)propertyValues[WeekendBorderStylePropertyName];
				DesignTextStyle fontStyle = (DesignTextStyle)propertyValues[WeekendFontStylePropertyName];
				ExpressionInfo textAlign = (ExpressionInfo)propertyValues[WeekendTextAlignPropertyName];
				ExpressionInfo verticalAlign = (ExpressionInfo)propertyValues[WeekendVerticalAlignPropertyName];
				return new WeekendStyle(backColor, borderStyle, fontStyle, textAlign, verticalAlign);
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
			if (!(obj is WeekendStyle)) { return -1; }
			return CompareTo((WeekendStyle)obj);
		}

		/// <summary>
		/// Compares this object to another <see cref="DesignLineStyle"/>.
		/// </summary>
		public int CompareTo(WeekendStyle other)
		{
			int delta = WeekendBackcolor == null ? -1 : WeekendBackcolor.CompareTo(other.WeekendBackcolor);
			if (delta == 0)
			{
				delta = WeekendBorder.CompareTo(other.WeekendBorder);
			}
			if (delta == 0)
			{
				delta = WeekendFont.CompareTo(other.WeekendFont);
			}
			if (delta == 0)
			{
				delta = WeekendTextAlign == null ? -1 : WeekendTextAlign.CompareTo(other.WeekendTextAlign);
			}
			if (delta == 0)
			{
				delta = WeekendVerticalAlign == null ? -1 : WeekendVerticalAlign.CompareTo(other.WeekendVerticalAlign);
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

		// weekend style prop names
		internal const string WeekendBackcolorPropertyName = "WeekendBackcolor"; // NOTE: the name is just a sopy of real property name
		internal const string WeekendBorderStylePropertyName = "WeekendBorder"; // NOTE: the name is just a sopy of real property name
		internal const string WeekendFontStylePropertyName = "WeekendFont"; // NOTE: the name is just a sopy of real property name
		internal const string WeekendTextAlignPropertyName = "WeekendTextAlign"; // NOTE: the name is just a sopy of real property name
		internal const string WeekendVerticalAlignPropertyName = "WeekendVerticalAlign"; // NOTE: the name is just a sopy of real property name

		private static readonly string[] PropertySortOrder =
			new[] { WeekendBackcolorPropertyName, WeekendBorderStylePropertyName, WeekendFontStylePropertyName, WeekendTextAlignPropertyName, WeekendVerticalAlignPropertyName };

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
