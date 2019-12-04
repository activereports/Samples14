<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.reportPreview = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.SuspendLayout()
		'
		'reportPreview
		'
		Me.reportPreview.CurrentPage = 0
		resources.ApplyResources(Me.reportPreview, "reportPreview")
		Me.reportPreview.Name = "reportPreview"
		Me.reportPreview.PreviewPages = 0
		'
		'
		'
		'
		'
		'
		Me.reportPreview.Sidebar.ParametersPanel.ContextMenu = Nothing
		Me.reportPreview.Sidebar.ParametersPanel.Text = "Parameters"
		Me.reportPreview.Sidebar.ParametersPanel.Width = 200
		'
		'
		'
		Me.reportPreview.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.reportPreview.Sidebar.SearchPanel.Text = "Search results"
		Me.reportPreview.Sidebar.SearchPanel.Width = 200
		'
		'
		'
		Me.reportPreview.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.reportPreview.Sidebar.ThumbnailsPanel.Text = "Page thumbnails"
		Me.reportPreview.Sidebar.ThumbnailsPanel.Width = 200
		Me.reportPreview.Sidebar.ThumbnailsPanel.Zoom = 0.1R
		'
		'
		'
		Me.reportPreview.Sidebar.TocPanel.ContextMenu = Nothing
		Me.reportPreview.Sidebar.TocPanel.Expanded = True
		Me.reportPreview.Sidebar.TocPanel.Text = "Document map"
		Me.reportPreview.Sidebar.TocPanel.Width = 200
		Me.reportPreview.Sidebar.Width = 200
		'
		'MainForm
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.reportPreview)
		Me.Name = "MainForm"
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents reportPreview As GrapeCity.ActiveReports.Viewer.Win.Viewer
End Class
