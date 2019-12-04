Imports PdfSharp.Drawing
Imports GrapeCity.ActiveReports.Drawing
Imports System.Linq
Imports System.Drawing
Imports System

Partial Class PdfContentGenerator
	Private Class Pen
		Inherits PenEx
		Public Overrides Property Color() As Color
		Public Overrides Property Width() As Single
		Public Overrides Property DashStyle() As PenStyleEx
		Public Overrides Property Alignment() As PenAlignment
		Public Overrides Property StartCap() As LineCap
		Public Overrides Property EndCap() As LineCap
		Public Overrides Property DashCap() As DashCap
		Public Overrides Property LineJoin() As LineJoin
		Public Overrides Property DashPattern() As Single()

		Public Overrides Function Clone() As Object
			Return New Pen() With {
					.Color = Color,
					.Width = Width,
					.DashStyle = DashStyle,
					.Alignment = Alignment,
					.StartCap = StartCap,
					.EndCap = EndCap,
					.DashCap = DashCap,
					.LineJoin = LineJoin,
					.DashPattern = DashPattern
				}
		End Function

		Public Shared Widening Operator CType(pen As Pen) As XPen
			Dim currentPen As New XPen(XColor.FromArgb(pen.Color), pen.Width / PdfConverter.TwipsPerPoint)
			currentPen.DashStyle = CType([Enum].Parse(GetType(XDashStyle), pen.DashStyle.ToString()), XDashStyle)
			currentPen.LineCap = CType([Enum].Parse(GetType(XLineCap), pen.StartCap.ToString()), XLineCap)
			If pen.LineJoin = GrapeCity.ActiveReports.Drawing.LineJoin.MiterClipped Then
				currentPen.LineJoin = XLineJoin.Miter
			Else
				currentPen.LineJoin = CType([Enum].Parse(GetType(XLineJoin), pen.LineJoin.ToString()), XLineJoin)
			End If
			If pen.DashPattern IsNot Nothing Then
				currentPen.DashPattern = pen.DashPattern.Cast(Of Double)().ToArray()
			End If
			Return currentPen
		End Operator
	End Class
End Class
