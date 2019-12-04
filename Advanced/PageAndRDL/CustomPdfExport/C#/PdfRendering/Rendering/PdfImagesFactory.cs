using System;
using System.Collections.Generic;
using System.IO;
using PdfSharp.Drawing;

namespace GrapeCity.ActiveReports.Samples.Export.Rendering.PdfSharp
{
	internal sealed class PdfImagesFactory : IDisposable
	{
		private readonly IDictionary<string, XImage> _images = new Dictionary<string, XImage>();

		public XImage GetPdfImage(string cacheId, Stream content)
		{
			XImage image;
			if (_images.TryGetValue(cacheId, out image))
				return image;
			image = XImage.FromStream(content);
			_images[cacheId] = image;
			return image;
		}

		#region IDisposable implementation

		void IDisposable.Dispose()
		{
			foreach (var image in _images.Values)
				image.Dispose();
			_images.Clear();
		}

		#endregion
	}
}
