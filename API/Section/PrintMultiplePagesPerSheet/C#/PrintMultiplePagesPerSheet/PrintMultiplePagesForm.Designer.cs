using System;
using GrapeCity.ActiveReports.SectionReportModel;

namespace GrapeCity.ActiveReports.Samples.PrintMultiplePagesPerSheet
{
	public partial class PrintMultiplePagesForm
	{
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
			}
			base.Dispose(disposing);
		}

  private System.Windows.Forms.PrintDialog dlgPrint;

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintMultiplePagesForm));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.lblPagenumAPI = new System.Windows.Forms.Label();
			this.cmbPageCountAPI = new System.Windows.Forms.ComboBox();
			this.btnAPIprint = new System.Windows.Forms.Button();
			this.apiViewer = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.lblPagenum = new System.Windows.Forms.Label();
			this.cmbPageCount = new System.Windows.Forms.ComboBox();
			this.btnPrint = new System.Windows.Forms.Button();
			this.arViewer = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this._printDocument = new System.Drawing.Printing.PrintDocument();
			this.dlgPrint = new System.Windows.Forms.PrintDialog();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tbAPIprint = new System.Windows.Forms.TabPage();
			this.tbClassicPrint = new System.Windows.Forms.TabPage();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tbAPIprint.SuspendLayout();
			this.tbClassicPrint.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			resources.ApplyResources(this.splitContainer1, "splitContainer1");
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.lblPagenumAPI);
			this.splitContainer1.Panel1.Controls.Add(this.cmbPageCountAPI);
			this.splitContainer1.Panel1.Controls.Add(this.btnAPIprint);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.apiViewer);
			// 
			// lblPagenumAPI
			// 
			resources.ApplyResources(this.lblPagenumAPI, "lblPagenumAPI");
			this.lblPagenumAPI.BackColor = System.Drawing.SystemColors.Control;
			this.lblPagenumAPI.Name = "lblPagenumAPI";
			// 
			// cmbPageCountAPI
			// 
			this.cmbPageCountAPI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPageCountAPI.FormattingEnabled = true;
			this.cmbPageCountAPI.Items.AddRange(new object[] {
            resources.GetString("cmbPageCountAPI.Items"),
            resources.GetString("cmbPageCountAPI.Items1"),
            resources.GetString("cmbPageCountAPI.Items2")});
			resources.ApplyResources(this.cmbPageCountAPI, "cmbPageCountAPI");
			this.cmbPageCountAPI.Name = "cmbPageCountAPI";
			// 
			// btnAPIprint
			// 
			this.btnAPIprint.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			resources.ApplyResources(this.btnAPIprint, "btnAPIprint");
			this.btnAPIprint.Name = "btnAPIprint";
			this.btnAPIprint.UseVisualStyleBackColor = false;
			this.btnAPIprint.Click += new System.EventHandler(this.btnAPIprint_Click);
			// 
			// apiViewer
			// 
			this.apiViewer.CurrentPage = 0;
			resources.ApplyResources(this.apiViewer, "apiViewer");
			this.apiViewer.Name = "apiViewer";
			this.apiViewer.PreviewPages = 0;
			// 
			// 
			// 
			// 
			// 
			// 
			this.apiViewer.Sidebar.ParametersPanel.ContextMenu = null;
			this.apiViewer.Sidebar.ParametersPanel.Enabled = false;
			this.apiViewer.Sidebar.ParametersPanel.Visible = false;
			this.apiViewer.Sidebar.ParametersPanel.Width = 200;
			// 
			// 
			// 
			this.apiViewer.Sidebar.SearchPanel.ContextMenu = null;
			this.apiViewer.Sidebar.SearchPanel.Width = 200;
			// 
			// 
			// 
			this.apiViewer.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.apiViewer.Sidebar.ThumbnailsPanel.Width = 200;
			this.apiViewer.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
			// 
			// 
			// 
			this.apiViewer.Sidebar.TocPanel.ContextMenu = null;
			this.apiViewer.Sidebar.TocPanel.Enabled = false;
			this.apiViewer.Sidebar.TocPanel.Expanded = true;
			this.apiViewer.Sidebar.TocPanel.Visible = false;
			this.apiViewer.Sidebar.TocPanel.Width = 200;
			this.apiViewer.Sidebar.Width = 200;
			// 
			// splitContainer2
			// 
			resources.ApplyResources(this.splitContainer2, "splitContainer2");
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.lblPagenum);
			this.splitContainer2.Panel1.Controls.Add(this.cmbPageCount);
			this.splitContainer2.Panel1.Controls.Add(this.btnPrint);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.arViewer);
			// 
			// lblPagenum
			// 
			resources.ApplyResources(this.lblPagenum, "lblPagenum");
			this.lblPagenum.BackColor = System.Drawing.SystemColors.Control;
			this.lblPagenum.Name = "lblPagenum";
			// 
			// cmbPageCount
			// 
			this.cmbPageCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPageCount.FormattingEnabled = true;
			this.cmbPageCount.Items.AddRange(new object[] {
            resources.GetString("cmbPageCount.Items"),
            resources.GetString("cmbPageCount.Items1"),
            resources.GetString("cmbPageCount.Items2"),
            resources.GetString("cmbPageCount.Items3"),
            resources.GetString("cmbPageCount.Items4")});
			resources.ApplyResources(this.cmbPageCount, "cmbPageCount");
			this.cmbPageCount.Name = "cmbPageCount";
			// 
			// btnPrint
			// 
			this.btnPrint.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			resources.ApplyResources(this.btnPrint, "btnPrint");
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.UseVisualStyleBackColor = false;
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
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
			this.arViewer.Sidebar.ParametersPanel.Enabled = false;
			this.arViewer.Sidebar.ParametersPanel.Visible = false;
			this.arViewer.Sidebar.ParametersPanel.Width = 200;
			// 
			// 
			// 
			this.arViewer.Sidebar.SearchPanel.ContextMenu = null;
			this.arViewer.Sidebar.SearchPanel.Width = 200;
			// 
			// 
			// 
			this.arViewer.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.arViewer.Sidebar.ThumbnailsPanel.Width = 200;
			this.arViewer.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
			// 
			// 
			// 
			this.arViewer.Sidebar.TocPanel.ContextMenu = null;
			this.arViewer.Sidebar.TocPanel.Enabled = false;
			this.arViewer.Sidebar.TocPanel.Expanded = true;
			this.arViewer.Sidebar.TocPanel.Visible = false;
			this.arViewer.Sidebar.TocPanel.Width = 200;
			this.arViewer.Sidebar.Width = 200;
			// 
			// _printDocument
			// 
			this._printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument_PrintPage);
			// 
			// dlgPrint
			// 
			this.dlgPrint.Document = this._printDocument;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tbAPIprint);
			this.tabControl.Controls.Add(this.tbClassicPrint);
			resources.ApplyResources(this.tabControl, "tabControl");
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			// 
			// tbAPIprint
			// 
			this.tbAPIprint.Controls.Add(this.splitContainer1);
			resources.ApplyResources(this.tbAPIprint, "tbAPIprint");
			this.tbAPIprint.Name = "tbAPIprint";
			this.tbAPIprint.UseVisualStyleBackColor = true;
			// 
			// tbClassicPrint
			// 
			this.tbClassicPrint.Controls.Add(this.splitContainer2);
			resources.ApplyResources(this.tbClassicPrint, "tbClassicPrint");
			this.tbClassicPrint.Name = "tbClassicPrint";
			this.tbClassicPrint.UseVisualStyleBackColor = true;
			// 
			// PrintMultiplePagesForm
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.tabControl);
			this.Name = "PrintMultiplePagesForm";
			this.Load += new System.EventHandler(this.PrintMultiplePagesPerSheetForm_Load);
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
			this.tabControl.ResumeLayout(false);
			this.tbAPIprint.ResumeLayout(false);
			this.tbClassicPrint.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tbAPIprint;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Label lblPagenumAPI;
		private System.Windows.Forms.ComboBox cmbPageCountAPI;
		private System.Windows.Forms.Button btnAPIprint;
		private System.Windows.Forms.TabPage tbClassicPrint;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private Viewer.Win.Viewer arViewer;
		private System.Windows.Forms.Label lblPagenum;
		private System.Windows.Forms.ComboBox cmbPageCount;
		private System.Windows.Forms.Button btnPrint;
		private Viewer.Win.Viewer apiViewer;



	}
}
