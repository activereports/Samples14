using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.PageReportModel;
using System;
using Color = System.Drawing.Color;

namespace GrapeCity.ActiveReports.Calendar.Components.Calendar
{
	/// <summary>
	/// Represents the type of a text style.
	/// </summary>
	public class TextStyle
	{
		public TextStyle(string family, Length size)
			: this(family, size, Drawing.FontStyle.Normal, Drawing.FontWeight.Normal, FontDecoration.None, Color.Black)
		{
		}

		public TextStyle(string family, Length size, Drawing.FontStyle style, Drawing.FontWeight weight, FontDecoration decoration, Color color)
		{
			if (string.IsNullOrEmpty(family))
				throw new ArgumentNullException("family");
			if (!size.IsValid || size.ToTwips() <= 0)
				throw new ArgumentOutOfRangeException("size");

			_fontFamily = family;
			_fontSize = size;
			_fontStyle = style;
			_fontWeight = weight;
			_fontDecoration = decoration;
			_fontColor = color;
		}

		/// <summary>
		/// Creates the <see cref="FontInfo"/> using a given <see cref="System.Drawing.FontStyle"/>.
		/// </summary>
		public FontInfo CreateFontInfo()
		{
			var fontInfo = new FontInfo(
				this.FontFamily, this.FontSize.ToPoints(),
				FontStyle, FontWeight, FontDecoration);
			return fontInfo;
		}

		/// <summary>
		/// Gets the font family represented by <see cref="String"/>.
		/// </summary>
		public string FontFamily
		{
			get { return _fontFamily; }
		}

		/// <summary>
		/// Gets the font size measured in points.
		/// </summary>
		public Length FontSize
		{
			get { return _fontSize; }
		}

		/// <summary>
		/// Gets the font style represented by <see cref="FontStyle"/>.
		/// </summary>
		public Drawing.FontStyle FontStyle
		{
			get { return _fontStyle; }
		}

		/// <summary>
		/// Gets the font weight represented by <see cref="FontWeight"/>.
		/// </summary>
		public Drawing.FontWeight FontWeight
		{
			get { return _fontWeight; }
		}

		/// <summary>
		/// Gets the font decoration represented by <see cref="FontDecoration"/>.
		/// </summary>
		public FontDecoration FontDecoration
		{
			get { return _fontDecoration; }
		}

		/// <summary>
		/// Gets the color to draw a text.
		/// </summary>
		public Color FontColor
		{
			get { return _fontColor; }
		}

		private readonly string _fontFamily;
		private readonly Length _fontSize;
		private readonly Drawing.FontStyle _fontStyle;
		private readonly Drawing.FontWeight _fontWeight;
		private readonly FontDecoration _fontDecoration;
		private readonly Color _fontColor;

		internal const string FontFamilyPropertyName = "FontFamily";
		internal const string FontSizePropertyName = "FontSize";
		internal const string FontStylePropertyName = "FontStyle";
		internal const string FontColorPropertyName = "FontColor";
		internal const string FontWeightPropertyName = "FontWeight";
		internal const string FontDecorationPropertyName = "FontDecoration";

		public static readonly Length MinWidth = new Length("1pt");
		public static readonly Length MaxWidth = new Length("200pt");
	}
}
