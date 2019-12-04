Imports System.Xml

Public Class ViewerForm

	Enum ReportType
		CrossSectionControls
		RepeatToFill
		PrintAtBottom
	End Enum

	Private Delegate Sub LoadReportInvoker(ByVal reportType As ReportType)

	Private Sub ViewerForm_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
		' Allow the Form to open while the report is rendering.
		BeginInvoke(New LoadReportInvoker(AddressOf LoadReport), ReportType.CrossSectionControls)
		BeginInvoke(New LoadReportInvoker(AddressOf LoadReport), ReportType.RepeatToFill)
		BeginInvoke(New LoadReportInvoker(AddressOf LoadReport), ReportType.PrintAtBottom)
	End Sub

	Private Sub LoadReport(ByVal rptType As ReportType)
		' Instantiate a new Invoice report
		Dim report As New SectionReport
		report.LoadLayout(XmlReader.Create(My.Resources.Invoice))
		report.MaxPages = 10

		' Set the connection string to the sample database.
		CType(report.DataSource, Data.OleDBDataSource).ConnectionString = My.Resources.ConnectionString
		Select Case rptType
			Case ReportType.CrossSectionControls
				cscViewer.LoadDocument(report)
			Case ReportType.RepeatToFill
				CType(report.Sections("Detail"), SectionReportModel.Detail).RepeatToFill = True
				repeatToFillViewer.LoadDocument(report)
			Case ReportType.PrintAtBottom
				CType(report.Sections("customerGroupFooter"), SectionReportModel.GroupFooter).PrintAtBottom = True
				printAtBottomViewer.LoadDocument(report)
		End Select
	End Sub

End Class
