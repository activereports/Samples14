using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.IO;
using GrapeCity.ActiveReports.Calendar.Components.Calendar;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Rendering.Tools;
using Image = System.Drawing.Image;
using System.Diagnostics;

namespace GrapeCity.ActiveReports.Calendar.Rendering
{
	/// <summary>
	/// ImageLocatorService
	/// </summary>
	public  class ImageLocatorService
	{
		protected  PageReport _parentPageReport;
		protected ResourceLocator _resourceLocator;
		private readonly Dictionary<string, Image> _cache = new Dictionary<string, Image>();

		public ImageLocatorService() { }

		public ImageLocatorService(IReport parentReport)
		{
			if (parentReport == null)
			{
				Debug.Fail("Unable to get the parent report for calendar report item");
				return;
			}
			_parentPageReport = parentReport.GetService(typeof(PageReport)) as PageReport;
			InitializeServices();
		}

		protected void InitializeServices()
		{
			if (_parentPageReport == null)
			{
				Trace.TraceWarning("Can get report definition from the host's root component");
				return;
			}
			if (_parentPageReport.Site != null)
			{
				_resourceLocator = _parentPageReport.Site.GetService(typeof(ResourceLocator)) as ResourceLocator;
			}
		}

		/// <summary>
		/// Resolves image using specified image style.
		/// </summary>
		/// <param name="imageStyle">Specifies how to resolve image.</param>
		/// <returns></returns>
		public Image GetImage(ImageStyle imageStyle)
		{
			if (imageStyle == null)
				return null;

			string key = ComputeHash(imageStyle.ImageValue);
			Image resultImage;
			if (_cache.TryGetValue(key, out resultImage))
				return resultImage;

			Stream imageStream = GetImageStream(imageStyle);
			if (imageStream == null) return null;

			using (imageStream)
			using (resultImage = LoadImageSafe(imageStream, key))
			{
				if (resultImage != null)
				{
					// case 194962: if we want to make byte operations with image after stream closing.
					resultImage = new System.Drawing.Bitmap(resultImage);
					_cache[key] = resultImage;
				}
			}

			return resultImage;
		}

		/// <summary>
		/// Resolves image data using specified image style.
		/// </summary>
		/// <param name="cache">The local cache of image data.</param>
		/// <param name="imageStyle">Specifies how to resolve image.</param>
		/// <returns></returns>
		public KeyValuePair<string, byte[]> GetImageData(Dictionary<string, byte[]> cache, ImageStyle imageStyle)
		{
			if (imageStyle == null)
			{
				return new KeyValuePair<string, byte[]>(null, null);
			}

			string key = ComputeHash(imageStyle.ImageValue);
			byte[] data;
			if (cache.TryGetValue(key, out data))
			{
				return new KeyValuePair<string, byte[]>(key, data);
			}

			Stream imageStream = GetImageStream(imageStyle);
			if (imageStream == null)
			{
				return new KeyValuePair<string, byte[]>(null, null);
			}

			MemoryStream imageMemoryStream = new MemoryStream();
			using (imageStream)
			{
				imageStream.CopyTo(imageMemoryStream);
			}

			using (imageMemoryStream)
			{
				// try to load image, do not store bad images in document
				using (Image resultImage = LoadImageSafe(imageMemoryStream, key))
					if (resultImage == null)
					{
						return new KeyValuePair<string, byte[]>(null, null);
					}

				imageMemoryStream.Position = 0;
				data = imageMemoryStream.ToArray();

				return new KeyValuePair<string, byte[]>(key, data);
			}
		}

		/// <summary>
		/// Tries to load image from specified stream.
		/// </summary>
		/// <param name="imageStream">The input stream to load image from.</param>
		/// <param name="key">The image key to be used for diagnostics purposes.</param>
		/// <returns>The <see cref="Image"/> object if succeeded; otherwise - <c>nulll</c>.</returns>
		private static Image LoadImageSafe(Stream imageStream, string key)
		{
			try
			{
				return Image.FromStream(imageStream);
			}
			catch (Exception) // catch everything -- there could be out of memory, file not found else???
			{
				Trace.TraceError("Unable to load image '{0}'.", key);
				return null;
			}
		}

		private Stream GetImageStream(ImageStyle imageStyle)
		{
			byte[] data = imageStyle.ImageValue as byte[];
			if (data != null)
			{
				return new MemoryStream(data);
			}
			switch (imageStyle.ImageSource)
			{
				case ImageSource.External:
					return GetExternalImageStream(imageStyle.ImageValue);
				case ImageSource.Embedded:
					return GetEmbeddedImageStream(imageStyle.ImageValue);
				case ImageSource.Database:
					return GetDatabaseImageStream(imageStyle.ImageValue);
				default:
					throw new NotSupportedException(string.Format("ImageSource '{0}' is not supported.", imageStyle.ImageSource));
			}
		}

		private Stream GetExternalImageStream(object value)
		{
			string name = value as string;
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			try
			{
				if (_resourceLocator != null)
				{
					Resource imageResource = _resourceLocator.GetResource(new ResourceInfo(name));
					if (imageResource.Value != null)
					{
						return imageResource.Value;
					}
				}
				if (File.Exists(name))
				{
					return File.OpenRead(name);
				}
				return null;
			}
			catch (Exception)
			{
				// catch everything -- there could be out of memory, file not found else???
				Trace.TraceError("Failed to load image from '{0}'.", value);
				return null;
			}
		}

		private Stream GetEmbeddedImageStream(object value)
		{
			if (!(value is string)) return null;
			string result = value as string;
			EmbeddedImage embeddedImage = FindEmbeddedImage(result);
			if (embeddedImage == null || string.IsNullOrEmpty(embeddedImage.ImageData))
			{
				return null;
			}
			try
			{
				return new MemoryStream(Convert.FromBase64String(embeddedImage.ImageData));
			}
			catch (Exception) // catch everything -- there could be out of memory, file not found else???
			{
				Trace.TraceError("Error reading embeded image data.");
				return null;
			}
		}

		private static Stream GetDatabaseImageStream(object value)
		{
			if (value == null) return null;
			byte[] bytes = value as byte[];
			if (bytes == null) return null;
			return new MemoryStream(bytes);
		}

		private EmbeddedImage FindEmbeddedImage(string valueName)
		{
			if (_parentPageReport == null)
				return null;

			if (_parentPageReport.Report.EmbeddedImages == null)
			{
				Trace.TraceInformation("Report.EmbededImages: there are no images, embeded in this report.");
				return null;
			}
			for (int i = 0; i < _parentPageReport.Report.EmbeddedImages.Count; i++)
			{
				if (string.Compare(_parentPageReport.Report.EmbeddedImages[i].Name, valueName, true, CultureInfo.InvariantCulture) == 0)
				{
					return _parentPageReport.Report.EmbeddedImages[i];
				}
			}
			Trace.TraceInformation("Report.EmbededImages: failed to find embeded image with name '{0}'.", valueName);
			return null;
		}
		/// <summary>
		/// Calculates hash for the specified value
		/// </summary>
		private static string ComputeHash(object value)
		{
			string key = string.Empty;
			if (value == null)
			{
				return key;
			}
			if (value is string)
			{
				key = value as string;
			}
			else
			{
				if (!(value is byte[]))
				{
					return key;
				}

				byte[] rawBytes = HashCalculator.ComputeMD5((byte[])value);
				StringBuilder sb = new StringBuilder(rawBytes.Length);
				for (int i = 0; i < rawBytes.Length; i++)
				{
					sb.Append(rawBytes[i].ToString("X2"));
				}
				key = sb.ToString();

			}
			return key;
		}
	}
}
