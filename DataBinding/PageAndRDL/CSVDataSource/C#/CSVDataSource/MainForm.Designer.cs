namespace GrapeCity.ActiveReports.Samples.CSVDataSource
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
			this.pnlOptions = new System.Windows.Forms.Panel();
			this.btnCSV = new System.Windows.Forms.Button();
			this.rbtnHeader = new System.Windows.Forms.RadioButton();
			this.rbtnNoHeader = new System.Windows.Forms.RadioButton();
			this.rbtnHeaderTab = new System.Windows.Forms.RadioButton();
			this.rbtnNoHeaderComma = new System.Windows.Forms.RadioButton();
			this.lblFixWData = new System.Windows.Forms.Label();
			this.lblCSVDelData = new System.Windows.Forms.Label();
			this.lblCSV = new System.Windows.Forms.Label();
			this.arvMain = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this.pnlOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlOptions
			// 
			this.pnlOptions.Controls.Add(this.btnCSV);
			this.pnlOptions.Controls.Add(this.rbtnHeader);
			this.pnlOptions.Controls.Add(this.rbtnNoHeader);
			this.pnlOptions.Controls.Add(this.rbtnHeaderTab);
			this.pnlOptions.Controls.Add(this.rbtnNoHeaderComma);
			this.pnlOptions.Controls.Add(this.lblFixWData);
			this.pnlOptions.Controls.Add(this.lblCSVDelData);
			this.pnlOptions.Controls.Add(this.lblCSV);
			resources.ApplyResources(this.pnlOptions, "pnlOptions");
			this.pnlOptions.Name = "pnlOptions";
			// 
			// btnCSV
			// 
			resources.ApplyResources(this.btnCSV, "btnCSV");
			this.btnCSV.Name = "btnCSV";
			this.btnCSV.UseVisualStyleBackColor = true;
			this.btnCSV.Click += new System.EventHandler(this.btnCSV_Click);
			// 
			// rbtnHeader
			// 
			resources.ApplyResources(this.rbtnHeader, "rbtnHeader");
			this.rbtnHeader.Name = "rbtnHeader";
			this.rbtnHeader.UseVisualStyleBackColor = true;
			// 
			// rbtnNoHeader
			// 
			resources.ApplyResources(this.rbtnNoHeader, "rbtnNoHeader");
			this.rbtnNoHeader.Name = "rbtnNoHeader";
			this.rbtnNoHeader.UseVisualStyleBackColor = true;
			// 
			// rbtnHeaderTab
			// 
			resources.ApplyResources(this.rbtnHeaderTab, "rbtnHeaderTab");
			this.rbtnHeaderTab.Name = "rbtnHeaderTab";
			this.rbtnHeaderTab.UseVisualStyleBackColor = true;
			// 
			// rbtnNoHeaderComma
			// 
			resources.ApplyResources(this.rbtnNoHeaderComma, "rbtnNoHeaderComma");
			this.rbtnNoHeaderComma.Checked = true;
			this.rbtnNoHeaderComma.Name = "rbtnNoHeaderComma";
			this.rbtnNoHeaderComma.TabStop = true;
			this.rbtnNoHeaderComma.UseVisualStyleBackColor = true;
			// 
			// lblFixWData
			// 
			resources.ApplyResources(this.lblFixWData, "lblFixWData");
			this.lblFixWData.Name = "lblFixWData";
			// 
			// lblCSVDelData
			// 
			resources.ApplyResources(this.lblCSVDelData, "lblCSVDelData");
			this.lblCSVDelData.Name = "lblCSVDelData";
			// 
			// lblCSV
			// 
			resources.ApplyResources(this.lblCSV, "lblCSV");
			this.lblCSV.Name = "lblCSV";
			// 
			// arvMain
			// 
			this.arvMain.BackColor = System.Drawing.SystemColors.Control;
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
			this.arvMain.Sidebar.ParametersPanel.Enabled = false;
			this.arvMain.Sidebar.ParametersPanel.Visible = false;
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
			this.arvMain.Sidebar.TocPanel.Enabled = false;
			this.arvMain.Sidebar.TocPanel.Expanded = true;
			this.arvMain.Sidebar.TocPanel.Visible = false;
			this.arvMain.Sidebar.TocPanel.Width = 200;
			this.arvMain.Sidebar.Width = 200;
			// 
			// MainForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.arvMain);
			this.Controls.Add(this.pnlOptions);
			this.Name = "MainForm";
			this.pnlOptions.ResumeLayout(false);
			this.pnlOptions.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlOptions;
		private System.Windows.Forms.Button btnCSV;
		private System.Windows.Forms.RadioButton rbtnHeader;
		private System.Windows.Forms.RadioButton rbtnNoHeader;
		private System.Windows.Forms.RadioButton rbtnHeaderTab;
		private System.Windows.Forms.RadioButton rbtnNoHeaderComma;
		private System.Windows.Forms.Label lblFixWData;
		private System.Windows.Forms.Label lblCSVDelData;
		private System.Windows.Forms.Label lblCSV;
		private GrapeCity.ActiveReports.Viewer.Win.Viewer arvMain;

	}
}
