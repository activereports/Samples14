using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Controls;
using GrapeCity.ActiveReports.SectionReportModel;
using GrapeCity.ActiveReports.Document.Section;

namespace GrapeCity.ActiveReports.Samples.CrossSectionControls
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

		

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewerForm));
			this.cscViewer = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.cscTab = new System.Windows.Forms.TabPage();
			this.repeatToFillTab = new System.Windows.Forms.TabPage();
			this.repeatToFillViewer = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this.printAtBottomTab = new System.Windows.Forms.TabPage();
			this.printAtBottomViewer = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this.tabControl1.SuspendLayout();
			this.cscTab.SuspendLayout();
			this.repeatToFillTab.SuspendLayout();
			this.printAtBottomTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// cscViewer
			// 
			resources.ApplyResources(this.cscViewer, "cscViewer");
			this.cscViewer.AnnotationDropDownVisible = false;
			this.cscViewer.BackColor = System.Drawing.SystemColors.Control;
			this.cscViewer.CurrentPage = 0;
			this.cscViewer.HyperlinkBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.cscViewer.HyperlinkForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.cscViewer.Name = "cscViewer";
			this.cscViewer.PagesBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.cscViewer.PreviewPages = 0;
			this.cscViewer.SearchResultsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.cscViewer.SearchResultsForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(139)))));
			// 
			// 
			// 
			// 
			// 
			// 
			this.cscViewer.Sidebar.ParametersPanel.ContextMenu = null;
			this.cscViewer.Sidebar.ParametersPanel.Width = 180;
			// 
			// 
			// 
			this.cscViewer.Sidebar.SearchPanel.ContextMenu = null;
			this.cscViewer.Sidebar.SearchPanel.Width = 180;
			this.cscViewer.Sidebar.SelectedIndex = 0;
			// 
			// 
			// 
			this.cscViewer.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.cscViewer.Sidebar.ThumbnailsPanel.Width = 180;
			// 
			// 
			// 
			this.cscViewer.Sidebar.TocPanel.ContextMenu = null;
			this.cscViewer.Sidebar.TocPanel.Width = 180;
			this.cscViewer.Sidebar.Width = 180;
			this.cscViewer.SplitView = false;
			this.cscViewer.ViewType = GrapeCity.Viewer.Common.Model.ViewType.SinglePage;
			this.cscViewer.Zoom = 1F;
			// 
			// tabControl1
			// 
			resources.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Controls.Add(this.cscTab);
			this.tabControl1.Controls.Add(this.repeatToFillTab);
			this.tabControl1.Controls.Add(this.printAtBottomTab);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			// 
			// cscTab
			// 
			resources.ApplyResources(this.cscTab, "cscTab");
			this.cscTab.Controls.Add(this.cscViewer);
			this.cscTab.Name = "cscTab";
			this.cscTab.UseVisualStyleBackColor = true;
			// 
			// repeatToFillTab
			// 
			resources.ApplyResources(this.repeatToFillTab, "repeatToFillTab");
			this.repeatToFillTab.Controls.Add(this.repeatToFillViewer);
			this.repeatToFillTab.Name = "repeatToFillTab";
			this.repeatToFillTab.UseVisualStyleBackColor = true;
			// 
			// repeatToFillViewer
			// 
			resources.ApplyResources(this.repeatToFillViewer, "repeatToFillViewer");
			this.repeatToFillViewer.AnnotationDropDownVisible = false;
			this.repeatToFillViewer.BackColor = System.Drawing.SystemColors.Control;
			this.repeatToFillViewer.CurrentPage = 0;
			this.repeatToFillViewer.HyperlinkBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.repeatToFillViewer.HyperlinkForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.repeatToFillViewer.Name = "repeatToFillViewer";
			this.repeatToFillViewer.PagesBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.repeatToFillViewer.PreviewPages = 0;
			this.repeatToFillViewer.SearchResultsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.repeatToFillViewer.SearchResultsForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(139)))));
			// 
			// 
			// 
			// 
			// 
			// 
			this.repeatToFillViewer.Sidebar.ParametersPanel.ContextMenu = null;
			this.repeatToFillViewer.Sidebar.ParametersPanel.Width = 180;
			// 
			// 
			// 
			this.repeatToFillViewer.Sidebar.SearchPanel.ContextMenu = null;
			this.repeatToFillViewer.Sidebar.SearchPanel.Width = 180;
			this.repeatToFillViewer.Sidebar.SelectedIndex = 0;
			// 
			// 
			// 
			this.repeatToFillViewer.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.repeatToFillViewer.Sidebar.ThumbnailsPanel.Width = 180;
			// 
			// 
			// 
			this.repeatToFillViewer.Sidebar.TocPanel.ContextMenu = null;
			this.repeatToFillViewer.Sidebar.TocPanel.Width = 180;
			this.repeatToFillViewer.Sidebar.Width = 180;
			this.repeatToFillViewer.SplitView = false;
			this.repeatToFillViewer.ViewType = GrapeCity.Viewer.Common.Model.ViewType.SinglePage;
			this.repeatToFillViewer.Zoom = 1F;
			// 
			// printAtBottomTab
			// 
			resources.ApplyResources(this.printAtBottomTab, "printAtBottomTab");
			this.printAtBottomTab.Controls.Add(this.printAtBottomViewer);
			this.printAtBottomTab.Name = "printAtBottomTab";
			this.printAtBottomTab.UseVisualStyleBackColor = true;
			// 
			// printAtBottomViewer
			// 
			resources.ApplyResources(this.printAtBottomViewer, "printAtBottomViewer");
			this.printAtBottomViewer.AnnotationDropDownVisible = false;
			this.printAtBottomViewer.BackColor = System.Drawing.SystemColors.Control;
			this.printAtBottomViewer.CurrentPage = 0;
			this.printAtBottomViewer.HyperlinkBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.printAtBottomViewer.HyperlinkForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.printAtBottomViewer.Name = "printAtBottomViewer";
			this.printAtBottomViewer.PagesBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.printAtBottomViewer.PreviewPages = 0;
			this.printAtBottomViewer.SearchResultsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.printAtBottomViewer.SearchResultsForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(139)))));
			// 
			// 
			// 
			// 
			// 
			// 
			this.printAtBottomViewer.Sidebar.ParametersPanel.ContextMenu = null;
			this.printAtBottomViewer.Sidebar.ParametersPanel.Width = 180;
			// 
			// 
			// 
			this.printAtBottomViewer.Sidebar.SearchPanel.ContextMenu = null;
			this.printAtBottomViewer.Sidebar.SearchPanel.Width = 180;
			this.printAtBottomViewer.Sidebar.SelectedIndex = 0;
			// 
			// 
			// 
			this.printAtBottomViewer.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.printAtBottomViewer.Sidebar.ThumbnailsPanel.Width = 180;
			// 
			// 
			// 
			this.printAtBottomViewer.Sidebar.TocPanel.ContextMenu = null;
			this.printAtBottomViewer.Sidebar.TocPanel.Width = 180;
			this.printAtBottomViewer.Sidebar.Width = 180;
			this.printAtBottomViewer.SplitView = false;
			this.printAtBottomViewer.ViewType = GrapeCity.Viewer.Common.Model.ViewType.SinglePage;
			this.printAtBottomViewer.Zoom = 1F;
			// 
			// ViewerForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl1);
			this.Name = "ViewerForm";
			this.Load += new System.EventHandler(this.ViewerForm_Load);
			this.tabControl1.ResumeLayout(false);
			this.cscTab.ResumeLayout(false);
			this.repeatToFillTab.ResumeLayout(false);
			this.printAtBottomTab.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		private GrapeCity.ActiveReports.Viewer.Win.Viewer cscViewer;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage cscTab;
		private System.Windows.Forms.TabPage repeatToFillTab;
		private GrapeCity.ActiveReports.Viewer.Win.Viewer repeatToFillViewer;
		private System.Windows.Forms.TabPage printAtBottomTab;
		private GrapeCity.ActiveReports.Viewer.Win.Viewer printAtBottomViewer;
	}
}
