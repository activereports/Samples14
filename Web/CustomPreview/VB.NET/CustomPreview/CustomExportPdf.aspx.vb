Imports System.IO
Imports System.Xml
Imports GrapeCity.ActiveReports.Export.Pdf.Section


'CustomerExportPdf - showcases exporting an ActiveReport to PDF over the web using streaming.
Public Class CustomExportPdf
	Inherits System.Web.UI.Page
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

	End Sub

	Private Sub Page_Init(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Init
		'CODEGEN: This call is required by the ASP.NET Web Form Designer.

		InitializeComponent()
	End Sub
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		Dim rpt As New SectionReport()
		Dim reportsPath As String = Path.Combine(Server.MapPath("~"), "Reports") + "\"
		rpt.ResourceLocator = New DefaultResourceLocator(New Uri(reportsPath))
		Dim xtr As New XmlTextReader(Path.Combine(reportsPath, "Invoice.rpx"))
		rpt.LoadLayout(xtr)
		xtr.Close()

		Try
			rpt.Run(False)
		Catch eRunReport As ReportException
			' Failure running report, just report the error to the user.
			Response.Clear()
			Response.Write(GetLocalResourceObject("Error"))
			Response.Write(eRunReport.ToString())
			Return
		End Try

		' Tell the browser this is a PDF document so it will use an appropriate viewer.
		'  If the report has been exported in a different format, the content-type will 
		' need to be changed as noted in the following table:
		'ExportType		(ContentType)
		'	PDF		   "application/pdf"  (needs to be in lowercase)
		'	RTF		  ("application/rtf")
		'	TIFF		  "image/tiff"	   (will open in separate viewer instead of browser)
		'	HTML		  "message/rfc822"   (only applies to compressed HTML pages that includes images)
		'	Excel		("application/vnd.ms-excel")
		'	Excel		 "application/excel" (either of these types should work) 
		'	Text		  ("text/plain")
		Response.ContentType = "application/pdf"


		Response.AddHeader("content-disposition", "inline; filename=MyPDF.PDF")

		' Create the PDF export object.
		Dim pdf As New PdfExport()
		' Create a new memory stream that will hold the pdf output.
		Dim memStream As New System.IO.MemoryStream()
		' Export the report to PDF.
		pdf.Export(rpt.Document, memStream)
		'  Write the PDF stream out.
		Response.BinaryWrite(memStream.ToArray())
		' Send all buffered content to the client.
		Response.End()
	End Sub 'Page_Load

End Class
