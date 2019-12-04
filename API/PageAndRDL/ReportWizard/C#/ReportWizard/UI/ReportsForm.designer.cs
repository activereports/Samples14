using GrapeCity.ActiveReports.Design;
namespace GrapeCity.ActiveReports.Samples.ReportWizard.UI 
{
	partial class ReportsForm
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
	 private void InitializeComponent()
   {
	   System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsForm));
	   this.arvMain = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
	   this.SuspendLayout();
	   // 
	   // arvMain
	   // 
	   this.arvMain.CurrentPage = 0;
	   resources.ApplyResources(this.arvMain, "arvMain");
	   this.arvMain.Name = "arvMain";
	   this.arvMain.PreviewPages = 0;
	   this.arvMain.Dock = System.Windows.Forms.DockStyle.Fill;
	   // 
	   // 
	   // 
	   resources.ApplyResources(this.arvMain.Sidebar.ParametersPanel, "arvMain.Sidebar.ParametersPanel");
	   this.arvMain.Sidebar.ParametersPanel.ContextMenu = null;
	   this.arvMain.Sidebar.ParametersPanel.Width = 200;
	   // 
	   // 
	   // 
	   resources.ApplyResources(this.arvMain.Sidebar.SearchPanel, "arvMain.Sidebar.SearchPanel");
	   this.arvMain.Sidebar.SearchPanel.ContextMenu = null;
	   this.arvMain.Sidebar.SearchPanel.Width = 200;
	   // 
	   // 
	   // 
	   resources.ApplyResources(this.arvMain.Sidebar.ThumbnailsPanel, "arvMain.Sidebar.ThumbnailsPanel");
	   this.arvMain.Sidebar.ThumbnailsPanel.ContextMenu = null;
	   this.arvMain.Sidebar.ThumbnailsPanel.Width = 200;
	   // 
	   // 
	   // 
	   resources.ApplyResources(this.arvMain.Sidebar.TocPanel, "arvMain.Sidebar.TocPanel");
	   this.arvMain.Sidebar.TocPanel.ContextMenu = null;
	   this.arvMain.Sidebar.TocPanel.Width = 200;
	   this.arvMain.Sidebar.Width = 200;
	   // 
	   // ReportsForm
	   // 
	   resources.ApplyResources(this, "$this");
	   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
	   this.Controls.Add(this.arvMain);
	   this.Name = "UnifiedDesignerForm";
	   this.Load += new System.EventHandler(this.ReportsForm_Load);
	   
	   this.ResumeLayout(false);

   }
   #endregion
   private Viewer.Win.Viewer arvMain;
	}
}
