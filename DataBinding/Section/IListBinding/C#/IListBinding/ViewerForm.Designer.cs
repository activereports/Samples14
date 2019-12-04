using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace GrapeCity.ActiveReports.Samples.IListBinding
{
	public partial class ViewerForm
	{
		#region Windows Form Designer generated code
		private GrapeCity.ActiveReports.Viewer.Win.Viewer arvMain;
private System.ComponentModel.Container components = null;

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewerForm));
			this.arvMain = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this.SuspendLayout();
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
			// ViewerForm
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.arvMain);
			this.Name = "ViewerForm";
			this.ResumeLayout(false);

		}
		#endregion

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
	}
}
