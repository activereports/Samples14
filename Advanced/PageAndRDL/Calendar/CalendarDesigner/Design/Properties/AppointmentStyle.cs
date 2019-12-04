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
	internal sealed class AppointmentStyle : IComparable
	{
		public AppointmentStyle(ExpressionInfo backgroundColor, ExpressionInfo borderColor, DesignTextStyle fontStyle, ExpressionInfo textAlign, ExpressionInfo format, DesignImage image)
		{
			_backgroundColor = backgroundColor;
			_borderColor = borderColor;
			_fontStyle = fontStyle;
			_textAlign = textAlign;
			_format = format;
			_image = image;
		}

		/// <summary>
		/// Gets or sets <see cref="ExpressionInfo"/> represents a backcolor of appointment.
		/// </summary>
		[TypeConverter(typeof(ColorExpressionInfoConverter))]
		[Editor(typeof(ColorTypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo AppointmentBackcolor
		{
			get
			{
				return _backgroundColor;
			}
			set
			{
				_backgroundColor = value;
			}
		}
		private ExpressionInfo _backgroundColor;

		/// <summary>
		/// Gets or sets the <see cref="ExpressionInfo"/> represented a appointment border color.
		/// </summary>
		[TypeConverter(typeof(ColorExpressionInfoConverter))]
		[Editor(typeof(ColorTypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo AppointmentBorderColor
		{
			get
			{
				return _borderColor;
			}
			set
			{
				_borderColor = value;
			}
		}
		private ExpressionInfo _borderColor;

		/// <summary>
		/// Gets or sets the <see cref="DesignTextStyle"/> represented a appointment font style.
		/// </summary>
		[TypeConverter(typeof(DesignTextStyle.Converter))]
		internal DesignTextStyle AppointmentFont
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
		/// Gets or sets the <see cref="ExpressionInfo"/> represented an appointment text align.
		/// </summary>
		[ExpressionBaseType(typeof(TextAlign))]
		[TypeConverter(typeof(EnumExpressionInfoConverter))]
		[Editor(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo AppointmentTextAlign
		{
			get
			{
				return _textAlign;
			}
			set
			{
				_textAlign = value;
			}
		}
		private ExpressionInfo _textAlign;

		/// <summary>
		/// Gets or sets the <see cref="ExpressionInfo"/> represented format expression of appointment.
		/// </summary>
		[TypeConverter(typeof(StringExpressionInfoConverter))]
		[Editor(typeof(FormatUITypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo AppointmentFormat
		{
			get
			{
				return _format;
			}
			set
			{
				_format = value;
			}
		}
		private ExpressionInfo _format;

		/// <summary>
		/// Gets or sets the <see cref="DesignImage"/> represented image of appointment.
		/// </summary>
		[TypeConverter(typeof(DesignImage.Converter))]
		internal DesignImage AppointmentImage
		{
			get
			{
				return _image;
			}
			set
			{
				_image = value;
			}
		}
		private DesignImage _image;

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

					string backColorString = String.Empty;
					if (styleList.Length > 0)
						backColorString = styleList[0];
					string borderColorString = String.Empty;
					if (styleList.Length > 1)
						borderColorString = styleList[1];
					string separator = culture.TextInfo.ListSeparator + " ";
					string fontStyleString = String.Join(separator, styleList, 2, 6);
					string textAlignString = String.Empty;
					if (styleList.Length > 8)
						textAlignString = styleList[8];
					string formatString = String.Empty;
					if (styleList.Length > 9)
						formatString = styleList[9];
					string imageString = string.Empty;
					if (styleList.Length > 10)
						imageString = styleList[10];
					return new AppointmentStyle(
						(ExpressionInfo)new ColorExpressionInfoConverter().ConvertFrom(context, culture, backColorString),
						(ExpressionInfo)new ColorExpressionInfoConverter().ConvertFrom(context, culture, borderColorString),
						(DesignTextStyle)new DesignTextStyle.Converter().ConvertFrom(context, culture, fontStyleString),
						ExpressionInfo.FromString(textAlignString),
						ExpressionInfo.FromString(formatString),
						(DesignImage)new DesignImage.Converter().ConvertFrom(context, culture, imageString));
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
				PropertyDescriptor[] properties = new PropertyDescriptor[6];
				// back color
				properties[0] = new ShadowProperty(
					"EventBackcolor",
					TypeDescriptor.CreateProperty(
					typeof(AppointmentStyle),
					EventBackcolorPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(typeof(ColorExpressionInfoConverter)),
					new DefaultValueAttribute((object)ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertColorToString(CalendarData.DefaultAppointmentBackcolor)))));
				// border color
				properties[1] = new ShadowProperty(
					"EventBorderColor",
					TypeDescriptor.CreateProperty(
					typeof(AppointmentStyle),
					EventBorderColorPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(typeof(ColorExpressionInfoConverter)),
					new DefaultValueAttribute((object)ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertColorToString(CalendarData.DefaultAppointmentBorderColor)))));
				// font style
				properties[2] = new ShadowProperty(
					"EventFont",
					TypeDescriptor.CreateProperty(
					typeof(AppointmentStyle),
					EventFontPropertyName,
					typeof(DesignTextStyle),
					new TypeConverterAttribute(typeof(DesignTextStyle.Converter)),
					new DefaultValueAttribute(typeof(DesignTextStyle), string.Format("{0}, {1}, {2}, {3}, {4}, {5}",
						ExpressionInfo.FromString(CalendarData.DefaultAppointmentFontFamily),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertLengthToString(CalendarData.DefaultAppointmentFontSize)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultAppointmentFontStyle)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultAppointmentFontWeight)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultAppointmentFontDecoration)),
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertColorToString(CalendarData.DefaultAppointmentFontColor))))));
				// text align
				properties[3] = new ShadowProperty(
					"EventTextAlign",
					TypeDescriptor.CreateProperty(
					typeof(AppointmentStyle),
					EventTextAlignPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(typeof(EnumExpressionInfoConverter)),
					new DefaultValueAttribute((object)ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultAppointmentTextAlign)))));
				// format
				properties[4] = new ShadowProperty(
					"EventFormat",
					TypeDescriptor.CreateProperty(
					typeof(AppointmentStyle),
					EventFormatPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(typeof(StringExpressionInfoConverter)),
					new DefaultValueAttribute((object)ExpressionInfo.FromString(CalendarData.DefaultAppointmentFormat))));
				// vertical align
				properties[5] = new ShadowProperty(
					"EventImage",
					TypeDescriptor.CreateProperty(
					typeof(AppointmentStyle),
					EventImagePropertyName,
					typeof(DesignImage),
					new TypeConverterAttribute(typeof(DesignImage.Converter)),
					new DefaultValueAttribute(typeof(DesignImage), string.Format("{0}, {1}, {2}",
						ExpressionInfo.FromString(ToolsNamespace.Utils.ConvertToString(CalendarData.DefaultAppointmentImageSource)),
						ExpressionInfo.FromString(CalendarData.DefaultAppointmentImageValue),
						ExpressionInfo.FromString(CalendarData.DefaultAppointmentMimeType)))
					));
				PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(properties);
				return pdc.Sort(PropertySortOrder);
			}

			public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
			{
				ExpressionInfo backColor = (ExpressionInfo)propertyValues[EventBackcolorPropertyName];
				ExpressionInfo borderColor = (ExpressionInfo)propertyValues[EventBorderColorPropertyName];
				DesignTextStyle fontStyle = (DesignTextStyle)propertyValues[EventFontPropertyName];
				ExpressionInfo textAlign = (ExpressionInfo)propertyValues[EventTextAlignPropertyName];
				ExpressionInfo format = (ExpressionInfo)propertyValues[EventFormatPropertyName];
				DesignImage image = (DesignImage)propertyValues[EventImagePropertyName];
				return new AppointmentStyle(backColor, borderColor, fontStyle, textAlign, format, image);
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
			if (!(obj is AppointmentStyle)) { return -1; }
			return CompareTo((AppointmentStyle)obj);
		}

		/// <summary>
		/// Compares this object to another <see cref="DesignLineStyle"/>.
		/// </summary>
		public int CompareTo(AppointmentStyle other)
		{
			int delta = AppointmentBackcolor == null ? -1 : AppointmentBackcolor.CompareTo(other.AppointmentBackcolor);
			if (delta == 0)
			{
				delta = AppointmentBorderColor.CompareTo(other.AppointmentBorderColor);
			}
			if (delta == 0)
			{
				delta = AppointmentFont.CompareTo(other.AppointmentFont);
			}
			if (delta == 0)
			{
				delta = AppointmentFormat == null ? -1 : AppointmentFormat.CompareTo(other.AppointmentFormat);
			}
			if (delta == 0)
			{
				delta = AppointmentTextAlign == null ? -1 : AppointmentTextAlign.CompareTo(other.AppointmentTextAlign);
			}
			if (delta == 0)
			{
				delta = AppointmentImage.CompareTo(other.AppointmentImage);
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

		// appointment style prop names
		internal const string EventBackcolorPropertyName = "AppointmentBackcolor";  // NOTE: the name is just a sopy of real property name
		internal const string EventBorderColorPropertyName = "AppointmentBorderColor";  // NOTE: the name is just a sopy of real property name
		internal const string EventFontPropertyName = "AppointmentFont";  // NOTE: the name is just a sopy of real property name
		internal const string EventTextAlignPropertyName = "AppointmentTextAlign";  // NOTE: the name is just a sopy of real property name
		internal const string EventFormatPropertyName = "AppointmentFormat";  // NOTE: the name is just a sopy of real property name
		internal const string EventImagePropertyName = "AppointmentImage";  // NOTE: the name is just a sopy of real property name

		private static readonly string[] PropertySortOrder =
			new[] { EventBackcolorPropertyName, EventBorderColorPropertyName, EventFontPropertyName, EventTextAlignPropertyName, EventFormatPropertyName, EventImagePropertyName };

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
