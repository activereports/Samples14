 _
Partial Class MainForm
	Inherits System.Windows.Forms.Form



	Private components As System.ComponentModel.Container = Nothing

	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.pnlOptions = New System.Windows.Forms.Panel()
		Me.tabDataBinding = New System.Windows.Forms.TabControl()
		Me.tabDataSet = New System.Windows.Forms.TabPage()
		Me.lblDataSet = New System.Windows.Forms.Label()
		Me.btnDataSet = New System.Windows.Forms.Button()
		Me.tabDataReader = New System.Windows.Forms.TabPage()
		Me.btnDataReader = New System.Windows.Forms.Button()
		Me.lblDataReader = New System.Windows.Forms.Label()
		Me.tabTextFile = New System.Windows.Forms.TabPage()
		Me.lblTextFile = New System.Windows.Forms.Label()
		Me.btnTextFile = New System.Windows.Forms.Button()
		Me.tabArray = New System.Windows.Forms.TabPage()
		Me.lblArray = New System.Windows.Forms.Label()
		Me.btnArray = New System.Windows.Forms.Button()
		Me.arvMain = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.pnlOptions.SuspendLayout()
		Me.tabDataBinding.SuspendLayout()
		Me.tabDataSet.SuspendLayout()
		Me.tabDataReader.SuspendLayout()
		Me.tabTextFile.SuspendLayout()
		Me.tabArray.SuspendLayout()
		Me.SuspendLayout()
		'
		'pnlOptions
		'
		Me.pnlOptions.Controls.Add(Me.tabDataBinding)
		resources.ApplyResources(Me.pnlOptions, "pnlOptions")
		Me.pnlOptions.Name = "pnlOptions"
		'
		'tabDataBinding
		'
		Me.tabDataBinding.Controls.Add(Me.tabDataSet)
		Me.tabDataBinding.Controls.Add(Me.tabDataReader)
		Me.tabDataBinding.Controls.Add(Me.tabTextFile)
		Me.tabDataBinding.Controls.Add(Me.tabArray)
		resources.ApplyResources(Me.tabDataBinding, "tabDataBinding")
		Me.tabDataBinding.Name = "tabDataBinding"
		Me.tabDataBinding.SelectedIndex = 0
		'
		'tabDataSet
		'
		Me.tabDataSet.Controls.Add(Me.lblDataSet)
		Me.tabDataSet.Controls.Add(Me.btnDataSet)
		resources.ApplyResources(Me.tabDataSet, "tabDataSet")
		Me.tabDataSet.Name = "tabDataSet"
		'
		'lblDataSet
		'
		resources.ApplyResources(Me.lblDataSet, "lblDataSet")
		Me.lblDataSet.Name = "lblDataSet"
		'
		'btnDataSet
		'
		resources.ApplyResources(Me.btnDataSet, "btnDataSet")
		Me.btnDataSet.Name = "btnDataSet"
		'
		'tabDataReader
		'
		Me.tabDataReader.Controls.Add(Me.btnDataReader)
		Me.tabDataReader.Controls.Add(Me.lblDataReader)
		resources.ApplyResources(Me.tabDataReader, "tabDataReader")
		Me.tabDataReader.Name = "tabDataReader"
		'
		'btnDataReader
		'
		resources.ApplyResources(Me.btnDataReader, "btnDataReader")
		Me.btnDataReader.Name = "btnDataReader"
		'
		'lblDataReader
		'
		resources.ApplyResources(Me.lblDataReader, "lblDataReader")
		Me.lblDataReader.Name = "lblDataReader"
		'
		'tabTextFile
		'
		Me.tabTextFile.Controls.Add(Me.lblTextFile)
		Me.tabTextFile.Controls.Add(Me.btnTextFile)
		resources.ApplyResources(Me.tabTextFile, "tabTextFile")
		Me.tabTextFile.Name = "tabTextFile"
		'
		'lblTextFile
		'
		resources.ApplyResources(Me.lblTextFile, "lblTextFile")
		Me.lblTextFile.Name = "lblTextFile"
		'
		'btnTextFile
		'
		resources.ApplyResources(Me.btnTextFile, "btnTextFile")
		Me.btnTextFile.Name = "btnTextFile"
		'
		'tabArray
		'
		Me.tabArray.Controls.Add(Me.lblArray)
		Me.tabArray.Controls.Add(Me.btnArray)
		resources.ApplyResources(Me.tabArray, "tabArray")
		Me.tabArray.Name = "tabArray"
		'
		'lblArray
		'
		resources.ApplyResources(Me.lblArray, "lblArray")
		Me.lblArray.Name = "lblArray"
		'
		'btnArray
		'
		resources.ApplyResources(Me.btnArray, "btnArray")
		Me.btnArray.Name = "btnArray"
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
		'MainForm
		'
		resources.ApplyResources(Me, "$this")
		Me.Controls.Add(Me.arvMain)
		Me.Controls.Add(Me.pnlOptions)
		Me.Name = "MainForm"
		Me.pnlOptions.ResumeLayout(False)
		Me.tabDataBinding.ResumeLayout(False)
		Me.tabDataSet.ResumeLayout(False)
		Me.tabDataReader.ResumeLayout(False)
		Me.tabTextFile.ResumeLayout(False)
		Me.tabArray.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Private pnlOptions As System.Windows.Forms.Panel
	Private arvMain As GrapeCity.ActiveReports.Viewer.Win.Viewer
	Private WithEvents tabDataBinding As System.Windows.Forms.TabControl
	Private tabDataSet As System.Windows.Forms.TabPage
	Private tabDataReader As System.Windows.Forms.TabPage
	Private WithEvents btnDataSet As System.Windows.Forms.Button
	Private lblDataSet As System.Windows.Forms.Label
	Private lblDataReader As System.Windows.Forms.Label
	Private WithEvents btnDataReader As System.Windows.Forms.Button
	Private tabTextFile As System.Windows.Forms.TabPage
	Private WithEvents btnTextFile As System.Windows.Forms.Button
	Private tabArray As System.Windows.Forms.TabPage
	Private WithEvents btnArray As System.Windows.Forms.Button
	Private lblTextFile As System.Windows.Forms.Label
	Private WithEvents lblArray As System.Windows.Forms.Label

End Class
