using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using GrapeCity.ActiveReports.Extensibility;
using GrapeCity.ActiveReports.Samples.CustomResourceLocator.Properties;

namespace GrapeCity.ActiveReports.Samples.CustomResourceLocator
{
	/// <summary>
	/// Look for the resources in My Pictures folder. <see cref="ResourceLocator"/> 
	/// </summary>
	internal sealed class MyPicturesLocator : ResourceLocator
	{
		private const string UriSchemeMyImages = "MyPictures:";
		/// <summary>
		/// Obtain and return the resource. 
		/// </summary>
		/// <param name="resourceInfo">The information about the resource to be obtained. </param>
		/// <returns>The resource, null if it was not found. </returns>
		public override Resource GetResource(ResourceInfo resourceInfo)
		{
			Resource resource;
			string name = resourceInfo.Name;
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(Resources.ResourceNameIsNull, "resourceInfo");
			}
			Uri uri = new Uri(name);
			if (uri.GetLeftPart(UriPartial.Scheme).StartsWith(UriSchemeMyImages, true, CultureInfo.InvariantCulture))
			{
				Stream stream = GetPictureFromSpecialFolder(uri);
				if (stream == null)
				{
					stream = new MemoryStream();
					Resources.NoImage.Save(stream, Resources.NoImage.RawFormat);
				}
				resource = new Resource(stream, uri);
			}
			else
			{
				throw new InvalidOperationException(Resources.ResourceSchemeIsNotSupported);
			}
			return resource;
		}

		/// <summary>
		/// Returns the specified image from My Pictures folder. 
		/// </summary>
		/// <param name="path">The uri of the image located in My Pictures code, i.e. MyImages:logo.gif.</param>
		/// <returns>The stream contains the image data or null if the picture can't be found or handled.</returns>
		private static Stream GetPictureFromSpecialFolder(Uri path)
		{
			int startPathPos = UriSchemeMyImages.Length;
			if (startPathPos >= path.ToString().Length)
			{
				return null;
			}
			string pictureName = path.ToString().Substring(startPathPos);
			string myPicturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
			if (!myPicturesPath.EndsWith("\\")) myPicturesPath += "\\";
			 string picturePath = Path.Combine(myPicturesPath, pictureName);
			if (!File.Exists(picturePath)) return null;
			MemoryStream stream = new MemoryStream();
			try
			{
				Image picture = Image.FromFile(picturePath);
				picture.Save(stream, picture.RawFormat);
				stream.Position = 0;
			}
			catch (OutOfMemoryException) // The file is not valid image, or GDI+ doesn't support such images.
			{
				return null;				
			}
			catch (ExternalException)
			{
				return null;
			}
			return stream;
		}
	}
}
