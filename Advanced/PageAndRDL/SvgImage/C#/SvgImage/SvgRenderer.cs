using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Numerics;
using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.Drawing.Gdi;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using Svg;


namespace GrapeCity.ActiveReports.Samples.Svg
{
	/// <summary>
	/// Convenience wrapper around a graphics object
	/// </summary>
	public sealed class SvgRenderer : ISvgRenderer, IGraphicsProvider
	{
		private const ushort MaxImageDimension = 4096;
		private const ushort PatternImageDimension = 1024;

		private readonly IDrawingCanvas _innerGraphics;
		private readonly RectangleF _bounds;
		private readonly Stack<ISvgBoundable> _boundables = new Stack<ISvgBoundable>();
		private readonly Stack<Tuple<Region, Matrix3x2, Matrix3x2>> _states = new Stack<Tuple<Region, Matrix3x2, Matrix3x2>>();
		private Graphics _measurer;
		private Region _currentClip;
		private Matrix3x2 _initialTransform;
		private Matrix3x2 _currentTransform;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ISvgRenderer"/> class.
		/// </summary>
		public SvgRenderer(GraphicsRenderContext context, RectangleF bounds)
		{
			_innerGraphics = context.Canvas;
			_bounds = bounds;
			_innerGraphics.PushState();
			_innerGraphics.IntersectClip(bounds);
		}
		
		void IDisposable.Dispose()
		{
			_innerGraphics.PopState();
			for (int i = _boundables.Count * 2; i > 0; i--)
				_innerGraphics.PopState();
			_boundables.Clear();
			_states.Clear();
			if (_measurer != null)
				_measurer.Dispose();
			_measurer = null;
			_currentClip = null;
		}
		
		Graphics IGraphicsProvider.GetGraphics()
		{
			if (_measurer == null)
				_measurer = SafeGraphics.CreateReferenceGraphics();
			return _measurer;
		}

		#region ISvgRenderer implementation

		void ISvgRenderer.SetBoundable(ISvgBoundable boundable)
		{
			_states.Push(new Tuple<Region, Matrix3x2, Matrix3x2>(_currentClip, _currentTransform, _initialTransform));
			_boundables.Push(boundable);

			// for transformation
			_innerGraphics.PushState();
			_initialTransform = _innerGraphics.Transform = Matrix3x2.Multiply(GetTransform(Convert(boundable.Bounds), _bounds), _innerGraphics.Transform);

			// for intersection
			_innerGraphics.PushState();
			
			_currentClip = null;
			_currentTransform = Matrix3x2.Identity;
		}

		ISvgBoundable ISvgRenderer.GetBoundable()
		{
			return _boundables.Peek();
		}

		ISvgBoundable ISvgRenderer.PopBoundable()
		{
			_innerGraphics.PopState(); // intersection
			_innerGraphics.PopState(); // transformation

			var state = _states.Pop();
			_currentClip = state.Item1;
			_currentTransform = state.Item2;
			_initialTransform = state.Item3;
			_innerGraphics.Transform = Matrix3x2.Multiply(_currentTransform, _initialTransform);
			
			return _boundables.Pop();
		}

		float ISvgRenderer.DpiY
		{
			get { return ((IGraphicsProvider)this).GetGraphics().DpiY; }
		}

		void ISvgRenderer.DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit graphicsUnit)
		{
			((ISvgRenderer)this).DrawImage(image, destRect, srcRect, graphicsUnit, 1);
		}
		
		void ISvgRenderer.DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit graphicsUnit, float opacity)
		{
			var stream = new MemoryStream();
			var srcRectInPixels = GetRectInPixels(srcRect, graphicsUnit, image.HorizontalResolution, image.VerticalResolution);
			var hScale = Math.Min(1, MaxImageDimension / (float)srcRectInPixels.Width);
			var vScale = Math.Min(1, MaxImageDimension / (float)srcRectInPixels.Height);
			var arOpacity = opacity * 100;
			if (srcRectInPixels.X == 0 && srcRectInPixels.Y == 0 &&
			    srcRectInPixels.Width == image.Width && srcRectInPixels.Height == image.Height &&
			    hScale == 1 && vScale == 1)
			{
				image.Save(stream, ImageFormat.Png);
			}
			else
			{
				using (var newImage = new Bitmap((int) (srcRectInPixels.Width * hScale), (int) (srcRectInPixels.Height * vScale)))
				{
					newImage.SetResolution(image.HorizontalResolution * hScale, image.VerticalResolution * vScale);
					using (var gfx = Graphics.FromImage(newImage))
					using (var ia = new ImageAttributes())
					{
						gfx.Clear(Color.Transparent);

						if (opacity < 1)
						{
							var matrix = new ColorMatrix {Matrix33 = opacity};
							ia.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
							arOpacity = 100;
						}

						// https://photosauce.net/blog/post/image-scaling-with-gdi-part-3-drawimage-and-the-settings-that-affect-it
						ia.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
						gfx.SmoothingMode = SmoothingMode.HighQuality;
						gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
						gfx.PixelOffsetMode = PixelOffsetMode.Half;

						gfx.DrawImage(image,
							new Point[] {Point.Empty, new Point(newImage.Width, 0), new Point(0, newImage.Height)},
							srcRectInPixels,
							GraphicsUnit.Pixel,
							ia);
					}

					newImage.Save(stream, ImageFormat.Png);
				}
			}

			stream.Position = 0;
			using (var imageEx = _innerGraphics.CreateImage(new ImageInfo(stream, "image/png")))
				_innerGraphics.DrawImage(imageEx, destRect.X * TwipsInPixel, destRect.Y * TwipsInPixel, destRect.Width * TwipsInPixel, destRect.Height * TwipsInPixel, arOpacity);
		}

		void ISvgRenderer.DrawImageUnscaled(Image image, Point location)
		{
			var stream = new MemoryStream();
			image.Save(stream, ImageFormat.Png);
			stream.Position = 0;
			using (var imageEx = _innerGraphics.CreateImage(new ImageInfo(stream, "image/png")))
				_innerGraphics.DrawImage(imageEx, location.X * TwipsInPixel, location.Y * TwipsInPixel, imageEx.Size.Width, imageEx.Size.Height);
		}

		void ISvgRenderer.DrawPath(Pen pen, GraphicsPath path)
		{
			if (path.PointCount == 0)
				return;
			if (pen == null || pen.Color.A == 0)
				return;
			
			using (var penEx = _innerGraphics.CreatePen(pen.Color, pen.Width * TwipsInPixel))
			{
				penEx.Alignment = (GrapeCity.ActiveReports.Drawing.PenAlignment)((int)pen.Alignment);
				penEx.DashCap = (GrapeCity.ActiveReports.Drawing.DashCap)((int)pen.DashCap);
				penEx.DashStyle = (GrapeCity.ActiveReports.Drawing.PenStyleEx)((int)pen.DashStyle);
				penEx.EndCap = (GrapeCity.ActiveReports.Drawing.LineCap)((int)pen.EndCap);
				penEx.LineJoin = (GrapeCity.ActiveReports.Drawing.LineJoin)((int)pen.LineJoin);
				penEx.StartCap = (GrapeCity.ActiveReports.Drawing.LineCap)((int)pen.StartCap);

				_innerGraphics.DrawAndFillPath(penEx, null, Convert(path));
			}
		}

		void ISvgRenderer.FillPath(Brush brush, GraphicsPath path)
		{
			if (path.PointCount == 0)
				return;

			if (brush is SolidBrush)
			{
				var solidBrush = (SolidBrush)brush;
				if (solidBrush.Color.A == 0)
					return;
				using (var brushEx = _innerGraphics.CreateSolidBrush(solidBrush.Color))
					_innerGraphics.DrawAndFillPath(null, brushEx, Convert(path));
			}
			else if (brush is HatchBrush)
			{
				var hatchBrush = (HatchBrush)brush;
				using (var brushEx = _innerGraphics.CreateHatchBrush((HatchStyleEx)((int)hatchBrush.HatchStyle), hatchBrush.ForegroundColor, hatchBrush.BackgroundColor))
					_innerGraphics.DrawAndFillPath(null, brushEx, Convert(path));
			}
			else
			{
				var bounds = path.GetBounds();
				if (bounds.Width < 1 || bounds.Height < 1)
					return;

				var width = Math.Min(PatternImageDimension, bounds.Width);
				var height = Math.Min(PatternImageDimension, bounds.Height);
				var scale = Math.Min(width / bounds.Width, height / bounds.Height);
				
				var stream = new MemoryStream();
				using (var image = CreateMetafile(stream, new SizeF(bounds.Width * scale, bounds.Height * scale)))
				using (var g = Graphics.FromImage(image))
				{
					g.Transform = new Matrix(scale, 0, 0, scale, -bounds.X * scale, -bounds.Y * scale);
					g.FillPath(brush, path);
				}
				
				stream.Position = 0;
				var newPath = Convert(path);
				var newBounds = newPath.GetBounds();

				_innerGraphics.PushState();
				_innerGraphics.IntersectClip(newPath);
				using (var imageEx = _innerGraphics.CreateImage(new ImageInfo(stream, "image/emf")))
					_innerGraphics.DrawImage(imageEx, newBounds.X, newBounds.Y, newBounds.Width, newBounds.Height);
				_innerGraphics.PopState();
			}
		}

		Region ISvgRenderer.GetClip()
		{
			if (_currentClip != null)
				return _currentClip;
			if (_boundables.Count == 0)
				return null;
			var region = new Region(_boundables.Peek().Bounds);
			region.Transform(((ISvgRenderer)this).Transform);
			return region;
		}

		void ISvgRenderer.SetClip(Region region, CombineMode combineMode)
		{
			if (_boundables.Count == 0)
				return;
			
			if (combineMode != CombineMode.Intersect && combineMode != CombineMode.Replace)
				throw new NotImplementedException();

			// reset current clip
			if (combineMode == CombineMode.Replace || region == null)
			{
				_innerGraphics.PopState();
				_innerGraphics.PushState();
			}
			_innerGraphics.Transform = Matrix3x2.Multiply(_currentTransform, _initialTransform);

			if (combineMode == CombineMode.Replace || _currentClip == null || region == null)
			{
				_currentClip = region;
			}
			else if (_currentClip != region)
			{
				_currentClip = _currentClip.Clone();
				_currentClip.Intersect(region);
			}

			// set default clip
			if (_currentClip == null)
				return;
			
			RectangleF bounds;
			foreach (var clipper in RegionParser.ParseRegion(_currentClip, ((IGraphicsProvider) this).GetGraphics(), out bounds))
				if (clipper is RectangleF)
				{
					var rect = (RectangleF) clipper;
					if (rect != _boundables.Peek().Bounds)
						_innerGraphics.IntersectClip(Convert(rect));
				}
				else if (clipper is GraphicsPath)
				{
					var path = (GraphicsPath) clipper;
					_innerGraphics.IntersectClip(Convert(path));
				}
		}

		void ISvgRenderer.RotateTransform(float fAngle, MatrixOrder order)
		{
			if (fAngle == 0)
				return;
			
			var rotation = Matrix3x2.CreateRotation((float)(fAngle * Math.PI / 180));
			_currentTransform = order == MatrixOrder.Append ? Matrix3x2.Multiply(_currentTransform, rotation) : Matrix3x2.Multiply(rotation, _currentTransform);
			_innerGraphics.Transform = Matrix3x2.Multiply(_currentTransform, _initialTransform);
		}

		void ISvgRenderer.ScaleTransform(float sx, float sy, MatrixOrder order)
		{
			if (sx == 1 && sy == 1)
				return;
			
			var scale = Matrix3x2.CreateScale(sx, sy);
			_currentTransform = order == MatrixOrder.Append ? Matrix3x2.Multiply(_currentTransform, scale) : Matrix3x2.Multiply(scale, _currentTransform);
			_innerGraphics.Transform = Matrix3x2.Multiply(_currentTransform, _initialTransform);
		}

		void ISvgRenderer.TranslateTransform(float dx, float dy, MatrixOrder order)
		{
			if (dx == 0 && dy == 0)
				return;
			
			var translation = Matrix3x2.CreateTranslation(dx * TwipsInPixel, dy * TwipsInPixel);
			_currentTransform = order == MatrixOrder.Append ? Matrix3x2.Multiply(_currentTransform, translation) : Matrix3x2.Multiply(translation, _currentTransform);
			_innerGraphics.Transform = Matrix3x2.Multiply(_currentTransform, _initialTransform);
		}

		Matrix ISvgRenderer.Transform
		{
			get
			{
				var tr = _currentTransform;
				return new Matrix(tr.M11, tr.M12, tr.M21, tr.M22, tr.M31 / TwipsInPixel, tr.M32 / TwipsInPixel);
			}
			set
			{
				var elems = value.Elements;
				var tr = new Matrix3x2(elems[0], elems[1], elems[2], elems[3], elems[4] * TwipsInPixel, elems[5] * TwipsInPixel);
				if (tr == _currentTransform)
					return;
				_currentTransform = tr;
				_innerGraphics.Transform = Matrix3x2.Multiply(_currentTransform, _initialTransform);
			}
		}

		SmoothingMode ISvgRenderer.SmoothingMode
		{
			get { return (SmoothingMode)Enum.Parse(typeof(SmoothingMode), _innerGraphics.SmoothingMode.ToString()); }
			set { _innerGraphics.SmoothingMode = (SmoothingModeEx)Enum.Parse(typeof(SmoothingModeEx), value.ToString()); }
		}

		#endregion

		#region Helpers

		private const int TwipsPerInch = 1440;

		private float TwipsInPixel
		{
			get { return TwipsPerInch / ((ISvgRenderer)this).DpiY; }
		}
		
		private static Matrix3x2 GetTransform(RectangleF srcRect, RectangleF dstRect)
		{
			var tr = Matrix3x2.CreateTranslation(dstRect.X, dstRect.Y);
			tr = tr.Scale(dstRect.Width / srcRect.Width, dstRect.Height / srcRect.Height);    
			tr = tr.Translate(-srcRect.X, srcRect.Y);
			return tr;
		}
		
		private PointF Convert(PointF p)
		{
			return new PointF(p.X * TwipsInPixel, p.Y * TwipsInPixel);
		}

		private RectangleF Convert(RectangleF rect)
		{
			return new RectangleF(rect.X * TwipsInPixel, rect.Y * TwipsInPixel, rect.Width * TwipsInPixel, rect.Height * TwipsInPixel);
		}

		private PathEx Convert(GraphicsPath gpath)
		{
			var path = new PathEx();
			var pointTypes = gpath.PathTypes;
			var pathPoints = gpath.PathPoints;
			int pointCount = gpath.PointCount;
			int iPoint = 0;
			var startPoint = PointF.Empty;
			while (iPoint < pointCount)
			{
				var pointType = (PathPointType)(pointTypes[iPoint] & (byte)PathPointType.PathTypeMask);

				switch (pointType)
				{
					case PathPointType.Bezier:
						path.AddBeziers(new PointF[] { Convert(startPoint), Convert(pathPoints[iPoint]), Convert(pathPoints[iPoint + 1]), Convert(pathPoints[iPoint + 2]) });
						iPoint += 2;
						startPoint = pathPoints[iPoint];
						break;
					case PathPointType.Line:
						path.AddLine(Convert(startPoint), Convert(pathPoints[iPoint]));
						startPoint = pathPoints[iPoint];
						break;
					case PathPointType.Start:
						startPoint = pathPoints[iPoint];
						if (iPoint > 0)
							path.CloseFigure();
						break;
				}
				iPoint++;

				bool closePath = 0 != (pointTypes[iPoint - 1] & (byte)PathPointType.CloseSubpath);
				if (closePath)
					path.CloseFigure();
			}
			return path;
		}
		
		private static Rectangle GetRectInPixels(RectangleF srcRect, GraphicsUnit srcUnit, float dpiX, float dpiY)
		{
			float multiplierX = 1;
			float multiplierY = 1;
			switch (srcUnit)
			{
				case GraphicsUnit.World:
					throw new NotImplementedException();
				case GraphicsUnit.Display:
					multiplierX = 19.2f * dpiX / TwipsPerInch;
					multiplierY = 19.2f * dpiY / TwipsPerInch;
					break;
				case GraphicsUnit.Document:
					multiplierX = 4.8f * dpiX / TwipsPerInch;
					multiplierY = 4.8f * dpiY / TwipsPerInch;
					break;
				case GraphicsUnit.Inch:
					multiplierX = dpiX;
					multiplierY = dpiY;
					break;
				case GraphicsUnit.Millimeter:
					multiplierX = 56.692913385826771653543307086614f * dpiX / TwipsPerInch;
					multiplierY = 56.692913385826771653543307086614f * dpiY / TwipsPerInch;
					break;
				case GraphicsUnit.Point:
					multiplierX = 20 * dpiX / TwipsPerInch;
					multiplierY = 20 * dpiY / TwipsPerInch;
					break;
			}
			return new Rectangle(
				(int)(srcRect.X * multiplierX),
				(int)(srcRect.Y * multiplierX),
				(int)(srcRect.Width * multiplierX),
				(int)(srcRect.Height * multiplierY));
		}
		
		private static Image CreateMetafile(MemoryStream ms, SizeF size)
		{
			using (Graphics gTemp = SafeGraphics.CreateReferenceGraphics())
			{
				var mf = new Metafile(ms, gTemp.GetHdc(), new RectangleF(Point.Empty, size), MetafileFrameUnit.Pixel, EmfType.EmfPlusDual);
				gTemp.ReleaseHdc();
				return mf;
			}
		}

		private static class RegionParser
		{
			private const int REG_RECT = 0x10000000;
			private const int REG_PATH = 0x10000001;
			private const int REG_EMPTY = 0x10000002;
			private const int REG_INF = 0x10000003;
	
			private const int OP_INTERSECT = 0x00000001;
			private const int OP_UNION = 0x00000002;
			private const int OP_XOR = 0x00000003;
			private const int OP_EXCLUDE = 0x00000004;
			private const int OP_COMPLEMENT = 0x00000005;
	
			private const int FMT_SHORT = 0x00004000;
	
			private const int HEADER_SIZE = 0x00000010;
	
			public static IEnumerable<object> ParseRegion(Region region, Graphics g, out RectangleF bounds)
			{
				var result = new List<object>();
				var data = region.GetRegionData().Data;
				int size = 8 + GetInt32(data, 0);
				int readPointer = HEADER_SIZE;
				while (readPointer < size)
				{
					Token T = NextToken(data, ref readPointer);
					int tokenType = GetInt32(T.RawData, 0);
					switch (tokenType)
					{
						case REG_RECT:
							result.Add(GetRectangle(T.RawData));
							break;
						case REG_PATH:
							result.Add(GetPath(T.RawData));
							break;
					}
				}
				bounds = region.GetBounds(g);
				return result;
			}
			
			private static RectangleF GetRectangle(byte[] data)
			{
				int index = 0;
				float X = GetSingle(data, index += 4);
				float Y = GetSingle(data, index += 4);
				float W = GetSingle(data, index += 4);
				float H = GetSingle(data, index += 4);
				return new RectangleF(X, Y, W, H);
			}
	
			private static Token NextToken(byte[] regdat, ref int index)
			{
				Token tok = new Token();
				int size = 4;
				int lookahead = GetInt32(regdat, index);
	
				switch (lookahead)
				{
					case OP_INTERSECT:
					case OP_UNION:
					case OP_XOR:
					case OP_EXCLUDE:
					case OP_COMPLEMENT:
						tok.Type = Token.OP;
						break;
					case REG_RECT:
						tok.Type = Token.DATA;
						size = 20;
						break;
					case REG_PATH:
						tok.Type = Token.DATA;
						size = 8 + GetInt32(regdat, index + 4);
						break;
					case REG_EMPTY:
					case REG_INF:
						tok.Type = Token.DATA;
						break;
					default:
						throw new ArgumentException();
				}
	
				tok.RawData = new byte[size];
				Array.Copy(regdat, index, tok.RawData, 0, size);
				index += size;
				return tok;
			}
	
			private struct Token
			{
				public const int OP = 1;
				public const int DATA = 2;
				public int Type;
				public byte[] RawData;
			}
	
			// Returns a graphics path from a given raw byte representation
			private static GraphicsPath GetPath(byte[] raw)
			{
				int nrPoints = GetInt32(raw, 12);
				PointF[] points = new PointF[nrPoints];
				byte[] types = new byte[nrPoints];
				int format = GetInt32(raw, 16);
				int index = 16;
				if (format >= FMT_SHORT)
				{
					short X, Y;
					index += 2;
					for (int n = 0; n < nrPoints; n++)
					{
						X = GetInt16(raw, index += 2);
						Y = GetInt16(raw, index += 2);
						points[n] = new PointF(X, Y);
					}
					index += 1;
				}
				else
				{
					float X, Y;
					for (int n = 0; n < nrPoints; n++)
					{
						X = GetSingle(raw, index += 4);
						Y = GetSingle(raw, index += 4);
						points[n] = new PointF(X, Y);
					}
					index += 3;
				}
				for (int n = 0; n < nrPoints; n++)
					types[n] = GetByte(raw, index += 1);
				return new GraphicsPath(points, types);
			}
	
			private static short GetInt16(byte[] bts, int index)
			{
				if (BitConverter.IsLittleEndian)
					return (short)(((int)(bts[index])) | (((int)(bts[index + 1])) << 8));
				return (short)(((int)(bts[index + 1])) | (((int)(bts[index])) << 8));
			}
	
			private static byte GetByte(byte[] bts, int index)
			{
				return bts[index];
			}
	
			private static int GetInt32(byte[] bts, int index)
			{
				if (BitConverter.IsLittleEndian)
					return (((int)(bts[index])) | (((int)(bts[index + 1])) << 8) | (((int)(bts[index + 2])) << 16) | (((int)(bts[index + 3])) << 24));
				return (((int)(bts[index + 3])) | (((int)(bts[index + 2])) << 8) | (((int)(bts[index + 1])) << 16) | (((int)(bts[index])) << 24));
			}
	
			private static float GetSingle(byte[] bts, int index)
			{
				return BitConverter.ToSingle(bts, index);
			}
		}

		#endregion
	}
}
