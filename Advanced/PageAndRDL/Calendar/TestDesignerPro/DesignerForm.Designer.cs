namespace GrapeCity.ActiveReports.Calendar
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesignerForm));
			this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this.reportPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.reportExplorer = new GrapeCity.ActiveReports.Design.ReportExplorer.ReportExplorer();
			this.tabReportExplorer = new System.Windows.Forms.TabPage();
			this.reportExplorerTabControl = new System.Windows.Forms.TabControl();
			this.explorerPropertyGridContainer = new System.Windows.Forms.SplitContainer();
			this.reportDesigner = new GrapeCity.ActiveReports.Design.Designer();
			this.designerExplorerPropertyGridContainer = new System.Windows.Forms.SplitContainer();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.GroupEditorContainer = new System.Windows.Forms.Panel();
			this.groupEditor = new GrapeCity.ActiveReports.Design.GroupEditor.GroupEditor();
			this.GroupEditorSeparator = new System.Windows.Forms.Panel();
			this.GroupEditorToggleButton = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.treeView = new System.Windows.Forms.TreeView();
			this.reportToolbox = new GrapeCity.ActiveReports.Design.Toolbox.Toolbox();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.bodyContainer = new System.Windows.Forms.SplitContainer();
			this.mainContainer = new System.Windows.Forms.SplitContainer();
			this.GroupPanelVisibility = new System.Windows.Forms.ToolTip(this.components);
			this.toolStripContainer.SuspendLayout();
			this.tabReportExplorer.SuspendLayout();
			this.reportExplorerTabControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.explorerPropertyGridContainer)).BeginInit();
			this.explorerPropertyGridContainer.Panel1.SuspendLayout();
			this.explorerPropertyGridContainer.Panel2.SuspendLayout();
			this.explorerPropertyGridContainer.SuspendLayout();
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
			((System.ComponentModel.ISupportInitialize)(this.reportToolbox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bodyContainer)).BeginInit();
			this.bodyContainer.Panel1.SuspendLayout();
			this.bodyContainer.Panel2.SuspendLayout();
			this.bodyContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mainContainer)).BeginInit();
			this.mainContainer.Panel1.SuspendLayout();
			this.mainContainer.Panel2.SuspendLayout();
			this.mainContainer.SuspendLayout();
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
			// reportPropertyGrid
			// 
			this.reportPropertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			resources.ApplyResources(this.reportPropertyGrid, "reportPropertyGrid");
			this.reportPropertyGrid.Name = "reportPropertyGrid";
			// 
			// reportExplorer
			// 
			resources.ApplyResources(this.reportExplorer, "reportExplorer");
			this.reportExplorer.Name = "reportExplorer";
			this.reportExplorer.ReportDesigner = null;
			// 
			// tabReportExplorer
			// 
			this.tabReportExplorer.Controls.Add(this.reportExplorer);
			resources.ApplyResources(this.tabReportExplorer, "tabReportExplorer");
			this.tabReportExplorer.Name = "tabReportExplorer";
			this.tabReportExplorer.UseVisualStyleBackColor = true;
			// 
			// reportExplorerTabControl
			// 
			this.reportExplorerTabControl.Controls.Add(this.tabReportExplorer);
			resources.ApplyResources(this.reportExplorerTabControl, "reportExplorerTabControl");
			this.reportExplorerTabControl.Name = "reportExplorerTabControl";
			this.reportExplorerTabControl.SelectedIndex = 0;
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
			// treeView
			// 
			resources.ApplyResources(this.treeView, "treeView");
			this.treeView.Name = "treeView";
			this.treeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCollapse);
			this.treeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterExpand);
			this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
			// 
			// reportToolbox
			// 
			this.reportToolbox.DesignerHost = null;
			resources.ApplyResources(this.reportToolbox, "reportToolbox");
			this.reportToolbox.Name = "reportToolbox";
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
			// DesignerForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.mainContainer);
			this.Name = "DesignerForm";
			this.Load += new System.EventHandler(this.DesignerForm_Load);
			this.toolStripContainer.ResumeLayout(false);
			this.toolStripContainer.PerformLayout();
			this.tabReportExplorer.ResumeLayout(false);
			this.reportExplorerTabControl.ResumeLayout(false);
			this.explorerPropertyGridContainer.Panel1.ResumeLayout(false);
			this.explorerPropertyGridContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.explorerPropertyGridContainer)).EndInit();
			this.explorerPropertyGridContainer.ResumeLayout(false);
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
			((System.ComponentModel.ISupportInitialize)(this.reportToolbox)).EndInit();
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.bodyContainer.Panel1.ResumeLayout(false);
			this.bodyContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.bodyContainer)).EndInit();
			this.bodyContainer.ResumeLayout(false);
			this.mainContainer.Panel1.ResumeLayout(false);
			this.mainContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mainContainer)).EndInit();
			this.mainContainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.PropertyGrid reportPropertyGrid;
		private ActiveReports.Design.ReportExplorer.ReportExplorer reportExplorer;
		private System.Windows.Forms.TabPage tabReportExplorer;
		private System.Windows.Forms.TabControl reportExplorerTabControl;
		private System.Windows.Forms.SplitContainer explorerPropertyGridContainer;
		private ActiveReports.Design.Designer reportDesigner;
		private System.Windows.Forms.SplitContainer designerExplorerPropertyGridContainer;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView treeView;
		private ActiveReports.Design.Toolbox.Toolbox reportToolbox;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.SplitContainer bodyContainer;
		private System.Windows.Forms.SplitContainer mainContainer;
		private System.Windows.Forms.ToolStripContainer toolStripContainer;
		private System.Windows.Forms.ToolTip GroupPanelVisibility;
		private System.Windows.Forms.Panel GroupEditorContainer;
		private ActiveReports.Design.GroupEditor.GroupEditor groupEditor;
		private System.Windows.Forms.Panel GroupEditorSeparator;
		private System.Windows.Forms.PictureBox GroupEditorToggleButton;
		private System.Windows.Forms.Label label1;
	}
}
