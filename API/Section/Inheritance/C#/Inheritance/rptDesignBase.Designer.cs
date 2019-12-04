namespace GrapeCity.ActiveReports.Sample.Inheritance
{
   
	partial class rptDesignBase
	{

 
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
			}
			base.Dispose(disposing);
		}

		#region  ActiveReports Designer generated code
	
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptDesignBase));
			this.imgLogo = new GrapeCity.ActiveReports.SectionReportModel.Picture();
			this.Label1 = new GrapeCity.ActiveReports.SectionReportModel.Label();
			this.riPageOf = new GrapeCity.ActiveReports.SectionReportModel.ReportInfo();
			this.Detail = new GrapeCity.ActiveReports.SectionReportModel.Detail();
			this.PageHeader = new GrapeCity.ActiveReports.SectionReportModel.PageHeader();
			this.PageFooter = new GrapeCity.ActiveReports.SectionReportModel.PageFooter();
			this.imgNW = new GrapeCity.ActiveReports.SectionReportModel.Picture();
			((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Label1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.riPageOf)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.imgNW)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// imgLogo
			// 
			this.imgLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			resources.ApplyResources(this.imgLogo, "imgLogo");
			this.imgLogo.ImageData = ((System.IO.Stream)(resources.GetObject("imgLogo.ImageData")));
			this.imgLogo.LineColor = System.Drawing.Color.Black;
			this.imgLogo.LineWeight = 1F;
			this.imgLogo.Name = "imgLogo";
			this.imgLogo.SizeMode = GrapeCity.ActiveReports.SectionReportModel.SizeModes.Zoom;
			// 
			// Label1
			// 
			resources.ApplyResources(this.Label1, "Label1");
			this.Label1.Name = "Label1";
			// 
			// riPageOf
			// 
			this.riPageOf.CanGrow = false;
			this.riPageOf.FormatString = "{PageNumber} ／ {PageCount}";
			resources.ApplyResources(this.riPageOf, "riPageOf");
			this.riPageOf.Name = "riPageOf";
			// 
			// Detail
			// 
			this.Detail.Height = 0.2291667F;
			this.Detail.Name = "Detail";
			// 
			// PageHeader
			// 
			this.PageHeader.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.imgLogo,
            this.Label1,
            this.imgNW});
			this.PageHeader.Height = 0.78125F;
			this.PageHeader.Name = "PageHeader";
			// 
			// PageFooter
			// 
			this.PageFooter.CanShrink = true;
			this.PageFooter.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.riPageOf});
			this.PageFooter.Height = 0.3326389F;
			this.PageFooter.Name = "PageFooter";
			// 
			// imgNW
			// 
			this.imgNW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			resources.ApplyResources(this.imgNW, "imgNW");
			this.imgNW.ImageData = ((System.IO.Stream)(resources.GetObject("imgNW.ImageData")));
			this.imgNW.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.imgNW.LineWeight = 1F;
			this.imgNW.Name = "imgNW";
			this.imgNW.SizeMode = GrapeCity.ActiveReports.SectionReportModel.SizeModes.Zoom;
			// 
			// rptDesignBase
			// 
			this.MasterReport = true;
			this.PageSettings.PaperHeight = 11F;
			this.PageSettings.PaperWidth = 8.5F;
			this.PrintWidth = 6F;
			this.Sections.Add(this.PageHeader);
			this.Sections.Add(this.Detail);
			this.Sections.Add(this.PageFooter);
			this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
            "l; font-size: 10pt; color: Black; ddo-char-set: 186", "Normal"));
			((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Label1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.riPageOf)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.imgNW)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
		#endregion

		private GrapeCity.ActiveReports.SectionReportModel.Detail Detail;
		private GrapeCity.ActiveReports.SectionReportModel.PageHeader PageHeader;
		private GrapeCity.ActiveReports.SectionReportModel.Picture imgLogo;
		private GrapeCity.ActiveReports.SectionReportModel.Label Label1;
		private GrapeCity.ActiveReports.SectionReportModel.PageFooter PageFooter;
		private GrapeCity.ActiveReports.SectionReportModel.ReportInfo riPageOf;
		private SectionReportModel.Picture imgNW;
	}
}
