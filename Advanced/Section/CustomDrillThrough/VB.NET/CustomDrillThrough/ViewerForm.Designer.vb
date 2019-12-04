 _
Partial Class ViewerForm
	Inherits System.Windows.Forms.Form



Private components As System.ComponentModel.Container = Nothing

Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ViewerForm))
		Me.arvMain = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.SuspendLayout()
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
		Me.Controls.Add(Me.arvMain)
		Me.Name = "ViewerForm"
		Me.ResumeLayout(False)

	End Sub
	Private WithEvents arvMain As ActiveReports.Viewer.Win.Viewer

End Class
