Imports System.IO
Imports PdfSharp.Drawing

Friend NotInheritable Class PdfImagesFactory
	Implements IDisposable
	Private ReadOnly _images As IDictionary(Of String, XImage) = New Dictionary(Of String, XImage)()

	Public Function GetPdfImage(cacheId As String, content As Stream) As XImage
		Dim image As XImage = Nothing
		If _images.TryGetValue(cacheId, image) Then
			Return image
		End If
		image = XImage.FromStream(content)
		_images(cacheId) = image
		Return image
	End Function

#Region "IDisposable implementation"

	Sub Dispose() Implements IDisposable.Dispose
		For Each image As XImage In _images.Values
			image.Dispose()
		Next
		_images.Clear()
	End Sub

#End Region
End Class
