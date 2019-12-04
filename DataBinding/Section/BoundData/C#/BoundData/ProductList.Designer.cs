namespace GrapeCity.ActiveReports.Samples.BoundData
{
	/// <summary>
	/// Summary description for ProductList.
	/// </summary>
	partial class ProductList
	{
		private GrapeCity.ActiveReports.SectionReportModel.PageHeader pageHeader;
		private GrapeCity.ActiveReports.SectionReportModel.Detail detail;
		private GrapeCity.ActiveReports.SectionReportModel.PageFooter pageFooter;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
			}
			base.Dispose(disposing);
		}

		#region ActiveReport Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductList));
			this.label1 = new GrapeCity.ActiveReports.SectionReportModel.Label();
			this.label2 = new GrapeCity.ActiveReports.SectionReportModel.Label();
			this.label3 = new GrapeCity.ActiveReports.SectionReportModel.Label();
			this.label4 = new GrapeCity.ActiveReports.SectionReportModel.Label();
			this.label5 = new GrapeCity.ActiveReports.SectionReportModel.Label();
			this.pageHeader = new GrapeCity.ActiveReports.SectionReportModel.PageHeader();
			this.shape1 = new GrapeCity.ActiveReports.SectionReportModel.Shape();
			this.line1 = new GrapeCity.ActiveReports.SectionReportModel.Line();
			this.detail = new GrapeCity.ActiveReports.SectionReportModel.Detail();
			this.textBoxProductName = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
			this.textBoxQuantityPerUnit = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
			this.textBoxUnitPrice = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
			this.textBoxUnitsOnOrder = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
			this.pageFooter = new GrapeCity.ActiveReports.SectionReportModel.PageFooter();
			((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.label4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.label5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textBoxProductName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textBoxQuantityPerUnit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textBoxUnitPrice)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textBoxUnitsOnOrder)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// label1
			// 
			this.label1.Height = 0.387F;
			this.label1.HyperLink = null;
			this.label1.Left = 0.428F;
			this.label1.Name = "label1";
			this.label1.Style = "font-family: Arial Narrow; font-size: 24pt";
			this.label1.Text = "Products list";
			this.label1.Top = 0.281F;
			this.label1.Width = 2.042F;
			// 
			// label2
			// 
			this.label2.Height = 0.2F;
			this.label2.HyperLink = null;
			this.label2.Left = 0.177F;
			this.label2.Name = "label2";
			this.label2.Style = "";
			this.label2.Text = "Product Name";
			this.label2.Top = 1.031F;
			this.label2.Width = 1.771F;
			// 
			// label3
			// 
			this.label3.Height = 0.2F;
			this.label3.HyperLink = null;
			this.label3.Left = 1.948F;
			this.label3.Name = "label3";
			this.label3.Style = "";
			this.label3.Text = "Quantity Per Unit";
			this.label3.Top = 1.031F;
			this.label3.Width = 1.562F;
			// 
			// label4
			// 
			this.label4.Height = 0.2F;
			this.label4.HyperLink = null;
			this.label4.Left = 4.854F;
			this.label4.Name = "label4";
			this.label4.Style = "";
			this.label4.Text = "Units On Order";
			this.label4.Top = 1.031F;
			this.label4.Width = 1.344001F;
			// 
			// label5
			// 
			this.label5.Height = 0.2F;
			this.label5.HyperLink = null;
			this.label5.Left = 3.51F;
			this.label5.Name = "label5";
			this.label5.Style = "";
			this.label5.Text = "Unit Price";
			this.label5.Top = 1.031F;
			this.label5.Width = 1.344F;
			// 
			// pageHeader
			// 
			this.pageHeader.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.shape1,
            this.label1,
            this.label2,
            this.label3,
            this.label4,
            this.line1,
            this.label5});
			this.pageHeader.Height = 1.385417F;
			this.pageHeader.Name = "pageHeader";
			// 
			// shape1
			// 
			this.shape1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(164)))), ((int)(((byte)(96)))));
			this.shape1.Height = 0.8229167F;
			this.shape1.Left = 0.08333334F;
			this.shape1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.shape1.LineStyle = GrapeCity.ActiveReports.SectionReportModel.LineStyle.Transparent;
			this.shape1.Name = "shape1";
			this.shape1.RoundingRadius = new GrapeCity.ActiveReports.Controls.CornersRadius(50F, null, null, null, null);
			this.shape1.Style = GrapeCity.ActiveReports.SectionReportModel.ShapeType.RoundRect;
			this.shape1.Top = 0.07291666F;
			this.shape1.Width = 6.114667F;
			// 
			// line1
			// 
			this.line1.Height = 0F;
			this.line1.Left = 0.1979167F;
			this.line1.LineWeight = 1F;
			this.line1.Name = "line1";
			this.line1.Top = 1.3125F;
			this.line1.Width = 6F;
			this.line1.X1 = 0.1979167F;
			this.line1.X2 = 6.197917F;
			this.line1.Y1 = 1.3125F;
			this.line1.Y2 = 1.3125F;
			// 
			// detail
			// 
			this.detail.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.textBoxProductName,
            this.textBoxQuantityPerUnit,
            this.textBoxUnitPrice,
            this.textBoxUnitsOnOrder});
			this.detail.Height = 0.2604167F;
			this.detail.Name = "detail";
			// 
			// textBoxProductName
			// 
			this.textBoxProductName.DataField = "ProductName";
			this.textBoxProductName.Height = 0.1979167F;
			this.textBoxProductName.Left = 0.177F;
			this.textBoxProductName.Name = "textBoxProductName";
			this.textBoxProductName.Text = null;
			this.textBoxProductName.Top = 0F;
			this.textBoxProductName.Width = 1.771F;
			// 
			// textBoxQuantityPerUnit
			// 
			this.textBoxQuantityPerUnit.DataField = "QuantityPerUnit";
			this.textBoxQuantityPerUnit.Height = 0.1979167F;
			this.textBoxQuantityPerUnit.Left = 1.948F;
			this.textBoxQuantityPerUnit.Name = "textBoxQuantityPerUnit";
			this.textBoxQuantityPerUnit.Text = "QuantityPerUnit";
			this.textBoxQuantityPerUnit.Top = 0F;
			this.textBoxQuantityPerUnit.Width = 1.562F;
			// 
			// textBoxUnitPrice
			// 
			this.textBoxUnitPrice.DataField = "UnitPrice";
			this.textBoxUnitPrice.Height = 0.1979167F;
			this.textBoxUnitPrice.Left = 3.51F;
			this.textBoxUnitPrice.Name = "textBoxUnitPrice";
			this.textBoxUnitPrice.Text = "UnitPrice";
			this.textBoxUnitPrice.Top = 0F;
			this.textBoxUnitPrice.Width = 1.344F;
			// 
			// textBoxUnitsOnOrder
			// 
			this.textBoxUnitsOnOrder.DataField = "UnitsOnOrder";
			this.textBoxUnitsOnOrder.Height = 0.1979167F;
			this.textBoxUnitsOnOrder.Left = 4.854F;
			this.textBoxUnitsOnOrder.Name = "textBoxUnitsOnOrder";
			this.textBoxUnitsOnOrder.Text = "UnitsOnOrder";
			this.textBoxUnitsOnOrder.Top = 0F;
			this.textBoxUnitsOnOrder.Width = 1.344F;
			// 
			// pageFooter
			// 
			this.pageFooter.Height = 0F;
			this.pageFooter.Name = "pageFooter";
			// 
			// ProductList
			// 
			this.MasterReport = false;
			this.PageSettings.PaperHeight = 11F;
			this.PageSettings.PaperWidth = 8.5F;
			this.PrintWidth = 6.271F;
			this.Sections.Add(this.pageHeader);
			this.Sections.Add(this.detail);
			this.Sections.Add(this.pageFooter);
			this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
            "l; font-size: 10pt; color: Black", "Normal"));
			this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
			this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
            "lic", "Heading2", "Normal"));
			this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
			((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.label4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.label5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textBoxProductName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textBoxQuantityPerUnit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textBoxUnitPrice)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textBoxUnitsOnOrder)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
		#endregion

		private SectionReportModel.Shape shape1;
		private SectionReportModel.TextBox textBoxProductName;
		private SectionReportModel.TextBox textBoxQuantityPerUnit;
		private SectionReportModel.TextBox textBoxUnitPrice;
		private SectionReportModel.TextBox textBoxUnitsOnOrder;
		private SectionReportModel.Line line1;
		private SectionReportModel.Label label1;
		private SectionReportModel.Label label2;
		private SectionReportModel.Label label3;
		private SectionReportModel.Label label4;
		private SectionReportModel.Label label5;
	}
}
