Imports GrapeCity.ActiveReports.Drawing
Imports System.Drawing
Imports PdfSharp.Drawing

Partial Class PdfContentGenerator
	Private MustInherit Class BrushBase
		Inherits BrushEx
		Public Sub New()
		End Sub

		Protected MustOverride Function Convert() As XBrush

		Public Shared Widening Operator CType(brush As BrushBase) As XBrush
			Return brush.Convert()
		End Operator
	End Class

	Private Class SolidBrush
		Inherits BrushBase
		Private ReadOnly _brushInfo As XSolidBrush

		Public Sub New(color As Color)
			_brushInfo = New XSolidBrush(color)
		End Sub

		Private Sub New(brushInfo As XSolidBrush)
			_brushInfo = brushInfo
		End Sub

		Public Overrides Function Clone() As Object
			Return New SolidBrush(_brushInfo)
		End Function

		Protected Overrides Function Convert() As XBrush
			Return _brushInfo
		End Function
	End Class

	Private Class LinearGradientBrush
		Inherits BrushBase
		Private ReadOnly _brushInfo As XLinearGradientBrush

		Public Sub New(point1 As PointF, point2 As PointF, color1 As Color, color2 As Color)
			_brushInfo = New XLinearGradientBrush(PdfConverter.Convert(point1), PdfConverter.Convert(point2), XColor.FromArgb(color1), XColor.FromArgb(color2))
		End Sub

		Private Sub New(brushInfo As XLinearGradientBrush)
			_brushInfo = brushInfo
		End Sub

		Public Overrides Function Clone() As Object
			Return New LinearGradientBrush(_brushInfo)
		End Function

		Protected Overrides Function Convert() As XBrush
			Return _brushInfo
		End Function
	End Class
End Class
