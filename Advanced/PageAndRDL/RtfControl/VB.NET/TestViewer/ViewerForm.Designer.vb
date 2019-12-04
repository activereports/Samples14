Partial Class ViewerForm
	Private components As System.ComponentModel.IContainer = Nothing

	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso (components IsNot Nothing) Then
			components.Dispose()
		End If

		MyBase.Dispose(disposing)
	End Sub

	Private Sub InitializeComponent()
		Me.viewer1 = New Viewer.Win.Viewer()
		Me.SuspendLayout()
		Me.viewer1.CurrentPage = 0
		Me.viewer1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.viewer1.Location = New System.Drawing.Point(0, 0)
		Me.viewer1.Name = "viewer1"
		Me.viewer1.PreviewPages = 0
		Me.viewer1.Sidebar.ParametersPanel.ContextMenu = Nothing
		Me.viewer1.Sidebar.ParametersPanel.Text = "Parameters"
		Me.viewer1.Sidebar.ParametersPanel.Width = 200
		Me.viewer1.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.viewer1.Sidebar.SearchPanel.Text = "Search results"
		Me.viewer1.Sidebar.SearchPanel.Width = 200
		Me.viewer1.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.viewer1.Sidebar.ThumbnailsPanel.Text = "Page thumbnails"
		Me.viewer1.Sidebar.ThumbnailsPanel.Width = 200
		Me.viewer1.Sidebar.ThumbnailsPanel.Zoom = 0.1R
		Me.viewer1.Sidebar.TocPanel.ContextMenu = Nothing
		Me.viewer1.Sidebar.TocPanel.Expanded = True
		Me.viewer1.Sidebar.TocPanel.Text = "Document map"
		Me.viewer1.Sidebar.TocPanel.Width = 200
		Me.viewer1.Sidebar.Width = 200
		Me.viewer1.Size = New System.Drawing.Size(800, 450)
		Me.viewer1.TabIndex = 0
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(800, 450)
		Me.Controls.Add(Me.viewer1)
		Me.Name = "ViewerForm"
		Me.Text = "RTF"
		Me.ResumeLayout(False)
	End Sub

	Private viewer1 As Viewer.Win.Viewer
End Class
