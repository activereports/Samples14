using GrapeCity.ActiveReports.PageReportModel;

namespace GrapeCity.ActiveReports.Calendar.Components.Calendar
{
	/// <summary>
	/// Represents the type of appointement image property.
	/// </summary>
	public sealed class ImageStyle
	{
		private const ImageSource DefaultSource = ImageSource.External;
		private static readonly object DefaultValue = string.Empty;
		private static readonly string DefaultMimeType = string.Empty;

		public const string SourcePropertyName = "ImageSource";
		public const string ValuePropertyName = "ImageValue";
		public const string MimeTypePropertyName = "MimeType";

		private readonly ImageSource _source;
		private readonly object _value;
		private readonly string _mimeType;

		public ImageSource ImageSource
		{
			get { return _source; }
		}

		public object ImageValue
		{
			get { return _value; }
		}

		public string ImageMimeType
		{
			get { return _mimeType; }
		}

		public ImageStyle() : this(DefaultSource, DefaultValue, DefaultMimeType)
		{
		}

		public ImageStyle(ImageSource source, object value, string mimeType)
		{
			_source = source;
			_value = value;
			_mimeType = mimeType;
		}
	}
}
