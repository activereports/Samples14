Imports PdfSharp.Drawing
Imports GrapeCity.ActiveReports.Drawing

Partial Class PdfContentGenerator
	Private Class Font
		Private ReadOnly _fontInfo As XFont

		Public Sub New(fontInfo As XFont)
			_fontInfo = fontInfo
		End Sub

		Public Function Clone() As Object
			Return New Font(_fontInfo)
		End Function

		Public Shared Widening Operator CType(font As Font) As XFont
			Return font._fontInfo
		End Operator
	End Class
End Class
