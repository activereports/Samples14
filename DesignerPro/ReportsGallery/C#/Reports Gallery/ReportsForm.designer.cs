using System.Windows.Forms;
namespace GrapeCity.ActiveReports.Samples.ReportsGallery
{
	partial class ReportsForm
	{
	  
		private System.ComponentModel.IContainer components = null;

	   
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsForm));
			this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this.mainContainer = new System.Windows.Forms.SplitContainer();
			this.bodyContainer = new System.Windows.Forms.SplitContainer();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.reportToolbox = new GrapeCity.ActiveReports.Design.Toolbox.Toolbox();
			this.treeView = new System.Windows.Forms.TreeView();
			this.designerExplorerPropertyGridContainer = new System.Windows.Forms.SplitContainer();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.reportDesigner = new GrapeCity.ActiveReports.Design.Designer();
			this.GroupEditorContainer = new System.Windows.Forms.Panel();
			this.groupEditor = new GrapeCity.ActiveReports.Design.GroupEditor.GroupEditor();
			this.GroupEditorSeparator = new System.Windows.Forms.Panel();
			this.GroupEditorToggleButton = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.explorerPropertyGridContainer = new System.Windows.Forms.SplitContainer();
			this.reportExplorerTabControl = new System.Windows.Forms.TabControl();
			this.tabReportExplorer = new System.Windows.Forms.TabPage();
			this.reportExplorer = new GrapeCity.ActiveReports.Design.ReportExplorer.ReportExplorer();
			this.tabLayers = new System.Windows.Forms.TabPage();
			this.layerList = new GrapeCity.ActiveReports.Design.LayerList();
			this.reportPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.GroupPanelVisibility = new System.Windows.Forms.ToolTip(this.components);
			this.toolStripContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mainContainer)).BeginInit();
			this.mainContainer.Panel1.SuspendLayout();
			this.mainContainer.Panel2.SuspendLayout();
			this.mainContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bodyContainer)).BeginInit();
			this.bodyContainer.Panel1.SuspendLayout();
			this.bodyContainer.Panel2.SuspendLayout();
			this.bodyContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.reportToolbox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.designerExplorerPropertyGridContainer)).BeginInit();
			this.designerExplorerPropertyGridContainer.Panel1.SuspendLayout();
			this.designerExplorerPropertyGridContainer.Panel2.SuspendLayout();
			this.designerExplorerPropertyGridContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.GroupEditorContainer.SuspendLayout();
			this.GroupEditorSeparator.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GroupEditorToggleButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.explorerPropertyGridContainer)).BeginInit();
			this.explorerPropertyGridContainer.Panel1.SuspendLayout();
			this.explorerPropertyGridContainer.Panel2.SuspendLayout();
			this.explorerPropertyGridContainer.SuspendLayout();
			this.reportExplorerTabControl.SuspendLayout();
			this.tabReportExplorer.SuspendLayout();
			this.tabLayers.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer
			// 
			// 
			// toolStripContainer.ContentPanel
			// 
			resources.ApplyResources(this.toolStripContainer.ContentPanel, "toolStripContainer.ContentPanel");
			resources.ApplyResources(this.toolStripContainer, "toolStripContainer");
			this.toolStripContainer.Name = "toolStripContainer";
			// 
			// mainContainer
			// 
			resources.ApplyResources(this.mainContainer, "mainContainer");
			this.mainContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.mainContainer.Name = "mainContainer";
			// 
			// mainContainer.Panel1
			// 
			this.mainContainer.Panel1.Controls.Add(this.toolStripContainer);
			// 
			// mainContainer.Panel2
			// 
			this.mainContainer.Panel2.Controls.Add(this.bodyContainer);
			// 
			// bodyContainer
			// 
			resources.ApplyResources(this.bodyContainer, "bodyContainer");
			this.bodyContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.bodyContainer.Name = "bodyContainer";
			// 
			// bodyContainer.Panel1
			// 
			this.bodyContainer.Panel1.Controls.Add(this.splitContainer);
			// 
			// bodyContainer.Panel2
			// 
			this.bodyContainer.Panel2.Controls.Add(this.designerExplorerPropertyGridContainer);
			// 
			// splitContainer
			// 
			resources.ApplyResources(this.splitContainer, "splitContainer");
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.reportToolbox);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.treeView);
			// 
			// reportToolbox
			// 
			this.reportToolbox.DesignerHost = null;
			resources.ApplyResources(this.reportToolbox, "reportToolbox");
			this.reportToolbox.Name = "reportToolbox";
			// 
			// treeView
			// 
			resources.ApplyResources(this.treeView, "treeView");
			this.treeView.Name = "treeView";
			this.treeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCollapse);
			this.treeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterExpand);
			this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
			// 
			// designerExplorerPropertyGridContainer
			// 
			resources.ApplyResources(this.designerExplorerPropertyGridContainer, "designerExplorerPropertyGridContainer");
			this.designerExplorerPropertyGridContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.designerExplorerPropertyGridContainer.Name = "designerExplorerPropertyGridContainer";
			// 
			// designerExplorerPropertyGridContainer.Panel1
			// 
			this.designerExplorerPropertyGridContainer.Panel1.Controls.Add(this.splitContainer1);
			// 
			// designerExplorerPropertyGridContainer.Panel2
			// 
			this.designerExplorerPropertyGridContainer.Panel2.Controls.Add(this.explorerPropertyGridContainer);
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.splitContainer1, "splitContainer1");
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.reportDesigner);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.GroupEditorContainer);
			// 
			// reportDesigner
			// 
			resources.ApplyResources(this.reportDesigner, "reportDesigner");
			this.reportDesigner.IsDirty = false;
			this.reportDesigner.LockControls = false;
			this.reportDesigner.Name = "reportDesigner";
			this.reportDesigner.PreviewPages = 10;
			this.reportDesigner.PromptUser = true;
			this.reportDesigner.PropertyGrid = null;
			this.reportDesigner.ReportTabsVisible = true;
			this.reportDesigner.ShowDataSourceIcon = true;
			this.reportDesigner.Toolbox = null;
			this.reportDesigner.ToolBoxItem = null;
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
			// explorerPropertyGridContainer
			// 
			resources.ApplyResources(this.explorerPropertyGridContainer, "explorerPropertyGridContainer");
			this.explorerPropertyGridContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.explorerPropertyGridContainer.Name = "explorerPropertyGridContainer";
			// 
			// explorerPropertyGridContainer.Panel1
			// 
			this.explorerPropertyGridContainer.Panel1.Controls.Add(this.reportExplorerTabControl);
			// 
			// explorerPropertyGridContainer.Panel2
			// 
			this.explorerPropertyGridContainer.Panel2.Controls.Add(this.reportPropertyGrid);
			// 
			// reportExplorerTabControl
			// 
			this.reportExplorerTabControl.Controls.Add(this.tabReportExplorer);
			this.reportExplorerTabControl.Controls.Add(this.tabLayers);
			resources.ApplyResources(this.reportExplorerTabControl, "reportExplorerTabControl");
			this.reportExplorerTabControl.Name = "reportExplorerTabControl";
			this.reportExplorerTabControl.SelectedIndex = 0;
			// 
			// tabReportExplorer
			// 
			this.tabReportExplorer.Controls.Add(this.reportExplorer);
			resources.ApplyResources(this.tabReportExplorer, "tabReportExplorer");
			this.tabReportExplorer.Name = "tabReportExplorer";
			this.tabReportExplorer.UseVisualStyleBackColor = true;
			// 
			// reportExplorer
			// 
			resources.ApplyResources(this.reportExplorer, "reportExplorer");
			this.reportExplorer.Name = "reportExplorer";
			this.reportExplorer.ReportDesigner = null;
			// 
			// tabLayers
			// 
			this.tabLayers.Controls.Add(this.layerList);
			resources.ApplyResources(this.tabLayers, "tabLayers");
			this.tabLayers.Name = "tabLayers";
			this.tabLayers.UseVisualStyleBackColor = true;
			// 
			// layerList
			// 
			resources.ApplyResources(this.layerList, "layerList");
			this.layerList.Name = "layerList";
			this.layerList.ReportDesigner = null;
			// 
			// reportPropertyGrid
			// 
			this.reportPropertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			resources.ApplyResources(this.reportPropertyGrid, "reportPropertyGrid");
			this.reportPropertyGrid.Name = "reportPropertyGrid";
			// 
			// ReportsForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.mainContainer);
			this.Name = "ReportsForm";
			this.Load += new System.EventHandler(this.ReportsForm_Load);
			this.toolStripContainer.ResumeLayout(false);
			this.toolStripContainer.PerformLayout();
			this.mainContainer.Panel1.ResumeLayout(false);
			this.mainContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mainContainer)).EndInit();
			this.mainContainer.ResumeLayout(false);
			this.bodyContainer.Panel1.ResumeLayout(false);
			this.bodyContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.bodyContainer)).EndInit();
			this.bodyContainer.ResumeLayout(false);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.reportToolbox)).EndInit();
			this.designerExplorerPropertyGridContainer.Panel1.ResumeLayout(false);
			this.designerExplorerPropertyGridContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.designerExplorerPropertyGridContainer)).EndInit();
			this.designerExplorerPropertyGridContainer.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.GroupEditorContainer.ResumeLayout(false);
			this.GroupEditorSeparator.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.GroupEditorToggleButton)).EndInit();
			this.explorerPropertyGridContainer.Panel1.ResumeLayout(false);
			this.explorerPropertyGridContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.explorerPropertyGridContainer)).EndInit();
			this.explorerPropertyGridContainer.ResumeLayout(false);
			this.reportExplorerTabControl.ResumeLayout(false);
			this.tabReportExplorer.ResumeLayout(false);
			this.tabLayers.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer mainContainer;
		private System.Windows.Forms.TreeView treeView;

		private System.Windows.Forms.ToolStripContainer toolStripContainer;
		private System.Windows.Forms.SplitContainer bodyContainer;
		private GrapeCity.ActiveReports.Design.Toolbox.Toolbox reportToolbox;
		private System.Windows.Forms.SplitContainer designerExplorerPropertyGridContainer;
		private System.Windows.Forms.SplitContainer explorerPropertyGridContainer;
		private System.Windows.Forms.PropertyGrid reportPropertyGrid;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.TabControl reportExplorerTabControl;
		private System.Windows.Forms.TabPage tabReportExplorer;
		private Design.ReportExplorer.ReportExplorer reportExplorer;
		private System.Windows.Forms.TabPage tabLayers;
		private Design.LayerList layerList;
		private SplitContainer splitContainer1;
		private Design.Designer reportDesigner;
		private Panel GroupEditorContainer;
		private Design.GroupEditor.GroupEditor groupEditor;
		private Panel GroupEditorSeparator;
		private PictureBox GroupEditorToggleButton;
		private Label label1;
		private ToolTip GroupPanelVisibility;
	}
}
