 _
Partial Class ViewerForm
	Inherits System.Windows.Forms.Form



Private components As System.ComponentModel.IContainer

<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ViewerForm))
		Me.splitContainer = New System.Windows.Forms.SplitContainer()
		Me.designTimeRptBtn = New System.Windows.Forms.Button()
		Me.runTimeRptBtn = New System.Windows.Forms.Button()
		Me.arvMain = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		CType(Me.splitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splitContainer.Panel1.SuspendLayout()
		Me.splitContainer.Panel2.SuspendLayout()
		Me.splitContainer.SuspendLayout()
		Me.SuspendLayout()
		'
		'splitContainer
		'
		resources.ApplyResources(Me.splitContainer, "splitContainer")
		Me.splitContainer.Name = "splitContainer"
		'
		'splitContainer.Panel1
		'
		Me.splitContainer.Panel1.BackColor = System.Drawing.SystemColors.Control
		Me.splitContainer.Panel1.Controls.Add(Me.designTimeRptBtn)
		Me.splitContainer.Panel1.Controls.Add(Me.runTimeRptBtn)
		'
		'splitContainer.Panel2
		'
		Me.splitContainer.Panel2.Controls.Add(Me.arvMain)
		'
		'designTimeRptBtn
		'
		resources.ApplyResources(Me.designTimeRptBtn, "designTimeRptBtn")
		Me.designTimeRptBtn.Name = "designTimeRptBtn"
		Me.designTimeRptBtn.UseVisualStyleBackColor = True
		'
		'runTimeRptBtn
		'
		resources.ApplyResources(Me.runTimeRptBtn, "runTimeRptBtn")
		Me.runTimeRptBtn.Name = "runTimeRptBtn"
		Me.runTimeRptBtn.UseVisualStyleBackColor = True
		'
		'arvMain
		'
		Me.arvMain.BackColor = System.Drawing.SystemColors.Control
		Me.arvMain.CurrentPage = 0
		resources.ApplyResources(Me.arvMain, "arvMain")
		Me.arvMain.Name = "arvMain"
		Me.arvMain.PreviewPages = 0
		'
		'
		'
		'
		'
		'
		Me.arvMain.Sidebar.ParametersPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.ParametersPanel.Width = 200
		'
		'
		'
		Me.arvMain.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.SearchPanel.Width = 200
		'
		'
		'
		Me.arvMain.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.ThumbnailsPanel.Width = 200
		Me.arvMain.Sidebar.ThumbnailsPanel.Zoom = 0.1R
		'
		'
		'
		Me.arvMain.Sidebar.TocPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.TocPanel.Expanded = True
		Me.arvMain.Sidebar.TocPanel.Width = 200
		Me.arvMain.Sidebar.Width = 200
		'
		'ViewerForm
		'
		resources.ApplyResources(Me, "$this")
		Me.Controls.Add(Me.splitContainer)
		Me.Name = "ViewerForm"
		Me.splitContainer.Panel1.ResumeLayout(False)
		Me.splitContainer.Panel2.ResumeLayout(False)
		CType(Me.splitContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splitContainer.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents splitContainer As System.Windows.Forms.SplitContainer
	Friend WithEvents runTimeRptBtn As System.Windows.Forms.Button
	Friend WithEvents arvMain As Viewer.Win.Viewer
	Friend WithEvents designTimeRptBtn As System.Windows.Forms.Button

End Class
