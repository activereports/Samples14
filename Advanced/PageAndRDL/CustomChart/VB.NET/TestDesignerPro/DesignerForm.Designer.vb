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
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DesignerForm))
		Me.reportExplorer = New GrapeCity.ActiveReports.Design.ReportExplorer.ReportExplorer()
		Me.toolStripContainer = New System.Windows.Forms.ToolStripContainer()
		Me.mainContainer = New System.Windows.Forms.SplitContainer()
		Me.bodyContainer = New System.Windows.Forms.SplitContainer()
		Me.reportToolbox = New GrapeCity.ActiveReports.Design.Toolbox.Toolbox()
		Me.designerexplorerpropertygridContainer = New System.Windows.Forms.SplitContainer()
		Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.reportDesigner = New GrapeCity.ActiveReports.Design.Designer()
		Me.GroupEditorContainer = New System.Windows.Forms.Panel()
		Me.groupEditor = New GrapeCity.ActiveReports.Design.GroupEditor.GroupEditor()
		Me.GroupEditorSeparator = New System.Windows.Forms.Panel()
		Me.GroupEditorToggleButton = New System.Windows.Forms.PictureBox()
		Me.label1 = New System.Windows.Forms.Label()
		Me.explorerpropertygridContainer = New System.Windows.Forms.SplitContainer()
		Me.propertyGrid = New System.Windows.Forms.PropertyGrid()
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
		CType(Me.reportToolbox, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.designerexplorerpropertygridContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.designerexplorerpropertygridContainer.Panel1.SuspendLayout()
		Me.designerexplorerpropertygridContainer.Panel2.SuspendLayout()
		Me.designerexplorerpropertygridContainer.SuspendLayout()
		CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splitContainer1.Panel1.SuspendLayout()
		Me.splitContainer1.Panel2.SuspendLayout()
		Me.splitContainer1.SuspendLayout()
		Me.GroupEditorContainer.SuspendLayout()
		Me.GroupEditorSeparator.SuspendLayout()
		CType(Me.GroupEditorToggleButton, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.explorerpropertygridContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.explorerpropertygridContainer.Panel1.SuspendLayout()
		Me.explorerpropertygridContainer.Panel2.SuspendLayout()
		Me.explorerpropertygridContainer.SuspendLayout()
		Me.SuspendLayout()
		'
		'reportExplorer
		'
		resources.ApplyResources(Me.reportExplorer, "reportExplorer")
		Me.reportExplorer.Name = "reportExplorer"
		Me.reportExplorer.ReportDesigner = Nothing
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
		Me.bodyContainer.Panel1.Controls.Add(Me.reportToolbox)
		'
		'bodyContainer.Panel2
		'
		Me.bodyContainer.Panel2.Controls.Add(Me.designerexplorerpropertygridContainer)
		'
		'reportToolbox
		'
		Me.reportToolbox.DesignerHost = Nothing
		resources.ApplyResources(Me.reportToolbox, "reportToolbox")
		Me.reportToolbox.Name = "reportToolbox"
		'
		'designerexplorerpropertygridContainer
		'
		resources.ApplyResources(Me.designerexplorerpropertygridContainer, "designerexplorerpropertygridContainer")
		Me.designerexplorerpropertygridContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
		Me.designerexplorerpropertygridContainer.Name = "designerexplorerpropertygridContainer"
		'
		'designerexplorerpropertygridContainer.Panel1
		'
		Me.designerexplorerpropertygridContainer.Panel1.Controls.Add(Me.splitContainer1)
		'
		'designerexplorerpropertygridContainer.Panel2
		'
		Me.designerexplorerpropertygridContainer.Panel2.Controls.Add(Me.explorerpropertygridContainer)
		'
		'splitContainer1
		'
		Me.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		resources.ApplyResources(Me.splitContainer1, "splitContainer1")
		Me.splitContainer1.Name = "splitContainer1"
		'
		'splitContainer1.Panel1
		'
		Me.splitContainer1.Panel1.Controls.Add(Me.reportDesigner)
		'
		'splitContainer1.Panel2
		'
		Me.splitContainer1.Panel2.Controls.Add(Me.GroupEditorContainer)
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
		Me.reportDesigner.ResourceLocator.ServiceProvider = Nothing
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
		'explorerpropertygridContainer
		'
		resources.ApplyResources(Me.explorerpropertygridContainer, "explorerpropertygridContainer")
		Me.explorerpropertygridContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.explorerpropertygridContainer.Name = "explorerpropertygridContainer"
		'
		'explorerpropertygridContainer.Panel1
		'
		Me.explorerpropertygridContainer.Panel1.Controls.Add(Me.reportExplorer)
		'
		'explorerpropertygridContainer.Panel2
		'
		Me.explorerpropertygridContainer.Panel2.Controls.Add(Me.propertyGrid)
		'
		'propertyGrid
		'
		Me.propertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText
		resources.ApplyResources(Me.propertyGrid, "propertyGrid")
		Me.propertyGrid.Name = "propertyGrid"
		'
		'DesignerForm
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.mainContainer)
		Me.Name = "DesignerForm"
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
		CType(Me.reportToolbox, System.ComponentModel.ISupportInitialize).EndInit()
		Me.designerexplorerpropertygridContainer.Panel1.ResumeLayout(False)
		Me.designerexplorerpropertygridContainer.Panel2.ResumeLayout(False)
		CType(Me.designerexplorerpropertygridContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.designerexplorerpropertygridContainer.ResumeLayout(False)
		Me.splitContainer1.Panel1.ResumeLayout(False)
		Me.splitContainer1.Panel2.ResumeLayout(False)
		CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splitContainer1.ResumeLayout(False)
		Me.GroupEditorContainer.ResumeLayout(False)
		Me.GroupEditorSeparator.ResumeLayout(False)
		CType(Me.GroupEditorToggleButton, System.ComponentModel.ISupportInitialize).EndInit()
		Me.explorerpropertygridContainer.Panel1.ResumeLayout(False)
		Me.explorerpropertygridContainer.Panel2.ResumeLayout(False)
		CType(Me.explorerpropertygridContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.explorerpropertygridContainer.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	Private WithEvents reportExplorer As GrapeCity.ActiveReports.Design.ReportExplorer.ReportExplorer
	Private WithEvents toolStripContainer As ToolStripContainer
	Private WithEvents mainContainer As SplitContainer
	Private WithEvents bodyContainer As SplitContainer
	Private WithEvents reportToolbox As GrapeCity.ActiveReports.Design.Toolbox.Toolbox
	Private WithEvents designerexplorerpropertygridContainer As SplitContainer
	Private WithEvents splitContainer1 As SplitContainer
	Private WithEvents reportDesigner As GrapeCity.ActiveReports.Design.Designer
	Private WithEvents explorerpropertygridContainer As SplitContainer
	Private WithEvents propertyGrid As PropertyGrid
	Private WithEvents GroupEditorContainer As Panel
	Private WithEvents groupEditor As Design.GroupEditor.GroupEditor
	Private WithEvents GroupEditorSeparator As Panel
	Private WithEvents GroupEditorToggleButton As PictureBox
	Private WithEvents label1 As Label
	Friend WithEvents GroupPanelVisibility As ToolTip
End Class
