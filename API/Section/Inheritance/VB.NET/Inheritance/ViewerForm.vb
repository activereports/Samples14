Public Class ViewerForm
	Inherits System.Windows.Forms.Form

	Public Sub New()
		MyBase.New()

		' Required for Windows Form Designer support
		InitializeComponent()

		' Add any initialization after the call () InitializeComponent.

	End Sub

	' ActiveReport overrides dispose to clean up for the list of components.
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	' 
	

	' Note: The following procedure is required by the ActiveReport designer.
	'ActiveReport can be changed by using the designer
	'Please do not modify it using the code editor.
	
	Private Sub runTimeRptBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles runTimeRptBtn.Click
		Dim rpt As New rptInheritChild
		arvMain.LoadDocument(rpt)
	End Sub

	Private Sub designTimeRptBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles designTimeRptBtn.Click
		Dim rpt As New rptDesignChild
		arvMain.LoadDocument(rpt)
	End Sub

End Class
