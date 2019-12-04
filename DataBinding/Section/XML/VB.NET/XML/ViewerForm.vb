'Viewer window for the XML based ActiveReport
Public Class ViewerForm
	Inherits System.Windows.Forms.Form
	
	Public Sub New()
		MyBase.New()

		'Required designer variable.
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

	'Set the document to the viewer on the form.
	Public Sub LoadReport(ByVal report As SectionReport)
		arvMain.LoadDocument(report)
	End Sub

End Class 'ViewerForm
