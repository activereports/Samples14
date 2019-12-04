Imports System.Web

' CustomExportHtml - showcases HTML Export over the web.
Public Class CustomExportHtml
	Inherits System.Web.UI.Page
	'Required method for Designer support - do not modify
	'the contents of this method with the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

	End Sub

	Private Sub Page_Init(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Init
		'CODEGEN: This call is required by the ASP.NET Web Form Designer.

		InitializeComponent()
	End Sub

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		' Tell the browser and the "network" that this resulting data of this page should be cached since this could be a dynamic report that changes upon each request.
		Response.Cache.SetCacheability(HttpCacheability.NoCache)
		' Tell the browser this is an HTML document so it will use an appropriate viewer.
		Response.ContentType = "text/html"

		Response.Redirect("Reports/NwindLabels.rpx")
	End Sub	'Page_Load

End Class
