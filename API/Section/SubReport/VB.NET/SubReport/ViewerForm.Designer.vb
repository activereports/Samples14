 _
Partial Class ViewerForm
	Inherits System.Windows.Forms.Form



Private components As System.ComponentModel.IContainer

<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ViewerForm))
		Me.splitContainer = New System.Windows.Forms.SplitContainer()
		Me.cboReport = New System.Windows.Forms.ComboBox()
		Me.arvMain = New GrapeCity.ActiveReports.Viewer.Win.Viewer()

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
		Me.splitContainer.Panel1.Controls.Add(Me.cboReport)
		'
		'splitContainer.Panel2
		'
		Me.splitContainer.Panel2.Controls.Add(Me.arvMain)
		'
		'cboReport
		'
		Me.cboReport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		resources.ApplyResources(Me.cboReport, "cboReport")
		Me.cboReport.FormattingEnabled = True
		Me.cboReport.Name = "cboReport"
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
		Me.arvMain.Sidebar.ParametersPanel.Width = 180
		'
		'
		'
		Me.arvMain.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.SearchPanel.Width = 180
		'
		'
		'
		Me.arvMain.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.ThumbnailsPanel.Width = 180
		'
		'
		'
		Me.arvMain.Sidebar.TocPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.TocPanel.Width = 180
		Me.arvMain.Sidebar.Width = 180
		'
		'ViewerForm
		'
		resources.ApplyResources(Me, "$this")
		Me.Controls.Add(Me.splitContainer)
		Me.Name = "ViewerForm"
		Me.splitContainer.Panel1.ResumeLayout(False)
		Me.splitContainer.Panel2.ResumeLayout(False)

		Me.splitContainer.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents splitContainer As System.Windows.Forms.SplitContainer
	Friend WithEvents cboReport As System.Windows.Forms.ComboBox
	Friend WithEvents arvMain As GrapeCity.ActiveReports.Viewer.Win.Viewer

End Class
