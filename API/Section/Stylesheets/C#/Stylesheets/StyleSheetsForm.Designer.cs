using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Controls;
using GrapeCity.ActiveReports.SectionReportModel;
using GrapeCity.ActiveReports.Document.Section;

namespace GrapeCity.ActiveReports.Samples.StyleSheets
{
	partial class StyleSheetsForm
	{
		/// <summary>
		///Required designer variable. 
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///Clean up any resources being used. 
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}


		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StyleSheetsForm));
			this.groupBoxChooseReport = new System.Windows.Forms.GroupBox();
			this.radioButtonCategoriesReport = new System.Windows.Forms.RadioButton();
			this.radioButtonProductListReport = new System.Windows.Forms.RadioButton();
			this.groupBoxChooseStyle = new System.Windows.Forms.GroupBox();
			this.buttonChooseExtStyle = new System.Windows.Forms.Button();
			this.radioButtonExternalStyleSheet = new System.Windows.Forms.RadioButton();
			this.radioButtonColoredStyle = new System.Windows.Forms.RadioButton();
			this.radioButtonClassicStyle = new System.Windows.Forms.RadioButton();
			this.reportViewer = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this.buttonRunReport = new System.Windows.Forms.Button();
			this.groupBoxChooseReport.SuspendLayout();
			this.groupBoxChooseStyle.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBoxChooseReport
			// 
			resources.ApplyResources(this.groupBoxChooseReport, "groupBoxChooseReport");
			this.groupBoxChooseReport.Controls.Add(this.radioButtonCategoriesReport);
			this.groupBoxChooseReport.Controls.Add(this.radioButtonProductListReport);
			this.groupBoxChooseReport.Name = "groupBoxChooseReport";
			this.groupBoxChooseReport.TabStop = false;
			// 
			// radioButtonCategoriesReport
			// 
			resources.ApplyResources(this.radioButtonCategoriesReport, "radioButtonCategoriesReport");
			this.radioButtonCategoriesReport.Checked = true;
			this.radioButtonCategoriesReport.Name = "radioButtonCategoriesReport";
			this.radioButtonCategoriesReport.TabStop = true;
			this.radioButtonCategoriesReport.UseVisualStyleBackColor = true;
			// 
			// radioButtonProductListReport
			// 
			resources.ApplyResources(this.radioButtonProductListReport, "radioButtonProductListReport");
			this.radioButtonProductListReport.Name = "radioButtonProductListReport";
			this.radioButtonProductListReport.UseVisualStyleBackColor = true;
			// 
			// groupBoxChooseStyle
			// 
			resources.ApplyResources(this.groupBoxChooseStyle, "groupBoxChooseStyle");
			this.groupBoxChooseStyle.Controls.Add(this.buttonChooseExtStyle);
			this.groupBoxChooseStyle.Controls.Add(this.radioButtonExternalStyleSheet);
			this.groupBoxChooseStyle.Controls.Add(this.radioButtonColoredStyle);
			this.groupBoxChooseStyle.Controls.Add(this.radioButtonClassicStyle);
			this.groupBoxChooseStyle.Name = "groupBoxChooseStyle";
			this.groupBoxChooseStyle.TabStop = false;
			// 
			// buttonChooseExtStyle
			// 
			resources.ApplyResources(this.buttonChooseExtStyle, "buttonChooseExtStyle");
			this.buttonChooseExtStyle.Name = "buttonChooseExtStyle";
			this.buttonChooseExtStyle.UseVisualStyleBackColor = true;
			this.buttonChooseExtStyle.Click += new System.EventHandler(this.buttonChooseExtStyle_Click);
			// 
			// radioButtonExternalStyleSheet
			// 
			resources.ApplyResources(this.radioButtonExternalStyleSheet, "radioButtonExternalStyleSheet");
			this.radioButtonExternalStyleSheet.Name = "radioButtonExternalStyleSheet";
			this.radioButtonExternalStyleSheet.UseVisualStyleBackColor = true;
			// 
			// radioButtonColoredStyle
			// 
			resources.ApplyResources(this.radioButtonColoredStyle, "radioButtonColoredStyle");
			this.radioButtonColoredStyle.Name = "radioButtonColoredStyle";
			this.radioButtonColoredStyle.UseVisualStyleBackColor = true;
			// 
			// radioButtonClassicStyle
			// 
			resources.ApplyResources(this.radioButtonClassicStyle, "radioButtonClassicStyle");
			this.radioButtonClassicStyle.Checked = true;
			this.radioButtonClassicStyle.Name = "radioButtonClassicStyle";
			this.radioButtonClassicStyle.TabStop = true;
			this.radioButtonClassicStyle.UseVisualStyleBackColor = true;
			// 
			// reportViewer
			// 
			resources.ApplyResources(this.reportViewer, "reportViewer");
			this.reportViewer.BackColor = System.Drawing.SystemColors.Control;
			this.reportViewer.CurrentPage = 0;
			this.reportViewer.HyperlinkBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.reportViewer.HyperlinkForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.reportViewer.Name = "reportViewer";
			this.reportViewer.PagesBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.reportViewer.PreviewPages = 0;
			this.reportViewer.SearchResultsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.reportViewer.SearchResultsForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(139)))));
			// 
			// 
			// 
			// 
			// 
			// 
			this.reportViewer.Sidebar.ParametersPanel.ContextMenu = null;
			this.reportViewer.Sidebar.ParametersPanel.Width = 180;
			// 
			// 
			// 
			this.reportViewer.Sidebar.SearchPanel.ContextMenu = null;
			this.reportViewer.Sidebar.SearchPanel.Width = 180;
			this.reportViewer.Sidebar.SelectedIndex = 0;
			// 
			// 
			// 
			this.reportViewer.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.reportViewer.Sidebar.ThumbnailsPanel.Width = 180;
			// 
			// 
			// 
			this.reportViewer.Sidebar.TocPanel.ContextMenu = null;
			this.reportViewer.Sidebar.TocPanel.Width = 180;
			this.reportViewer.Sidebar.Width = 180;
			this.reportViewer.SplitView = false;
			this.reportViewer.ViewType = GrapeCity.Viewer.Common.Model.ViewType.SinglePage;
			this.reportViewer.Zoom = 1F;
			// 
			// buttonRunReport
			// 
			resources.ApplyResources(this.buttonRunReport, "buttonRunReport");
			this.buttonRunReport.Name = "buttonRunReport";
			this.buttonRunReport.UseVisualStyleBackColor = true;
			this.buttonRunReport.Click += new System.EventHandler(this.buttonRunReport_Click);
			// 
			// StyleSheetsForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.buttonRunReport);
			this.Controls.Add(this.reportViewer);
			this.Controls.Add(this.groupBoxChooseStyle);
			this.Controls.Add(this.groupBoxChooseReport);
			this.Name = "StyleSheetsForm";
			this.groupBoxChooseReport.ResumeLayout(false);
			this.groupBoxChooseReport.PerformLayout();
			this.groupBoxChooseStyle.ResumeLayout(false);
			this.groupBoxChooseStyle.PerformLayout();
			this.ResumeLayout(false);

		}

		private System.Windows.Forms.GroupBox groupBoxChooseReport;
		private System.Windows.Forms.GroupBox groupBoxChooseStyle;
		private System.Windows.Forms.RadioButton radioButtonClassicStyle;
		private System.Windows.Forms.RadioButton radioButtonColoredStyle;
		private System.Windows.Forms.RadioButton radioButtonProductListReport;
		private System.Windows.Forms.Button buttonChooseExtStyle;
		private System.Windows.Forms.RadioButton radioButtonExternalStyleSheet;
		private System.Windows.Forms.RadioButton radioButtonCategoriesReport;
		private GrapeCity.ActiveReports.Viewer.Win.Viewer reportViewer;
		private System.Windows.Forms.Button buttonRunReport;
	}
}
