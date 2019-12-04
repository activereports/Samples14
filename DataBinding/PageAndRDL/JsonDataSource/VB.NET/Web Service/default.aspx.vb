Public Class _default
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		messageLabel.Text = Web_Service.My.Resources.Resource.bodyOfMessage
	End Sub
End Class
