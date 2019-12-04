<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ReportsForm
	Inherits System.Windows.Forms.Form


	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub


	Private components As System.ComponentModel.IContainer




	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportsForm))
		Me.toolStripContainer = New System.Windows.Forms.ToolStripContainer()
		Me.mainContainer = New System.Windows.Forms.SplitContainer()
		Me.bodyContainer = New System.Windows.Forms.SplitContainer()
		Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
		Me.splitContainer = New System.Windows.Forms.SplitContainer()
		Me.reportToolbox = New GrapeCity.ActiveReports.Design.Toolbox.Toolbox()
		Me.treeView = New System.Windows.Forms.TreeView()
		Me.reportsLibrary = New GrapeCity.ActiveReports.Design.ReportsLibrary.ReportsLibrary()
		Me.designerExplorerPropertyGridContainer = New System.Windows.Forms.SplitContainer()
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.reportDesigner = New GrapeCity.ActiveReports.Design.Designer()
		Me.GroupEditorContainer = New System.Windows.Forms.Panel()
		Me.groupEditor = New GrapeCity.ActiveReports.Design.GroupEditor.GroupEditor()
		Me.GroupEditorSeparator = New System.Windows.Forms.Panel()
		Me.GroupEditorToggleButton = New System.Windows.Forms.PictureBox()
		Me.label1 = New System.Windows.Forms.Label()
		Me.explorerPropertyGridContainer = New System.Windows.Forms.SplitContainer()
		Me.reportExplorerTabControl = New System.Windows.Forms.TabControl()
		Me.tabReportExplorer = New System.Windows.Forms.TabPage()
		Me.reportExplorer = New GrapeCity.ActiveReports.Design.ReportExplorer.ReportExplorer()
		Me.tabLayers = New System.Windows.Forms.TabPage()
		Me.layerList = New GrapeCity.ActiveReports.Design.LayerList()
		Me.reportPropertyGrid = New System.Windows.Forms.PropertyGrid()
		Me.GroupPanelVisibility = New System.Windows.Forms.ToolTip(Me.components)
		Me.toolStripContainer.SuspendLayout()
		CType(Me.mainContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.mainContainer.Panel1.SuspendLayout()
		Me.mainContainer.Panel2.SuspendLayout()
		Me.mainContainer.SuspendLayout()
		CType(Me.bodyContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.bodyContainer.Panel1.SuspendLayout()
		Me.bodyContainer.Panel2.SuspendLayout()
		Me.bodyContainer.SuspendLayout()
		CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer2.Panel1.SuspendLayout()
		Me.SplitContainer2.Panel2.SuspendLayout()
		Me.SplitContainer2.SuspendLayout()
		CType(Me.splitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splitContainer.Panel1.SuspendLayout()
		Me.splitContainer.Panel2.SuspendLayout()
		Me.splitContainer.SuspendLayout()
		CType(Me.reportToolbox, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.designerExplorerPropertyGridContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.designerExplorerPropertyGridContainer.Panel1.SuspendLayout()
		Me.designerExplorerPropertyGridContainer.Panel2.SuspendLayout()
		Me.designerExplorerPropertyGridContainer.SuspendLayout()
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer1.Panel1.SuspendLayout()
		Me.SplitContainer1.Panel2.SuspendLayout()
		Me.SplitContainer1.SuspendLayout()
		Me.GroupEditorContainer.SuspendLayout()
		Me.GroupEditorSeparator.SuspendLayout()
		CType(Me.GroupEditorToggleButton, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.explorerPropertyGridContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.explorerPropertyGridContainer.Panel1.SuspendLayout()
		Me.explorerPropertyGridContainer.Panel2.SuspendLayout()
		Me.explorerPropertyGridContainer.SuspendLayout()
		Me.reportExplorerTabControl.SuspendLayout()
		Me.tabReportExplorer.SuspendLayout()
		Me.tabLayers.SuspendLayout()
		Me.SuspendLayout()
		'
		'toolStripContainer
		'
		'
		'toolStripContainer.ContentPanel
		'
		resources.ApplyResources(Me.toolStripContainer.ContentPanel, "toolStripContainer.ContentPanel")
		resources.ApplyResources(Me.toolStripContainer, "toolStripContainer")
		Me.toolStripContainer.Name = "toolStripContainer"
		'
		'mainContainer
		'
		resources.ApplyResources(Me.mainContainer, "mainContainer")
		Me.mainContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.mainContainer.Name = "mainContainer"
		'
		'mainContainer.Panel1
		'
		Me.mainContainer.Panel1.Controls.Add(Me.toolStripContainer)
		'
		'mainContainer.Panel2
		'
		Me.mainContainer.Panel2.Controls.Add(Me.bodyContainer)
		'
		'bodyContainer
		'
		resources.ApplyResources(Me.bodyContainer, "bodyContainer")
		Me.bodyContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.bodyContainer.Name = "bodyContainer"
		'
		'bodyContainer.Panel1
		'
		Me.bodyContainer.Panel1.Controls.Add(Me.SplitContainer2)
		'
		'bodyContainer.Panel2
		'
		Me.bodyContainer.Panel2.Controls.Add(Me.designerExplorerPropertyGridContainer)
		'
		'SplitContainer2
		'
		resources.ApplyResources(Me.SplitContainer2, "SplitContainer2")
		Me.SplitContainer2.Name = "SplitContainer2"
		'
		'SplitContainer2.Panel1
		'
		Me.SplitContainer2.Panel1.Controls.Add(Me.splitContainer)
		'
		'SplitContainer2.Panel2
		'
		Me.SplitContainer2.Panel2.Controls.Add(Me.reportsLibrary)
		'
		'splitContainer
		'
		resources.ApplyResources(Me.splitContainer, "splitContainer")
		Me.splitContainer.Name = "splitContainer"
		'
		'splitContainer.Panel1
		'
		Me.splitContainer.Panel1.Controls.Add(Me.reportToolbox)
		'
		'splitContainer.Panel2
		'
		Me.splitContainer.Panel2.Controls.Add(Me.treeView)
		'
		'reportToolbox
		'
		Me.reportToolbox.DesignerHost = Nothing
		resources.ApplyResources(Me.reportToolbox, "reportToolbox")
		Me.reportToolbox.Name = "reportToolbox"
		'
		'treeView
		'
		resources.ApplyResources(Me.treeView, "treeView")
		Me.treeView.Name = "treeView"
		'
		'reportsLibrary
		'
		resources.ApplyResources(Me.reportsLibrary, "reportsLibrary")
		Me.reportsLibrary.Name = "reportsLibrary"
		Me.reportsLibrary.ReportDesigner = Nothing
		'
		'designerExplorerPropertyGridContainer
		'
		resources.ApplyResources(Me.designerExplorerPropertyGridContainer, "designerExplorerPropertyGridContainer")
		Me.designerExplorerPropertyGridContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
		Me.designerExplorerPropertyGridContainer.Name = "designerExplorerPropertyGridContainer"
		'
		'designerExplorerPropertyGridContainer.Panel1
		'
		Me.designerExplorerPropertyGridContainer.Panel1.Controls.Add(Me.SplitContainer1)
		'
		'designerExplorerPropertyGridContainer.Panel2
		'
		Me.designerExplorerPropertyGridContainer.Panel2.Controls.Add(Me.explorerPropertyGridContainer)
		'
		'SplitContainer1
		'
		Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		resources.ApplyResources(Me.SplitContainer1, "SplitContainer1")
		Me.SplitContainer1.Name = "SplitContainer1"
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.Controls.Add(Me.reportDesigner)
		'
		'SplitContainer1.Panel2
		'
		Me.SplitContainer1.Panel2.Controls.Add(Me.GroupEditorContainer)
		'
		'reportDesigner
		'
		resources.ApplyResources(Me.reportDesigner, "reportDesigner")
		Me.reportDesigner.IsDirty = False
		Me.reportDesigner.LockControls = False
		Me.reportDesigner.Name = "reportDesigner"
		Me.reportDesigner.PreviewPages = 10
		Me.reportDesigner.PromptUser = True
		Me.reportDesigner.PropertyGrid = Nothing
		Me.reportDesigner.ReportTabsVisible = True
		Me.reportDesigner.ShowDataSourceIcon = True
		Me.reportDesigner.Toolbox = Nothing
		Me.reportDesigner.ToolBoxItem = Nothing
		'
		'GroupEditorContainer
		'
		Me.GroupEditorContainer.BackColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer))
		Me.GroupEditorContainer.Controls.Add(Me.groupEditor)
		Me.GroupEditorContainer.Controls.Add(Me.GroupEditorSeparator)
		resources.ApplyResources(Me.GroupEditorContainer, "GroupEditorContainer")
		Me.GroupEditorContainer.Name = "GroupEditorContainer"
		'
		'groupEditor
		'
		Me.groupEditor.BackColor = System.Drawing.Color.White
		resources.ApplyResources(Me.groupEditor, "groupEditor")
		Me.groupEditor.Name = "groupEditor"
		Me.groupEditor.ReportDesigner = Nothing
		'
		'GroupEditorSeparator
		'
		Me.GroupEditorSeparator.BackColor = System.Drawing.Color.Gainsboro
		Me.GroupEditorSeparator.Controls.Add(Me.GroupEditorToggleButton)
		Me.GroupEditorSeparator.Controls.Add(Me.label1)
		resources.ApplyResources(Me.GroupEditorSeparator, "GroupEditorSeparator")
		Me.GroupEditorSeparator.Name = "GroupEditorSeparator"
		'
		'GroupEditorToggleButton
		'
		resources.ApplyResources(Me.GroupEditorToggleButton, "GroupEditorToggleButton")
		Me.GroupEditorToggleButton.Name = "GroupEditorToggleButton"
		Me.GroupEditorToggleButton.TabStop = False
		'
		'label1
		'
		resources.ApplyResources(Me.label1, "label1")
		Me.label1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.label1.Name = "label1"
		Me.label1.UseCompatibleTextRendering = True
		'
		'explorerPropertyGridContainer
		'
		resources.ApplyResources(Me.explorerPropertyGridContainer, "explorerPropertyGridContainer")
		Me.explorerPropertyGridContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.explorerPropertyGridContainer.Name = "explorerPropertyGridContainer"
		'
		'explorerPropertyGridContainer.Panel1
		'
		Me.explorerPropertyGridContainer.Panel1.Controls.Add(Me.reportExplorerTabControl)
		'
		'explorerPropertyGridContainer.Panel2
		'
		Me.explorerPropertyGridContainer.Panel2.Controls.Add(Me.reportPropertyGrid)
		'
		'reportExplorerTabControl
		'
		Me.reportExplorerTabControl.Controls.Add(Me.tabReportExplorer)
		Me.reportExplorerTabControl.Controls.Add(Me.tabLayers)
		resources.ApplyResources(Me.reportExplorerTabControl, "reportExplorerTabControl")
		Me.reportExplorerTabControl.Name = "reportExplorerTabControl"
		Me.reportExplorerTabControl.SelectedIndex = 0
		'
		'tabReportExplorer
		'
		Me.tabReportExplorer.Controls.Add(Me.reportExplorer)
		resources.ApplyResources(Me.tabReportExplorer, "tabReportExplorer")
		Me.tabReportExplorer.Name = "tabReportExplorer"
		Me.tabReportExplorer.UseVisualStyleBackColor = True
		'
		'reportExplorer
		'
		resources.ApplyResources(Me.reportExplorer, "reportExplorer")
		Me.reportExplorer.Name = "reportExplorer"
		Me.reportExplorer.ReportDesigner = Nothing
		'
		'tabLayers
		'
		Me.tabLayers.Controls.Add(Me.layerList)
		resources.ApplyResources(Me.tabLayers, "tabLayers")
		Me.tabLayers.Name = "tabLayers"
		Me.tabLayers.Tag = "Layers"
		Me.tabLayers.UseVisualStyleBackColor = True
		'
		'layerList
		'
		resources.ApplyResources(Me.layerList, "layerList")
		Me.layerList.Name = "layerList"
		Me.layerList.ReportDesigner = Nothing
		'
		'reportPropertyGrid
		'
		Me.reportPropertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText
		resources.ApplyResources(Me.reportPropertyGrid, "reportPropertyGrid")
		Me.reportPropertyGrid.Name = "reportPropertyGrid"
		'
		'ReportsForm
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.mainContainer)
		Me.Name = "ReportsForm"
		Me.toolStripContainer.ResumeLayout(False)
		Me.toolStripContainer.PerformLayout()
		Me.mainContainer.Panel1.ResumeLayout(False)
		Me.mainContainer.Panel2.ResumeLayout(False)
		CType(Me.mainContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.mainContainer.ResumeLayout(False)
		Me.bodyContainer.Panel1.ResumeLayout(False)
		Me.bodyContainer.Panel2.ResumeLayout(False)
		CType(Me.bodyContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.bodyContainer.ResumeLayout(False)
		Me.SplitContainer2.Panel1.ResumeLayout(False)
		Me.SplitContainer2.Panel2.ResumeLayout(False)
		CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer2.ResumeLayout(False)
		Me.splitContainer.Panel1.ResumeLayout(False)
		Me.splitContainer.Panel2.ResumeLayout(False)
		CType(Me.splitContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splitContainer.ResumeLayout(False)
		CType(Me.reportToolbox, System.ComponentModel.ISupportInitialize).EndInit()
		Me.designerExplorerPropertyGridContainer.Panel1.ResumeLayout(False)
		Me.designerExplorerPropertyGridContainer.Panel2.ResumeLayout(False)
		CType(Me.designerExplorerPropertyGridContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.designerExplorerPropertyGridContainer.ResumeLayout(False)
		Me.SplitContainer1.Panel1.ResumeLayout(False)
		Me.SplitContainer1.Panel2.ResumeLayout(False)
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer1.ResumeLayout(False)
		Me.GroupEditorContainer.ResumeLayout(False)
		Me.GroupEditorSeparator.ResumeLayout(False)
		CType(Me.GroupEditorToggleButton, System.ComponentModel.ISupportInitialize).EndInit()
		Me.explorerPropertyGridContainer.Panel1.ResumeLayout(False)
		Me.explorerPropertyGridContainer.Panel2.ResumeLayout(False)
		CType(Me.explorerPropertyGridContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.explorerPropertyGridContainer.ResumeLayout(False)
		Me.reportExplorerTabControl.ResumeLayout(False)
		Me.tabReportExplorer.ResumeLayout(False)
		Me.tabLayers.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents toolStripContainer As System.Windows.Forms.ToolStripContainer
	Friend WithEvents mainContainer As System.Windows.Forms.SplitContainer
	Friend WithEvents bodyContainer As System.Windows.Forms.SplitContainer
	Friend WithEvents splitContainer As System.Windows.Forms.SplitContainer
	Friend WithEvents reportToolbox As GrapeCity.ActiveReports.Design.Toolbox.Toolbox
	Friend WithEvents treeView As System.Windows.Forms.TreeView
	Friend WithEvents designerExplorerPropertyGridContainer As System.Windows.Forms.SplitContainer
	Friend WithEvents explorerPropertyGridContainer As System.Windows.Forms.SplitContainer
	Friend WithEvents reportPropertyGrid As System.Windows.Forms.PropertyGrid
	Friend WithEvents reportExplorerTabControl As System.Windows.Forms.TabControl
	Friend WithEvents tabReportExplorer As System.Windows.Forms.TabPage
	Friend WithEvents tabLayers As System.Windows.Forms.TabPage
	Friend WithEvents reportExplorer As GrapeCity.ActiveReports.Design.ReportExplorer.ReportExplorer
	Friend WithEvents layerList As GrapeCity.ActiveReports.Design.LayerList
	Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
	Friend WithEvents reportDesigner As GrapeCity.ActiveReports.Design.Designer
	Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
	Friend WithEvents reportsLibrary As GrapeCity.ActiveReports.Design.ReportsLibrary.ReportsLibrary
	Private WithEvents GroupEditorContainer As Panel
	Private WithEvents groupEditor As Design.GroupEditor.GroupEditor
	Private WithEvents GroupEditorSeparator As Panel
	Private WithEvents GroupEditorToggleButton As PictureBox
	Private WithEvents label1 As Label
	Private WithEvents GroupPanelVisibility As ToolTip

End Class
