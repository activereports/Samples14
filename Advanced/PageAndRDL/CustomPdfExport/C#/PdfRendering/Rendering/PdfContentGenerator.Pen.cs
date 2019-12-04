using System;
using System.Drawing;
using System.Linq;
using GrapeCity.ActiveReports.Drawing;
using PdfSharp.Drawing;

namespace GrapeCity.ActiveReports.Samples.Export.Rendering.PdfSharp
{
	partial class PdfContentGenerator
	{
		private class Pen : PenEx
		{
			public override Color Color { get; set; }
			public override float Width { get; set; }
			public override PenStyleEx DashStyle { get; set; }
			public override PenAlignment Alignment { get; set; }
			public override LineCap StartCap { get; set; }
			public override LineCap EndCap { get; set; }
			public override DashCap DashCap { get; set; }
			public override LineJoin LineJoin { get; set; }
			public override float[] DashPattern { get; set; }

			public override object Clone()
			{
				return new Pen
				{
					Color = Color,
					Width = Width,
					DashStyle = DashStyle,
					Alignment = Alignment,
					StartCap = StartCap,
					EndCap = EndCap,
					DashCap = DashCap,
					LineJoin = LineJoin,
					DashPattern = DashPattern
				};
			}

			public static implicit operator XPen(Pen pen)
			{
				var currentPen = new XPen(XColor.FromArgb(pen.Color.ToArgb()), pen.Width / PdfConverter.TwipsPerPoint);
				currentPen.DashStyle = (XDashStyle)Enum.Parse(typeof(XDashStyle), pen.DashStyle.ToString());
				currentPen.LineCap = (XLineCap)Enum.Parse(typeof(XLineCap), pen.StartCap.ToString());
				if (pen.LineJoin == LineJoin.MiterClipped)
					currentPen.LineJoin = XLineJoin.Miter;
				else
					currentPen.LineJoin = (XLineJoin)Enum.Parse(typeof(XLineJoin), pen.LineJoin.ToString());
				if (pen.DashPattern != null)
					currentPen.DashPattern = pen.DashPattern.Cast<double>().ToArray();
				return currentPen;
			}
		}
	}
}
