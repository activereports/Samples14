 _
Partial Class StartForm
	Inherits System.Windows.Forms.Form



Private components As System.ComponentModel.Container = Nothing

	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StartForm))
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.button1 = New System.Windows.Forms.Button()
		Me.comboBox1 = New System.Windows.Forms.ComboBox()
		Me.Splitter1 = New System.Windows.Forms.Splitter()
		Me.arvMain = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer1.Panel1.SuspendLayout()
		Me.SplitContainer1.Panel2.SuspendLayout()
		Me.SplitContainer1.SuspendLayout()
		Me.SuspendLayout()
		'
		'SplitContainer1
		'
		resources.ApplyResources(Me.SplitContainer1, "SplitContainer1")
		Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.SplitContainer1.Name = "SplitContainer1"
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.Controls.Add(Me.button1)
		Me.SplitContainer1.Panel1.Controls.Add(Me.comboBox1)
		Me.SplitContainer1.Panel1.Controls.Add(Me.Splitter1)
		'
		'SplitContainer1.Panel2
		'
		Me.SplitContainer1.Panel2.Controls.Add(Me.arvMain)
		'
		'button1
		'
		resources.ApplyResources(Me.button1, "button1")
		Me.button1.Name = "button1"
		Me.button1.UseVisualStyleBackColor = True
		'
		'comboBox1
		'
		Me.comboBox1.FormattingEnabled = True
		resources.ApplyResources(Me.comboBox1, "comboBox1")
		Me.comboBox1.Name = "comboBox1"
		'
		'Splitter1
		'
		resources.ApplyResources(Me.Splitter1, "Splitter1")
		Me.Splitter1.Name = "Splitter1"
		Me.Splitter1.TabStop = False
		'
		'arvMain
		'
		resources.ApplyResources(Me.arvMain, "arvMain")
		Me.arvMain.CurrentPage = 0
		Me.arvMain.Name = "arvMain"
		Me.arvMain.PreviewPages = 0
		'
		'
		'
		'
		'
		'
		Me.arvMain.Sidebar.ParametersPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.ParametersPanel.Text = "Parameters"
		Me.arvMain.Sidebar.ParametersPanel.Width = 200
		'
		'
		'
		Me.arvMain.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.SearchPanel.Text = "Search results"
		Me.arvMain.Sidebar.SearchPanel.Width = 200
		'
		'
		'
		Me.arvMain.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.ThumbnailsPanel.Text = "Page thumbnails"
		Me.arvMain.Sidebar.ThumbnailsPanel.Width = 200
		Me.arvMain.Sidebar.ThumbnailsPanel.Zoom = 0.1R
		'
		'
		'
		Me.arvMain.Sidebar.TocPanel.ContextMenu = Nothing
		Me.arvMain.Sidebar.TocPanel.Expanded = True
		Me.arvMain.Sidebar.TocPanel.Text = "Document map"
		Me.arvMain.Sidebar.TocPanel.Width = 200
		Me.arvMain.Sidebar.Width = 200
		'
		'StartForm
		'
		resources.ApplyResources(Me, "$this")
		Me.Controls.Add(Me.SplitContainer1)
		Me.Name = "StartForm"
		Me.SplitContainer1.Panel1.ResumeLayout(False)
		Me.SplitContainer1.Panel2.ResumeLayout(False)
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents SplitContainer1 As SplitContainer
	Friend WithEvents Splitter1 As Splitter
	Friend WithEvents arvMain As Viewer.Win.Viewer
	Private WithEvents button1 As Button
	Private WithEvents comboBox1 As ComboBox
End Class
