using System.Windows.Forms;
using GrapeCity.ActiveReports.Export.Pdf.Page;
using GrapeCity.ActiveReports.Samples.Export.Rendering.Pdf;
using GrapeCity.ActiveReports.Samples.Export.Rendering.Properties;

namespace GrapeCity.ActiveReports.Samples.Export.Rendering
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.saveAsButton = new System.Windows.Forms.Button();
			this.reports = new System.Windows.Forms.ComboBox();
			this.exportButton = new System.Windows.Forms.Button();
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.pdfTime = new System.Windows.Forms.Label();
			this.arTime = new System.Windows.Forms.Label();
			this.pdfLabel = new System.Windows.Forms.Label();
			this.arLabel = new System.Windows.Forms.Label();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.arViewer = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this.pdfViewer = new PdfiumViewer.PdfViewer();
			this.saveAsPdf = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
			resources.ApplyResources(this.splitContainer1, "splitContainer1");
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.radioButton2);
			this.splitContainer1.Panel1.Controls.Add(this.radioButton1);
			this.splitContainer1.Panel1.Controls.Add(this.saveAsButton);
			this.splitContainer1.Panel1.Controls.Add(this.reports);
			this.splitContainer1.Panel1.Controls.Add(this.exportButton);
			this.splitContainer1.Panel1.Controls.Add(this.propertyGrid);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			// 
			// radioButton2
			// 
			resources.ApplyResources(this.radioButton2, "radioButton2");
			this.radioButton2.Checked = true;
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.TabStop = true;
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.Click += new System.EventHandler(this.exports_SelectedIndexChanged);
			// 
			// radioButton1
			// 
			resources.ApplyResources(this.radioButton1, "radioButton1");
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.Click += new System.EventHandler(this.exports_SelectedIndexChanged);
			// 
			// saveAsButton
			// 
			resources.ApplyResources(this.saveAsButton, "saveAsButton");
			this.saveAsButton.Name = "saveAsButton";
			this.saveAsButton.UseVisualStyleBackColor = true;
			this.saveAsButton.Click += new System.EventHandler(this.saveAsButton_Click);
			// 
			// reports
			// 
			resources.ApplyResources(this.reports, "reports");
			this.reports.FormattingEnabled = true;
			this.reports.Name = "reports";
			this.reports.SelectedIndexChanged += new System.EventHandler(this.reports_SelectedIndexChanged);
			// 
			// exportButton
			// 
			resources.ApplyResources(this.exportButton, "exportButton");
			this.exportButton.Name = "exportButton";
			this.exportButton.UseVisualStyleBackColor = true;
			this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
			// 
			// propertyGrid
			// 
			resources.ApplyResources(this.propertyGrid, "propertyGrid");
			this.propertyGrid.LineColor = System.Drawing.SystemColors.ControlDark;
			this.propertyGrid.Name = "propertyGrid";
			// 
			// splitContainer2
			// 
			resources.ApplyResources(this.splitContainer2, "splitContainer2");
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.pdfTime);
			this.splitContainer2.Panel1.Controls.Add(this.arTime);
			this.splitContainer2.Panel1.Controls.Add(this.pdfLabel);
			this.splitContainer2.Panel1.Controls.Add(this.arLabel);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
			// 
			// pdfTime
			// 
			resources.ApplyResources(this.pdfTime, "pdfTime");
			this.pdfTime.Name = "pdfTime";
			// 
			// arTime
			// 
			resources.ApplyResources(this.arTime, "arTime");
			this.arTime.Name = "arTime";
			// 
			// pdfLabel
			// 
			resources.ApplyResources(this.pdfLabel, "pdfLabel");
			this.pdfLabel.Name = "pdfLabel";
			// 
			// arLabel
			// 
			resources.ApplyResources(this.arLabel, "arLabel");
			this.arLabel.Name = "arLabel";
			// 
			// splitContainer3
			// 
			resources.ApplyResources(this.splitContainer3, "splitContainer3");
			this.splitContainer3.Name = "splitContainer3";
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.arViewer);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.pdfViewer);
			// 
			// arViewer
			// 
			this.arViewer.CurrentPage = 0;
			resources.ApplyResources(this.arViewer, "arViewer");
			this.arViewer.Name = "arViewer";
			this.arViewer.PreviewPages = 0;
			// 
			// 
			// 
			// 
			// 
			// 
			this.arViewer.Sidebar.ParametersPanel.ContextMenu = null;
			this.arViewer.Sidebar.ParametersPanel.Text = "Parameters";
			this.arViewer.Sidebar.ParametersPanel.Width = 200;
			// 
			// 
			// 
			this.arViewer.Sidebar.SearchPanel.ContextMenu = null;
			this.arViewer.Sidebar.SearchPanel.Text = "Search results";
			this.arViewer.Sidebar.SearchPanel.Width = 200;
			// 
			// 
			// 
			this.arViewer.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.arViewer.Sidebar.ThumbnailsPanel.Text = "Page thumbnails";
			this.arViewer.Sidebar.ThumbnailsPanel.Width = 200;
			this.arViewer.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
			// 
			// 
			// 
			this.arViewer.Sidebar.TocPanel.ContextMenu = null;
			this.arViewer.Sidebar.TocPanel.Expanded = true;
			this.arViewer.Sidebar.TocPanel.Text = "Document map";
			this.arViewer.Sidebar.TocPanel.Width = 200;
			this.arViewer.Sidebar.Width = 200;
			this.arViewer.ViewType = GrapeCity.Viewer.Common.Model.ViewType.Continuous;
			this.arViewer.Zoom = -1F;
			// 
			// pdfViewer
			// 
			resources.ApplyResources(this.pdfViewer, "pdfViewer");
			this.pdfViewer.Name = "pdfViewer";
			this.pdfViewer.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitWidth;
			// 
			// saveAsPdf
			// 
			resources.ApplyResources(this.saveAsPdf, "saveAsPdf");
			this.saveAsPdf.RestoreDirectory = true;
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private PdfiumViewer.PdfViewer pdfViewer;
		private Viewer.Win.Viewer arViewer;
		private System.Windows.Forms.Button saveAsButton;
		private System.Windows.Forms.ComboBox reports;
		private System.Windows.Forms.Button exportButton;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.Label pdfTime;
		private System.Windows.Forms.Label arTime;
		private System.Windows.Forms.Label pdfLabel;
		private System.Windows.Forms.Label arLabel;
		private System.Windows.Forms.SaveFileDialog saveAsPdf;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton radioButton1;
	}
}
