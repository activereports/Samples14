using System;
using System.Web;

namespace GrapeCity.ActiveReports.Samples.Web.CustomPreview
{
	/// <summary>
	/// CustomExportHtml - showcases HTML Export over the web.
	/// </summary>
	public partial class CustomExportHtml : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			// Tell the browser and the "network" that this resulting data of this page should be cached since this could be a dynamic report that changes upon each request.
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			// Tell the browser this is an HTML document so it will use an appropriate viewer.
			Response.ContentType = "text/html";

			Response.Redirect("Reports/NwindLabels.rpx");
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
