using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Rendering.Tools;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

namespace GrapeCity.ActiveReports.Samples.Export.Rendering.PdfSharp
{
	internal sealed partial class PdfContentGenerator : IDrawingCanvas
	{
		private readonly XGraphics _graphics;
		private readonly PdfImagesFactory _images;
		private readonly PdfFontsFactory _fonts;

		private readonly Stack<XGraphicsContainer> _states = new Stack<XGraphicsContainer>();
		private RectangleF _clipBounds = RectangleF.Empty;

		public PdfContentGenerator(XGraphics graphics, PdfImagesFactory images, PdfFontsFactory fonts)
		{
			_graphics = graphics;
			_images = images;
			_fonts = fonts;
		}

		#region IDrawingCanvas implementation

		BrushEx IDrawingCanvas.CreateSolidBrush(Color color)
		{
			return new SolidBrush(color);
		}

		BrushEx IDrawingCanvas.CreateLinearGradientBrush(PointF point1, PointF point2, Color color1, Color color2, BlendEx blend)
		{
			return new LinearGradientBrush(point1, point2, color1, color2);
		}

		BrushEx IDrawingCanvas.CreateRadialGradientBrush(PointF centerPoint, float radiusX, float radiusY, Color centerColor, Color surroundingColor)
		{
			return new RadialGradientBrush(centerPoint, Math.Max(radiusX, radiusY), centerColor, surroundingColor);
		}

		BrushEx IDrawingCanvas.CreateHatchBrush(HatchStyleEx style, Color foreColor, Color backColor)
		{
			return new SolidBrush(foreColor);
		}

		PenEx IDrawingCanvas.CreatePen(Color color, float width)
		{
			return new Pen { Color = color, Width = width };
		}

		PenEx IDrawingCanvas.CreatePen(Color color)
		{
			return new Pen { Color = color, Width = 15 /* 1px */ };
		}


		ImageEx IDrawingCanvas.CreateImage(ImageInfo image)
		{
			var cacheId = Convert.ToBase64String(HashCalculator.ComputeSimpleHash(image.Stream));
			image.Stream.Position = 0;
			return new Image(_images.GetPdfImage(cacheId, image.Stream));
		}

		ImageEx IDrawingCanvas.CreateImage(ImageInfo image, string cacheId)
		{
			return new Image(_images.GetPdfImage(cacheId, image.Stream));
		}

		void IDrawingCanvas.DrawImage(ImageEx image, float x, float y, float width, float height, float opacity)
		{
			_graphics.DrawImage((Image)image, PdfConverter.Convert(new RectangleF(x, y, width, height)));
		}

		void IDrawingCanvas.DrawLine(PenEx pen, PointF from, PointF to)
		{
			_graphics.DrawLine((Pen)pen, PdfConverter.Convert(from), PdfConverter.Convert(to));
		}

		void IDrawingCanvas.DrawString(string value, FontInfo font, BrushEx brush, RectangleF rect, StringFormatEx format)
		{
			if (string.IsNullOrEmpty(value)) return;

			XGraphicsContainer clipState = _graphics.BeginContainer();
			var xRect = PdfConverter.Convert(rect);
			var xFont = _fonts.GetPdfFont(font);
			_graphics.IntersectClip(xRect);
			var resultRect = _graphics.MeasureString(value, xFont, GetFormat(format));
			if ((format.WrapMode == WrapMode.NoWrap && !value.Contains('\n')) ||
				(resultRect.Width * PdfConverter.TwipsPerPoint <= rect.Width))
			{
				_graphics.DrawString(value, xFont, (BrushBase)brush, xRect, GetFormat(format));
				_graphics.EndContainer(clipState);
				return;
			}

			if (format.WrapMode == WrapMode.NoWrap && value.Contains('\n'))
			{
				var lines = value.Split('\n');
				var y = xRect.Y;
				for (var i = 0 ; i < lines.Length; i++)
				{
					var line = lines[i].Trim('\r');
					_graphics.DrawString(line, xFont, (BrushBase)brush, new XRect(xRect.X, y, xRect.Width, resultRect.Height), GetFormat(format));
					y += resultRect.Height;
				}
				_graphics.EndContainer(clipState);
				return;
			}

			// http://developer.th-soft.com/developer/2015/07/17/pdfsharp-improving-the-xtextformatter-class-measuring-the-height-of-the-text/
			// http://developer.th-soft.com/developer/2015/09/21/xtextformatter-revisited-xtextformatterex2-for-pdfsharp-1-50-beta-2/
			var tf = new XTextFormatter(_graphics) { Alignment = GetAlignment(format) };
			tf.DrawString(value, xFont, (BrushBase)brush, xRect);

			_graphics.EndContainer(clipState);
		}

		TextRenderingHintEx IDrawingCanvas.TextRenderingHint { get; set; }

		SmoothingModeEx IDrawingCanvas.SmoothingMode
		{
			get { return (SmoothingModeEx)Enum.Parse(typeof(SmoothingModeEx), _graphics.SmoothingMode.ToString()); }
			set { _graphics.SmoothingMode = (XSmoothingMode)Enum.Parse(typeof(XSmoothingMode), value.ToString()); }
		}

		void IDrawingCanvas.DrawRectangle(PenEx pen, RectangleF rect)
		{
			_graphics.DrawRectangle((Pen)pen, PdfConverter.Convert(rect));
		}

		void IDrawingCanvas.FillRectangle(BrushEx brush, RectangleF rect)
		{
			_graphics.DrawRectangle((BrushBase)brush, PdfConverter.Convert(rect));
		}

		RectangleF IDrawingCanvas.ClipBounds
		{
			get { return new RectangleF(_clipBounds.X, _clipBounds.Y, _clipBounds.Width, _clipBounds.Height); }
		}

		void IDrawingCanvas.IntersectClip(RectangleF rect)
		{
			if (_clipBounds.Width == 0 || _clipBounds.Height == 0)
				_clipBounds = rect;
			else
				_clipBounds = RectangleF.Intersect(_clipBounds, rect);
			_graphics.IntersectClip(PdfConverter.Convert(rect));
		}

		void IDrawingCanvas.IntersectClip(PathEx path)
		{
			if (_clipBounds.Width == 0 || _clipBounds.Height == 0)
				_clipBounds = path.GetBounds();
			else
				_clipBounds = RectangleF.Intersect(_clipBounds, path.GetBounds());
			_graphics.IntersectClip(GetPath(path));
		}

		void IDrawingCanvas.PushState()
		{
			_states.Push(_graphics.BeginContainer());
		}

		void IDrawingCanvas.PopState()
		{
			_graphics.EndContainer(_states.Pop());
		}

		void IDrawingCanvas.DrawEllipse(PenEx pen, RectangleF rect)
		{
			_graphics.DrawEllipse((Pen)pen, PdfConverter.Convert(rect));
		}

		void IDrawingCanvas.FillEllipse(BrushEx brush, RectangleF rect)
		{
			_graphics.DrawEllipse((BrushBase)brush, PdfConverter.Convert(rect));
		}

		void IDrawingCanvas.DrawPolygon(PenEx pen, PointF[] points)
		{
			_graphics.DrawPolygon((Pen)pen, points.Select(p => PdfConverter.Convert(p)).ToArray());
		}

		void IDrawingCanvas.FillPolygon(BrushEx brush, PointF[] polygon)
		{
			_graphics.DrawPolygon((BrushBase)brush, polygon.Select(p => PdfConverter.Convert(p)).ToArray(), XFillMode.Winding);
		}

		void IDrawingCanvas.DrawLines(PenEx pen, PointF[] polyLine)
		{
			_graphics.DrawLines((Pen)pen, polyLine.Select(p => PdfConverter.Convert(p)).ToArray());
		}

		Matrix3x2 IDrawingCanvas.Transform
		{
			get
			{
				var tr = _graphics.Transform;
				return new Matrix3x2((float)tr.M11, (float)tr.M12, (float)tr.M21, (float)tr.M22, (float)tr.OffsetX * PdfConverter.TwipsPerPoint, (float)tr.OffsetY * PdfConverter.TwipsPerPoint);
			}
			set
			{
				var tr = _graphics.Transform;
				if (value.IsIdentity && tr.IsIdentity)
					return;
				var transform = new Matrix3x2((float)tr.M11, (float)tr.M12, (float)tr.M21, (float)tr.M22, (float)tr.OffsetX * PdfConverter.TwipsPerPoint, (float)tr.OffsetY * PdfConverter.TwipsPerPoint);
				if (value == transform)
					return;
				if (!transform.IsIdentity)
				{
					transform = transform.Invert();
					if (!value.IsIdentity)
						transform = Matrix3x2.Multiply(transform, value);
				}
				else
				{
					transform = value;
				}
				if (transform.M31 != 0 || transform.M32 != 0)
					_graphics.TranslateTransform(transform.M31 / PdfConverter.TwipsPerPoint, transform.M32 / PdfConverter.TwipsPerPoint);
				if (transform.M11 != 1)
					_graphics.RotateTransform((float)Math.Acos(transform.M11));

				_clipBounds = RectangleF.Empty;
			}
		}

		void IDrawingCanvas.DrawAndFillPath(PenEx pen, BrushEx brush, PathEx path)
		{
			var pdfPath = GetPath(path);
			if (brush != null)
				_graphics.DrawPath((BrushBase)brush, pdfPath);
			if (pen != null)
				_graphics.DrawPath((Pen)pen, pdfPath);
		}

		#endregion

		#region Helpers

		public static XStringFormat GetFormat(StringFormatEx stringFormat)
		{
			var format = new XStringFormat();
			format.Alignment = (XStringAlignment)Enum.Parse(typeof(XStringAlignment), stringFormat.Alignment.ToString());
			format.LineAlignment = (XLineAlignment)Enum.Parse(typeof(XLineAlignment), stringFormat.LineAlignment.ToString());
			return format;
		}

		public static XGraphicsPath GetPath(PathEx pathEx)
		{
			var path = new XGraphicsPath();
			path.FillMode = (XFillMode)Enum.Parse(typeof(XFillMode), pathEx.FillMode.ToString());
			path.StartFigure();
			var start = new XPoint();
			foreach (var segment in pathEx.Segments)
			{
				switch (segment.Type)
				{
					case PathEx.SegmentType.MoveTo:
						start = PdfConverter.Convert(segment.Point1.ToPoint());
						break;
					case PathEx.SegmentType.LineTo:
						var end = PdfConverter.Convert(segment.Point1.ToPoint());
						path.AddLine(start, end);
						start = end;
						break;
					case PathEx.SegmentType.BezierTo:
						var p1 = PdfConverter.Convert(segment.Point1.ToPoint());
						var p2 = PdfConverter.Convert(segment.Point2.ToPoint());
						var bEnd = PdfConverter.Convert(segment.Point3.ToPoint());
						path.AddBezier(start, p1, p2, bEnd);
						start = bEnd;
						break;
					case PathEx.SegmentType.FigureEnd:
						if (segment.Closed)
							path.CloseFigure();
						break;
				}
			}
			return path;
		}

		private static XParagraphAlignment GetAlignment(StringFormatEx format)
		{
			if (format.IsJustified)
				return XParagraphAlignment.Justify;
			switch (format.LineAlignment)
			{
				case StringAlignmentEx.Near:
					return XParagraphAlignment.Left;
				case StringAlignmentEx.Center:
					return XParagraphAlignment.Center;
				case StringAlignmentEx.Far:
					return XParagraphAlignment.Right;
			}
			return XParagraphAlignment.Default;
		}

		#endregion
	}
}
