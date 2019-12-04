using System;
using GrapeCity.ActiveReports.SectionReportModel;

namespace GrapeCity.ActiveReports.Sample.Inheritance
{
	public partial class rptInheritChild
	{
  protected override void Dispose(bool disposing)
	{
	if (disposing)
	  {
	  }
	base.Dispose(disposing);
	}
		#region ActiveReports Designer generated code
		
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptInheritChild));
			this.TextBox = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
			this.TextBox1 = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
			this.TextBox2 = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
			this.TextBox3 = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
			this.TextBox4 = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
			this.TextBox7 = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
			this.TextBox8 = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
			this.Detail = new GrapeCity.ActiveReports.SectionReportModel.Detail();
			((System.ComponentModel.ISupportInitialize)(this.TextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TextBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TextBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TextBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TextBox4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TextBox7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TextBox8)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// TextBox
			// 
			this.TextBox.CanShrink = true;
			this.TextBox.DataField = "CustomerID";
			resources.ApplyResources(this.TextBox, "TextBox");
			this.TextBox.Name = "TextBox";
			// 
			// TextBox1
			// 
			this.TextBox1.CanShrink = true;
			this.TextBox1.DataField = "CompanyName";
			resources.ApplyResources(this.TextBox1, "TextBox1");
			this.TextBox1.Name = "TextBox1";
			// 
			// TextBox2
			// 
			this.TextBox2.CanShrink = true;
			this.TextBox2.DataField = "ContactName";
			resources.ApplyResources(this.TextBox2, "TextBox2");
			this.TextBox2.Name = "TextBox2";
			// 
			// TextBox3
			// 
			this.TextBox3.CanShrink = true;
			this.TextBox3.DataField = "ContactTitle";
			resources.ApplyResources(this.TextBox3, "TextBox3");
			this.TextBox3.Name = "TextBox3";
			// 
			// TextBox4
			// 
			this.TextBox4.CanShrink = true;
			this.TextBox4.DataField = "Address";
			resources.ApplyResources(this.TextBox4, "TextBox4");
			this.TextBox4.Name = "TextBox4";
			// 
			// TextBox7
			// 
			this.TextBox7.CanShrink = true;
			this.TextBox7.DataField = "PostalCode";
			resources.ApplyResources(this.TextBox7, "TextBox7");
			this.TextBox7.Name = "TextBox7";
			// 
			// TextBox8
			// 
			this.TextBox8.CanShrink = true;
			this.TextBox8.DataField = "Country";
			resources.ApplyResources(this.TextBox8, "TextBox8");
			this.TextBox8.Name = "TextBox8";
			// 
			// Detail
			// 
			this.Detail.CanShrink = true;
			this.Detail.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
			this.TextBox,
			this.TextBox1,
			this.TextBox2,
			this.TextBox3,
			this.TextBox4,
			this.TextBox7,
			this.TextBox8});
			this.Detail.Height = 0.6555555F;
			this.Detail.KeepTogether = true;
			this.Detail.Name = "Detail";
			// 
			// rptInheritChild
			// 
			this.MasterReport = false;
			this.PageSettings.PaperHeight = 11F;
			this.PageSettings.PaperWidth = 8.5F;
			this.PrintWidth = 6F;
			this.Sections.Add(this.Detail);
			this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
						"l; font-size: 10pt; color: Black; ddo-char-set: 186", "Normal"));
			((System.ComponentModel.ISupportInitialize)(this.TextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TextBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TextBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TextBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TextBox4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TextBox7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TextBox8)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
		#endregion


		private Detail Detail;
		private TextBox TextBox;
		private TextBox TextBox1;
		private TextBox TextBox2;
		private TextBox TextBox3;
		private TextBox TextBox4;
		private TextBox TextBox7;
		private TextBox TextBox8;

	}
}
