Imports System.Xml

'<summary>
' Summary description for BindIListToDataGridSample.
'</summary>
Public Class BindIListToDataGridSample
	Inherits Form
	Dim _resourceManager As Resources.ResourceManager
	Public Sub New()
		MyBase.New()
		'Required for Windows Form Designer support
		InitializeComponent()
	End Sub

	'Clean up any resources being used.
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub



	'<summary>
	' btnGenerateReport_Click - Opens a new viewer form to display the
	' report bound to the DataLayer object 
	'</summary>

	Private Sub btnGenerateReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGenerateReport.Click
		' Create new report object
		Dim rpt As SectionReport
		rpt = New SectionReport
		rpt.LoadLayout(XmlReader.Create("IlistReportSample.rpx"))
		rpt.DataSource = productCollection

		' Pass the document to show in the viewer form
		Dim frmViewer As New ViewerForm
		frmViewer.Show()
		frmViewer.LoadReport(rpt)
	End Sub

	Private Sub BindIListToDataGridSample_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		dataGridView.ColumnHeadersDefaultCellStyle.Font = New Font(My.Resources.Resource.DefaultFontName, 10)

	End Sub
End Class
