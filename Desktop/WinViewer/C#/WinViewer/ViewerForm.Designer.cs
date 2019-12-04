using GrapeCity.ActiveReports.Viewer.Win;

namespace GrapeCity.ActiveReports.Viewer.Win
{
	partial class ViewerForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewerForm));
			this.viewer = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsRDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// viewer
			// 
			this.viewer.AnnotationDropDownVisible = true;
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
			this.viewer.Sidebar.ParametersPanel.Width = 240;
			// 
			// 
			// 
			this.viewer.Sidebar.SearchPanel.ContextMenu = null;
			this.viewer.Sidebar.SearchPanel.Width = 240;
			// 
			// 
			// 
			this.viewer.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.viewer.Sidebar.ThumbnailsPanel.Width = 240;
			this.viewer.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
			// 
			// 
			// 
			this.viewer.Sidebar.TocPanel.ContextMenu = null;
			this.viewer.Sidebar.TocPanel.Expanded = true;
			this.viewer.Sidebar.TocPanel.Width = 240;
			this.viewer.Sidebar.Width = 240;
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
			resources.ApplyResources(this.menuStrip, "menuStrip");
			this.menuStrip.Name = "menuStrip";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.saveAsRDFToolStripMenuItem,
            this.closeToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
			this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenMenuItemHandler);
			// 
			// exportToolStripMenuItem
			// 
			resources.ApplyResources(this.exportToolStripMenuItem, "exportToolStripMenuItem");
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Click += new System.EventHandler(this.ExportMenuItemHandler);
			// 
			// saveAsRDFToolStripMenuItem
			// 
			resources.ApplyResources(this.saveAsRDFToolStripMenuItem, "saveAsRDFToolStripMenuItem");
			this.saveAsRDFToolStripMenuItem.Name = "saveAsRDFToolStripMenuItem";
			this.saveAsRDFToolStripMenuItem.Click += new System.EventHandler(this.SaveAsRDFItemHandler);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			resources.ApplyResources(this.closeToolStripMenuItem, "closeToolStripMenuItem");
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseMenuItemHandler);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutMenuItemHandler);
			// 
			// openFileDialog
			// 
			resources.ApplyResources(this.openFileDialog, "openFileDialog");
			// 
			// ViewerForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.viewer);
			this.Controls.Add(this.menuStrip);
			this.Name = "ViewerForm";
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private GrapeCity.ActiveReports.Viewer.Win.Viewer viewer;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsRDFToolStripMenuItem;
	}
}
