Imports GrapeCity.ActiveReports.Rendering.IO
Imports GrapeCity.ActiveReports.PageReportModel
Imports GrapeCity.ActiveReports.Extensibility.Rendering.IO
Imports GrapeCity.ActiveReports.Extensibility.Rendering
Imports GrapeCity.ActiveReports.Document
Imports System.Threading
Imports System.IO
Imports System.Collections.Specialized

Partial Public NotInheritable Class MainForm
	Inherits Form
	Private Const ReportsPath As String = "..\..\..\..\Reports\"

	Private _startTimeout As DateTime = DateTime.Now

	Public Sub New()
		InitializeComponent()

		AddHandler arViewer.RefreshReport, Sub(_s, __)
											   _startTimeout = DateTime.Now
										   End Sub
		AddHandler arViewer.LoadCompleted, Sub(_s, __)
											   BeginInvoke(New MethodInvoker(Sub()
																				 Try
																					 arTime.Text = Math.Ceiling((DateTime.Now - _startTimeout).TotalMilliseconds).ToString() + "ms"
																				 Catch ex As Exception
																					 MessageBox.Show(Me, String.Format(My.Resources.ErrorMessage, ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
																				 End Try

																			 End Sub))

										   End Sub
	End Sub

	Protected Overrides Sub OnLoad(e As EventArgs)
		splitContainer1.Panel1MinSize = 190
        splitContainer1.Panel2MinSize = 400
        splitContainer3.Panel1MinSize = 250
        splitContainer3.Panel2MinSize = 250
		MyBase.OnLoad(e)


		For Each reportFile In Directory.GetFiles(ReportsPath, "*.rdlx")
			reports.Items.Add(Path.GetFileName(reportFile))
		Next
	End Sub

	Private Sub reports_SelectedIndexChanged(sender As Object, e As EventArgs) Handles reports.SelectedIndexChanged
		SetPropertyGrid()
		ButtonsEnable()
	End Sub

	Private Sub exports_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioButton2.Click, RadioButton1.Click
		SetPropertyGrid()
		ButtonsEnable()
	End Sub

	Private Function IsFpl(reportFile As String) As Boolean
		Using pageReport As New PageReport(New FileInfo(Path.Combine(ReportsPath, reportFile)))
			Dim report = pageReport.Report
			If report Is Nothing OrElse report.Body Is Nothing Then
				Return False
			End If
			Dim items = report.Body.ReportItems
			Return Not IsNothing(items) AndAlso items.Count = 1 AndAlso TypeOf items(0) Is FixedPage
		End Using
	End Function

	Private Sub exportButton_Click(sender As Object, e As EventArgs) Handles exportButton.Click
		_startTimeout = DateTime.Now
		Dim reportPath = Path.Combine(ReportsPath, CType(reports.SelectedItem, String))
		arViewer.LoadDocument(New PageReport(New FileInfo(reportPath)).Document)
		RenderPdf(New MemoryStreamProvider(), Sub(streams)
												  BeginInvoke(New MethodInvoker(Sub()
																					Try
																						pdfTime.Text = Math.Ceiling((DateTime.Now - _startTimeout).TotalMilliseconds).ToString() + "ms"
																						pdfViewer.Document = PdfiumViewer.PdfDocument.Load(streams.GetPrimaryStream().OpenStream())
																					Catch ex As Exception
																						MessageBox.Show(Me, String.Format(My.Resources.ErrorMessage, ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
																					End Try

																				End Sub))

											  End Sub)
	End Sub

	Private Sub saveAsButton_Click(sender As Object, e As EventArgs) Handles saveAsButton.Click
		If saveAsPdf.ShowDialog(Me) <> DialogResult.OK OrElse String.IsNullOrEmpty(saveAsPdf.FileName) Then
			Return
		End If
		Dim pdfPath = saveAsPdf.FileName
		RenderPdf(New FileStreamProvider(pdfPath), Sub(_s)
													   Try
														   Process.Start(pdfPath)
													   Catch ex As Exception
														   MessageBox.Show(Me, String.Format(My.Resources.ErrorMessage, ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
													   End Try

												   End Sub)
	End Sub

	Private Sub SetPropertyGrid()
		If Not RadioButton1.Checked And Not RadioButton2.Checked Then
			Return
		End If

		Dim export As RenderingExtensionWrapper = If(RadioButton1.Checked, RadioButton1, RadioButton2)
		If ((RadioButton1.Checked Or RadioButton2.Checked) AndAlso Not IsNothing(reports.SelectedItem)) Then
			propertyGrid.SelectedObject = export.GetSupportedSettings(If(Not IsNothing(reports.SelectedItem), IsFpl(CType(reports.SelectedItem, String)), False))
		End If
	End Sub

	Private Sub ButtonsEnable()
		If (RadioButton1.Checked Or RadioButton2.Checked) AndAlso Not IsNothing(reports.SelectedItem) Then
			exportButton.Enabled = True
			saveAsButton.Enabled = True
		End If
	End Sub

	Private Sub RenderPdf(streams As StreamProvider, postAction As Action(Of StreamProvider))
		Dim reportPath = Path.Combine(ReportsPath, CType(reports.SelectedItem, String))
		Dim pdfSettings As ISettings = CType(PropertyGrid.SelectedObject, ISettings)
		Dim export As RenderingExtensionWrapper = If(RadioButton1.Checked, RadioButton1, RadioButton2)

		Cursor = Cursors.WaitCursor
		Enabled = False
		Dim thread As New Thread(Sub(_s)
									 Try
										 Using report As New PageReport(New FileInfo(reportPath))
											 export.Render(report.Document, streams, pdfSettings.GetSettings())
										 End Using
										 postAction(streams)
									 Catch ex As Exception
										 BeginInvoke(New MethodInvoker(Sub()
																		   MessageBox.Show(Me, String.Format(My.Resources.ErrorMessage, ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])

																	   End Sub))
									 Finally
										 BeginInvoke(New MethodInvoker(Sub()
																		   Enabled = True

																	   End Sub))
										 BeginInvoke(New MethodInvoker(Sub()
																		   Cursor = Cursors.[Default]

																	   End Sub))
									 End Try

								 End Sub)
		thread.Start()
	End Sub

	Private NotInheritable Class RenderingExtensionWrapper
		Inherits RadioButton
		Private ReadOnly _re As IRenderingExtension
		Private ReadOnly _name As String

		Public Sub New(re As IRenderingExtension, name As String)
			_re = re
			_name = name
		End Sub

		Public Overrides Property Text As String
			Get
				Return _name
			End Get
			Set(value As String)
			End Set
		End Property

		Public Overrides Function ToString() As String
			Return _name
		End Function

		Public Function GetSupportedSettings(fpl As Boolean) As ISettings
			If TypeOf _re Is IConfigurable Then
				Return (CType(_re, IConfigurable)).GetSupportedSettings(fpl)
			End If
			Return Nothing
		End Function

		Public Sub Render(document As PageDocument, streams As StreamProvider, settings As NameValueCollection)
			document.Render(_re, streams, settings)
		End Sub
	End Class
End Class
