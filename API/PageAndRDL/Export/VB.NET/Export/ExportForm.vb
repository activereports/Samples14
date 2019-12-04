Imports System.Collections.Specialized
Imports System.IO
Imports System.Text
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Extensibility.Rendering

Public Class ExportForm

	Private Sub FillReportsNames()
		Dim directoryInfo As Object = New DirectoryInfo("../../../../Reports/")
		For Each file As Object In directoryInfo.GetFiles()
			reportsNames.Items.Add(file.Name)
		Next
	End Sub

	Private Sub exportButton_Click(sender As Object, e As EventArgs) Handles exportButton.Click

		Const _json As String = "json"
		Const _pdf As String = "pdf"
		Const _xlsx As String = "xlsx"
		Const _docx As String = "docx"
		Const _csv As String = "csv"
		Const _mht As String = "mht"

		Dim reportName = CType(reportsNames.SelectedItem, String)
		Dim exportType = CType(exportTypes.SelectedItem, String)
		Dim renderingExtension As IRenderingExtension = Nothing
		Dim settings As NameValueCollection = Nothing

		Dim exporTypeLower As String = exportType.ToLower()
		Select Case exporTypeLower
			Case _pdf
				renderingExtension = New GrapeCity.ActiveReports.Export.Pdf.Page.PdfRenderingExtension()
				settings = New GrapeCity.ActiveReports.Export.Pdf.Page.Settings()
				Exit Select
			Case _xlsx
				renderingExtension = New GrapeCity.ActiveReports.Export.Excel.Page.ExcelRenderingExtension()
				Dim excelSettings As ISettings = New GrapeCity.ActiveReports.Export.Excel.Page.ExcelRenderingExtensionSettings() With {
					.FileFormat = GrapeCity.ActiveReports.Export.Excel.Page.FileFormat.Xlsx
				}
				settings = excelSettings.GetSettings()
				Exit Select
			Case _csv
				settings = New GrapeCity.ActiveReports.Export.Text.Page.CsvRenderingExtension.Settings() With {
					.ColumnsDelimiter = ",",
					.RowsDelimiter = vbCr & vbLf,
					.QuotationSymbol = """"c,
					.Encoding = Encoding.UTF8
				}

				renderingExtension = New GrapeCity.ActiveReports.Export.Text.Page.CsvRenderingExtension()
				Exit Select
			Case _docx
				renderingExtension = New GrapeCity.ActiveReports.Export.Word.Page.WordRenderingExtension()
				settings = New GrapeCity.ActiveReports.Export.Word.Page.Settings() With {
					.FileFormat = GrapeCity.ActiveReports.Export.Word.Page.FileFormat.OOXML
				}
				Exit Select
			Case _mht
				renderingExtension = New GrapeCity.ActiveReports.Export.Html.Page.HtmlRenderingExtension()
				settings = New GrapeCity.ActiveReports.Export.Html.Page.Settings() With {
					.MhtOutput = True,
					.OutputTOC = True,
					.Fragment = False
				}
				Exit Select
			Case _json
				settings = New GrapeCity.ActiveReports.Export.Text.Page.JsonRenderingExtension.Settings() With {
					.Formatted = True
				}
				renderingExtension = New GrapeCity.ActiveReports.Export.Text.Page.JsonRenderingExtension()
				Exit Select
		End Select

		Dim report = New PageReport(New FileInfo("..\..\..\..\Reports\" + reportName))
		Dim fileName = Path.GetFileNameWithoutExtension(reportName)
		Dim saveFileDialog = New SaveFileDialog()
		saveFileDialog.FileName = fileName + GetSaveDialogExtension(exporTypeLower)
		saveFileDialog.Filter = GetSaveDialogFilter(exporTypeLower)

		If saveFileDialog.ShowDialog() <> DialogResult.OK Then
			Return
		End If

		Dim outputDirectory As Object = New DirectoryInfo(Path.GetDirectoryName(saveFileDialog.FileName))
		Dim outputProvider As Object = New Rendering.IO.FileStreamProvider(outputDirectory, Path.GetFileNameWithoutExtension(saveFileDialog.FileName))

		outputProvider.OverwriteOutputFile = True

		exportButton.Enabled = False
		report.Document.Render(renderingExtension, outputProvider, settings)
		exportButton.Enabled = True
	End Sub

	Private Sub ExportForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		FillReportsNames()
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
