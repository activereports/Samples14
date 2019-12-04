Imports GrapeCity.ActiveReports.Design
Namespace UI
	Partial Class ReportsForm
        ' <summary>
        'Required designer variable.
        ' </summary>
        Private components As System.ComponentModel.IContainer = Nothing
        ' <summary>
        'Clean up any resources being used.
        ' </summary>
        '<param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso Not ((components) Is Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
        ' <summary>

        'Required method For Designer support - Do Not modify
        'the contents of this method with the code editor.
        '</summary>

        Private Sub InitializeComponent()
			Me.Size = New System.Drawing.Size(1012, 652)
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportsForm))
			Me.Viewer1 = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
			Me.Size = New System.Drawing.Size(1012, 652)
			Me.SuspendLayout()
			'Viewer1
			Me.Viewer1.CurrentPage = 0
			resources.ApplyResources(Me.Viewer1, "Viewer1")
			Me.Viewer1.Name = "Viewer1"
            Me.Viewer1.PreviewPages = 0
            '
            resources.ApplyResources(Me.Viewer1.Sidebar.ParametersPanel, "Viewer1.Sidebar.ParametersPanel")
            Me.Viewer1.Sidebar.ParametersPanel.ContextMenu = Nothing
            Me.Viewer1.Sidebar.ParametersPanel.Width = 200
            '
            resources.ApplyResources(Me.Viewer1.Sidebar.SearchPanel, "Viewer1.Sidebar.SearchPanel")
            Me.Viewer1.Sidebar.SearchPanel.ContextMenu = Nothing
            Me.Viewer1.Sidebar.SearchPanel.Width = 200
            '
            resources.ApplyResources(Me.Viewer1.Sidebar.ThumbnailsPanel, "Viewer1.Sidebar.ThumbnailsPanel")
            Me.Viewer1.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
            Me.Viewer1.Sidebar.ThumbnailsPanel.Width = 200
            '
            resources.ApplyResources(Me.Viewer1.Sidebar.TocPanel, "Viewer1.Sidebar.TocPanel")
            Me.Viewer1.Sidebar.TocPanel.ContextMenu = Nothing
            Me.Viewer1.Sidebar.TocPanel.Width = 200
			Me.Viewer1.Sidebar.Width = 200
			'ReportsForm
			resources.ApplyResources(Me, "$this")
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.Viewer1)
			Me.Name = "ReportsForm"
			Me.ResumeLayout(False)
			Me.Viewer1.Dock = System.Windows.Forms.DockStyle.Fill
		End Sub
		Friend WithEvents Viewer1 As GrapeCity.ActiveReports.Viewer.Win.Viewer
	End Class

End Namespace
