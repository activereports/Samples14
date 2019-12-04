using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace GrapeCity.ActiveReports.Samples.Charting
{
	public partial class ViewerForm
	{
		private SplitContainer splitContainer;
internal System.Windows.Forms.Label lblStyle;
internal System.Windows.Forms.Label lblCustom;
internal ComboBox cboCustom;
internal ComboBox cboStyle;
internal Button btnReport;
private GrapeCity.ActiveReports.Viewer.Win.Viewer arvMain;
private System.ComponentModel.Container components = null;

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewerForm));
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.lblStyle = new System.Windows.Forms.Label();
			this.lblCustom = new System.Windows.Forms.Label();
			this.cboCustom = new System.Windows.Forms.ComboBox();
			this.cboStyle = new System.Windows.Forms.ComboBox();
			this.btnReport = new System.Windows.Forms.Button();
			this.arvMain = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			resources.ApplyResources(this.splitContainer, "splitContainer");
			this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.lblStyle);
			this.splitContainer.Panel1.Controls.Add(this.lblCustom);
			this.splitContainer.Panel1.Controls.Add(this.cboCustom);
			this.splitContainer.Panel1.Controls.Add(this.cboStyle);
			this.splitContainer.Panel1.Controls.Add(this.btnReport);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.arvMain);
			// 
			// lblStyle
			// 
			resources.ApplyResources(this.lblStyle, "lblStyle");
			this.lblStyle.Name = "lblStyle";
			// 
			// lblCustom
			// 
			resources.ApplyResources(this.lblCustom, "lblCustom");
			this.lblCustom.Name = "lblCustom";
			// 
			// cboCustom
			// 
			this.cboCustom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			resources.ApplyResources(this.cboCustom, "cboCustom");
			this.cboCustom.FormattingEnabled = true;
			this.cboCustom.Name = "cboCustom";
			// 
			// cboStyle
			// 
			this.cboStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			resources.ApplyResources(this.cboStyle, "cboStyle");
			this.cboStyle.FormattingEnabled = true;
			this.cboStyle.Name = "cboStyle";
			this.cboStyle.SelectedIndexChanged += new System.EventHandler(this.cboStyle_SelectedIndexChanged);
			// 
			// btnReport
			// 
			resources.ApplyResources(this.btnReport, "btnReport");
			this.btnReport.Name = "btnReport";
			this.btnReport.UseVisualStyleBackColor = true;
			this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
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
			this.Controls.Add(this.splitContainer);
			this.Name = "ViewerForm";
			this.Load += new System.EventHandler(this.ViewerForm_Load);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel1.PerformLayout();
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}

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
