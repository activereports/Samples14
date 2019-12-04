Imports PdfSharp.Drawing
Imports System.Drawing

Friend Class PdfConverter
	Public Const PointsPerInch As Integer = 72
	Public Const TwipsPerPoint As Integer = 20
	Public Const TwipsPerInch As Integer = PointsPerInch * TwipsPerPoint

	Public Shared Function Convert(area As RectangleF) As XRect
		Return New XRect(area.X / TwipsPerPoint, area.Y / TwipsPerPoint, area.Width / TwipsPerPoint, area.Height / TwipsPerPoint)
	End Function

	Public Shared Function Convert(point As PointF) As XPoint
		Return New XPoint(point.X / TwipsPerPoint, point.Y / TwipsPerPoint)
	End Function
End Class
