<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DesignerForm
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
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


	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer


	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DesignerForm))
		Me.splitContainerGroupEditor = New System.Windows.Forms.SplitContainer()
		Me.arDesigner = New GrapeCity.ActiveReports.Design.Designer()
		Me.arPropertyGrid = New System.Windows.Forms.PropertyGrid()
		Me.GroupEditorContainer = New System.Windows.Forms.Panel()
		Me.groupEditor = New GrapeCity.ActiveReports.Design.GroupEditor.GroupEditor()
		Me.GroupEditorSeparator = New System.Windows.Forms.Panel()
		Me.GroupEditorToggleButton = New System.Windows.Forms.PictureBox()
		Me.label1 = New System.Windows.Forms.Label()
		Me.toolStripContainer1 = New System.Windows.Forms.ToolStripContainer()
		Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
		Me.toolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.splitContainerToolbox = New System.Windows.Forms.SplitContainer()
		Me.splitContainerReportsLibrary = New System.Windows.Forms.SplitContainer()
		Me.arToolbox = New GrapeCity.ActiveReports.Design.Toolbox.Toolbox()
		Me.reportsLibrary = New GrapeCity.ActiveReports.Design.ReportsLibrary.ReportsLibrary()
		Me.splitContainerDesigner = New System.Windows.Forms.SplitContainer()
		Me.splitContainerProperties = New System.Windows.Forms.SplitContainer()
		Me.tabControl1 = New System.Windows.Forms.TabControl()
		Me.reportExplorerTabPage = New System.Windows.Forms.TabPage()
		Me.reportExplorer = New GrapeCity.ActiveReports.Design.ReportExplorer.ReportExplorer()
		Me.layersTabPage = New System.Windows.Forms.TabPage()
		Me.layerList = New GrapeCity.ActiveReports.Design.LayerList()
		Me.GroupPanelVisibility = New System.Windows.Forms.ToolTip(Me.components)
		Me.saveDialog = New System.Windows.Forms.SaveFileDialog()
		Me.openDialog = New System.Windows.Forms.OpenFileDialog()
		CType(Me.splitContainerGroupEditor, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splitContainerGroupEditor.Panel1.SuspendLayout()
		Me.splitContainerGroupEditor.Panel2.SuspendLayout()
		Me.splitContainerGroupEditor.SuspendLayout()
		Me.GroupEditorContainer.SuspendLayout()
		Me.GroupEditorSeparator.SuspendLayout()
		CType(Me.GroupEditorToggleButton, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.toolStripContainer1.BottomToolStripPanel.SuspendLayout()
		Me.toolStripContainer1.ContentPanel.SuspendLayout()
		Me.toolStripContainer1.SuspendLayout()
		Me.statusStrip1.SuspendLayout()
		CType(Me.splitContainerToolbox, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splitContainerToolbox.Panel1.SuspendLayout()
		Me.splitContainerToolbox.Panel2.SuspendLayout()
		Me.splitContainerToolbox.SuspendLayout()
		CType(Me.splitContainerReportsLibrary, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splitContainerReportsLibrary.Panel1.SuspendLayout()
		Me.splitContainerReportsLibrary.Panel2.SuspendLayout()
		Me.splitContainerReportsLibrary.SuspendLayout()
		CType(Me.arToolbox, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.splitContainerDesigner, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splitContainerDesigner.Panel1.SuspendLayout()
		Me.splitContainerDesigner.Panel2.SuspendLayout()
		Me.splitContainerDesigner.SuspendLayout()
		CType(Me.splitContainerProperties, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splitContainerProperties.Panel1.SuspendLayout()
		Me.splitContainerProperties.Panel2.SuspendLayout()
		Me.splitContainerProperties.SuspendLayout()
		Me.tabControl1.SuspendLayout()
		Me.reportExplorerTabPage.SuspendLayout()
		Me.layersTabPage.SuspendLayout()
		Me.SuspendLayout()
		'
		'splitContainerGroupEditor
		'
		resources.ApplyResources(Me.splitContainerGroupEditor, "splitContainerGroupEditor")
		Me.splitContainerGroupEditor.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
		Me.splitContainerGroupEditor.Name = "splitContainerGroupEditor"
		'
		'splitContainerGroupEditor.Panel1
		'
		Me.splitContainerGroupEditor.Panel1.Controls.Add(Me.arDesigner)
		'
		'splitContainerGroupEditor.Panel2
		'
		Me.splitContainerGroupEditor.Panel2.Controls.Add(Me.GroupEditorContainer)
		resources.ApplyResources(Me.splitContainerGroupEditor.Panel2, "splitContainerGroupEditor.Panel2")
		'
		'arDesigner
		'
		resources.ApplyResources(Me.arDesigner, "arDesigner")
		Me.arDesigner.IsDirty = False
		Me.arDesigner.LockControls = False
		Me.arDesigner.Name = "arDesigner"
		Me.arDesigner.PreviewPages = 10
		Me.arDesigner.PromptUser = False
		Me.arDesigner.PropertyGrid = Me.arPropertyGrid
		Me.arDesigner.ReportTabsVisible = True
		Me.arDesigner.ShowDataSourceIcon = True
		Me.arDesigner.Toolbox = Nothing
		Me.arDesigner.ToolBoxItem = Nothing
		'
		'arPropertyGrid
		'
		resources.ApplyResources(Me.arPropertyGrid, "arPropertyGrid")
		Me.arPropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar
		Me.arPropertyGrid.Name = "arPropertyGrid"
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
		'toolStripContainer1
		'
		'
		'toolStripContainer1.BottomToolStripPanel
		'
		Me.toolStripContainer1.BottomToolStripPanel.Controls.Add(Me.statusStrip1)
		'
		'toolStripContainer1.ContentPanel
		'
		Me.toolStripContainer1.ContentPanel.Controls.Add(Me.splitContainerToolbox)
		resources.ApplyResources(Me.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel")
		resources.ApplyResources(Me.toolStripContainer1, "toolStripContainer1")
		Me.toolStripContainer1.Name = "toolStripContainer1"
		'
		'statusStrip1
		'
		resources.ApplyResources(Me.statusStrip1, "statusStrip1")
		Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripStatusLabel1})
		Me.statusStrip1.Name = "statusStrip1"
		'
		'toolStripStatusLabel1
		'
		Me.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
		Me.toolStripStatusLabel1.Name = "toolStripStatusLabel1"
		resources.ApplyResources(Me.toolStripStatusLabel1, "toolStripStatusLabel1")
		Me.toolStripStatusLabel1.Spring = True
		'
		'splitContainerToolbox
		'
		resources.ApplyResources(Me.splitContainerToolbox, "splitContainerToolbox")
		Me.splitContainerToolbox.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.splitContainerToolbox.Name = "splitContainerToolbox"
		'
		'splitContainerToolbox.Panel1
		'
		Me.splitContainerToolbox.Panel1.Controls.Add(Me.splitContainerReportsLibrary)
		'
		'splitContainerToolbox.Panel2
		'
		Me.splitContainerToolbox.Panel2.Controls.Add(Me.splitContainerDesigner)
		'
		'splitContainerReportsLibrary
		'
		resources.ApplyResources(Me.splitContainerReportsLibrary, "splitContainerReportsLibrary")
		Me.splitContainerReportsLibrary.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
		Me.splitContainerReportsLibrary.Name = "splitContainerReportsLibrary"
		'
		'splitContainerReportsLibrary.Panel1
		'
		Me.splitContainerReportsLibrary.Panel1.Controls.Add(Me.arToolbox)
		'
		'splitContainerReportsLibrary.Panel2
		'
		Me.splitContainerReportsLibrary.Panel2.Controls.Add(Me.reportsLibrary)
		'
		'arToolbox
		'
		Me.arToolbox.DesignerHost = Nothing
		resources.ApplyResources(Me.arToolbox, "arToolbox")
		Me.arToolbox.Name = "arToolbox"
		'
		'reportsLibrary
		'
		resources.ApplyResources(Me.reportsLibrary, "reportsLibrary")
		Me.reportsLibrary.Name = "reportsLibrary"
		Me.reportsLibrary.ReportDesigner = Nothing
		'
		'splitContainerDesigner
		'
		resources.ApplyResources(Me.splitContainerDesigner, "splitContainerDesigner")
		Me.splitContainerDesigner.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
		Me.splitContainerDesigner.Name = "splitContainerDesigner"
		'
		'splitContainerDesigner.Panel1
		'
		Me.splitContainerDesigner.Panel1.Controls.Add(Me.splitContainerGroupEditor)
		'
		'splitContainerDesigner.Panel2
		'
		Me.splitContainerDesigner.Panel2.Controls.Add(Me.splitContainerProperties)
		'
		'splitContainerProperties
		'
		resources.ApplyResources(Me.splitContainerProperties, "splitContainerProperties")
		Me.splitContainerProperties.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.splitContainerProperties.Name = "splitContainerProperties"
		'
		'splitContainerProperties.Panel1
		'
		Me.splitContainerProperties.Panel1.Controls.Add(Me.tabControl1)
		'
		'splitContainerProperties.Panel2
		'
		Me.splitContainerProperties.Panel2.Controls.Add(Me.arPropertyGrid)
		'
		'tabControl1
		'
		Me.tabControl1.Controls.Add(Me.reportExplorerTabPage)
		Me.tabControl1.Controls.Add(Me.layersTabPage)
		resources.ApplyResources(Me.tabControl1, "tabControl1")
		Me.tabControl1.Name = "tabControl1"
		Me.tabControl1.SelectedIndex = 0
		'
		'reportExplorerTabPage
		'
		Me.reportExplorerTabPage.Controls.Add(Me.reportExplorer)
		resources.ApplyResources(Me.reportExplorerTabPage, "reportExplorerTabPage")
		Me.reportExplorerTabPage.Name = "reportExplorerTabPage"
		'
		'reportExplorer
		'
		resources.ApplyResources(Me.reportExplorer, "reportExplorer")
		Me.reportExplorer.Name = "reportExplorer"
		Me.reportExplorer.ReportDesigner = Nothing
		'
		'layersTabPage
		'
		Me.layersTabPage.Controls.Add(Me.layerList)
		resources.ApplyResources(Me.layersTabPage, "layersTabPage")
		Me.layersTabPage.Name = "layersTabPage"
		'
		'layerList
		'
		resources.ApplyResources(Me.layerList, "layerList")
		Me.layerList.Name = "layerList"
		Me.layerList.ReportDesigner = Nothing
		'
		'openDialog
		'
		resources.ApplyResources(Me.openDialog, "openDialog")
		'
		'DesignerForm
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.toolStripContainer1)
		Me.Name = "DesignerForm"
		Me.splitContainerGroupEditor.Panel1.ResumeLayout(False)
		Me.splitContainerGroupEditor.Panel2.ResumeLayout(False)
		CType(Me.splitContainerGroupEditor, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splitContainerGroupEditor.ResumeLayout(False)
		Me.GroupEditorContainer.ResumeLayout(False)
		Me.GroupEditorSeparator.ResumeLayout(False)
		CType(Me.GroupEditorToggleButton, System.ComponentModel.ISupportInitialize).EndInit()
		Me.toolStripContainer1.BottomToolStripPanel.ResumeLayout(False)
		Me.toolStripContainer1.BottomToolStripPanel.PerformLayout()
		Me.toolStripContainer1.ContentPanel.ResumeLayout(False)
		Me.toolStripContainer1.ResumeLayout(False)
		Me.toolStripContainer1.PerformLayout()
		Me.statusStrip1.ResumeLayout(False)
		Me.statusStrip1.PerformLayout()
		Me.splitContainerToolbox.Panel1.ResumeLayout(False)
		Me.splitContainerToolbox.Panel2.ResumeLayout(False)
		CType(Me.splitContainerToolbox, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splitContainerToolbox.ResumeLayout(False)
		Me.splitContainerReportsLibrary.Panel1.ResumeLayout(False)
		Me.splitContainerReportsLibrary.Panel2.ResumeLayout(False)
		CType(Me.splitContainerReportsLibrary, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splitContainerReportsLibrary.ResumeLayout(False)
		CType(Me.arToolbox, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splitContainerDesigner.Panel1.ResumeLayout(False)
		Me.splitContainerDesigner.Panel2.ResumeLayout(False)
		CType(Me.splitContainerDesigner, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splitContainerDesigner.ResumeLayout(False)
		Me.splitContainerProperties.Panel1.ResumeLayout(False)
		Me.splitContainerProperties.Panel2.ResumeLayout(False)
		CType(Me.splitContainerProperties, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splitContainerProperties.ResumeLayout(False)
		Me.tabControl1.ResumeLayout(False)
		Me.reportExplorerTabPage.ResumeLayout(False)
		Me.layersTabPage.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	Private WithEvents arDesigner As Design.Designer
	Private WithEvents arPropertyGrid As PropertyGrid
	Private WithEvents reportsLibrary As Design.ReportsLibrary.ReportsLibrary
	Private WithEvents groupEditor As Design.GroupEditor.GroupEditor
	Private WithEvents GroupEditorSeparator As Panel
	Private WithEvents GroupEditorToggleButton As PictureBox
	Private WithEvents label1 As Label
	Private WithEvents tabControl1 As TabControl
	Private WithEvents reportExplorerTabPage As TabPage
	Private WithEvents reportExplorer As Design.ReportExplorer.ReportExplorer
	Private WithEvents layersTabPage As TabPage
	Private WithEvents layerList As Design.LayerList
	Private WithEvents GroupEditorContainer As Panel
	Private WithEvents GroupPanelVisibility As ToolTip
	Private WithEvents saveDialog As SaveFileDialog
	Private WithEvents openDialog As OpenFileDialog
	Private WithEvents splitContainerProperties As SplitContainer
	Private WithEvents splitContainerGroupEditor As SplitContainer
	Private WithEvents splitContainerDesigner As SplitContainer
	Private WithEvents splitContainerReportsLibrary As SplitContainer
	Private WithEvents arToolbox As Design.Toolbox.Toolbox
	Private WithEvents splitContainerToolbox As SplitContainer
	Private WithEvents toolStripContainer1 As ToolStripContainer
	Private WithEvents statusStrip1 As StatusStrip
	Private WithEvents toolStripStatusLabel1 As ToolStripStatusLabel
End Class
