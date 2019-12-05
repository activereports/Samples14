using System;
using System.Globalization;
using GrapeCity.ActiveReports.Drawing.Gdi;

namespace GrapeCity.ActiveReports.Calendar.Rendering
{
	/// <summary>
	/// Contains some utility methods for rendering, parsing and converting.
	/// </summary>
	public static class RenderUtils
	{/// <summary>
	 /// Converts a value from twips to pixels.
	 /// </summary>
		public static int FromTwipsToPixels(float twips)
		{
			return FromTwipsToPixels(twips, SafeGraphics.HorizontalResolution);
		}

		/// <summary>
		/// Converts a value from twips to pixels.
		/// </summary>
		public static int FromTwipsToPixels(float twips, float dpi)
		{
			return Convert.ToInt32(twips * dpi / 1440f);
		}

		/// <summary>
		/// Converts a value from twips to pixels.
		/// </summary>
		public static float ConvertPixelsToTwips(float pixels, float dpi)
		{
			return pixels / dpi * 1440f;
		}

		/// <summary>
		/// Converts a value from twips to pixels.
		/// </summary>
		public static float ConvertPixelsToTwips(float pixels)
		{
			return ConvertPixelsToTwips(pixels, SafeGraphics.VerticalResolution);
		}

		#region Conversion

		/// <summary>
		/// Convert the given value to <see cref="double"/> type by using the invariant culture.
		/// </summary>
		/// <param name="value">the value to convert</param>
		public static double ConvertToDouble(object value)
		{
			return Convert.ToDouble(value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Convert the given value to <see cref="string"/> type by using the current UI culture.
		/// </summary>
		/// <param name="value">the value to convert</param>
		/// <param name="format">the format string to use in conversion</param>
		public static string ConvertToString(double value, string format)
		{
			// TODO: consider to read culture info from the report
			return value.ToString(format, CultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// Convert the given value to <see cref="DateTime"/> type by using the invariant culture.
		/// </summary>
		/// <param name="value">the value to convert</param>
		public static DateTime ConvertToDateTime(object value)
		{
			return Convert.ToDateTime(value, CultureInfo.InvariantCulture);
		}

		#endregion
	}
}
