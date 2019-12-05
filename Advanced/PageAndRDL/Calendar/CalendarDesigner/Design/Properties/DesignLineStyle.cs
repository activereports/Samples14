using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Text;
using GrapeCity.ActiveReports.Calendar.Validation;
using GrapeCity.ActiveReports.Design.DdrDesigner.Editors.ColorEditor;
using GrapeCity.ActiveReports.Design.DdrDesigner.Tools;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Rdl;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;
using BorderStyle = GrapeCity.ActiveReports.Calendar.Components.Calendar.BorderStyle;

namespace GrapeCity.ActiveReports.Calendar.Design.Properties
{
	/// <summary>
	/// Represents the line style type in design time.
	/// </summary>
	//[DoNotObfuscateType]
	[TypeConverter(typeof(Converter))]
	internal struct DesignLineStyle : IComparable, IValidatable
	{
		/// <summary>
		/// Creates <see cref="DesignLineStyle"/>.
		/// </summary>
		public DesignLineStyle(ExpressionInfo color, ExpressionInfo width, ExpressionInfo style)
			: this(color, width, style, false)
		{
		}

		/// <summary>
		/// Creates <see cref="DesignLineStyle"/>.
		/// </summary>
		public DesignLineStyle(ExpressionInfo color, ExpressionInfo width, ExpressionInfo style, bool isNullable)
		{
			_color = color;
			_width = width;
			_style = style;
			_isNullable = isNullable;
		}

		/// <summary>
		/// Gets the <see cref="ExpressionInfo"/> represented a line color.
		/// </summary>
		[EditorAttribute(typeof(ColorTypeEditor), typeof(UITypeEditor))]
		public ExpressionInfo LineColor
		{
			get { return _color; }
		}

		/// <summary>
		/// Gets the <see cref="ExpressionInfo"/> represented a line width.
		/// </summary>
		[EditorAttribute(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		public ExpressionInfo LineWidth
		{
			get { return _width; }
		}

		/// <summary>
		/// Gets the <see cref="ExpressionInfo"/> represented a line style.
		/// </summary>
		[ExpressionBaseTypeAttribute(typeof(BorderStyle))]
		[EditorAttribute(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		public ExpressionInfo LineStyle
		{
			get { return _style; }
		}

		/// <summary>
		/// Gets the flag that represents permit to assign null value for properties.
		/// </summary>
		private bool IsNullable
		{
			get { return _isNullable; }
		}

		/// <summary>
		/// Performs validation using the specified evaluator.
		/// </summary>
		public void Validate(EvaluatorService evaluator)
		{
			ValidationUtils.ValidateLength(evaluator, LineWidth, _minWidth, _maxWidth);
		}

		#region Converters

		/// <summary>
		/// Represents line style converter.
		/// </summary>
		internal sealed class Converter : RootExpandableConverter
		{
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value is string)
				{
					string[] styles = new[] { DefaultColor, DefaultWidth, DefaultStyle };
					string originalString = ((string)value).Trim();
					string[] styleList = GetValuesFromList(originalString, culture);

					for (int i = 0; i < styleList.Length && i < styles.Length; i++)
					{
						styles[i] = styleList[i];
					}
					return new DesignLineStyle(
						ExpressionInfo.FromString(styles[0]), // color
						ExpressionInfo.FromString(styles[1]), // width
						ExpressionInfo.FromString(styles[2]));// style
				}
				return base.ConvertFrom(context, culture, value);
			}

			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					if (!(value is DesignLineStyle))
						return string.Empty;

					DesignLineStyle lineStyle = (DesignLineStyle)value;
					if (lineStyle.IsNullable)
						return string.Empty;

					if (culture == null)
						culture = CultureInfo.CurrentUICulture;

					ColorExpressionInfoConverter colorStyleConverter = new ColorExpressionInfoConverter();
					LengthExpressionInfoConverter widthStyleConverter = new LengthExpressionInfoConverter();
					EnumExpressionInfoConverter borderStyleConverter = new EnumExpressionInfoConverter();
					string separator = culture.TextInfo.ListSeparator + " ";

					StringBuilder sb = new StringBuilder();
					sb.Append(colorStyleConverter.ConvertTo(context, culture, lineStyle.LineColor, destinationType));
					sb.Append(separator);
					sb.Append(widthStyleConverter.ConvertTo(context, culture, lineStyle.LineWidth, destinationType));
					sb.Append(separator);
					sb.Append(borderStyleConverter.ConvertTo(context, culture, lineStyle.LineStyle, destinationType));

					return sb.ToString();
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				DesignLineStyle lineStyle = (DesignLineStyle)value;
				const int N = 3;
				PropertyDescriptor[] properties = new PropertyDescriptor[N];
				properties[0] = new ShadowProperty(LineColorDisplayName,
					TypeDescriptor.CreateProperty(
					typeof(DesignLineStyle),
					LineColorPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(lineStyle.IsNullable
						? typeof(NullableColorExpressionInfoConverter)
						: typeof(ColorExpressionInfoConverter))
					));
				properties[1] = new ShadowProperty(LineWidthDisplayName,
					TypeDescriptor.CreateProperty(
					typeof(DesignLineStyle),
					LineWidthPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(lineStyle.IsNullable
						? typeof(NullableLengthExpressionInfoConverter)
						: typeof(LengthExpressionInfoConverter))
					));
				properties[2] = new ShadowProperty(LineStyleDisplayName,
					TypeDescriptor.CreateProperty(
					typeof(DesignLineStyle),
					LineStylePropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(lineStyle.IsNullable
						? typeof(NullableEnumExpressionInfoConverter)
						: typeof(EnumExpressionInfoConverter))
					));

				PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(properties);
				return pdc.Sort(PropertySortOrder);
			}

			public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
			{
				ExpressionInfo color = (ExpressionInfo)propertyValues[LineColorPropertyName];
				ExpressionInfo width = (ExpressionInfo)propertyValues[LineWidthPropertyName];
				ExpressionInfo style = (ExpressionInfo)propertyValues[LineStylePropertyName];
				return new DesignLineStyle(color, width, style);
			}
		}

		#endregion

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
			if (!(obj is DesignLineStyle)) { return -1; }
			return CompareTo((DesignLineStyle)obj);
		}

		/// <summary>
		/// Compares this object to another <see cref="DesignLineStyle"/>.
		/// </summary>
		public int CompareTo(DesignLineStyle other)
		{
			int delta = this.LineColor == null ? -1 : this.LineColor.CompareTo(other.LineColor);
			if (delta == 0)
			{
				delta = this.LineWidth == null ? -1 : this.LineWidth.CompareTo(other.LineWidth);
			}
			if (delta == 0)
			{
				delta = this.LineStyle == null ? -1 : this.LineStyle.CompareTo(other.LineStyle);
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

		private readonly ExpressionInfo _color;
		private readonly ExpressionInfo _width;
		private readonly ExpressionInfo _style;
		private readonly bool _isNullable;
		private static readonly Length _minWidth = new Length("0.25pt");
		private static readonly Length _maxWidth = new Length("20pt");

		private const string DefaultColor = "Black";
		private const string DefaultWidth = "1pt";
		private const string DefaultStyle = "Solid";
		private const string LineColorDisplayName = "LineColor";
		private const string LineWidthDisplayName = "LineWidth";
		private const string LineStyleDisplayName = "LineStyle";
		public const string LineColorPropertyName = "LineColor"; // NOTE: the name is just a copy of real property name
		public const string LineWidthPropertyName = "LineWidth"; // NOTE: the name is just a copy of real property name
		public const string LineStylePropertyName = "LineStyle"; // NOTE: the name is just a copy of real property name
		private static readonly string[] PropertySortOrder = new[] { LineColorPropertyName, LineWidthPropertyName, LineStylePropertyName };

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
