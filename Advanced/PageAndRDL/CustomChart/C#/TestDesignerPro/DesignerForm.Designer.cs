namespace GrapeCity.ActiveReports.Samples.Radar
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
			this.mainContainer = new System.Windows.Forms.SplitContainer();
			this.bodyContainer = new System.Windows.Forms.SplitContainer();
			this.reportToolbox = new GrapeCity.ActiveReports.Design.Toolbox.Toolbox();
			this.designerexplorerpropertygridContainer = new System.Windows.Forms.SplitContainer();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.reportDesigner = new GrapeCity.ActiveReports.Design.Designer();
			this.GroupEditorContainer = new System.Windows.Forms.Panel();
			this.groupEditor = new GrapeCity.ActiveReports.Design.GroupEditor.GroupEditor();
			this.GroupEditorSeparator = new System.Windows.Forms.Panel();
			this.GroupEditorToggleButton = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.explorerpropertygridContainer = new System.Windows.Forms.SplitContainer();
			this.reportExplorer = new GrapeCity.ActiveReports.Design.ReportExplorer.ReportExplorer();
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
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
			((System.ComponentModel.ISupportInitialize)(this.reportToolbox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.designerexplorerpropertygridContainer)).BeginInit();
			this.designerexplorerpropertygridContainer.Panel1.SuspendLayout();
			this.designerexplorerpropertygridContainer.Panel2.SuspendLayout();
			this.designerexplorerpropertygridContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.GroupEditorContainer.SuspendLayout();
			this.GroupEditorSeparator.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GroupEditorToggleButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.explorerpropertygridContainer)).BeginInit();
			this.explorerpropertygridContainer.Panel1.SuspendLayout();
			this.explorerpropertygridContainer.Panel2.SuspendLayout();
			this.explorerpropertygridContainer.SuspendLayout();
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
			this.bodyContainer.Panel1.Controls.Add(this.reportToolbox);
			// 
			// bodyContainer.Panel2
			// 
			this.bodyContainer.Panel2.Controls.Add(this.designerexplorerpropertygridContainer);
			// 
			// reportToolbox
			// 
			this.reportToolbox.DesignerHost = null;
			resources.ApplyResources(this.reportToolbox, "reportToolbox");
			this.reportToolbox.Name = "reportToolbox";
			// 
			// designerexplorerpropertygridContainer
			// 
			resources.ApplyResources(this.designerexplorerpropertygridContainer, "designerexplorerpropertygridContainer");
			this.designerexplorerpropertygridContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.designerexplorerpropertygridContainer.Name = "designerexplorerpropertygridContainer";
			// 
			// designerexplorerpropertygridContainer.Panel1
			// 
			this.designerexplorerpropertygridContainer.Panel1.Controls.Add(this.splitContainer1);
			// 
			// designerexplorerpropertygridContainer.Panel2
			// 
			this.designerexplorerpropertygridContainer.Panel2.Controls.Add(this.explorerpropertygridContainer);
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
			// explorerpropertygridContainer
			// 
			resources.ApplyResources(this.explorerpropertygridContainer, "explorerpropertygridContainer");
			this.explorerpropertygridContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.explorerpropertygridContainer.Name = "explorerpropertygridContainer";
			// 
			// explorerpropertygridContainer.Panel1
			// 
			this.explorerpropertygridContainer.Panel1.Controls.Add(this.reportExplorer);
			// 
			// explorerpropertygridContainer.Panel2
			// 
			this.explorerpropertygridContainer.Panel2.Controls.Add(this.propertyGrid);
			// 
			// reportExplorer
			// 
			resources.ApplyResources(this.reportExplorer, "reportExplorer");
			this.reportExplorer.Name = "reportExplorer";
			this.reportExplorer.ReportDesigner = null;
			// 
			// propertyGrid
			// 
			this.propertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			resources.ApplyResources(this.propertyGrid, "propertyGrid");
			this.propertyGrid.Name = "propertyGrid";
			// 
			// DesignerForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.mainContainer);
			this.Name = "DesignerForm";
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
			((System.ComponentModel.ISupportInitialize)(this.reportToolbox)).EndInit();
			this.designerexplorerpropertygridContainer.Panel1.ResumeLayout(false);
			this.designerexplorerpropertygridContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.designerexplorerpropertygridContainer)).EndInit();
			this.designerexplorerpropertygridContainer.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.GroupEditorContainer.ResumeLayout(false);
			this.GroupEditorSeparator.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.GroupEditorToggleButton)).EndInit();
			this.explorerpropertygridContainer.Panel1.ResumeLayout(false);
			this.explorerpropertygridContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.explorerpropertygridContainer)).EndInit();
			this.explorerpropertygridContainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		private System.Windows.Forms.SplitContainer mainContainer;
		private System.Windows.Forms.ToolStripContainer toolStripContainer;
		private System.Windows.Forms.SplitContainer bodyContainer;
		private GrapeCity.ActiveReports.Design.Toolbox.Toolbox reportToolbox;
		private System.Windows.Forms.SplitContainer designerexplorerpropertygridContainer;
		private System.Windows.Forms.SplitContainer explorerpropertygridContainer;
		private GrapeCity.ActiveReports.Design.ReportExplorer.ReportExplorer reportExplorer;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Design.Designer reportDesigner;
		private System.Windows.Forms.Panel GroupEditorContainer;
		private Design.GroupEditor.GroupEditor groupEditor;
		private System.Windows.Forms.Panel GroupEditorSeparator;
		private System.Windows.Forms.PictureBox GroupEditorToggleButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolTip GroupPanelVisibility;
	}
}
