Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Controls
Imports GrapeCity.ActiveReports.SectionReportModel
Imports GrapeCity.ActiveReports.Document.Section


	''' <summary>
	''' ActiveReports Custom Preview Start Page
	''' </summary>

Partial Public Class DefaultPage
	Inherits System.Web.UI.Page
	Protected Sub Page_Load(sender As Object, e As System.EventArgs)
		' Put user code to initialize the page here.
	End Sub

	Protected Overrides Sub OnInit(e As EventArgs)
		'
		' CODEGEN: This call is required by the ASP.NET Web Form Designer.
		'
		InitializeComponent()
		MyBase.OnInit(e)
	End Sub

	''' <summary>
	''' Required method for Designer support - do not modify
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()

	End Sub
End Class
