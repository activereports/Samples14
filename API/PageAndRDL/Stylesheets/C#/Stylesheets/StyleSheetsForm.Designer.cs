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
			this.radioButtonPageReport = new System.Windows.Forms.RadioButton();
			this.radioButtonRDLReport = new System.Windows.Forms.RadioButton();
			this.groupBoxChooseStyle = new System.Windows.Forms.GroupBox();
			this.radioButtonCustomStyle = new System.Windows.Forms.RadioButton();
			this.radioButtonEmbeddedStyle = new System.Windows.Forms.RadioButton();
			this.buttonChooseExtStyle = new System.Windows.Forms.Button();
			this.radioButtonBlueStyle = new System.Windows.Forms.RadioButton();
			this.radioButtonGreenStyle = new System.Windows.Forms.RadioButton();
			this.radioButtonOrangeStyle = new System.Windows.Forms.RadioButton();
			this.reportViewer = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this.buttonRunReport = new System.Windows.Forms.Button();
			this.groupBoxChooseReport.SuspendLayout();
			this.groupBoxChooseStyle.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBoxChooseReport
			// 
			this.groupBoxChooseReport.Controls.Add(this.radioButtonPageReport);
			this.groupBoxChooseReport.Controls.Add(this.radioButtonRDLReport);
			resources.ApplyResources(this.groupBoxChooseReport, "groupBoxChooseReport");
			this.groupBoxChooseReport.Name = "groupBoxChooseReport";
			this.groupBoxChooseReport.TabStop = false;
			// 
			// radioButtonPageReport
			// 
			resources.ApplyResources(this.radioButtonPageReport, "radioButtonPageReport");
			this.radioButtonPageReport.Checked = true;
			this.radioButtonPageReport.Name = "radioButtonPageReport";
			this.radioButtonPageReport.TabStop = true;
			this.radioButtonPageReport.UseVisualStyleBackColor = true;
			// 
			// radioButtonRDLReport
			// 
			resources.ApplyResources(this.radioButtonRDLReport, "radioButtonRDLReport");
			this.radioButtonRDLReport.Name = "radioButtonRDLReport";
			this.radioButtonRDLReport.UseVisualStyleBackColor = true;
			// 
			// groupBoxChooseStyle
			// 
			this.groupBoxChooseStyle.Controls.Add(this.radioButtonCustomStyle);
			this.groupBoxChooseStyle.Controls.Add(this.radioButtonEmbeddedStyle);
			this.groupBoxChooseStyle.Controls.Add(this.buttonChooseExtStyle);
			this.groupBoxChooseStyle.Controls.Add(this.radioButtonBlueStyle);
			this.groupBoxChooseStyle.Controls.Add(this.radioButtonGreenStyle);
			this.groupBoxChooseStyle.Controls.Add(this.radioButtonOrangeStyle);
			resources.ApplyResources(this.groupBoxChooseStyle, "groupBoxChooseStyle");
			this.groupBoxChooseStyle.Name = "groupBoxChooseStyle";
			this.groupBoxChooseStyle.TabStop = false;
			// 
			// radioButtonCustomStyle
			// 
			resources.ApplyResources(this.radioButtonCustomStyle, "radioButtonCustomStyle");
			this.radioButtonCustomStyle.Name = "radioButtonCustomStyle";
			this.radioButtonCustomStyle.UseVisualStyleBackColor = true;
			// 
			// radioButtonEmbeddedStyle
			// 
			resources.ApplyResources(this.radioButtonEmbeddedStyle, "radioButtonEmbeddedStyle");
			this.radioButtonEmbeddedStyle.Checked = true;
			this.radioButtonEmbeddedStyle.Name = "radioButtonEmbeddedStyle";
			this.radioButtonEmbeddedStyle.TabStop = true;
			this.radioButtonEmbeddedStyle.UseVisualStyleBackColor = true;
			// 
			// buttonChooseExtStyle
			// 
			resources.ApplyResources(this.buttonChooseExtStyle, "buttonChooseExtStyle");
			this.buttonChooseExtStyle.Name = "buttonChooseExtStyle";
			this.buttonChooseExtStyle.UseVisualStyleBackColor = true;
			this.buttonChooseExtStyle.Click += new System.EventHandler(this.buttonChooseExtStyle_Click);
			// 
			// radioButtonBlueStyle
			// 
			resources.ApplyResources(this.radioButtonBlueStyle, "radioButtonBlueStyle");
			this.radioButtonBlueStyle.Name = "radioButtonBlueStyle";
			this.radioButtonBlueStyle.UseVisualStyleBackColor = true;
			// 
			// radioButtonGreenStyle
			// 
			resources.ApplyResources(this.radioButtonGreenStyle, "radioButtonGreenStyle");
			this.radioButtonGreenStyle.Name = "radioButtonGreenStyle";
			this.radioButtonGreenStyle.UseVisualStyleBackColor = true;
			// 
			// radioButtonOrangeStyle
			// 
			resources.ApplyResources(this.radioButtonOrangeStyle, "radioButtonOrangeStyle");
			this.radioButtonOrangeStyle.Name = "radioButtonOrangeStyle";
			this.radioButtonOrangeStyle.UseVisualStyleBackColor = true;
			// 
			// reportViewer
			// 
			resources.ApplyResources(this.reportViewer, "reportViewer");
			this.reportViewer.BackColor = System.Drawing.SystemColors.Control;
			this.reportViewer.CurrentPage = 0;
			this.reportViewer.Name = "reportViewer";
			this.reportViewer.PreviewPages = 0;
			// 
			// 
			// 
			// 
			// 
			// 
			this.reportViewer.Sidebar.ParametersPanel.ContextMenu = null;
			this.reportViewer.Sidebar.ParametersPanel.Width = 200;
			// 
			// 
			// 
			this.reportViewer.Sidebar.SearchPanel.ContextMenu = null;
			this.reportViewer.Sidebar.SearchPanel.Width = 200;
			// 
			// 
			// 
			this.reportViewer.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.reportViewer.Sidebar.ThumbnailsPanel.Width = 200;
			this.reportViewer.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
			// 
			// 
			// 
			this.reportViewer.Sidebar.TocPanel.ContextMenu = null;
			this.reportViewer.Sidebar.TocPanel.Expanded = true;
			this.reportViewer.Sidebar.TocPanel.Width = 200;
			this.reportViewer.Sidebar.Width = 200;
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
		private System.Windows.Forms.RadioButton radioButtonOrangeStyle;
		private System.Windows.Forms.RadioButton radioButtonGreenStyle;
		private System.Windows.Forms.RadioButton radioButtonRDLReport;
		private System.Windows.Forms.Button buttonChooseExtStyle;
		private System.Windows.Forms.RadioButton radioButtonBlueStyle;
		private System.Windows.Forms.RadioButton radioButtonPageReport;
		private GrapeCity.ActiveReports.Viewer.Win.Viewer reportViewer;
		private System.Windows.Forms.Button buttonRunReport;
		private System.Windows.Forms.RadioButton radioButtonEmbeddedStyle;
		private System.Windows.Forms.RadioButton radioButtonCustomStyle;
	}
}
