Imports System.Xml

Public Class StartForm
	Inherits System.Windows.Forms.Form

	' Demonstrates unbound data, conditional highlighting, distributing data 
	' across columns to create a cross tab view and data aggregation.
	Public Sub New()
		MyBase.New()

		InitializeComponent()
	End Sub

	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	Private Sub StartForm_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
		Try
			'Create a report and display it in the Viewer.
			Dim rpt As New SectionReport()
			rpt.LoadLayout(XmlReader.Create(My.Resources.ProductWeeklySales))
			arvMain.LoadDocument(rpt)
		Catch ex As ReportException
			MessageBox.Show(ex.ToString(), Text)
		End Try
	End Sub

End Class
