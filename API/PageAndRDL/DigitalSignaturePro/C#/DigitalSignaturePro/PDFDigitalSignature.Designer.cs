namespace GrapeCity.ActiveReports.Sample.DigitalSignaturePro
{
	partial class PDFDigitalSignature
	{
   
		private System.ComponentModel.IContainer components = null;

	  
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

	 
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PDFDigitalSignature));
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.lblVisibilityType = new System.Windows.Forms.Label();
			this.cmbVisibilityType = new System.Windows.Forms.ComboBox();
			this.chkTimeStamp = new System.Windows.Forms.CheckBox();
			this.pdfExportButton = new System.Windows.Forms.Button();
			this.arvMain = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			resources.ApplyResources(this.splitContainer, "splitContainer");
			this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.lblVisibilityType);
			this.splitContainer.Panel1.Controls.Add(this.cmbVisibilityType);
			this.splitContainer.Panel1.Controls.Add(this.chkTimeStamp);
			this.splitContainer.Panel1.Controls.Add(this.pdfExportButton);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.arvMain);
			// 
			// lblVisibilityType
			// 
			resources.ApplyResources(this.lblVisibilityType, "lblVisibilityType");
			this.lblVisibilityType.Name = "lblVisibilityType";
			// 
			// cmbVisibilityType
			// 
			this.cmbVisibilityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbVisibilityType.FormattingEnabled = true;
			this.cmbVisibilityType.Items.AddRange(new object[] {
            resources.GetString("cmbVisibilityType.Items"),
            resources.GetString("cmbVisibilityType.Items1"),
            resources.GetString("cmbVisibilityType.Items2"),
            resources.GetString("cmbVisibilityType.Items3")});
			resources.ApplyResources(this.cmbVisibilityType, "cmbVisibilityType");
			this.cmbVisibilityType.Name = "cmbVisibilityType";
			// 
			// chkTimeStamp
			// 
			resources.ApplyResources(this.chkTimeStamp, "chkTimeStamp");
			this.chkTimeStamp.Checked = true;
			this.chkTimeStamp.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkTimeStamp.Name = "chkTimeStamp";
			this.chkTimeStamp.UseVisualStyleBackColor = true;
			// 
			// pdfExportButton
			// 
			resources.ApplyResources(this.pdfExportButton, "pdfExportButton");
			this.pdfExportButton.Name = "pdfExportButton";
			this.pdfExportButton.UseVisualStyleBackColor = true;
			this.pdfExportButton.Click += new System.EventHandler(this.pdfExportButton_Click);
			// 
			// arvMain
			// 
			this.arvMain.CurrentPage = 0;
			resources.ApplyResources(this.arvMain, "arvMain");
			this.arvMain.Name = "arvMain";
			this.arvMain.PreviewPages = 0;
			// 
			// 
			// 
			// 
			// 
			// 
			this.arvMain.Sidebar.ParametersPanel.ContextMenu = null;
			this.arvMain.Sidebar.ParametersPanel.Width = 200;
			// 
			// 
			// 
			this.arvMain.Sidebar.SearchPanel.ContextMenu = null;
			this.arvMain.Sidebar.SearchPanel.Width = 200;
			// 
			// 
			// 
			this.arvMain.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.arvMain.Sidebar.ThumbnailsPanel.Width = 200;
			this.arvMain.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
			// 
			// 
			// 
			this.arvMain.Sidebar.TocPanel.ContextMenu = null;
			this.arvMain.Sidebar.TocPanel.Expanded = true;
			this.arvMain.Sidebar.TocPanel.Width = 200;
			this.arvMain.Sidebar.Width = 200;
			// 
			// PDFDigitalSignature
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer);
			this.Name = "PDFDigitalSignature";
			this.Load += new System.EventHandler(this.PDFDigitalSignature_Load);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel1.PerformLayout();
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.Label lblVisibilityType;
		private System.Windows.Forms.ComboBox cmbVisibilityType;
		private System.Windows.Forms.CheckBox chkTimeStamp;
		private System.Windows.Forms.Button pdfExportButton;
		private GrapeCity.ActiveReports.Viewer.Win.Viewer arvMain;
	}
}
