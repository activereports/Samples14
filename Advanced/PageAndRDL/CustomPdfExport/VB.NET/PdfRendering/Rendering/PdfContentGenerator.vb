Imports PdfSharp.Drawing
Imports GrapeCity.ActiveReports.Rendering.Tools
Imports GrapeCity.ActiveReports.Extensibility.Rendering
Imports GrapeCity.ActiveReports.Drawing
Imports System.Numerics
Imports System.Drawing
Imports PdfSharp.Drawing.Layout

Partial Friend NotInheritable Class PdfContentGenerator
	Implements IDrawingCanvas
	Private ReadOnly _graphics As XGraphics
	Private ReadOnly _images As PdfImagesFactory
	Private ReadOnly _fonts As PdfFontsFactory

	Private ReadOnly _states As New Stack(Of XGraphicsContainer)()
	Private _clipBounds As RectangleF = RectangleF.Empty

	Public Sub New(graphics As XGraphics, images As PdfImagesFactory, fonts As PdfFontsFactory)
		_graphics = graphics
		_images = images
		_fonts = fonts
	End Sub

#Region "IDrawingCanvas implementation"

	Function CreateSolidBrush(color As Color) As BrushEx Implements IDrawingCanvas.CreateSolidBrush
		Return New SolidBrush(color)
	End Function

	Function CreateLinearGradientBrush(point1 As PointF, point2 As PointF, color1 As Color, color2 As Color, Optional blend As BlendEx = Nothing) As BrushEx Implements IDrawingCanvas.CreateLinearGradientBrush
		Return New LinearGradientBrush(point1, point2, color1, color2)
	End Function

	Function CreateRadialGradientBrush(centerPoint As PointF, radiusX As Single, radiusY As Single, centerColor As Color, surroundColor As Color) As BrushEx Implements IDrawingCanvas.CreateRadialGradientBrush
		Return New SolidBrush(centerColor)
	End Function

	Function CreateHatchBrush(style As HatchStyleEx, foreColor As Color, backColor As Color) As BrushEx Implements IDrawingCanvas.CreateHatchBrush
		Return New SolidBrush(foreColor)
	End Function

	Function CreatePen(color As Color, width As Single) As PenEx Implements IDrawingCanvas.CreatePen
		Return New Pen() With {
			.Color = color,
			.Width = width
		}
	End Function

	Function CreatePen(color As Color) As PenEx Implements IDrawingCanvas.CreatePen
		' 1px 
		Return New Pen() With {
			.Color = color,
			.Width = 15
		}
	End Function

	Function CreateImage(image As ImageInfo) As ImageEx Implements IDrawingCanvas.CreateImage
		Dim cacheId = Convert.ToBase64String(HashCalculator.ComputeSimpleHash(image.Stream))
		image.Stream.Position = 0
		Return New Image(_images.GetPdfImage(cacheId, image.Stream))
	End Function

	Function CreateImage(image As ImageInfo, cacheId As String) As ImageEx Implements IDrawingCanvas.CreateImage
		Return New Image(_images.GetPdfImage(cacheId, image.Stream))
	End Function

	Sub DrawImage(image As ImageEx, x As Single, y As Single, width As Single, height As Single, Optional opacity As Single = 100) Implements IDrawingCanvas.DrawImage
		_graphics.DrawImage(CType(image, Image), PdfConverter.Convert(New RectangleF(x, y, width, height)))
	End Sub

	Sub DrawLine(pen As PenEx, from As PointF, [to] As PointF) Implements IDrawingCanvas.DrawLine
		_graphics.DrawLine(CType(pen, Pen), PdfConverter.Convert(from), PdfConverter.Convert([to]))
	End Sub

	Sub DrawString(value As String, font As FontInfo, brush As BrushEx, rect As RectangleF, format As StringFormatEx) Implements IDrawingCanvas.DrawString
		Dim clipState As XGraphicsContainer = _graphics.BeginContainer()
		Dim xRect = PdfConverter.Convert(rect)
		_graphics.IntersectClip(xRect)
		Dim xFont = _fonts.GetPdfFont(font)
		Dim resultRect = _graphics.MeasureString(value, xFont, GetFormat(format))
		If (format.WrapMode = WrapMode.NoWrap AndAlso Not value.Contains(ControlChars.Lf)) OrElse (resultRect.Width * PdfConverter.TwipsPerPoint <= rect.Width) Then
			_graphics.DrawString(value, xFont, CType(brush, BrushBase), xRect, GetFormat(format))
			_graphics.EndContainer(clipState)
			Return
		End If

		If format.WrapMode = WrapMode.NoWrap AndAlso value.Contains(ControlChars.Lf) Then
			Dim lines = value.Split(ControlChars.Lf)
			Dim y = xRect.Y
			Dim i = 0
			While i < lines.Length
				Dim line = lines(i).Trim(ControlChars.Cr)

				_graphics.DrawString(line, xFont, CType(brush, BrushBase), New XRect(xRect.X, y, xRect.Width, resultRect.Height), GetFormat(format))
				y += resultRect.Height
				i += 1
			End While
			_graphics.EndContainer(clipState)
			Return
		End If

		' http://developer.th-soft.com/developer/2015/07/17/pdfsharp-improving-the-xtextformatter-class-measuring-the-height-of-the-text/
		' http://developer.th-soft.com/developer/2015/09/21/xtextformatter-revisited-xtextformatterex2-for-pdfsharp-1-50-beta-2/
		Dim tf As New XTextFormatter(_graphics) With {
			.Alignment = GetAlignment(format)
		}
		tf.DrawString(value, xFont, CType(brush, BrushBase), xRect)

		_graphics.EndContainer(clipState)
	End Sub

	Property TextRenderingHint() As TextRenderingHintEx Implements IDrawingCanvas.TextRenderingHint

	Property SmoothingMode() As SmoothingModeEx Implements IDrawingCanvas.SmoothingMode
		Get
			Return CType([Enum].Parse(GetType(SmoothingModeEx), _graphics.SmoothingMode.ToString()), SmoothingModeEx)
		End Get
		Set
			_graphics.SmoothingMode = CType([Enum].Parse(GetType(XSmoothingMode), Value.ToString()), XSmoothingMode)
		End Set
	End Property

	Sub DrawRectangle(pen As PenEx, rect As RectangleF) Implements IDrawingCanvas.DrawRectangle
		_graphics.DrawRectangle(CType(pen, Pen), PdfConverter.Convert(rect))
	End Sub

	Sub FillRectangle(brush As BrushEx, rect As RectangleF) Implements IDrawingCanvas.FillRectangle
		_graphics.DrawRectangle(CType(brush, BrushBase), PdfConverter.Convert(rect))
	End Sub

	ReadOnly Property ClipBounds() As RectangleF Implements IDrawingCanvas.ClipBounds
		Get
			Return New RectangleF(_clipBounds.X, _clipBounds.Y, _clipBounds.Width, _clipBounds.Height)
		End Get
	End Property

	Sub IntersectClip(rect As RectangleF) Implements IDrawingCanvas.IntersectClip
		If _clipBounds.Width = 0 OrElse _clipBounds.Height = 0 Then
			_clipBounds = rect
		Else
			_clipBounds = RectangleF.Intersect(_clipBounds, rect)
		End If
		_graphics.IntersectClip(PdfConverter.Convert(rect))
	End Sub

	Sub IntersectClip(path As PathEx) Implements IDrawingCanvas.IntersectClip
		If _clipBounds.Width = 0 OrElse _clipBounds.Height = 0 Then
			_clipBounds = path.GetBounds()
		Else
			_clipBounds = RectangleF.Intersect(_clipBounds, path.GetBounds())
		End If
		_graphics.IntersectClip(GetPath(path))
	End Sub

	Sub PushState() Implements IDrawingCanvas.PushState
		_states.Push(_graphics.BeginContainer())
	End Sub

	Sub PopState() Implements IDrawingCanvas.PopState
		_graphics.EndContainer(_states.Pop())
	End Sub

	Sub DrawEllipse(pen As PenEx, rect As RectangleF) Implements IDrawingCanvas.DrawEllipse
		_graphics.DrawEllipse(CType(pen, Pen), PdfConverter.Convert(rect))
	End Sub

	Sub FillEllipse(brush As BrushEx, rect As RectangleF) Implements IDrawingCanvas.FillEllipse
		_graphics.DrawEllipse(CType(brush, BrushBase), PdfConverter.Convert(rect))
	End Sub

	Sub DrawPolygon(pen As PenEx, points As PointF()) Implements IDrawingCanvas.DrawPolygon
		_graphics.DrawPolygon(CType(pen, Pen), points.[Select](Function(p) PdfConverter.Convert(p)).ToArray())
	End Sub

	Sub FillPolygon(brush As BrushEx, polygon As PointF()) Implements IDrawingCanvas.FillPolygon
		_graphics.DrawPolygon(CType(brush, BrushBase), polygon.[Select](Function(p) PdfConverter.Convert(p)).ToArray(), XFillMode.Winding)
	End Sub

	Sub DrawLines(pen As PenEx, polyLine As PointF()) Implements IDrawingCanvas.DrawLines
		_graphics.DrawLines(CType(pen, Pen), polyLine.[Select](Function(p) PdfConverter.Convert(p)).ToArray())
	End Sub

	Property Transform() As Matrix3x2 Implements IDrawingCanvas.Transform
		Get
			Dim tr = _graphics.Transform
			Return New Matrix3x2(CType(tr.M11, Single), CType(tr.M12, Single), CType(tr.M21, Single), CType(tr.M22, Single), CType(tr.OffsetX, Single) * PdfConverter.TwipsPerPoint, CType(tr.OffsetY, Single) * PdfConverter.TwipsPerPoint)
		End Get
		Set
			Dim tr = _graphics.Transform
			If Value.IsIdentity AndAlso tr.IsIdentity Then
				Return
			End If
			Dim transform As New Matrix3x2(CType(tr.M11, Single), CType(tr.M12, Single), CType(tr.M21, Single), CType(tr.M22, Single), CType(tr.OffsetX, Single) * PdfConverter.TwipsPerPoint, CType(tr.OffsetY, Single) * PdfConverter.TwipsPerPoint)
			If Value = transform Then
				Return
			End If
			If Not transform.IsIdentity Then
				transform = transform.Invert()
				If Not Value.IsIdentity Then
					transform = Matrix3x2.Multiply(transform, Value)
				End If
			Else
				transform = Value
			End If
			If transform.M31 <> 0 OrElse transform.M32 <> 0 Then
				_graphics.TranslateTransform(transform.M31 / PdfConverter.TwipsPerPoint, transform.M32 / PdfConverter.TwipsPerPoint)
			End If
			If transform.M11 <> 1 Then
				_graphics.RotateTransform(CType(Math.Acos(transform.M11), Single))
			End If

			_clipBounds = RectangleF.Empty
		End Set
	End Property

	Sub DrawAndFillPath(pen As PenEx, brush As BrushEx, path As PathEx) Implements IDrawingCanvas.DrawAndFillPath
		Dim pdfPath = GetPath(path)
		If brush IsNot Nothing Then
			_graphics.DrawPath(CType(brush, BrushBase), pdfPath)
		End If
		If pen IsNot Nothing Then
			_graphics.DrawPath(CType(pen, Pen), pdfPath)
		End If
	End Sub

#End Region

#Region "Helpers"

	Public Shared Function GetFormat(stringFormat As StringFormatEx) As XStringFormat
		Dim format As New XStringFormat()
		format.Alignment = CType([Enum].Parse(GetType(XStringAlignment), stringFormat.Alignment.ToString()), XStringAlignment)
		format.LineAlignment = CType([Enum].Parse(GetType(XLineAlignment), stringFormat.LineAlignment.ToString()), XLineAlignment)
		Return format
	End Function

	Public Shared Function GetPath(pathEx As PathEx) As XGraphicsPath
		Dim path As New XGraphicsPath()
		path.FillMode = CType([Enum].Parse(GetType(XFillMode), pathEx.FillMode.ToString()), XFillMode)
		path.StartFigure()
		Dim start As New XPoint()
		For Each segment As PathEx.Segment In pathEx.Segments
			Select Case segment.Type
				Case PathEx.SegmentType.MoveTo
					start = PdfConverter.Convert(segment.Point1.ToPoint())
					Exit Select
				Case PathEx.SegmentType.LineTo
					Dim _end As XPoint = PdfConverter.Convert(segment.Point1.ToPoint())
					path.AddLine(start, _end)
					start = _end
					Exit Select
				Case PathEx.SegmentType.BezierTo
					Dim p1 = PdfConverter.Convert(segment.Point1.ToPoint())
					Dim p2 = PdfConverter.Convert(segment.Point2.ToPoint())
					Dim bEnd = PdfConverter.Convert(segment.Point3.ToPoint())
					path.AddBezier(start, p1, p2, bEnd)
					start = bEnd
					Exit Select
				Case PathEx.SegmentType.FigureEnd
					If segment.Closed Then
						path.CloseFigure()
					End If
					Exit Select
			End Select
		Next
		Return path
	End Function

	Private Shared Function GetAlignment(format As StringFormatEx) As XParagraphAlignment
		If format.IsJustified Then
			Return XParagraphAlignment.Justify
		End If
		Select Case format.LineAlignment
			Case StringAlignmentEx.Near
				Return XParagraphAlignment.Left
			Case StringAlignmentEx.Center
				Return XParagraphAlignment.Center
			Case StringAlignmentEx.Far
				Return XParagraphAlignment.Right
		End Select
		Return XParagraphAlignment.[Default]
	End Function

#End Region
End Class
