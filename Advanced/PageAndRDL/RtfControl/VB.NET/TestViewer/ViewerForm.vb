Imports System.IO
Imports GrapeCity.ActiveReports.Document

Public Class ViewerForm
	Inherits Form

	Public Sub New()
		InitializeComponent()
	End Sub

	Protected Overrides Sub OnLoad(ByVal e As EventArgs)
		MyBase.OnLoad(e)
		Dim reportFile = New FileInfo("..\..\..\..\Report\Rtf.rdlx")

		Dim report = New PageReport(reportFile)
		Dim document = New PageDocument(report)

		viewer1.LoadDocument(document)
	End Sub
End Class
