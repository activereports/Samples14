Imports System
Imports System.Windows.Forms
Imports System.Xml

'UnboundDataSample - Illustrates the use of unbound data in ActiveReports.
Public Class MainForm
	Inherits System.Windows.Forms.Form

	Public Sub New()
		MyBase.New()

		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub

	'Form overrides dispose to clean up the component list.
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	'The main entry point for the application.
	<STAThread()> _
	Shared Sub Main()
		Application.Run(New MainForm())
	End Sub 'Main

	'tabDataBinding_SelectedIndexChanged - clears the viewer out when switching
	'tabs
	Private Sub tabDataBinding_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabDataBinding.SelectedIndexChanged
		'Clear existing pages
		arvMain.Document = New Document.SectionDocument()
	End Sub


	'btnDataSet_Click -  Illustrates using a DataSet as a data source.
	Private Sub btnDataSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDataSet.Click
		'Create the report.
		Dim rpt As New SectionReport
		rpt.LoadLayout(XmlReader.Create(My.Resources.UnboundDSInvoice))
		rpt.PrintWidth = 6.5!
		'Run and view the report
		arvMain.LoadDocument(rpt)
	End Sub 'btnDataSet_Click

	'btnDataReader_Click - Illustrates using a DataReader as a data source.
	Private Sub btnDataReader_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDataReader.Click
		'Create the report.
		Dim rpt As New SectionReport
		rpt.LoadLayout(XmlReader.Create(My.Resources.UnboundDRInvoice))
		rpt.PrintWidth = 6.5!
		'Run and view the report.
		arvMain.LoadDocument(rpt)
	End Sub 'btnDataReader_Click

	'btnTextFile_Click - Illustrates using a TextFile as a data source.
	Private Sub btnTextFile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTextFile.Click
		'Create the report.
		Dim rpt As New SectionReport
		rpt.LoadLayout(XmlReader.Create(My.Resources.UnboundTFInvoice))
		rpt.PrintWidth = 6.5!
		'Run and view the report.
		arvMain.LoadDocument(rpt)
	End Sub 'btnTextFile_Click

	'btnArray_Click - Illustrates using a Array as a data source.
	Private Sub btnArray_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnArray.Click
		'Create the report.
		Dim rpt As New SectionReport
		rpt.LoadLayout(XmlReader.Create(My.Resources.UnboundDAInvoice))
		rpt.PrintWidth = 6.5!
		'Run and view the report.
		arvMain.LoadDocument(rpt)
	End Sub 'btnArray_Click

End Class
