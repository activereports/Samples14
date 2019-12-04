Public Class ViewerForm

	Protected Overrides Sub OnLoad(e As EventArgs)
		MyBase.OnLoad(e)
		viewer1.LoadDocument("../../DemoReport.rdlx")
	End Sub
End Class
