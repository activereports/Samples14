<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()>
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
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.viewer = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.saveAsButton = New System.Windows.Forms.Button()
		Me.exports = New System.Windows.Forms.ComboBox()
		Me.reports = New System.Windows.Forms.ComboBox()
		CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splitContainer1.Panel1.SuspendLayout()
		Me.splitContainer1.Panel2.SuspendLayout()
		Me.splitContainer1.SuspendLayout()
		Me.SuspendLayout()
		'
		'viewer
		'
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
		Me.viewer.Sidebar.ParametersPanel.Text = "Parameters"
		Me.viewer.Sidebar.ParametersPanel.Width = 200
		'
		'
		'
		Me.viewer.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.viewer.Sidebar.SearchPanel.Text = "Search results"
		Me.viewer.Sidebar.SearchPanel.Width = 200
		'
		'
		'
		Me.viewer.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.viewer.Sidebar.ThumbnailsPanel.Text = "Page thumbnails"
		Me.viewer.Sidebar.ThumbnailsPanel.Width = 200
		Me.viewer.Sidebar.ThumbnailsPanel.Zoom = 0.1R
		'
		'
		'
		Me.viewer.Sidebar.TocPanel.ContextMenu = Nothing
		Me.viewer.Sidebar.TocPanel.Expanded = True
		Me.viewer.Sidebar.TocPanel.Text = "Document map"
		Me.viewer.Sidebar.TocPanel.Width = 200
		Me.viewer.Sidebar.Width = 200
		'
		'splitContainer1
		'
		resources.ApplyResources(Me.splitContainer1, "splitContainer1")
		Me.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.splitContainer1.Name = "splitContainer1"
		'
		'splitContainer1.Panel1
		'
		Me.splitContainer1.Panel1.Controls.Add(Me.saveAsButton)
		Me.splitContainer1.Panel1.Controls.Add(Me.exports)
		Me.splitContainer1.Panel1.Controls.Add(Me.reports)
		'
		'splitContainer1.Panel2
		'
		Me.splitContainer1.Panel2.Controls.Add(Me.viewer)
		'
		'saveAsButton
		'
		resources.ApplyResources(Me.saveAsButton, "saveAsButton")
		Me.saveAsButton.Name = "saveAsButton"
		Me.saveAsButton.UseVisualStyleBackColor = True
		'
		'exports
		'
		resources.ApplyResources(Me.exports, "exports")
		Me.exports.FormattingEnabled = True
		Me.exports.Name = "exports"
		'
		'reports
		'
		resources.ApplyResources(Me.reports, "reports")
		Me.reports.FormattingEnabled = True
		Me.reports.Name = "reports"
		'
		'MainForm
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.splitContainer1)
		Me.Name = "MainForm"
		Me.splitContainer1.Panel1.ResumeLayout(False)
		Me.splitContainer1.Panel2.ResumeLayout(False)
		CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splitContainer1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	Private WithEvents viewer As GrapeCity.ActiveReports.Viewer.Win.Viewer
	Private WithEvents splitContainer1 As SplitContainer
	Private WithEvents saveAsButton As Button
	Private WithEvents exports As ComboBox
	Private WithEvents reports As ComboBox
End Class
