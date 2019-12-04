using System;
using System.IO;
using GrapeCity.ActiveReports.Export.Pdf.Section;

namespace GrapeCity.ActiveReports.Samples.Web.CustomPreview
{
	/// <summary>
	/// CustomerExportPdf - showcases exporting an ActiveReport to PDF over the web using streaming.
	/// </summary>
	public partial class CustomExportPdf : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			SectionReport rpt = new SectionReport();

			var reportsPath =Path.Combine(Server.MapPath("~"), "Reports") + @"\";
			rpt.ResourceLocator = new DefaultResourceLocator(new Uri(reportsPath));
			System.Xml.XmlTextReader xtr = new System.Xml.XmlTextReader(Path.Combine(reportsPath, "Invoice.rpx"));
			rpt.LoadLayout(xtr);
			xtr.Close();
			try
			{
				rpt.Run(false);
			}
			catch (ReportException eRunReport)
			{
				// Failure running report, just report the error to the user.
				Response.Clear();
				Response.Write( GetLocalResourceObject("Error"));
				Response.Write(eRunReport.ToString());
				return;
			}

			// Tell the browser this is a PDF document so it will use an appropriate viewer.
			// If the report has been exported in a different format, the content-type will 
			// need to be changed as noted in the following table:
			//	ExportType  ContentType
			//	PDF	   "application/pdf"  (needs to be in lowercase)
			//	RTF	   "application/rtf"
			//	TIFF	  "image/tiff"	   (will open in separate viewer instead of browser)
			//	HTML	  "message/rfc822"   (only applies to compressed HTML pages that includes images)
			//	Excel	 "application/vnd.ms-excel"
			//	Excel	 "application/excel" (either of these types should work) 
			//	Text	  "text/plain"  
			Response.ContentType = "application/pdf";

			Response.AddHeader("content-disposition", "inline; filename=MyPDF.PDF");

			// Create the PDF export object.
			PdfExport pdf = new PdfExport();
			// Create a new memory stream that will hold the pdf output.
			System.IO.MemoryStream memStream = new System.IO.MemoryStream();
			// Export the report to PDF.
			pdf.Export(rpt.Document, memStream);
			// Write the PDF stream to the output stream.
			Response.BinaryWrite(memStream.ToArray());
			// Send all buffered content to the client.
			Response.End();
		}

		
		override protected void OnInit(EventArgs e)
		{
			//
			//CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
	}
}
