using System.Drawing;
using GrapeCity.ActiveReports.Drawing;
using PdfSharp.Drawing;

namespace GrapeCity.ActiveReports.Samples.Export.Rendering.PdfSharp
{
	partial class PdfContentGenerator
	{
		private abstract class BrushBase : BrushEx
		{
			protected abstract XBrush Convert();

			public static implicit operator XBrush(BrushBase brush)
			{
				return brush.Convert();
			}
		}

		private class SolidBrush : BrushBase
		{
			private readonly XBrush _brushInfo;

			public SolidBrush(Color color)
			{
				_brushInfo = new XSolidBrush(XColor.FromArgb(color.ToArgb()));
			}

			private SolidBrush(XBrush brushInfo)
			{
				_brushInfo = brushInfo;
			}

			public override object Clone()
			{
				return new SolidBrush(_brushInfo);
			}

			protected override XBrush Convert()
			{
				return _brushInfo;
			}
		}

		private class LinearGradientBrush : BrushBase
		{
			private readonly XBrush _brushInfo;

			public LinearGradientBrush(PointF point1, PointF point2, Color color1, Color color2)
			{
				_brushInfo = new XLinearGradientBrush(PdfConverter.Convert(point1), PdfConverter.Convert(point2), XColor.FromArgb(color1.ToArgb()), XColor.FromArgb(color2.ToArgb()));
			}

			private LinearGradientBrush(XBrush brushInfo)
			{
				_brushInfo = brushInfo;
			}

			public override object Clone()
			{
				return new LinearGradientBrush(_brushInfo);
			}

			protected override XBrush Convert()
			{
				return _brushInfo;
			}
		}

		private class RadialGradientBrush : BrushBase
		{
			private readonly XBrush _brushInfo;

			public RadialGradientBrush(PointF centerPoint, float radius, Color centerColor, Color surroundingColor)
			{
				// Radial brush was partly implemented in upcoming beta and not ready for use
				//_brushInfo = new XRadialGradientBrush(PdfConverter.Convert(centerPoint), PdfConverter.Convert(centerPoint), radius, radius, XColor.FromArgb(centerColor.ToArgb()), XColor.FromArgb(surroundingColor.ToArgb()));
				_brushInfo = new XSolidBrush(XColor.FromArgb(centerColor.ToArgb()));
			}

			private RadialGradientBrush(XBrush brushInfo)
			{
				_brushInfo = brushInfo;
			}

			public override object Clone()
			{
				return new RadialGradientBrush(_brushInfo);
			}

			protected override XBrush Convert()
			{
				return _brushInfo;
			}
		}
	}
}
