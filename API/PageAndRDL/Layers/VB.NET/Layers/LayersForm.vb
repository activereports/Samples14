Imports GrapeCity.ActiveReports.Rendering.IO
Imports System.IO
Imports System.Windows.Forms
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports.PageReportModel
Imports GrapeCity.ActiveReports.Export.Pdf.Page

Public Class LayersForm

	Shared _targetDevices As New TargetDevices()
	Private _pageReport As New PageReport()
	Private _reportRuntime As PageDocument = Nothing

	'Layer Constructor.
	Sub New()

		InitializeComponent()
		_targetDevices = TargetDevices.All

	End Sub

	'Preview the Layer report and set the target device for the layer to the report.
	Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
		_pageReport.Load(New FileInfo("Layer.rdlx"))
		_reportRuntime = New PageDocument(_pageReport)
		_reportRuntime.PageReport.Report.Layers(1).TargetDevice = _targetDevices
		reportViewer.LoadDocument(_reportRuntime)
		btnPdfExport.Enabled = True
	End Sub

	'Export the report displayed in Viewer to PDF.
	Private Sub btnPdfExport_Click(sender As Object, e As EventArgs) Handles btnPdfExport.Click
		If _reportRuntime IsNot Nothing Then
			Dim settings As New Settings()
			settings.HideToolbar = True
			settings.HideMenubar = True
			settings.HideWindowUI = True
			saveFileDialog.Filter = My.Resources.Resources.PDFFilter

			Dim renderingExtension As New PdfRenderingExtension()
			If saveFileDialog.ShowDialog() = DialogResult.OK Then
				If File.Exists(saveFileDialog.FileName) Then
					File.Delete(saveFileDialog.FileName)
				End If

				Dim exportFile As New FileStreamProvider(New DirectoryInfo(Path.GetDirectoryName(saveFileDialog.FileName)), Path.GetFileNameWithoutExtension(saveFileDialog.FileName))
				_reportRuntime.Render(renderingExtension, exportFile, settings)
			End If
		End If
	End Sub

	'Add or delete the target device by checked checkbox.
	Private Sub checkBox_CheckedChanged(sender As Object, e As EventArgs) Handles checkBoxScreen.CheckedChanged, checkBoxPDF.CheckedChanged, checkBoxPaper.CheckedChanged
		_targetDevices = TargetDevices.None
		btnPdfExport.Enabled = False
		If checkBoxPaper.Checked Then
			_targetDevices = _targetDevices Xor TargetDevices.Paper
		End If
		If checkBoxPDF.Checked Then
			_targetDevices = _targetDevices Xor TargetDevices.Export
		End If
		If checkBoxScreen.Checked Then
			_targetDevices = _targetDevices Xor TargetDevices.Screen
		End If
	End Sub

End Class
