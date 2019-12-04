using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Text;
using GrapeCity.ActiveReports.Calendar.Validation;
using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.Design.DdrDesigner.Editors.ColorEditor;
using GrapeCity.ActiveReports.Design.DdrDesigner.Tools;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;
using GrapeCity.ActiveReports.Rdl;
using GrapeCity.ActiveReports.PageReportModel;

namespace GrapeCity.ActiveReports.Calendar.Design.Properties
{
	/// <summary>
	/// Represents the text style type in design time.
	/// </summary>
	//[DoNotObfuscateType]
	[TypeConverter(typeof(Converter))]
	internal struct DesignTextStyle : IComparable, IValidatable
	{
		/// <summary>
		/// Creates <see cref="DesignTextStyle"/>.
		/// </summary>
		public DesignTextStyle(ExpressionInfo family, ExpressionInfo size, ExpressionInfo style,
			ExpressionInfo weight, ExpressionInfo decoration, ExpressionInfo color)
			: this(family, size, style, weight, decoration, color, false)
		{
		}

		/// <summary>
		/// Creates <see cref="DesignTextStyle"/>.
		/// </summary>
		public DesignTextStyle(ExpressionInfo family, ExpressionInfo size, ExpressionInfo style,
			ExpressionInfo weight, ExpressionInfo decoration, ExpressionInfo color, bool isNullable)
		{
			_family = family;
			_size = size;
			_style = style;
			_weight = weight;
			_decoration = decoration;
			_color = color;
			_isNullable = isNullable;
		}

		/// <summary>
		/// Gets the <see cref="ExpressionInfo"/> represented a font family.
		/// </summary>
		[EditorAttribute(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		public ExpressionInfo Family
		{
			get { return _family; }
		}

		/// <summary>
		/// Gets the <see cref="ExpressionInfo"/> represented a font size.
		/// </summary>
		[EditorAttribute(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		public ExpressionInfo Size
		{
			get { return _size; }
		}

		/// <summary>
		/// Gets the <see cref="ExpressionInfo"/> represented a font style.
		/// </summary>
		[ExpressionBaseTypeAttribute(typeof(Drawing.FontStyle))]
		[EditorAttribute(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		public ExpressionInfo Style
		{
			get { return _style; }
		}

		/// <summary>
		/// Gets the <see cref="ExpressionInfo"/> represented a font weight.
		/// </summary>
		[ExpressionBaseTypeAttribute(typeof(Drawing.FontWeight))]
		[EditorAttribute(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		public ExpressionInfo Weight
		{
			get { return _weight; }
		}

		/// <summary>
		/// Gets the <see cref="ExpressionInfo"/> represented a font decoration.
		/// </summary>
		[ExpressionBaseTypeAttribute(typeof(FontDecoration))]
		[EditorAttribute(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		public ExpressionInfo Decoration
		{
			get { return _decoration; }
		}
		/// <summary>
		/// Gets the <see cref="ExpressionInfo"/> represented a font color.
		/// </summary>
		[EditorAttribute(typeof(ColorTypeEditor), typeof(UITypeEditor))]
		public ExpressionInfo Color
		{
			get { return _color; }
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
			ValidationUtils.ValidateLength(evaluator, Size, _minSize, _maxSize);
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
					string[] styles = new string[]
						{
							DefaultFamily, DefaultSize, DefaultStyle, DefaultWeight, DefaultDecoration, DefaultColor
						};
					string originalString = ((string)value).Trim();
					string[] styleList = GetValuesFromList(originalString, culture);

					for (int i = 0; i < styleList.Length && i < styles.Length; i++)
					{
						styles[i] = styleList[i];
					}
					return new DesignTextStyle(
						ExpressionInfo.FromString(styles[0]), // family
						ExpressionInfo.FromString(styles[1]), // size
						ExpressionInfo.FromString(styles[2]), // style
						ExpressionInfo.FromString(styles[3]), // weight
						ExpressionInfo.FromString(styles[4]), // decoration
						ExpressionInfo.FromString(styles[5]));// color
				}
				return base.ConvertFrom(context, culture, value);
			}

			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					if (!(value is DesignTextStyle))
						return string.Empty;

					DesignTextStyle textStyle = (DesignTextStyle)value;
					if (textStyle.IsNullable)
						return string.Empty;

					if (culture == null)
						culture = CultureInfo.CurrentUICulture;

					StringExpressionInfoConverter stringConverter = new StringExpressionInfoConverter();
					ColorExpressionInfoConverter colorConverter = new ColorExpressionInfoConverter();
					LengthExpressionInfoConverter lengthConverter = new LengthExpressionInfoConverter();
					EnumExpressionInfoConverter enumConverter = new EnumExpressionInfoConverter();
					string separator = culture.TextInfo.ListSeparator + " ";

					StringBuilder sb = new StringBuilder();
					sb.Append(stringConverter.ConvertTo(context, culture, textStyle.Family, destinationType));
					sb.Append(separator);
					sb.Append(lengthConverter.ConvertTo(context, culture, textStyle.Size, destinationType));
					sb.Append(separator);
					sb.Append(enumConverter.ConvertTo(context, culture, textStyle.Style, destinationType));
					sb.Append(separator);
					sb.Append(enumConverter.ConvertTo(context, culture, textStyle.Weight, destinationType));
					sb.Append(separator);
					sb.Append(enumConverter.ConvertTo(context, culture, textStyle.Decoration, destinationType));
					sb.Append(separator);
					sb.Append(colorConverter.ConvertTo(context, culture, textStyle.Color, destinationType));

					return sb.ToString();
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				DesignTextStyle lineStyle = (DesignTextStyle)value;
				const int N = 6;
				PropertyDescriptor[] properties = new PropertyDescriptor[N];
				properties[0] = new ShadowProperty(FontFamilyDisplayName,
					TypeDescriptor.CreateProperty(
					typeof(DesignTextStyle),
					FontFamilyPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(lineStyle.IsNullable
						? typeof(NullableFontFamilyExpressionInfoConverter)
						: typeof(FontFamilyExpressionInfoConverter))
					));
				properties[1] = new ShadowProperty(FontSizeDisplayName,
					TypeDescriptor.CreateProperty(
					typeof(DesignTextStyle),
					FontSizePropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(lineStyle.IsNullable
						? typeof(NullableLengthExpressionInfoConverter)
						: typeof(LengthExpressionInfoConverter))
					));
				properties[2] = new ShadowProperty(FontStyleDisplayName,
					TypeDescriptor.CreateProperty(
					typeof(DesignTextStyle),
					FontStylePropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(lineStyle.IsNullable
						? typeof(NullableEnumExpressionInfoConverter)
						: typeof(EnumExpressionInfoConverter))
					));
				properties[3] = new ShadowProperty(FontWeightDisplayName,
					TypeDescriptor.CreateProperty(
					typeof(DesignTextStyle),
					FontWeightPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(lineStyle.IsNullable
						? typeof(NullableEnumExpressionInfoConverter)
						: typeof(EnumExpressionInfoConverter))
					));
				properties[4] = new ShadowProperty(FontDecorationDisplayName,
					TypeDescriptor.CreateProperty(
					typeof(DesignTextStyle),
					FontDecorationPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(lineStyle.IsNullable
						? typeof(NullableEnumExpressionInfoConverter)
						: typeof(EnumExpressionInfoConverter))
					));
				properties[5] = new ShadowProperty(FontColorDisplayName,
					TypeDescriptor.CreateProperty(
					typeof(DesignTextStyle),
					FontColorPropertyName,
					typeof(ExpressionInfo),
					new TypeConverterAttribute(lineStyle.IsNullable
						? typeof(NullableColorExpressionInfoConverter)
						: typeof(ColorExpressionInfoConverter))
					));

				PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(properties);
				return pdc.Sort(PropertySortOrder);
			}

			public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
			{
				ExpressionInfo family = (ExpressionInfo)propertyValues[FontFamilyPropertyName];
				ExpressionInfo size = (ExpressionInfo)propertyValues[FontSizePropertyName];
				ExpressionInfo style = (ExpressionInfo)propertyValues[FontStylePropertyName];
				ExpressionInfo weight = (ExpressionInfo)propertyValues[FontWeightPropertyName];
				ExpressionInfo decor = (ExpressionInfo)propertyValues[FontDecorationPropertyName];
				ExpressionInfo color = (ExpressionInfo)propertyValues[FontColorPropertyName];
				return new DesignTextStyle(family, size, style, weight, decor, color);
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
			if (!(obj is DesignTextStyle)) { return -1; }
			return this.CompareTo((DesignTextStyle)obj);
		}

		/// <summary>
		/// Compares this object to another <see cref="DesignLineStyle"/>.
		/// </summary>
		public int CompareTo(DesignTextStyle other)
		{
			int delta = this.Family == null ? -1 : this.Family.CompareTo(other.Family);
			if (delta == 0)
			{
				delta = this.Size == null ? -1 : this.Size.CompareTo(other.Size);
			}
			if (delta == 0)
			{
				delta = this.Style == null ? -1 : this.Style.CompareTo(other.Style);
			}
			if (delta == 0)
			{
				delta = this.Weight == null ? -1 : this.Weight.CompareTo(other.Weight);
			}
			if (delta == 0)
			{
				delta = this.Decoration == null ? -1 : this.Decoration.CompareTo(other.Decoration);
			}
			if (delta == 0)
			{
				delta = this.Color == null ? -1 : this.Color.CompareTo(other.Color);
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

		private readonly ExpressionInfo _family;
		private readonly ExpressionInfo _size;
		private readonly ExpressionInfo _style;
		private readonly ExpressionInfo _weight;
		private readonly ExpressionInfo _decoration;
		private readonly ExpressionInfo _color;
		private readonly bool _isNullable;
		private static readonly Length _minSize = new Length("1pt");
		private static readonly Length _maxSize = new Length("200pt");

		private static readonly string DefaultFamily = "Arial";
		private static readonly string DefaultSize = "10pt";
		private static readonly string DefaultStyle = "Normal";
		private static readonly string DefaultWeight = "Normal";
		private static readonly string DefaultDecoration = "None";
		private static readonly string DefaultColor = "Black";
		private const string FontFamilyDisplayName = "FontFamily";
		private const string FontSizeDisplayName = "FontSize";
		private const string FontStyleDisplayName = "FontStyle";
		private const string FontWeightDisplayName = "FontWeight";
		private const string FontDecorationDisplayName = "FontDecoration";
		private const string FontColorDisplayName = "Color";
		public const string FontFamilyPropertyName = "Family";
		public const string FontSizePropertyName = "Size";
		public const string FontStylePropertyName = "Style";
		public const string FontWeightPropertyName = "Weight";
		public const string FontDecorationPropertyName = "Decoration";
		public const string FontColorPropertyName = "Color";
		private static readonly string[] PropertySortOrder =
			new[]
				{
					FontFamilyPropertyName, FontSizePropertyName, FontStylePropertyName, FontWeightPropertyName, FontDecorationPropertyName, FontColorPropertyName
				};

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
