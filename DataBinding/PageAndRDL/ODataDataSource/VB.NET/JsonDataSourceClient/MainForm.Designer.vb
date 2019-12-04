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
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.comboBox1 = New System.Windows.Forms.ComboBox()
		Me.label1 = New System.Windows.Forms.Label()
		Me.reportPreview = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer1.Panel1.SuspendLayout()
		Me.SplitContainer1.Panel2.SuspendLayout()
		Me.SplitContainer1.SuspendLayout()
		Me.SuspendLayout()
		'
		'SplitContainer1
		'
		resources.ApplyResources(Me.SplitContainer1, "SplitContainer1")
		Me.SplitContainer1.Name = "SplitContainer1"
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.Controls.Add(Me.comboBox1)
		Me.SplitContainer1.Panel1.Controls.Add(Me.label1)
		'
		'SplitContainer1.Panel2
		'
		Me.SplitContainer1.Panel2.Controls.Add(Me.reportPreview)
		'
		'comboBox1
		'
		Me.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.comboBox1.FormattingEnabled = True
		Me.comboBox1.Items.AddRange(New Object() {resources.GetString("comboBox1.Items"), resources.GetString("comboBox1.Items1")})
		resources.ApplyResources(Me.comboBox1, "comboBox1")
		Me.comboBox1.Name = "comboBox1"
		'
		'label1
		'
		resources.ApplyResources(Me.label1, "label1")
		Me.label1.Name = "label1"
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
		Me.Controls.Add(Me.SplitContainer1)
		Me.Name = "MainForm"
		Me.SplitContainer1.Panel1.ResumeLayout(False)
		Me.SplitContainer1.Panel1.PerformLayout()
		Me.SplitContainer1.Panel2.ResumeLayout(False)
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents SplitContainer1 As SplitContainer
	Private WithEvents comboBox1 As ComboBox
	Private WithEvents label1 As Label
	Friend WithEvents reportPreview As GrapeCity.ActiveReports.Viewer.Win.Viewer
End Class
