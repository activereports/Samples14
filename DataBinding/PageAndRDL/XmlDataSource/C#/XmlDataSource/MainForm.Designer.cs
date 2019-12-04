using System;
using System.Resources;

namespace GrapeCity.ActiveReports.Samples.XmlDataSource
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

		#region Windows Forms Designer generated code

		private GrapeCity.ActiveReports.Viewer.Win.Viewer reportPreview;

		private void InitializeComponent()
		{
			this.reportPreview = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this.SuspendLayout();
			// 
			// reportPreview
			// 
			this.reportPreview.CurrentPage = 0;
			this.reportPreview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.reportPreview.Location = new System.Drawing.Point(0, 0);
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
			this.reportPreview.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
			// 
			// 
			// 
			this.reportPreview.Sidebar.TocPanel.ContextMenu = null;
			this.reportPreview.Sidebar.TocPanel.Expanded = true;
			this.reportPreview.Sidebar.TocPanel.Width = 200;
			this.reportPreview.Sidebar.Width = 200;
			this.reportPreview.Size = new System.Drawing.Size(824, 502);
			this.reportPreview.TabIndex = 0;
			// 
			// MainForm
			// 
			this.ClientSize = new System.Drawing.Size(824, 502);
			this.Controls.Add(this.reportPreview);
			this.Name = "MainForm";
			this.Text = "Xml Data Source Sample";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
