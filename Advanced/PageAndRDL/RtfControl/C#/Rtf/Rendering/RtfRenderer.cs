using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using GrapeCity.ActiveReports.Samples.Rtf.Native;

namespace GrapeCity.ActiveReports.Samples.Rtf.Rendering
{
	public static class RtfRenderer
	{
		public static Stream RenderToStream(string rtfContent, SizeF sizeInInches)
		{
			var stream = new MemoryStream();

			using (RenderImage(rtfContent, sizeInInches, (hdc, size) => new Metafile(stream, hdc, new RectangleF(Point.Empty, size), MetafileFrameUnit.Pixel, EmfType.EmfPlusDual))) { }

			stream.Position = 0;
			return stream;
		}

		public static Image RenderMetafile(string rtfContent, SizeF sizeInInches)
		{
			return RenderImage(rtfContent, sizeInInches, (hdc, size) => new Metafile(hdc, new RectangleF(Point.Empty, size), MetafileFrameUnit.Pixel, EmfType.EmfPlusDual));
		}

		private static Image RenderImage(string rtfContent, SizeF sizeInInches, Func<IntPtr, Size, Image> createImage)
		{
			using (var richText = new RichTextBox())
			{
				richText.Rtf = rtfContent;
				richText.CreateControl();

				using (var g = richText.CreateGraphics())
				{
					var dpiX = g.DpiX;
					var dpiY = g.DpiY;

					richText.Width = (int)(sizeInInches.Width * dpiX);
					richText.Height = (int)(sizeInInches.Height * dpiY);

					var hdc = g.GetHdc();
					var image = createImage(hdc, new Size(richText.Width, richText.Height));
					using (var imageG = Graphics.FromImage(image))
					{
						var formatRange = new NativeMethods.FORMATRANGE();
						formatRange.hdc = formatRange.hdcTarget = imageG.GetHdc();

						formatRange.rc.right = formatRange.rcPage.right = (int)Math.Ceiling(sizeInInches.Width * 1440);
						formatRange.rc.bottom = formatRange.rcPage.bottom = (int)Math.Ceiling(sizeInInches.Height * 1440);
						formatRange.chrg.cpMin = 0;
						formatRange.chrg.cpMax = -1;

						var lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(formatRange));
						Marshal.StructureToPtr(formatRange, lParam, false);
						NativeMethods.SendMessage(richText.Handle, NativeMethods.EM_FORMATRANGE, 1, lParam);
						Marshal.FreeCoTaskMem(lParam);

						imageG.ReleaseHdc(formatRange.hdc);
					}
					g.ReleaseHdc(hdc);

					return image;
				}
			}
		}
	}
}
