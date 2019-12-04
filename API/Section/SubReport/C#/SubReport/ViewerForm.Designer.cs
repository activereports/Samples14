using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace GrapeCity.ActiveReports.Samples.SubReport
{
	public partial class ViewerForm
	{
		#region Windows Form Designer generated code
		private SplitContainer splitContainer;
internal ComboBox cboReport;
private GrapeCity.ActiveReports.Viewer.Win.Viewer arvMain;
private System.ComponentModel.Container components = null;

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewerForm));
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.cboReport = new System.Windows.Forms.ComboBox();
			this.arvMain = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			resources.ApplyResources(this.splitContainer, "splitContainer");
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.cboReport);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.arvMain);
			// 
			// cboReport
			// 
			this.cboReport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			resources.ApplyResources(this.cboReport, "cboReport");
			this.cboReport.FormattingEnabled = true;
			this.cboReport.Name = "cboReport";
			this.cboReport.SelectedIndexChanged += new System.EventHandler(this.cboReport_SelectedIndexChanged);
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
			this.arvMain.Sidebar.ParametersPanel.Width = 180;
			// 
			// 
			// 
			this.arvMain.Sidebar.SearchPanel.ContextMenu = null;
			this.arvMain.Sidebar.SearchPanel.Width = 180;
			// 
			// 
			// 
			this.arvMain.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.arvMain.Sidebar.ThumbnailsPanel.Width = 180;
			// 
			// 
			// 
			this.arvMain.Sidebar.TocPanel.ContextMenu = null;
			this.arvMain.Sidebar.TocPanel.Width = 180;
			this.arvMain.Sidebar.Width = 180;		   
			// 
			// ViewerForm
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.splitContainer);
			this.Name = "ViewerForm";
			this.Load += new System.EventHandler(this.ViewerForm_Load);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
	 
			this.splitContainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
	}
}
