using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace GrapeCity.ActiveReports.Samples.CalculatedFields
{
	public partial class StartForm
	{
private System.ComponentModel.Container components = null;

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
			this.arvMain = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
			this.button1 = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.Splitter1 = new System.Windows.Forms.Splitter();
			((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
			this.SplitContainer1.Panel1.SuspendLayout();
			this.SplitContainer1.Panel2.SuspendLayout();
			this.SplitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// arvMain
			// 
			resources.ApplyResources(this.arvMain, "arvMain");
			this.arvMain.CurrentPage = 0;
			this.arvMain.Name = "arvMain";
			this.arvMain.PreviewPages = 0;
			// 
			// 
			// 
			// 
			// 
			// 
			this.arvMain.Sidebar.ParametersPanel.ContextMenu = null;
			this.arvMain.Sidebar.ParametersPanel.Text = "Parameters";
			this.arvMain.Sidebar.ParametersPanel.Width = 200;
			// 
			// 
			// 
			this.arvMain.Sidebar.SearchPanel.ContextMenu = null;
			this.arvMain.Sidebar.SearchPanel.Text = "Search results";
			this.arvMain.Sidebar.SearchPanel.Width = 200;
			// 
			// 
			// 
			this.arvMain.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.arvMain.Sidebar.ThumbnailsPanel.Text = "Page thumbnails";
			this.arvMain.Sidebar.ThumbnailsPanel.Width = 200;
			this.arvMain.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
			// 
			// 
			// 
			this.arvMain.Sidebar.TocPanel.ContextMenu = null;
			this.arvMain.Sidebar.TocPanel.Expanded = true;
			this.arvMain.Sidebar.TocPanel.Text = "Document map";
			this.arvMain.Sidebar.TocPanel.Width = 200;
			this.arvMain.Sidebar.Width = 200;
			// 
			// SplitContainer1
			// 
			resources.ApplyResources(this.SplitContainer1, "SplitContainer1");
			this.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.SplitContainer1.Name = "SplitContainer1";
			// 
			// SplitContainer1.Panel1
			// 
			this.SplitContainer1.Panel1.Controls.Add(this.button1);
			this.SplitContainer1.Panel1.Controls.Add(this.comboBox1);
			this.SplitContainer1.Panel1.Controls.Add(this.Splitter1);
			// 
			// SplitContainer1.Panel2
			// 
			this.SplitContainer1.Panel2.Controls.Add(this.arvMain);
			// 
			// button1
			// 
			resources.ApplyResources(this.button1, "button1");
			this.button1.Name = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button_Click);
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			resources.ApplyResources(this.comboBox1, "comboBox1");
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			this.comboBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox1_KeyPress);
			// 
			// Splitter1
			// 
			resources.ApplyResources(this.Splitter1, "Splitter1");
			this.Splitter1.Name = "Splitter1";
			this.Splitter1.TabStop = false;
			// 
			// StartForm
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.SplitContainer1);
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Name = "StartForm";
			this.SplitContainer1.Panel1.ResumeLayout(false);
			this.SplitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
			this.SplitContainer1.ResumeLayout(false);
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

		internal Viewer.Win.Viewer arvMain;
		internal SplitContainer SplitContainer1;
		private Button button1;
		private ComboBox comboBox1;
		internal Splitter Splitter1;
	}
}
