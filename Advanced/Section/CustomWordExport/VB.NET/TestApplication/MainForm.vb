Imports System.IO
Imports GrapeCity.ActiveReports.Export
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports.Export.Word.Section

Public Class MainForm
	Private Const _path As String = "../../../../Reports/"

	Public Sub New()
		InitializeComponent()
		Dim files = Directory.GetFiles(_path)

		For Each file As String In files
			reports.Items.Add(Path.GetFileName(file))
		Next

		exports.Items.Add(New ExportWrapper(New RtfExport(), My.Resources.ARExport))
		exports.Items.Add(New ExportWrapper(New HtmlToOpenXmlExport(), My.Resources.HtmlToOpenXmlExport))
		exports.Items.Add(New ExportWrapper(New MariGoldExport(), My.Resources.MariGoldExport))
	End Sub

	Private Sub reports_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles reports.SelectedIndexChanged
		Dim filePath = Path.Combine(_path, CType(reports.SelectedItem, String))
		viewer.LoadDocument(filePath)
		EnableSaveButton()
	End Sub

	Private Sub saveAsButton_Click(sender As Object, e As System.EventArgs) Handles saveAsButton.Click
		Dim exportType = CType(exports.SelectedItem, ExportWrapper).GetExportType()
		Dim saveFileDialog = New SaveFileDialog() With {
			.FileName = Path.GetFileNameWithoutExtension(CType(reports.SelectedItem, String)) + "." + exportType,
			.Filter = exportType + " files| *." + exportType
		}

		If saveFileDialog.ShowDialog() <> DialogResult.OK Then
			Return
		End If

		Dim fileName = saveFileDialog.FileName

		Dim docExport = CType(exports.SelectedItem, ExportWrapper)
		docExport.Export(viewer.Document, fileName)
		Process.Start(fileName)
	End Sub

	''' <summary>
	''' Wrapper for export.
	''' </summary>
	Private NotInheritable Class ExportWrapper
		Private _docExp As IDocumentExport
		Private _name As String
		Private Const _docxExportType As String = "docx"
		Private Const _docExportType As String = "doc"

		Public Sub New(docExp As IDocumentExport, name As String)
			_docExp = docExp
			_name = name
		End Sub

		Public Overrides Function ToString() As String
			Return _name
		End Function

		Public Function GetExportType() As String
			Return If(TypeOf _docExp Is RtfExport, _docExportType, _docxExportType)
		End Function

		Public Sub Export(document As SectionDocument, destFilePath As String)
			Try
				_docExp.Export(document, destFilePath, Nothing)
			Catch ex As Exception
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
			End Try
		End Sub
	End Class

	Private Sub exports_SelectedIndexChanged(sender As Object, e As EventArgs) Handles exports.SelectedIndexChanged
		EnableSaveButton()
	End Sub

	Private Sub EnableSaveButton()
		If (Not saveAsButton.Enabled AndAlso Not IsNothing(exports.SelectedItem) AndAlso Not IsNothing(reports.SelectedItem)) Then
			saveAsButton.Enabled = True
		End If
	End Sub
End Class
