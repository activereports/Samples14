Imports System.IO
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports.PageReportModel

Public Class StyleSheetsForm
	Private _externalStyleSheet As String = String.Empty
	Private Const RdlReport As String = "ReorderList.rdlx"
	Private Const PageReport As String = "Delivery Slip.rdlx"
	Private ReadOnly _outputFolder As String = Application.StartupPath
	Private _reportsPath As String = Path.GetFullPath(Path.Combine(_outputFolder, "..\..\..\..\Reports"))
	Private _file As FileInfo

	Private Sub buttonChooseExtStyle_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles buttonChooseExtStyle.Click

		'Select an external stylsheet to apply.
		'

		Dim openFileDialog As FileDialog = New OpenFileDialog()
		openFileDialog.Filter = My.Resources.FilterText
		openFileDialog.InitialDirectory = _reportsPath
		openFileDialog.CheckFileExists = True

		If openFileDialog.ShowDialog(Me) = DialogResult.OK Then
			Dim styleSheetFile As FileInfo = New FileInfo(openFileDialog.FileName)
			_externalStyleSheet = styleSheetFile.FullName
			radioButtonCustomStyle.Text = My.Resources.StyleSheetText + styleSheetFile.Name
			radioButtonCustomStyle.AutoEllipsis = True
			radioButtonCustomStyle.Checked = True
		End If
	End Sub

	Private Sub buttonRunReport_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles buttonRunReport.Click
		Dim report As PageReport

		'Select the report in the viewer.
		If radioButtonRDLReport.Checked Then
			_file = New FileInfo(Path.Combine(_reportsPath, RdlReport))
		ElseIf radioButtonPageReport.Checked Then
			_file = New FileInfo(Path.Combine(_reportsPath, PageReport))
		End If
		report = New PageReport(_file)

		If radioButtonRDLReport.Checked Then
			_file = New FileInfo(Path.Combine(_reportsPath, RdlReport))
		End If

		If radioButtonPageReport.Checked Then
			_file = New FileInfo(Path.Combine(_reportsPath, PageReport))
		End If

		If radioButtonEmbeddedStyle.Checked Then
			report.Report.StyleSheetSource = StyleSheetSource.Embedded
			report.Report.StyleSheetValue = "BaseStyle"
		Else
			report.Report.StyleSheetSource = StyleSheetSource.External
		End If

		If radioButtonOrangeStyle.Checked Then
			report.Report.StyleSheetValue = Path.Combine(_reportsPath, "ModernStyle.rdlx-styles")
		End If

		If radioButtonGreenStyle.Checked Then
			report.Report.StyleSheetValue = Path.Combine(_reportsPath, "FaxSheetStyle.rdlx-styles")
		End If

		If radioButtonBlueStyle.Checked Then
			report.Report.StyleSheetValue = Path.Combine(_reportsPath, "HighContrastStyle.rdlx-styles")
		End If

		If radioButtonCustomStyle.Checked Then
			report.Report.StyleSheetValue = Path.Combine(_reportsPath, _externalStyleSheet)
		End If

		Dim pageDocument As PageDocument = New PageDocument(report)
		reportViewer.LoadDocument(pageDocument)
	End Sub

End Class
