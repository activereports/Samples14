namespace GrapeCity.ActiveReports.Sample.Inheritance
{
	/// <summary>
 
	/// </summary>
	partial class rptDesignChild
	{
		/// <summary>

		/// </summary>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptDesignChild));
			GrapeCity.ActiveReports.Data.OleDBDataSource oleDBDataSource1 = new GrapeCity.ActiveReports.Data.OleDBDataSource();
			this.txtCustomerID1 = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
			this.txtCompanyName1 = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
			this.txtContactName1 = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
			this.txtCountry1 = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
			this.txtAddress1 = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
			this.Detail = new GrapeCity.ActiveReports.SectionReportModel.Detail();
			((System.ComponentModel.ISupportInitialize)(this.txtCustomerID1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCompanyName1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtContactName1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCountry1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtAddress1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// txtCustomerID1
			// 
			this.txtCustomerID1.CanShrink = true;
			this.txtCustomerID1.DataField = "CustomerID";
			resources.ApplyResources(this.txtCustomerID1, "txtCustomerID1");
			this.txtCustomerID1.Name = "txtCustomerID1";
			// 
			// txtCompanyName1
			// 
			this.txtCompanyName1.CanShrink = true;
			this.txtCompanyName1.DataField = "CompanyName";
			resources.ApplyResources(this.txtCompanyName1, "txtCompanyName1");
			this.txtCompanyName1.Name = "txtCompanyName1";
			// 
			// txtContactName1
			// 
			this.txtContactName1.CanShrink = true;
			this.txtContactName1.DataField = "ContactName";
			resources.ApplyResources(this.txtContactName1, "txtContactName1");
			this.txtContactName1.Name = "txtContactName1";
			// 
			// txtCountry1
			// 
			this.txtCountry1.CanShrink = true;
			this.txtCountry1.DataField = "Country";
			resources.ApplyResources(this.txtCountry1, "txtCountry1");
			this.txtCountry1.Name = "txtCountry1";
			// 
			// txtAddress1
			// 
			this.txtAddress1.CanShrink = true;
			this.txtAddress1.DataField = "Address";
			resources.ApplyResources(this.txtAddress1, "txtAddress1");
			this.txtAddress1.Name = "txtAddress1";
			// 
			// Detail
			// 
			this.Detail.CanShrink = true;
			this.Detail.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
			this.txtCustomerID1,
			this.txtCompanyName1,
			this.txtContactName1,
			this.txtCountry1,
			this.txtAddress1});
			this.Detail.Height = 0.375F;
			this.Detail.KeepTogether = true;
			this.Detail.Name = "Detail";
			// 
			// rptDesignChild
			// 

			this.DataSource = oleDBDataSource1;
			this.MasterReport = false;
			this.PageSettings.PaperHeight = 11F;
			this.PageSettings.PaperWidth = 8.5F;
			this.Sections.Add(this.Detail);
			this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-style: normal; text-decoration: none; font-weight: normal; font-size: 10pt; " +
						"color: Black; font-family: MS UI Gothic; ddo-char-set: 186", "Normal"));
			((System.ComponentModel.ISupportInitialize)(this.txtCustomerID1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCompanyName1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtContactName1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCountry1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtAddress1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
		#endregion

		private GrapeCity.ActiveReports.SectionReportModel.Detail Detail;
		private GrapeCity.ActiveReports.SectionReportModel.TextBox txtCustomerID1;
		private GrapeCity.ActiveReports.SectionReportModel.TextBox txtCompanyName1;
		private GrapeCity.ActiveReports.SectionReportModel.TextBox txtContactName1;
		private GrapeCity.ActiveReports.SectionReportModel.TextBox txtCountry1;
		private GrapeCity.ActiveReports.SectionReportModel.TextBox txtAddress1;
 
	 
	   

	 
	   
	 
	}
}
