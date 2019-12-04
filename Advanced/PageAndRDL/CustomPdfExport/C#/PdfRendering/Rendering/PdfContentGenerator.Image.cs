using System.Drawing;
using GrapeCity.ActiveReports.Drawing;
using PdfSharp.Drawing;

namespace GrapeCity.ActiveReports.Samples.Export.Rendering.PdfSharp
{
	partial class PdfContentGenerator
	{
		private class Image : ImageEx
		{
			private readonly XImage _imageInfo;

			public Image(XImage imageInfo)
			{
				_imageInfo = imageInfo;
			}

			public override object Clone()
			{
				return new Image(_imageInfo);
			}

			public override SizeF Size
			{
				get { return new SizeF((float)(_imageInfo.PixelWidth * 1440 / _imageInfo.HorizontalResolution), (float)(_imageInfo.PixelHeight * 1440 / _imageInfo.VerticalResolution)); }
			}

			public static implicit operator XImage(Image image)
			{
				return image._imageInfo;
			}
		}
	}
}
