Public Class ViewerForm
	Inherits System.Windows.Forms.Form

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

	Public Sub LoadReport(ByVal report As SectionReport)
		arvMain.LoadDocument(report)
	End Sub
End Class
