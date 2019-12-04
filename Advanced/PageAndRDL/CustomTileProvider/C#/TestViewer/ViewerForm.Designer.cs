namespace GrapeCity.ActiveReports.Samples.TestViewer
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
			this.viewer1 = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this.SuspendLayout();
			// 
			// viewer1
			// 
			this.viewer1.CurrentPage = 0;
			this.viewer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.viewer1.Location = new System.Drawing.Point(0, 0);
			this.viewer1.Name = "viewer1";
			this.viewer1.PreviewPages = 0;
			// 
			// 
			// 
			// 
			// 
			// 
			this.viewer1.Sidebar.ParametersPanel.ContextMenu = null;
			this.viewer1.Sidebar.ParametersPanel.Text = "Parameters";
			this.viewer1.Sidebar.ParametersPanel.Width = 200;
			// 
			// 
			// 
			this.viewer1.Sidebar.SearchPanel.ContextMenu = null;
			this.viewer1.Sidebar.SearchPanel.Text = "Search results";
			this.viewer1.Sidebar.SearchPanel.Width = 200;
			// 
			// 
			// 
			this.viewer1.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.viewer1.Sidebar.ThumbnailsPanel.Text = "Page thumbnails";
			this.viewer1.Sidebar.ThumbnailsPanel.Width = 200;
			this.viewer1.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
			// 
			// 
			// 
			this.viewer1.Sidebar.TocPanel.ContextMenu = null;
			this.viewer1.Sidebar.TocPanel.Expanded = true;
			this.viewer1.Sidebar.TocPanel.Text = "Document map";
			this.viewer1.Sidebar.TocPanel.Width = 200;
			this.viewer1.Sidebar.Width = 200;
			this.viewer1.Size = new System.Drawing.Size(800, 450);
			this.viewer1.TabIndex = 0;
			// 
			// ViewerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.viewer1);
			this.Name = "ViewerForm";
			this.Text = "CustomTileProvider Viewer";
			this.ResumeLayout(false);

		}

		#endregion

		private GrapeCity.ActiveReports.Viewer.Win.Viewer viewer1;
	}
}
