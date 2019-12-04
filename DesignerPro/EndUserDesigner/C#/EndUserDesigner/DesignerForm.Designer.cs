using System.Windows.Forms;
using GrapeCity.ActiveReports.Design;

namespace GrapeCity.ActiveReports.Designer.Win
{
	partial class DesignerForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesignerForm));
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.splitContainerToolbox = new System.Windows.Forms.SplitContainer();
			this.splitContainerReportsLibrary = new System.Windows.Forms.SplitContainer();
			this.arToolbox = new GrapeCity.ActiveReports.Design.Toolbox.Toolbox();
			this.reportsLibrary = new GrapeCity.ActiveReports.Design.ReportsLibrary.ReportsLibrary();
			this.splitContainerDesigner = new System.Windows.Forms.SplitContainer();
			this.splitContainerGroupEditor = new System.Windows.Forms.SplitContainer();
			this.arDesigner = new GrapeCity.ActiveReports.Design.Designer();
			this.arPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.GroupEditorContainer = new System.Windows.Forms.Panel();
			this.groupEditor = new GrapeCity.ActiveReports.Design.GroupEditor.GroupEditor();
			this.GroupEditorSeparator = new System.Windows.Forms.Panel();
			this.GroupEditorToggleButton = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.splitContainerProperties = new System.Windows.Forms.SplitContainer();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.reportExplorerTabPage = new System.Windows.Forms.TabPage();
			this.reportExplorer = new GrapeCity.ActiveReports.Design.ReportExplorer.ReportExplorer();
			this.layersTabPage = new System.Windows.Forms.TabPage();
			this.layerList = new GrapeCity.ActiveReports.Design.LayerList();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.openDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveDialog = new System.Windows.Forms.SaveFileDialog();
			this.GroupPanelVisibility = new System.Windows.Forms.ToolTip(this.components);
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerToolbox)).BeginInit();
			this.splitContainerToolbox.Panel1.SuspendLayout();
			this.splitContainerToolbox.Panel2.SuspendLayout();
			this.splitContainerToolbox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerReportsLibrary)).BeginInit();
			this.splitContainerReportsLibrary.Panel1.SuspendLayout();
			this.splitContainerReportsLibrary.Panel2.SuspendLayout();
			this.splitContainerReportsLibrary.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.arToolbox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerDesigner)).BeginInit();
			this.splitContainerDesigner.Panel1.SuspendLayout();
			this.splitContainerDesigner.Panel2.SuspendLayout();
			this.splitContainerDesigner.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerGroupEditor)).BeginInit();
			this.splitContainerGroupEditor.Panel1.SuspendLayout();
			this.splitContainerGroupEditor.Panel2.SuspendLayout();
			this.splitContainerGroupEditor.SuspendLayout();
			this.GroupEditorContainer.SuspendLayout();
			this.GroupEditorSeparator.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GroupEditorToggleButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerProperties)).BeginInit();
			this.splitContainerProperties.Panel1.SuspendLayout();
			this.splitContainerProperties.Panel2.SuspendLayout();
			this.splitContainerProperties.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.reportExplorerTabPage.SuspendLayout();
			this.layersTabPage.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainerToolbox);
			resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
			resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
			this.toolStripContainer1.Name = "toolStripContainer1";
			// 
			// splitContainerToolbox
			// 
			resources.ApplyResources(this.splitContainerToolbox, "splitContainerToolbox");
			this.splitContainerToolbox.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainerToolbox.Name = "splitContainerToolbox";
			// 
			// splitContainerToolbox.Panel1
			// 
			this.splitContainerToolbox.Panel1.Controls.Add(this.splitContainerReportsLibrary);
			// 
			// splitContainerToolbox.Panel2
			// 
			this.splitContainerToolbox.Panel2.Controls.Add(this.splitContainerDesigner);
			// 
			// splitContainerReportsLibrary
			// 
			resources.ApplyResources(this.splitContainerReportsLibrary, "splitContainerReportsLibrary");
			this.splitContainerReportsLibrary.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainerReportsLibrary.Name = "splitContainerReportsLibrary";
			// 
			// splitContainerReportsLibrary.Panel1
			// 
			this.splitContainerReportsLibrary.Panel1.Controls.Add(this.arToolbox);
			// 
			// splitContainerReportsLibrary.Panel2
			// 
			this.splitContainerReportsLibrary.Panel2.Controls.Add(this.reportsLibrary);
			// 
			// arToolbox
			// 
			this.arToolbox.DesignerHost = null;
			resources.ApplyResources(this.arToolbox, "arToolbox");
			this.arToolbox.Name = "arToolbox";
			// 
			// reportsLibrary
			// 
			resources.ApplyResources(this.reportsLibrary, "reportsLibrary");
			this.reportsLibrary.Name = "reportsLibrary";
			this.reportsLibrary.ReportDesigner = null;
			// 
			// splitContainerDesigner
			// 
			resources.ApplyResources(this.splitContainerDesigner, "splitContainerDesigner");
			this.splitContainerDesigner.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainerDesigner.Name = "splitContainerDesigner";
			// 
			// splitContainerDesigner.Panel1
			// 
			this.splitContainerDesigner.Panel1.Controls.Add(this.splitContainerGroupEditor);
			// 
			// splitContainerDesigner.Panel2
			// 
			this.splitContainerDesigner.Panel2.Controls.Add(this.splitContainerProperties);
			// 
			// splitContainerGroupEditor
			// 
			resources.ApplyResources(this.splitContainerGroupEditor, "splitContainerGroupEditor");
			this.splitContainerGroupEditor.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainerGroupEditor.Name = "splitContainerGroupEditor";
			// 
			// splitContainerGroupEditor.Panel1
			// 
			this.splitContainerGroupEditor.Panel1.Controls.Add(this.arDesigner);
			// 
			// splitContainerGroupEditor.Panel2
			// 
			this.splitContainerGroupEditor.Panel2.Controls.Add(this.GroupEditorContainer);
			resources.ApplyResources(this.splitContainerGroupEditor.Panel2, "splitContainerGroupEditor.Panel2");
			// 
			// arDesigner
			// 
			resources.ApplyResources(this.arDesigner, "arDesigner");
			this.arDesigner.IsDirty = false;
			this.arDesigner.LockControls = false;
			this.arDesigner.Name = "arDesigner";
			this.arDesigner.PreviewPages = 10;
			this.arDesigner.PromptUser = false;
			this.arDesigner.PropertyGrid = this.arPropertyGrid;
			this.arDesigner.ReportTabsVisible = true;
			this.arDesigner.ShowDataSourceIcon = true;
			this.arDesigner.Toolbox = null;
			this.arDesigner.ToolBoxItem = null;
			// 
			// arPropertyGrid
			// 
			resources.ApplyResources(this.arPropertyGrid, "arPropertyGrid");
			this.arPropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.arPropertyGrid.Name = "arPropertyGrid";
			// 
			// GroupEditorContainer
			// 
			this.GroupEditorContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
			this.GroupEditorContainer.Controls.Add(this.groupEditor);
			this.GroupEditorContainer.Controls.Add(this.GroupEditorSeparator);
			resources.ApplyResources(this.GroupEditorContainer, "GroupEditorContainer");
			this.GroupEditorContainer.Name = "GroupEditorContainer";
			// 
			// groupEditor
			// 
			this.groupEditor.BackColor = System.Drawing.Color.White;
			resources.ApplyResources(this.groupEditor, "groupEditor");
			this.groupEditor.Name = "groupEditor";
			this.groupEditor.ReportDesigner = null;
			// 
			// GroupEditorSeparator
			// 
			this.GroupEditorSeparator.BackColor = System.Drawing.Color.Gainsboro;
			this.GroupEditorSeparator.Controls.Add(this.GroupEditorToggleButton);
			this.GroupEditorSeparator.Controls.Add(this.label1);
			resources.ApplyResources(this.GroupEditorSeparator, "GroupEditorSeparator");
			this.GroupEditorSeparator.Name = "GroupEditorSeparator";
			// 
			// GroupEditorToggleButton
			// 
			resources.ApplyResources(this.GroupEditorToggleButton, "GroupEditorToggleButton");
			this.GroupEditorToggleButton.Name = "GroupEditorToggleButton";
			this.GroupEditorToggleButton.TabStop = false;
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Name = "label1";
			this.label1.UseCompatibleTextRendering = true;
			// 
			// splitContainerProperties
			// 
			resources.ApplyResources(this.splitContainerProperties, "splitContainerProperties");
			this.splitContainerProperties.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainerProperties.Name = "splitContainerProperties";
			// 
			// splitContainerProperties.Panel1
			// 
			this.splitContainerProperties.Panel1.Controls.Add(this.tabControl1);
			// 
			// splitContainerProperties.Panel2
			// 
			this.splitContainerProperties.Panel2.Controls.Add(this.arPropertyGrid);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.reportExplorerTabPage);
			this.tabControl1.Controls.Add(this.layersTabPage);
			resources.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			// 
			// reportExplorerTabPage
			// 
			this.reportExplorerTabPage.Controls.Add(this.reportExplorer);
			resources.ApplyResources(this.reportExplorerTabPage, "reportExplorerTabPage");
			this.reportExplorerTabPage.Name = "reportExplorerTabPage";
			// 
			// reportExplorer
			// 
			resources.ApplyResources(this.reportExplorer, "reportExplorer");
			this.reportExplorer.Name = "reportExplorer";
			this.reportExplorer.ReportDesigner = null;
			// 
			// layersTabPage
			// 
			this.layersTabPage.Controls.Add(this.layerList);
			resources.ApplyResources(this.layersTabPage, "layersTabPage");
			this.layersTabPage.Name = "layersTabPage";
			// 
			// layerList
			// 
			resources.ApplyResources(this.layerList, "layerList");
			this.layerList.Name = "layerList";
			this.layerList.ReportDesigner = null;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			resources.ApplyResources(this.statusStrip1, "statusStrip1");
			this.statusStrip1.Name = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
			this.toolStripStatusLabel1.Spring = true;
			// 
			// openDialog
			// 
			resources.ApplyResources(this.openDialog, "openDialog");
			// 
			// DesignerForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.toolStripContainer1);
			this.Controls.Add(this.statusStrip1);
			this.Name = "DesignerForm";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.splitContainerToolbox.Panel1.ResumeLayout(false);
			this.splitContainerToolbox.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerToolbox)).EndInit();
			this.splitContainerToolbox.ResumeLayout(false);
			this.splitContainerReportsLibrary.Panel1.ResumeLayout(false);
			this.splitContainerReportsLibrary.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerReportsLibrary)).EndInit();
			this.splitContainerReportsLibrary.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.arToolbox)).EndInit();
			this.splitContainerDesigner.Panel1.ResumeLayout(false);
			this.splitContainerDesigner.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerDesigner)).EndInit();
			this.splitContainerDesigner.ResumeLayout(false);
			this.splitContainerGroupEditor.Panel1.ResumeLayout(false);
			this.splitContainerGroupEditor.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerGroupEditor)).EndInit();
			this.splitContainerGroupEditor.ResumeLayout(false);
			this.GroupEditorContainer.ResumeLayout(false);
			this.GroupEditorSeparator.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.GroupEditorToggleButton)).EndInit();
			this.splitContainerProperties.Panel1.ResumeLayout(false);
			this.splitContainerProperties.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerProperties)).EndInit();
			this.splitContainerProperties.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.reportExplorerTabPage.ResumeLayout(false);
			this.layersTabPage.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PropertyGrid arPropertyGrid;
		private GrapeCity.ActiveReports.Design.ReportExplorer.ReportExplorer reportExplorer;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.SplitContainer splitContainerProperties;
		private System.Windows.Forms.SplitContainer splitContainerToolbox;
		private GrapeCity.ActiveReports.Design.Toolbox.Toolbox arToolbox;
		private System.Windows.Forms.SplitContainer splitContainerDesigner;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.OpenFileDialog openDialog;
		private System.Windows.Forms.SaveFileDialog saveDialog;
		private TabControl tabControl1;
		private TabPage reportExplorerTabPage;
		private TabPage layersTabPage;
		private LayerList layerList;
		private SplitContainer splitContainerGroupEditor;
		private Design.Designer arDesigner;
		private Panel GroupEditorContainer;
		private GrapeCity.ActiveReports.Design.GroupEditor.GroupEditor groupEditor;
		private Panel GroupEditorSeparator;
		private PictureBox GroupEditorToggleButton;
		private Label label1;
		private ToolTip GroupPanelVisibility;
		private SplitContainer splitContainerReportsLibrary;
		private Design.ReportsLibrary.ReportsLibrary reportsLibrary;
	}
}
