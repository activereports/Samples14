using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using GrapeCity.ActiveReports.Calendar.Design.Converters;
using GrapeCity.ActiveReports.Calendar.Design.TypeEditors;
using GrapeCity.ActiveReports.Design.DdrDesigner.Tools;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Rdl;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;

namespace GrapeCity.ActiveReports.Calendar.Design.Properties
{
	/// <summary>
	/// Represents the image type in design time.
	/// </summary>
	//[DoNotObfuscateType]
	[TypeConverter(typeof(Converter))]
	internal struct DesignImage : IComparable
	{
		/// <summary>
		/// Creates <see cref="DesignImage"/>.
		/// </summary>
		public DesignImage(ExpressionInfo source, ExpressionInfo value, ExpressionInfo mimeType)
		{
			_source = source;
			_value = value;
			_mimeType = mimeType;
		}

		/// <summary>
		/// Gets the <see cref="ExpressionInfo"/> represented a image source.
		/// </summary>
		[ExpressionBaseType(typeof(ImageSource))]
		[TypeConverter(typeof(EnumExpressionInfoConverter))]
		[Editor(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		public ExpressionInfo ImageSource
		{
			get { return _source; }
		}

		/// <summary>
		/// Gets the <see cref="ExpressionInfo"/> represented a image value.
		/// </summary>
		[TypeConverter(typeof(DesignImageValueTypeConverter))]
		[EditorAttribute(typeof(DesignImageValueTypeEditor), typeof(UITypeEditor))]
		public ExpressionInfo ImageValue
		{
			get { return _value; }
		}

		/// <summary>
		/// Gets the <see cref="ExpressionInfo"/> represented a image mime type.
		/// </summary>
		[TypeConverter(typeof(MimeTypeExpressionInfoConverter))]
		[EditorAttribute(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		public ExpressionInfo MimeType
		{
			get { return _mimeType; }
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
					string[] styles = new[] { DefaultSource, DefaultValue, DefaultMimeType };
					string originalString = ((string)value).Trim();
					string[] styleList = GetValuesFromList(originalString, culture);

					if (styleList.Length == 1) // the only value was passed
					{
						styles[1] = styleList[0]; // value
					}
					else
					{
						for (int i = 0; i < styleList.Length && i < styles.Length; i++)
						{
							styles[i] = styleList[i];
						}
					}
					return new DesignImage(
						ExpressionInfo.FromString(styles[0]), // source
						ExpressionInfo.FromString(styles[1]), // value
						ExpressionInfo.FromString(styles[2]));// mime type
				}
				return base.ConvertFrom(context, culture, value);
			}

			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					if (!(value is DesignImage))
						return string.Empty;

					DesignImage image = (DesignImage)value;
					if (culture == null)
						culture = CultureInfo.CurrentUICulture;

					VariantExpressionInfoConverter variantConverter = new VariantExpressionInfoConverter();
					return variantConverter.ConvertTo(context, culture, image.ImageValue, destinationType);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				PropertyDescriptorCollection pdc = base.GetProperties(context, value, attributes);
				return pdc.Sort(PropertySortOrder);
			}

			public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
			{
				ExpressionInfo source = (ExpressionInfo)propertyValues[ImageSourcePropertyName];
				ExpressionInfo value = (ExpressionInfo)propertyValues[ImageValuePropertyName];
				ExpressionInfo mimeType = (ExpressionInfo)propertyValues[MimeTypePropertyName];
				return new DesignImage(source, value, mimeType);
			}
		}

		#endregion

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
			if (!(obj is DesignImage)) { return -1; }
			return CompareTo((DesignImage)obj);
		}

		/// <summary>
		/// Compares this object to another <see cref="DesignImage"/>.
		/// </summary>
		public int CompareTo(DesignImage other)
		{
			int delta = ImageSource == null ? -1 : ImageSource.CompareTo(other.ImageSource);
			if (delta == 0)
			{
				delta = ImageValue == null ? -1 : ImageValue.CompareTo(other.ImageValue);
			}
			if (delta == 0)
			{
				delta = MimeType == null ? -1 : MimeType.CompareTo(other.MimeType);
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

		private readonly ExpressionInfo _source;
		private readonly ExpressionInfo _value;
		private readonly ExpressionInfo _mimeType;

		private const string DefaultSource = "External";
		private const string DefaultValue = "";
		private const string DefaultMimeType = "";
		private const string ImageSourcePropertyName = "ImageSource"; // NOTE: the name is just a copy of real property name
		private const string ImageValuePropertyName = "ImageValue"; // NOTE: the name is just a copy of real property name
		private const string MimeTypePropertyName = "MimeType"; // NOTE: the name is just a copy of real property name
		private static readonly string[] PropertySortOrder = new[] { ImageSourcePropertyName, ImageValuePropertyName, MimeTypePropertyName };

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
