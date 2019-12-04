Imports GrapeCity.ActiveReports.Document

Public Class PreviewForm

	Dim _reportRuntime As PageDocument

	Public Sub New(ByVal runtime As PageDocument)

		' This call is required by the Windows Form Designer.
		InitializeComponent()
		_reportRuntime = runtime
		' Add any initialization after the InitializeComponent() call.

	End Sub
	Private Sub PreviewForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If (Not _reportRuntime Is Nothing) Then
			arvMain.LoadDocument(_reportRuntime)
		End If
	End Sub
End Class
