Imports System
Imports System.Windows.Forms
Imports System.Xml
Imports GrapeCity.ActiveReports.SectionReportModel

'Demonstrates how to setup an XML data source through the customer order list.
' It also displays subreports and multi-level reports.
Public Class StartForm
	Inherits System.Windows.Forms.Form

	Public Sub New()
		MyBase.New()

		' Required designer variable.
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

	'The main entry point for the application.
	<STAThread()>
	Shared Sub Main()
		Application.Run(New StartForm())
	End Sub 'Main

	'btnCustomers_Click - Checks the radio options and sets the report object's data
	Private Sub btnCustomers_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCustomers.Click
		Try
			'Dim rpt As New CustomersOrders()
			Dim rpt As New SectionReport()
			rpt.LoadLayout(XmlReader.Create(My.Resources.CustomersOrders))

			CType(rpt.DataSource, Data.XMLDataSource).FileURL = My.Resources.ConnectionString
			If radioAll.Checked Then 'Show all data
				CType(rpt.DataSource, Data.XMLDataSource).RecordsetPattern = "//CUSTOMER"
			ElseIf radioID.Checked Then 'Show data where ID = 2301
				CType(rpt.DataSource, Data.XMLDataSource).RecordsetPattern = "//CUSTOMER[@id = " + """" + "2301" + """" + "]"
			ElseIf radioEmail.Checked Then 'Show data where Email is valid
				CType(rpt.DataSource, Data.XMLDataSource).RecordsetPattern = "//CUSTOMER[@email]"
			End If

			Dim frm As New ViewerForm()
			frm.Show()
			frm.LoadReport(rpt)
		Catch ex As ReportException
			MessageBox.Show(ex.ToString(), Text)
		End Try
	End Sub 'btnCustomers_Click

	'btnCustomersLeveled_Click - Creates a OrdersLeveled report and sets
	' the data source for it.
	Private Sub btnCustomersLeveled_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCustomersLeveled.Click
		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(My.Resources.OrdersLeveled))
		CType(rpt.DataSource, Data.XMLDataSource).FileURL = My.Resources.ConnectionString
		rpt.Run()
		Dim frm As New ViewerForm()
		frm.Show()
		frm.LoadReport(rpt)
	End Sub 'btnCustomersLeveled_Click

End Class 'StartForm
