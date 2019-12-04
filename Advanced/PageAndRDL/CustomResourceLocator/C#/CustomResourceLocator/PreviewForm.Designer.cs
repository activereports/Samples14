namespace GrapeCity.ActiveReports.Samples.CustomResourceLocator
{
	partial class PreviewForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">True if managed resources should be disposed; otherwise, false. </param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}


	
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewForm));
			this.reportPreview = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this.SuspendLayout();
			// 
			// reportPreview
			// 
			resources.ApplyResources(this.reportPreview, "reportPreview");
			this.reportPreview.CurrentPage = 0;
			this.reportPreview.Name = "reportPreview";
			this.reportPreview.PreviewPages = 0;
			// 
			// 
			// 
			// 
			// 
			// 
			this.reportPreview.Sidebar.ParametersPanel.ContextMenu = null;
			this.reportPreview.Sidebar.ParametersPanel.Enabled = false;
			this.reportPreview.Sidebar.ParametersPanel.Visible = false;
			this.reportPreview.Sidebar.ParametersPanel.Width = 180;
			// 
			// 
			// 
			this.reportPreview.Sidebar.SearchPanel.ContextMenu = null;
			this.reportPreview.Sidebar.SearchPanel.Width = 180;
			// 
			// 
			// 
			this.reportPreview.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.reportPreview.Sidebar.ThumbnailsPanel.Width = 180;
			// 
			// 
			// 
			this.reportPreview.Sidebar.TocPanel.ContextMenu = null;
			this.reportPreview.Sidebar.TocPanel.Enabled = false;
			this.reportPreview.Sidebar.TocPanel.Visible = false;
			this.reportPreview.Sidebar.TocPanel.Width = 180;
			this.reportPreview.Sidebar.Width = 180;
			// 
			// PreviewForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.reportPreview);
			this.Name = "PreviewForm";
			this.ResumeLayout(false);

		}

		private GrapeCity.ActiveReports.Viewer.Win.Viewer reportPreview;


	}
}
