Imports MariGold.OpenXHTML
Imports GrapeCity.ActiveReports.Export.Html.Section
Imports GrapeCity.ActiveReports.Export.Html
Imports GrapeCity.ActiveReports.Export
Imports GrapeCity.ActiveReports.Document
Imports System.IO

Public Class MariGoldExport
	Implements IDocumentExport
	Private Const _htmlPath As String = "..\..\htmlTemp\"
	Private Const _htmlFilePath As String = _htmlPath + "temp.html"

	Private Function HtmlExport(document As SectionDocument, pageRange As String) As String
		If Not Directory.Exists(_htmlPath) Then
			Directory.CreateDirectory(_htmlPath)
		End If

		Using export = New HtmlExport
			export.Export(document, _htmlFilePath, pageRange)
		End Using
		Return GetHtmlString()
	End Function

	Private Sub HtmlToWord(htmlString As String, destFilePath As String)
		Dim doc As New WordDocument(destFilePath)
		doc.ImagePath = Path.GetFullPath(_htmlPath)
		doc.Process(New HtmlParser(htmlString))
		doc.Save()
	End Sub

	Private Function GetHtmlString() As String
		Using stream As New StreamReader(_htmlFilePath)
			Return stream.ReadToEnd()
		End Using
	End Function

	Private Sub ClearHtmlPath()
		For Each file In New DirectoryInfo(_htmlPath).GetFiles()
			file.Delete()
		Next
		Directory.Delete(Path.GetDirectoryName(_htmlPath))
	End Sub

	Sub Export(document As SectionDocument, filePath As String, pageRange As String) Implements IDocumentExport.Export
		Dim html = HtmlExport(document, pageRange)
		HtmlToWord(html, filePath)
		ClearHtmlPath()
	End Sub

	Sub Export(document As SectionDocument, filePath As String) Implements IDocumentExport.Export
		CType(Me, IDocumentExport).Export(document, filePath, String.Empty)
	End Sub

	Sub Export(document As SectionDocument, outputStream As Stream) Implements IDocumentExport.Export
		CType(Me, IDocumentExport).Export(document, outputStream, String.Empty)
	End Sub

	Sub Export(document As SectionDocument, outputStream As Stream, pageRange As String) Implements IDocumentExport.Export
		Dim html = HtmlExport(document, pageRange)
		HtmlToWord(html, (CType(outputStream, FileStream)).Name)
		ClearHtmlPath()
	End Sub

	Sub Export(document As SectionDocument, outputHandler As IOutputHtml, pageRange As String) Implements IDocumentExport.Export
		Throw New NotSupportedException("It's not allowed to use IOutputHtml")
	End Sub
End Class
