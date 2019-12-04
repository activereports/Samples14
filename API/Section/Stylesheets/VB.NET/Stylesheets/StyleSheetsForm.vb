Imports System.IO
Imports System.Xml

Public Class StyleSheetsForm

	Private externalStyleSheet As String = ""

	Private Sub buttonChooseExtStyle_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles buttonChooseExtStyle.Click
		'Select an external stylsheet to apply.
		Dim openFileDialog As FileDialog = New OpenFileDialog()
		openFileDialog.Filter = My.Resources.Filter
		openFileDialog.InitialDirectory = New FileInfo(Me.GetType().Assembly.Location).DirectoryName
		openFileDialog.CheckFileExists = True

		If openFileDialog.ShowDialog(Me) = DialogResult.OK Then
			Dim styleSheetFile As FileInfo = New FileInfo(openFileDialog.FileName)
			externalStyleSheet = styleSheetFile.FullName
			radioButtonExternalStyleSheet.Text = My.Resources.ExternalSstylesheet + styleSheetFile.Name
			radioButtonExternalStyleSheet.Checked = True
		End If
	End Sub

	Private Sub buttonRunReport_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles buttonRunReport.Click
		Dim xmlReader As XmlReader = Nothing
		'Select the report in the viewer.
		If radioButtonProductListReport.Checked Then
			xmlReader = XmlReader.Create(My.Resources.ProductsReport)
		ElseIf radioButtonCategoriesReport.Checked Then
			xmlReader = XmlReader.Create(My.Resources.CategoryReport)
		End If

		Dim report As New SectionReport
		report.LoadLayout(xmlReader)
		'Apply stylesheet on the report.
		Dim outputFolder As String
		outputFolder = New FileInfo(Me.GetType().Assembly.Location).DirectoryName & "\"

		Dim styleSheet As String = ""
		If radioButtonClassicStyle.Checked Then
			styleSheet = outputFolder & "Classic.reportstyle"
		ElseIf radioButtonColoredStyle.Checked Then
			styleSheet = outputFolder & "Colored.reportstyle"
		ElseIf externalStyleSheet <> "" Then
			styleSheet = externalStyleSheet
		End If

		If styleSheet <> "" Then
			report.LoadStyles(styleSheet)
		End If
		reportViewer.LoadDocument(report)

	End Sub

End Class
