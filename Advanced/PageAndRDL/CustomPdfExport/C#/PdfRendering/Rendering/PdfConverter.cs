using System.Drawing;
using PdfSharp.Drawing;

namespace GrapeCity.ActiveReports.Samples.Export.Rendering.PdfSharp
{
	internal static class PdfConverter
	{
		public const int PointsPerInch = 72;
		public const int TwipsPerPoint = 20;
		public const int TwipsPerInch = PointsPerInch * TwipsPerPoint;

		public static XRect Convert(RectangleF area)
		{
			return new XRect(area.X / TwipsPerPoint, area.Y / TwipsPerPoint, area.Width / TwipsPerPoint, area.Height / TwipsPerPoint);
		}

		public static XPoint Convert(PointF point)
		{
			return new XPoint(point.X / TwipsPerPoint, point.Y / TwipsPerPoint);
		}
	}
}
