Imports System.IO
Imports GrapeCity.ActiveReports

Public Class ExportForm

	Const _pdf As String = "pdf"
	Const _xlsx As String = "xlsx"
	Const _mht As String = "mht"
	Const _rtf As String = "rtf"
	Const _csv As String = "csv"

	Private Sub exportButton_Click(sender As Object, e As EventArgs) Handles exportButton.Click
		Dim reportName = CType(reportsNames.SelectedItem, String)
		Dim exportType = CType(exportTypes.SelectedItem, String)
		Dim documentExportEx As GrapeCity.ActiveReports.Export.IDocumentExportEx = Nothing

		Dim report As SectionReport = New SectionReport()
		Dim xtr As New System.Xml.XmlTextReader("..\..\..\..\Reports\" + reportName)
		report.LoadLayout(xtr)
		report.Run()

		Dim exporTypeLower = exportType.ToLower()
		Select Case exporTypeLower
			Case _pdf
				documentExportEx = New GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport()
				Exit Select
			Case _xlsx
				documentExportEx = New GrapeCity.ActiveReports.Export.Excel.Section.XlsExport() With {
					.FileFormat = GrapeCity.ActiveReports.Export.Excel.Section.FileFormat.Xlsx
				}
				Exit Select
			Case _rtf
				documentExportEx = New GrapeCity.ActiveReports.Export.Word.Section.RtfExport()
				Exit Select
			Case _mht
				documentExportEx = New GrapeCity.ActiveReports.Export.Html.Section.HtmlExport()
				Exit Select
			Case _csv
				documentExportEx = New GrapeCity.ActiveReports.Export.Xml.Section.TextExport()
				Exit Select
		End Select

		Dim fileName = Path.GetFileNameWithoutExtension(reportName)
		Dim saveFileDialog = New SaveFileDialog() With {
			.FileName = fileName + GetSaveDialogExtension(exporTypeLower),
			.Filter = GetSaveDialogFilter(exporTypeLower)
		}

		If saveFileDialog.ShowDialog() <> DialogResult.OK Then
			Return
		End If

		exportButton.Enabled = False
		Using stream As FileStream = New FileStream(saveFileDialog.FileName, FileMode.Create)
			documentExportEx.Export(report.Document, stream)
		End Using
		exportButton.Enabled = True
	End Sub

	Private Sub ExportForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		reportsNames.SelectedIndex = 0
		exportTypes.SelectedIndex = 0
	End Sub

	Private Function GetSaveDialogFilter(exportType As String) As String
		Return exportType + " files | *." + exportType
	End Function

	Private Function GetSaveDialogExtension(exportType As String) As String
		Return "." + exportType
	End Function

End Class
