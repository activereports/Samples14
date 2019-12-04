namespace GrapeCity.ActiveReports.Samples.WordExport
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
			this.saveAsButton = new System.Windows.Forms.Button();
			this.exports = new System.Windows.Forms.ComboBox();
			this.reports = new System.Windows.Forms.ComboBox();
			this.viewer = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
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
			this.splitContainer1.Panel1.Controls.Add(this.saveAsButton);
			this.splitContainer1.Panel1.Controls.Add(this.exports);
			this.splitContainer1.Panel1.Controls.Add(this.reports);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.viewer);
			// 
			// saveAsButton
			// 
			resources.ApplyResources(this.saveAsButton, "saveAsButton");
			this.saveAsButton.Name = "saveAsButton";
			this.saveAsButton.UseVisualStyleBackColor = true;
			this.saveAsButton.Click += new System.EventHandler(this.saveAsButton_Click);
			// 
			// exports
			// 
			resources.ApplyResources(this.exports, "exports");
			this.exports.FormattingEnabled = true;
			this.exports.Name = "exports";
			this.exports.SelectedIndexChanged += new System.EventHandler(this.exports_SelectedIndexChanged);
			// 
			// reports
			// 
			resources.ApplyResources(this.reports, "reports");
			this.reports.FormattingEnabled = true;
			this.reports.Name = "reports";
			this.reports.SelectedIndexChanged += new System.EventHandler(this.reports_SelectedIndexChanged);
			// 
			// viewer
			// 
			this.viewer.CurrentPage = 0;
			resources.ApplyResources(this.viewer, "viewer");
			this.viewer.Name = "viewer";
			this.viewer.PreviewPages = 0;
			// 
			// 
			// 
			// 
			// 
			// 
			this.viewer.Sidebar.ParametersPanel.ContextMenu = null;
			this.viewer.Sidebar.ParametersPanel.Text = "Parameters";
			this.viewer.Sidebar.ParametersPanel.Width = 200;
			// 
			// 
			// 
			this.viewer.Sidebar.SearchPanel.ContextMenu = null;
			this.viewer.Sidebar.SearchPanel.Text = "Search results";
			this.viewer.Sidebar.SearchPanel.Width = 200;
			// 
			// 
			// 
			this.viewer.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.viewer.Sidebar.ThumbnailsPanel.Text = "Page thumbnails";
			this.viewer.Sidebar.ThumbnailsPanel.Width = 200;
			this.viewer.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
			// 
			// 
			// 
			this.viewer.Sidebar.TocPanel.ContextMenu = null;
			this.viewer.Sidebar.TocPanel.Expanded = true;
			this.viewer.Sidebar.TocPanel.Text = "Document map";
			this.viewer.Sidebar.TocPanel.Width = 200;
			this.viewer.Sidebar.Width = 200;
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "MainForm";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private GrapeCity.ActiveReports.Viewer.Win.Viewer viewer;
		private System.Windows.Forms.Button saveAsButton;
		private System.Windows.Forms.ComboBox exports;
		private System.Windows.Forms.ComboBox reports;
	}
}
