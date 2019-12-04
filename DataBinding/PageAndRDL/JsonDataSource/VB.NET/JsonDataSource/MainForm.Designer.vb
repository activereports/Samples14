<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	'
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso components IsNot Nothing Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	'Required designer variable.
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer. 
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.ReportPreview1 = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.SuspendLayout()
		'
		'ReportPreview1
		'
		Me.ReportPreview1.CurrentPage = 0
		resources.ApplyResources(Me.ReportPreview1, "ReportPreview1")
		Me.ReportPreview1.Name = "ReportPreview1"
		Me.ReportPreview1.PreviewPages = 0
		'
		'
		'
		'
		'
		'
		Me.ReportPreview1.Sidebar.ParametersPanel.ContextMenu = Nothing
		Me.ReportPreview1.Sidebar.ParametersPanel.Width = 200
		'
		'
		'
		Me.ReportPreview1.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.ReportPreview1.Sidebar.SearchPanel.Width = 200
		'
		'
		'
		Me.ReportPreview1.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.ReportPreview1.Sidebar.ThumbnailsPanel.Width = 200
		Me.ReportPreview1.Sidebar.ThumbnailsPanel.Zoom = 0.1R
		'
		'
		'
		Me.ReportPreview1.Sidebar.TocPanel.ContextMenu = Nothing
		Me.ReportPreview1.Sidebar.TocPanel.Expanded = True
		Me.ReportPreview1.Sidebar.TocPanel.Width = 200
		Me.ReportPreview1.Sidebar.Width = 200
		'
		'MainForm
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.ReportPreview1)
		Me.Name = "MainForm"
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents ReportPreview1 As GrapeCity.ActiveReports.Viewer.Win.Viewer

End Class
