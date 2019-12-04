Imports Win_Viewer = GrapeCity.ActiveReports.Viewer.Win.Viewer

Partial Class ViewerForm
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If

			MyBase.Dispose(disposing)
		End Sub

#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ViewerForm))
		Me.viewer = New Win_Viewer()
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog()
		Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
		Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.SaveAsRDFToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.MenuStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'viewer
		'
		Me.viewer.AnnotationDropDownVisible = True
		Me.viewer.CurrentPage = 0
		resources.ApplyResources(Me.viewer, "viewer")
		Me.viewer.Name = "viewer"
		Me.viewer.PreviewPages = 0
		'
		'
		'
		'
		'
		'
		Me.viewer.Sidebar.ParametersPanel.ContextMenu = Nothing
		Me.viewer.Sidebar.ParametersPanel.Width = 240
		'
		'
		'
		Me.viewer.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.viewer.Sidebar.SearchPanel.Width = 240
		'
		'
		'
		Me.viewer.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.viewer.Sidebar.ThumbnailsPanel.Width = 240
		Me.viewer.Sidebar.ThumbnailsPanel.Zoom = 0.1R
		'
		'
		'
		Me.viewer.Sidebar.TocPanel.ContextMenu = Nothing
		Me.viewer.Sidebar.TocPanel.Expanded = True
		Me.viewer.Sidebar.TocPanel.Width = 240
		Me.viewer.Sidebar.Width = 240
		'
		'MenuStrip1
		'
		Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.HelpToolStripMenuItem})
		resources.ApplyResources(Me.MenuStrip1, "MenuStrip1")
		Me.MenuStrip1.Name = "MenuStrip1"
		'
		'FileToolStripMenuItem
		'
		Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem, Me.ExportToolStripMenuItem, Me.SaveAsRDFToolStripMenuItem, Me.ExitToolStripMenuItem})
		Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
		resources.ApplyResources(Me.FileToolStripMenuItem, "FileToolStripMenuItem")
		'
		'OpenToolStripMenuItem
		'
		Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
		resources.ApplyResources(Me.OpenToolStripMenuItem, "OpenToolStripMenuItem")
		'
		'ExportToolStripMenuItem
		'
		resources.ApplyResources(Me.ExportToolStripMenuItem, "ExportToolStripMenuItem")
		Me.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
		'
		'SaveAsRDFToolStripMenuItem
		'
		resources.ApplyResources(Me.SaveAsRDFToolStripMenuItem, "SaveAsRDFToolStripMenuItem")
		Me.SaveAsRDFToolStripMenuItem.Name = "SaveAsRDFToolStripMenuItem"
		'
		'ExitToolStripMenuItem
		'
		Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
		resources.ApplyResources(Me.ExitToolStripMenuItem, "ExitToolStripMenuItem")
		'
		'HelpToolStripMenuItem
		'
		Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
		Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
		resources.ApplyResources(Me.HelpToolStripMenuItem, "HelpToolStripMenuItem")
		'
		'AboutToolStripMenuItem
		'
		Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
		resources.ApplyResources(Me.AboutToolStripMenuItem, "AboutToolStripMenuItem")
		'
		'ViewerForm
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.viewer)
		Me.Controls.Add(Me.MenuStrip1)
		Me.MainMenuStrip = Me.MenuStrip1
		Me.Name = "ViewerForm"
		Me.MenuStrip1.ResumeLayout(False)
		Me.MenuStrip1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private viewer As Win_Viewer
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Friend WithEvents MenuStrip1 As MenuStrip
	Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents ExportToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
	Private WithEvents SaveAsRDFToolStripMenuItem As ToolStripMenuItem
End Class
