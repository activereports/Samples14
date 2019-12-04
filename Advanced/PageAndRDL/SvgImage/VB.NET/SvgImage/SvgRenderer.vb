Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Numerics
Imports GrapeCity.ActiveReports.Drawing
Imports GrapeCity.ActiveReports.Drawing.Gdi
Imports GrapeCity.ActiveReports.Extensibility.Rendering
Imports Svg

Public NotInheritable Class SvgRenderer
	Implements ISvgRenderer
	Implements IGraphicsProvider

	Private Const MaxImageDimension As UShort = 4096
	Private Const PatternImageDimension As UShort = 1024

	Private ReadOnly _innerGraphics As IDrawingCanvas
	Private ReadOnly _bounds As RectangleF
	Private ReadOnly _boundables As Stack(Of ISvgBoundable) = New Stack(Of ISvgBoundable)()
	Private ReadOnly _states As Stack(Of Tuple(Of Region, Matrix3x2, Matrix3x2)) = New Stack(Of Tuple(Of Region, Matrix3x2, Matrix3x2))()
	Private _measurer As Graphics
	Private _currentClip As Region
	Private _initialTransform As Matrix3x2
	Private _currentTransform As Matrix3x2

	Public Sub New(context As GraphicsRenderContext, bounds As RectangleF)
		_innerGraphics = context.Canvas
		_bounds = bounds
		_innerGraphics.PushState()
		_innerGraphics.IntersectClip(bounds)
	End Sub

	Sub Dispose() Implements IDisposable.Dispose
		_innerGraphics.PopState()

		For i As Integer = 0 To _boundables.Count * 2 - 1
			_innerGraphics.PopState()
		Next

		_boundables.Clear()
		_states.Clear()
		If _measurer IsNot Nothing Then _measurer.Dispose()
		_measurer = Nothing
		_currentClip = Nothing
	End Sub

	Function GetGraphics() As Graphics Implements IGraphicsProvider.GetGraphics
		If _measurer Is Nothing Then _measurer = SafeGraphics.CreateReferenceGraphics()
		Return _measurer
	End Function

#Region "ISvgRenderer implementation"

	Sub SetBoundable(boundable As ISvgBoundable) Implements ISvgRenderer.SetBoundable
		_states.Push(New Tuple(Of Region, Matrix3x2, Matrix3x2)(_currentClip, _currentTransform, _initialTransform))
		_boundables.Push(boundable)
		_innerGraphics.PushState()
		_initialTransform = Matrix3x2.Multiply(GetTransform(Convert(boundable.Bounds), _bounds), _innerGraphics.Transform)
		_innerGraphics.Transform = _initialTransform
		_innerGraphics.PushState()
		_currentClip = Nothing
		_currentTransform = Matrix3x2.Identity
	End Sub

	Function GetBoundable() As ISvgBoundable Implements ISvgRenderer.GetBoundable
		Return _boundables.Peek()
	End Function

	Function PopBoundable() As ISvgBoundable Implements ISvgRenderer.PopBoundable
		_innerGraphics.PopState()
		_innerGraphics.PopState()
		Dim state = _states.Pop()
		_currentClip = state.Item1
		_currentTransform = state.Item2
		_initialTransform = state.Item3
		_innerGraphics.Transform = Matrix3x2.Multiply(_currentTransform, _initialTransform)
		Return _boundables.Pop()
	End Function

	ReadOnly Property DpiY() As Single Implements ISvgRenderer.DpiY
		Get
			Return GetGraphics().DpiY
		End Get
	End Property

	Sub DrawImage(image As Image, destRect As RectangleF, srcRect As RectangleF, graphicsUnit As GraphicsUnit) Implements ISvgRenderer.DrawImage
		DrawImage(image, destRect, srcRect, graphicsUnit, 1)
	End Sub

	Sub DrawImage(image As Image, destRect As RectangleF, srcRect As RectangleF, graphicsUnit As GraphicsUnit, opacity As Single) Implements ISvgRenderer.DrawImage
		Dim stream = New MemoryStream()
		Dim srcRectInPixels = GetRectInPixels(srcRect, graphicsUnit, image.HorizontalResolution, image.VerticalResolution)
		Dim hScale = Math.Min(1, MaxImageDimension / CSng(srcRectInPixels.Width))
		Dim vScale = Math.Min(1, MaxImageDimension / CSng(srcRectInPixels.Height))
		Dim arOpacity = opacity * 100

		If (srcRectInPixels.X = 0 And srcRectInPixels.Y = 0 And srcRectInPixels.Width = image.Width And srcRectInPixels.Height = image.Height And hScale = 1 And vScale = 1) Then
			image.Save(stream, ImageFormat.Png)
		Else
			Using newImage = New Bitmap(CInt((srcRectInPixels.Width * hScale)), CInt((srcRectInPixels.Height * vScale)))
				newImage.SetResolution(image.HorizontalResolution * hScale, image.VerticalResolution * vScale)

				Using gfx = Graphics.FromImage(newImage)
					Using ia = New ImageAttributes()
						gfx.Clear(Color.Transparent)

						If opacity < 1 Then
							Dim matrix = New ColorMatrix With {
								.Matrix33 = opacity
							}
							ia.SetColorMatrix(matrix, ColorMatrixFlag.[Default], ColorAdjustType.Bitmap)
							arOpacity = 100
						End If

						ia.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY)
						gfx.SmoothingMode = SmoothingMode.HighQuality
						gfx.InterpolationMode = InterpolationMode.HighQualityBicubic
						gfx.PixelOffsetMode = PixelOffsetMode.Half
						gfx.DrawImage(image, New Point() {Point.Empty, New Point(newImage.Width, 0), New Point(0, newImage.Height)}, srcRectInPixels, GraphicsUnit.Pixel, ia)
					End Using
				End Using

				newImage.Save(stream, ImageFormat.Png)
			End Using
		End If

		stream.Position = 0
		Using imageEx = _innerGraphics.CreateImage(New ImageInfo(stream, "image/png"))
			_innerGraphics.DrawImage(imageEx, destRect.X * TwipsInPixel, destRect.Y * TwipsInPixel, destRect.Width * TwipsInPixel, destRect.Height * TwipsInPixel, arOpacity)
		End Using
	End Sub

	Sub DrawImageUnscaled(image As Image, location As Point) Implements ISvgRenderer.DrawImageUnscaled
		Dim stream = New MemoryStream()
		image.Save(stream, ImageFormat.Png)
		stream.Position = 0

		Using imageEx = _innerGraphics.CreateImage(New ImageInfo(stream, "image/png"))
			_innerGraphics.DrawImage(imageEx, location.X * TwipsInPixel, location.Y * TwipsInPixel, imageEx.Size.Width, imageEx.Size.Height)
		End Using
	End Sub

	Sub DrawPath(pen As Pen, path As GraphicsPath) Implements ISvgRenderer.DrawPath
		If (path.PointCount = 0) Then Return
		If (pen Is Nothing Or pen.Color.A = 0) Then Return

		Using penEx = _innerGraphics.CreatePen(pen.Color, pen.Width * TwipsInPixel)
			penEx.Alignment = CType((CType(pen.Alignment, Integer)), GrapeCity.ActiveReports.Drawing.PenAlignment)
			penEx.DashCap = CType((CType(pen.DashCap, Integer)), GrapeCity.ActiveReports.Drawing.DashCap)
			penEx.DashStyle = CType((CType(pen.DashStyle, Integer)), GrapeCity.ActiveReports.Drawing.PenStyleEx)
			penEx.EndCap = CType((CType(pen.EndCap, Integer)), GrapeCity.ActiveReports.Drawing.LineCap)
			penEx.LineJoin = CType((CType(pen.LineJoin, Integer)), GrapeCity.ActiveReports.Drawing.LineJoin)
			penEx.StartCap = CType((CType(pen.StartCap, Integer)), GrapeCity.ActiveReports.Drawing.LineCap)
			_innerGraphics.DrawAndFillPath(penEx, Nothing, Convert(path))
		End Using
	End Sub

	Sub FillPath(brush As Brush, path As GraphicsPath) Implements ISvgRenderer.FillPath
		If (path.PointCount = 0) Then Return
		If (path.PointCount = 0) Then Return

		If TypeOf brush Is SolidBrush Then
			Dim solidBrush = CType(brush, SolidBrush)
			If (solidBrush.Color.A = 0) Then Return
			Using brushEx = _innerGraphics.CreateSolidBrush(solidBrush.Color)
				_innerGraphics.DrawAndFillPath(Nothing, brushEx, Convert(path))
			End Using
		ElseIf TypeOf brush Is HatchBrush Then
			Dim hatchBrush = CType(brush, HatchBrush)
			Using brushEx = _innerGraphics.CreateHatchBrush(CType((CInt(hatchBrush.HatchStyle)), HatchStyleEx), hatchBrush.ForegroundColor, hatchBrush.BackgroundColor)
				_innerGraphics.DrawAndFillPath(Nothing, brushEx, Convert(path))
			End Using
		Else
			Dim bounds = path.GetBounds()
			If bounds.Width < 1 Or bounds.Height < 1 Then Return

			Dim width = Math.Min(PatternImageDimension, bounds.Width)
			Dim height = Math.Min(PatternImageDimension, bounds.Height)
			Dim scale = Math.Min(width / bounds.Width, height / bounds.Height)
			Dim stream = New MemoryStream()
			Using image = CreateMetafile(stream, New SizeF(bounds.Width * scale, bounds.Height * scale))
				Using g = Graphics.FromImage(image)
					g.Transform = New Matrix(scale, 0, 0, scale, -bounds.X * scale, -bounds.Y * scale)
					g.FillPath(brush, path)
				End Using
			End Using
			stream.Position = 0
			Dim newPath = Convert(path)
			Dim newBounds = newPath.GetBounds()
			_innerGraphics.PushState()
			_innerGraphics.IntersectClip(newPath)
			Using imageEx = _innerGraphics.CreateImage(New ImageInfo(stream, "image/emf"))
				_innerGraphics.DrawImage(imageEx, newBounds.X, newBounds.Y, newBounds.Width, newBounds.Height)
			End Using
			_innerGraphics.PopState()
		End If
	End Sub

	Function GetClip() As Region Implements ISvgRenderer.GetClip
		If _currentClip IsNot Nothing Then Return _currentClip
		If (_boundables.Count = 0) Then Return Nothing
		Dim region = New Region(_boundables.Peek().Bounds)
		region.Transform(Transform)
		Return region
	End Function

	Sub SetClip(region As Region, Optional combineMode As CombineMode = CombineMode.Replace) Implements ISvgRenderer.SetClip
		If _boundables.Count = 0 Then Return
		If Not (combineMode = CombineMode.Intersect) And Not (combineMode = CombineMode.Replace) Then Throw New NotImplementedException()
		If combineMode = CombineMode.Replace Then
			_innerGraphics.PopState()
			_innerGraphics.PushState()
		End If
		_innerGraphics.Transform = Matrix3x2.Multiply(_currentTransform, _initialTransform)
		If combineMode = CombineMode.Replace Or _currentClip Is Nothing Or region Is Nothing Then
			_currentClip = region
		ElseIf _currentClip IsNot region Then
			_currentClip = _currentClip.Clone()
			_currentClip.Intersect(region)
		End If
		If _currentClip Is Nothing Then Return
		Dim bounds As RectangleF
		For Each clipper In RegionParser.ParseRegion(_currentClip, (CType(Me, IGraphicsProvider)).GetGraphics(), bounds)
			If TypeOf clipper Is RectangleF Then
				Dim rect = CType(clipper, RectangleF)
				If Not (rect = _boundables.Peek().Bounds) Then _innerGraphics.IntersectClip(Convert(rect))
			ElseIf TypeOf clipper Is GraphicsPath Then
				Dim path = CType(clipper, GraphicsPath)
				_innerGraphics.IntersectClip(Convert(path))
			End If
		Next
	End Sub

	Sub RotateTransform(fAngle As Single, Optional order As MatrixOrder = MatrixOrder.Append) Implements ISvgRenderer.RotateTransform
		If fAngle = 0 Then Return
		Dim rotation = Matrix3x2.CreateRotation(CSng((fAngle * Math.PI / 180)))
		_currentTransform = If(order = MatrixOrder.Append, Matrix3x2.Multiply(_currentTransform, rotation), Matrix3x2.Multiply(rotation, _currentTransform))
		_innerGraphics.Transform = Matrix3x2.Multiply(_currentTransform, _initialTransform)
	End Sub

	Sub ScaleTransform(sx As Single, sy As Single, Optional order As MatrixOrder = MatrixOrder.Append) Implements ISvgRenderer.ScaleTransform
		If sx = 1 And sy = 1 Then Return
		Dim scale = Matrix3x2.CreateScale(sx, sy)
		_currentTransform = If(order = MatrixOrder.Append, Matrix3x2.Multiply(_currentTransform, scale), Matrix3x2.Multiply(scale, _currentTransform))
		_innerGraphics.Transform = Matrix3x2.Multiply(_currentTransform, _initialTransform)
	End Sub

	Sub TranslateTransform(dx As Single, dy As Single, Optional order As MatrixOrder = MatrixOrder.Append) Implements ISvgRenderer.TranslateTransform
		If dx = 0 And dy = 0 Then Return
		Dim translation = Matrix3x2.CreateTranslation(dx * TwipsInPixel, dy * TwipsInPixel)
		_currentTransform = If(order = MatrixOrder.Append, Matrix3x2.Multiply(_currentTransform, translation), Matrix3x2.Multiply(translation, _currentTransform))
		_innerGraphics.Transform = Matrix3x2.Multiply(_currentTransform, _initialTransform)
	End Sub

	Property Transform() As Matrix Implements ISvgRenderer.Transform
		Get
			Dim tr = _currentTransform
			Return New Matrix(tr.M11, tr.M12, tr.M21, tr.M22, tr.M31 / TwipsInPixel, tr.M32 / TwipsInPixel)
		End Get
		Set
			Dim elems = Value.Elements
			Dim tr = New Matrix3x2(elems(0), elems(1), elems(2), elems(3), elems(4) * TwipsInPixel, elems(5) * TwipsInPixel)
			If (tr = _currentTransform) Then Return
			_currentTransform = tr
			_innerGraphics.Transform = Matrix3x2.Multiply(_currentTransform, _initialTransform)
		End Set
	End Property

	Property SmoothingMode() As SmoothingMode Implements ISvgRenderer.SmoothingMode
		Get
			Return CType([Enum].Parse(GetType(SmoothingMode), _innerGraphics.SmoothingMode.ToString()), SmoothingMode)
		End Get
		Set
			_innerGraphics.SmoothingMode = CType([Enum].Parse(GetType(SmoothingModeEx), Value.ToString()), SmoothingModeEx)
		End Set
	End Property

#End Region

#Region "Helpers"

	Private Const TwipsPerInch As Integer = 1440

	Private ReadOnly Property TwipsInPixel As Single
		Get
			Return TwipsPerInch / (CType(Me, ISvgRenderer)).DpiY
		End Get
	End Property

	Private Shared Function GetTransform(srcRect As RectangleF, dstRect As RectangleF) As Matrix3x2
		Dim tr = Matrix3x2.CreateTranslation(dstRect.X, dstRect.Y)
		tr = tr.Scale(dstRect.Width / srcRect.Width, dstRect.Height / srcRect.Height)
		tr = tr.Translate(-srcRect.X, srcRect.Y)
		Return tr
	End Function

	Private Function Convert(p As PointF) As PointF
		Return New PointF(p.X * TwipsInPixel, p.Y * TwipsInPixel)
	End Function

	Private Function Convert(rect As RectangleF) As RectangleF
		Return New RectangleF(rect.X * TwipsInPixel, rect.Y * TwipsInPixel, rect.Width * TwipsInPixel, rect.Height * TwipsInPixel)
	End Function

	Private Function Convert(gpath As GraphicsPath) As PathEx
		Dim path = New PathEx()
		Dim pointTypes = gpath.PathTypes
		Dim pathPoints = gpath.PathPoints
		Dim pointCount As Integer = gpath.PointCount
		Dim iPoint As Integer = 0
		Dim startPoint = PointF.Empty

		While iPoint < pointCount
			Dim pointType = CType((pointTypes(iPoint) And CByte(PathPointType.PathTypeMask)), PathPointType)

			Select Case pointType
				Case PathPointType.Bezier
					path.AddBeziers(New PointF() {Convert(startPoint), Convert(pathPoints(iPoint)), Convert(pathPoints(iPoint + 1)), Convert(pathPoints(iPoint + 2))})
					iPoint += 2
					startPoint = pathPoints(iPoint)
					Exit Select
				Case PathPointType.Line
					path.AddLine(Convert(startPoint), Convert(pathPoints(iPoint)))
					startPoint = pathPoints(iPoint)
					Exit Select
				Case PathPointType.Start
					startPoint = pathPoints(iPoint)
					If iPoint > 0 Then path.CloseFigure()
					Exit Select
			End Select

			iPoint += 1
			Dim closePath As Boolean = 0 <> (pointTypes(iPoint - 1) And CType(PathPointType.CloseSubpath, Byte))
			If closePath Then path.CloseFigure()
		End While

		Return path
	End Function

	Private Shared Function GetRectInPixels(srcRect As RectangleF, srcUnit As GraphicsUnit, dpiX As Single, dpiY As Single) As Rectangle
		Dim multiplierX As Single = 1
		Dim multiplierY As Single = 1

		Select Case srcUnit
			Case GraphicsUnit.World
				Throw New NotImplementedException()
			Case GraphicsUnit.Display
				multiplierX = 19.2F * dpiX / TwipsPerInch
				multiplierY = 19.2F * dpiY / TwipsPerInch
			Case GraphicsUnit.Document
				multiplierX = 4.8F * dpiX / TwipsPerInch
				multiplierY = 4.8F * dpiY / TwipsPerInch
			Case GraphicsUnit.Inch
				multiplierX = dpiX
				multiplierY = dpiY
			Case GraphicsUnit.Millimeter
				multiplierX = 56.6929131F * dpiX / TwipsPerInch
				multiplierY = 56.6929131F * dpiY / TwipsPerInch
			Case GraphicsUnit.Point
				multiplierX = 20 * dpiX / TwipsPerInch
				multiplierY = 20 * dpiY / TwipsPerInch
		End Select

		Return New Rectangle(CInt((srcRect.X * multiplierX)), CInt((srcRect.Y * multiplierX)), CInt((srcRect.Width * multiplierX)), CInt((srcRect.Height * multiplierY)))
	End Function

	Private Shared Function CreateMetafile(ms As MemoryStream, size As SizeF) As Image
		Using gTemp As Graphics = SafeGraphics.CreateReferenceGraphics()
			Dim mf = New Metafile(ms, gTemp.GetHdc(), New RectangleF(Point.Empty, size), MetafileFrameUnit.Pixel, EmfType.EmfPlusDual)
			gTemp.ReleaseHdc()
			Return mf
		End Using
	End Function

	Private Class RegionParser
		Private Const REG_RECT As Integer = &H10000000
		Private Const REG_PATH As Integer = &H10000001
		Private Const REG_EMPTY As Integer = &H10000002
		Private Const REG_INF As Integer = &H10000003
		Private Const OP_INTERSECT As Integer = &H1
		Private Const OP_UNION As Integer = &H2
		Private Const OP_XOR As Integer = &H3
		Private Const OP_EXCLUDE As Integer = &H4
		Private Const OP_COMPLEMENT As Integer = &H5
		Private Const FMT_SHORT As Integer = &H4000
		Private Const HEADER_SIZE As Integer = &H10

		Shared Function ParseRegion(region As Region, g As Graphics, ByRef bounds As RectangleF) As IEnumerable(Of Object)
			Dim result = New List(Of Object)()
			Dim data = region.GetRegionData().Data
			Dim size As Integer = 8 + GetInt32(data, 0)
			Dim readPointer As Integer = HEADER_SIZE

			While readPointer < size
				Dim T As Token = NextToken(data, readPointer)
				Dim tokenType As Integer = GetInt32(T.RawData, 0)

				Select Case tokenType
					Case REG_RECT
						result.Add(GetRectangle(T.RawData))
					Case REG_PATH
						result.Add(GetPath(T.RawData))
				End Select
			End While

			bounds = region.GetBounds(g)
			Return result
		End Function

		Private Shared Function GetRectangle(data As Byte()) As RectangleF
			Dim index As Integer = 4
			Dim X As Single = GetSingle(data, index)
			index += 4
			Dim Y As Single = GetSingle(data, index)
			index += 4
			Dim W As Single = GetSingle(data, index)
			index += 4
			Dim H As Single = GetSingle(data, index)
			Return New RectangleF(X, Y, W, H)
		End Function

		Private Shared Function NextToken(regdat As Byte(), ByRef index As Integer) As Token
			Dim tok As Token = New Token()
			Dim size As Integer = 4
			Dim lookahead As Integer = GetInt32(regdat, index)

			Select Case lookahead
				Case OP_INTERSECT, OP_UNION, OP_XOR, OP_EXCLUDE, OP_COMPLEMENT
					tok.Type = Token.OP
					Exit Select
				Case REG_RECT
					tok.Type = Token.DATA
					size = 20
					Exit Select
				Case REG_PATH
					tok.Type = Token.DATA
					size = 8 + GetInt32(regdat, index + 4)
					Exit Select
				Case REG_EMPTY, REG_INF
					tok.Type = Token.DATA
					Exit Select
				Case Else
					Throw New ArgumentException()
			End Select

			tok.RawData = New Byte(size - 1) {}
			Array.Copy(regdat, index, tok.RawData, 0, size)
			index += size
			Return tok
		End Function

		Private Structure Token
			Public Const OP As Integer = 1
			Public Const DATA As Integer = 2
			Public Type As Integer
			Public RawData As Byte()
		End Structure

		Private Shared Function GetPath(raw As Byte()) As GraphicsPath
			Dim nrPoints As Integer = GetInt32(raw, 12)
			Dim points As PointF() = New PointF(nrPoints - 1) {}
			Dim types As Byte() = New Byte(nrPoints - 1) {}
			Dim format As Integer = GetInt32(raw, 16)
			Dim index As Integer = 16

			If format >= FMT_SHORT Then
				Dim X, Y As Short
				index += 2

				For n As Integer = 0 To nrPoints - 1
					index += 2
					X = GetInt16(raw, index)
					index += 2
					Y = GetInt16(raw, index)
					points(n) = New PointF(X, Y)
				Next

				index += 1
			Else
				Dim X, Y As Single

				For n As Integer = 0 To nrPoints - 1
					index += 4
					X = GetSingle(raw, index)
					index += 4
					Y = GetSingle(raw, index)
					points(n) = New PointF(X, Y)
				Next

				index += 3
			End If

			For n As Integer = 0 To nrPoints - 1
				index += 1
				types(n) = GetByte(raw, index)
			Next

			Return New GraphicsPath(points, types)
		End Function

		Private Shared Function GetInt16(bts As Byte(), index As Integer) As Short
			If BitConverter.IsLittleEndian Then
				Return CType((CType(bts(index), Integer) Or (CType(bts((index + 1)), Integer) << 8)), Short)
			End If
			Return CType((CType(bts((index + 1)), Integer) Or (CType(bts(index), Integer) << 8)), Short)
		End Function

		Private Shared Function GetByte(bts As Byte(), index As Integer) As Byte
			Return bts(index)
		End Function

		Private Shared Function GetInt32(bts As Byte(), index As Integer) As Integer
			If BitConverter.IsLittleEndian Then
				Return (CType(bts(index), Integer) Or ((CType(bts((index + 1)), Integer) << 8) Or ((CType(bts((index + 2)), Integer) << 16) Or (CType(bts((index + 3)), Integer) << 24))))
			End If
			Return (CType(bts((index + 3)), Integer) Or ((CType(bts((index + 2)), Integer) << 8) Or ((CType(bts((index + 1)), Integer) << 16) Or (CType(bts(index), Integer) << 24))))
		End Function

		Private Shared Function GetSingle(bts As Byte(), index As Integer) As Single
			Return BitConverter.ToSingle(bts, index)
		End Function
	End Class

#End Region

End Class
