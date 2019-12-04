using System;
namespace GrapeCity.ActiveReports.Samples.ObjectDataSource
{
	public partial class MainForm
	{
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		private GrapeCity.ActiveReports.Viewer.Win.Viewer reportPreview;
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
			this.reportPreview.Sidebar.ParametersPanel.Width = 200;
			// 
			// 
			// 
			this.reportPreview.Sidebar.SearchPanel.ContextMenu = null;
			this.reportPreview.Sidebar.SearchPanel.Width = 200;
			// 
			// 
			// 
			this.reportPreview.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.reportPreview.Sidebar.ThumbnailsPanel.Width = 200;
			// 
			// 
			// 
			this.reportPreview.Sidebar.TocPanel.ContextMenu = null;
			this.reportPreview.Sidebar.TocPanel.Width = 200;
			this.reportPreview.Sidebar.Width = 200;
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.reportPreview);
			this.Name = "MainForm";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

	}
}
