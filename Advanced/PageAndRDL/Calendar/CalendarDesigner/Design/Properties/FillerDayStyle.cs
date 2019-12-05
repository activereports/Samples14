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
	internal sealed class FillerDayStyle : IComparable
	{
		public FillerDayStyle(ExpressionInfo fillerDayBackcolor, DesignLineStyle fillerDayBorderStyle, DesignTextStyle fillerDayFontStyle, ExpressionInfo fillerDayTextAlign, ExpressionInfo fillerDayVerticalAlign)
		{
			_fillerDayBackcolor = fillerDayBackcolor;
			_fillerDayBorderStyle = fillerDayBorderStyle;
			_fillerDayFontStyle = fillerDayFontStyle;
			_fillerDayTextAlign = fillerDayTextAlign;
			_fillerDayVerticalAlign = fillerDayVerticalAlign;
		}

		/// <summary>
		/// Gets or sets <see cref="ExpressionInfo"/> represents a backcolor of filler day.
		/// </summary>
		[TypeConverter(typeof(ColorExpressionInfoConverter))]
		[Editor(typeof(ColorTypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo FillerDayBackcolor
		{
			get
			{
				return _fillerDayBackcolor;
			}
			set
			{
				_fillerDayBackcolor = value;
			}
		}
		private ExpressionInfo _fillerDayBackcolor;

		/// <summary>
		/// Gets or sets the <see cref="DesignLineStyle"/> represented a filler day border style.
		/// </summary>
		[TypeConverter(typeof(DesignLineStyle.Converter))]
		internal DesignLineStyle FillerDayBorder
		{
			get
			{
				return _fillerDayBorderStyle;
			}
			set
			{
				_fillerDayBorderStyle = value;
			}
		}
		private DesignLineStyle _fillerDayBorderStyle;

		/// <summary>
		/// Gets or sets the <see cref="DesignTextStyle"/> represented a filler day font style.
		/// </summary>
		[TypeConverter(typeof(DesignTextStyle.Converter))]
		internal DesignTextStyle FillerDayFont
		{
			get
			{
				return _fillerDayFontStyle;
			}
			set
			{
				_fillerDayFontStyle = value;
			}
		}
		private DesignTextStyle _fillerDayFontStyle;


		/// <summary>
		/// Gets or sets the <see cref="ExpressionInfo"/> represented a filler day text align.
		/// </summary>
		[ExpressionBaseType(typeof(TextAlign))]
		[TypeConverter(typeof(EnumExpressionInfoConverter))]
		[Editor(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo FillerDayTextAlign
		{
			get
			{
				return _fillerDayTextAlign;
			}
			set
			{
				_fillerDayTextAlign = value;
			}
		}
		private ExpressionInfo _fillerDayTextAlign;

		/// <summary>
		/// Gets or sets <see cref="ExpressionInfo"/> represents a filler day vertical align.
		/// </summary>
		[ExpressionBaseType(typeof(VerticalAlign))]
		[TypeConverter(typeof(EnumExpressionInfoConverter))]
		[Editor(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo FillerDayVerticalAlign
		{
			get
			{
				return _fillerDayVerticalAlign;
			}
			set
			{
				_fillerDayVerticalAlign = value;
			}
		}
		private ExpressionInfo _fillerDayVerticalAlign;

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
					return new FillerDayStyle(
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
					"FillerDayBackcolor",
					TypeDescriptor.CreateProperty(
					typeof(FillerDayStyle),
					FillerDayBackcolorPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(typeof(ColorExpressionInfoConverter)),
					new DefaultValueAttribute((object)ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertColorToString(CalendarData.DefaultFillerDayBackcolor)))));
				// border style
				properties[1] = new ShadowProperty(
					"FillerDayBorderStyle",
					TypeDescriptor.CreateProperty(
					typeof(FillerDayStyle),
					FillerDayBorderStylePropertyName,
					typeof(DesignLineStyle),
					new TypeConverterAttribute(typeof(DesignLineStyle.Converter)),
					new DefaultValueAttribute(typeof(DesignLineStyle), string.Format("{0}, {1}, {2}",
						EvaluatorService.EmptyExpression,
						EvaluatorService.EmptyExpression,
						EvaluatorService.EmptyExpression))));
				// font style
				properties[2] = new ShadowProperty(
					"FillerDayFontStyle",
					TypeDescriptor.CreateProperty(
					typeof(FillerDayStyle),
					FillerDayFontStylePropertyName,
					typeof(DesignTextStyle),
					new TypeConverterAttribute(typeof(DesignTextStyle.Converter)),
					new DefaultValueAttribute(typeof(DesignTextStyle), string.Format("{0}, {1}, {2}, {3}, {4}, {5}",
						ExpressionInfo.FromString(CalendarData.DefaultFillerDayFontFamily),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertLengthToString(CalendarData.DefaultFillerDayFontSize)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultFillerDayFontStyle)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultFillerDayFontWeight)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultFillerDayFontDecoration)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertColorToString(CalendarData.DefaultFillerDayFontColor))))));
				// text align
				properties[3] = new ShadowProperty(
					"FillerDayTextAlign",
					TypeDescriptor.CreateProperty(
					typeof(FillerDayStyle),
					FillerDayTextAlignPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(typeof(EnumExpressionInfoConverter)),
					new DefaultValueAttribute((object)ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultFillerDayTextAlign)))));
				// vertical align
				properties[4] = new ShadowProperty(
					"FillerDayVerticalAlign",
					TypeDescriptor.CreateProperty(
					typeof(FillerDayStyle),
					FillerDayVerticalAlignPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(typeof(EnumExpressionInfoConverter)),
					new DefaultValueAttribute((object)ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultFillerDayVerticalAlign)))));
				PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(properties);
				return pdc;
			}

			public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
			{
				ExpressionInfo backColor = (ExpressionInfo)propertyValues[FillerDayBackcolorPropertyName];
				DesignLineStyle borderStyle = (DesignLineStyle)propertyValues[FillerDayBorderStylePropertyName];
				DesignTextStyle fontStyle = (DesignTextStyle)propertyValues[FillerDayFontStylePropertyName];
				ExpressionInfo textAlign = (ExpressionInfo)propertyValues[FillerDayTextAlignPropertyName];
				ExpressionInfo verticalAlign = (ExpressionInfo)propertyValues[FillerDayVerticalAlignPropertyName];
				return new FillerDayStyle(backColor, borderStyle, fontStyle, textAlign, verticalAlign);
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
			if (!(obj is FillerDayStyle)) { return -1; }
			return CompareTo((FillerDayStyle)obj);
		}

		/// <summary>
		/// Compares this object to another <see cref="DesignLineStyle"/>.
		/// </summary>
		public int CompareTo(FillerDayStyle other)
		{
			int delta = FillerDayBackcolor == null ? -1 : FillerDayBackcolor.CompareTo(other.FillerDayBackcolor);
			if (delta == 0)
			{
				delta = FillerDayBorder.CompareTo(other.FillerDayBorder);
			}
			if (delta == 0)
			{
				delta = FillerDayFont.CompareTo(other.FillerDayFont);
			}
			if (delta == 0)
			{
				delta = FillerDayTextAlign == null ? -1 : FillerDayTextAlign.CompareTo(other.FillerDayTextAlign);
			}
			if (delta == 0)
			{
				delta = FillerDayVerticalAlign == null ? -1 : FillerDayVerticalAlign.CompareTo(other.FillerDayVerticalAlign);
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

		// filler day style prop names
		internal const string FillerDayBackcolorPropertyName = "FillerDayBackcolor"; // NOTE: the name is just a sopy of real property name
		internal const string FillerDayBorderStylePropertyName = "FillerDayBorder"; // NOTE: the name is just a sopy of real property name
		internal const string FillerDayFontStylePropertyName = "FillerDayFont"; // NOTE: the name is just a sopy of real property name
		internal const string FillerDayTextAlignPropertyName = "FillerDayTextAlign"; // NOTE: the name is just a sopy of real property name
		internal const string FillerDayVerticalAlignPropertyName = "FillerDayVerticalAlign"; // NOTE: the name is just a sopy of real property name

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
