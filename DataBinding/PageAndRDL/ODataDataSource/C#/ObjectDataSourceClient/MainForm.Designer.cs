namespace ODataDataSource
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
			this.reportPreview = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this.SuspendLayout();
			// 
			// reportPreview
			// 
			this.reportPreview.CurrentPage = 0;
			resources.ApplyResources(this.reportPreview, "reportPreview");
			this.reportPreview.Name = "reportPreview";
			this.reportPreview.PreviewPages = 0;
			// 
			// 
			// 
			// 
			// 
			// 
			this.reportPreview.Sidebar.ParametersPanel.ContextMenu = null;
			this.reportPreview.Sidebar.ParametersPanel.Text = "Parameters";
			this.reportPreview.Sidebar.ParametersPanel.Width = 200;
			// 
			// 
			// 
			this.reportPreview.Sidebar.SearchPanel.ContextMenu = null;
			this.reportPreview.Sidebar.SearchPanel.Text = "Search results";
			this.reportPreview.Sidebar.SearchPanel.Width = 200;
			// 
			// 
			// 
			this.reportPreview.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.reportPreview.Sidebar.ThumbnailsPanel.Text = "Page thumbnails";
			this.reportPreview.Sidebar.ThumbnailsPanel.Width = 200;
			this.reportPreview.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
			// 
			// 
			// 
			this.reportPreview.Sidebar.TocPanel.ContextMenu = null;
			this.reportPreview.Sidebar.TocPanel.Expanded = true;
			this.reportPreview.Sidebar.TocPanel.Text = "Document map";
			this.reportPreview.Sidebar.TocPanel.Width = 200;
			this.reportPreview.Sidebar.Width = 200;
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.reportPreview);
			this.Name = "MainForm";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private GrapeCity.ActiveReports.Viewer.Win.Viewer reportPreview;
	}
}
