Imports System.IO
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports

Public Class ViewerForm
	Protected Overrides Sub OnLoad(e As EventArgs)
		MyBase.OnLoad(e)

		Dim reportFile As FileInfo = New FileInfo("..\..\..\..\Report\Svg.rdlx")
		Dim report As PageReport = New PageReport(reportFile)
		Dim document As PageDocument = New PageDocument(report)

		viewer1.LoadDocument(document)
	End Sub
End Class
