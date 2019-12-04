Imports PdfSharp.Drawing
Imports GrapeCity.ActiveReports.Drawing
Imports System.Drawing


Partial Class PdfContentGenerator
	Private Class Image
		Inherits ImageEx
		Private ReadOnly _imageInfo As XImage

		Public Sub New(imageInfo As XImage)
			_imageInfo = imageInfo
		End Sub

		Public Overrides Function Clone() As Object
			Return New Image(_imageInfo)
		End Function

		Public Overrides ReadOnly Property Size() As SizeF
			Get
				Return New SizeF(CType(_imageInfo.PixelWidth * 1440 / _imageInfo.HorizontalResolution, Single), CType(_imageInfo.PixelHeight * 1440 / _imageInfo.VerticalResolution, Single))
			End Get
		End Property

		Public Shared Widening Operator CType(image As Image) As XImage
			Return image._imageInfo
		End Operator
	End Class
End Class
