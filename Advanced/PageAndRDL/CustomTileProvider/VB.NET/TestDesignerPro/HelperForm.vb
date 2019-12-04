Public Class HelperForm

	Private Sub rtfHelp_LinkClicked(sender As Object, e As LinkClickedEventArgs) Handles rtfHelp.LinkClicked
		Process.Start(e.LinkText)
	End Sub

	Private Sub HelperForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
		e.Cancel = True
		Hide()
	End Sub

End Class
