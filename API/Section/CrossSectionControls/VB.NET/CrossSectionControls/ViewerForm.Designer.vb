<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ViewerForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ViewerForm))
		Me.tabControl = New System.Windows.Forms.TabControl()
		Me.cscTab = New System.Windows.Forms.TabPage()
		Me.cscViewer = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.repeatToFillTab = New System.Windows.Forms.TabPage()
		Me.repeatToFillViewer = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.printAtBottomTab = New System.Windows.Forms.TabPage()
		Me.printAtBottomViewer = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.tabControl.SuspendLayout()
		Me.cscTab.SuspendLayout()
		Me.repeatToFillTab.SuspendLayout()
		Me.printAtBottomTab.SuspendLayout()
		Me.SuspendLayout()
		'
		'tabControl
		'
		resources.ApplyResources(Me.tabControl, "tabControl")
		Me.tabControl.Controls.Add(Me.cscTab)
		Me.tabControl.Controls.Add(Me.repeatToFillTab)
		Me.tabControl.Controls.Add(Me.printAtBottomTab)
		Me.tabControl.Name = "tabControl"
		Me.tabControl.SelectedIndex = 0
		'
		'cscTab
		'
		resources.ApplyResources(Me.cscTab, "cscTab")
		Me.cscTab.Controls.Add(Me.cscViewer)
		Me.cscTab.Name = "cscTab"
		Me.cscTab.UseVisualStyleBackColor = True
		'
		'cscViewer
		'
		resources.ApplyResources(Me.cscViewer, "cscViewer")
		Me.cscViewer.AnnotationDropDownVisible = False
		Me.cscViewer.BackColor = System.Drawing.SystemColors.Control
		Me.cscViewer.CurrentPage = 0
		Me.cscViewer.HyperlinkBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.cscViewer.HyperlinkForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
		Me.cscViewer.Name = "cscViewer"
		Me.cscViewer.PagesBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.cscViewer.PreviewPages = 0
		Me.cscViewer.SearchResultsBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
		Me.cscViewer.SearchResultsForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(139, Byte), Integer))
		'
		'
		'
		'
		'
		'
		Me.cscViewer.Sidebar.ParametersPanel.ContextMenu = Nothing
		Me.cscViewer.Sidebar.ParametersPanel.Width = 180
		'
		'
		'
		Me.cscViewer.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.cscViewer.Sidebar.SearchPanel.Width = 180
		Me.cscViewer.Sidebar.SelectedIndex = 0
		'
		'
		'
		Me.cscViewer.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.cscViewer.Sidebar.ThumbnailsPanel.Width = 180
		'
		'
		'
		Me.cscViewer.Sidebar.TocPanel.ContextMenu = Nothing
		Me.cscViewer.Sidebar.TocPanel.Width = 180
		Me.cscViewer.Sidebar.Width = 180
		Me.cscViewer.SplitView = False
		Me.cscViewer.ViewType = GrapeCity.Viewer.Common.Model.ViewType.SinglePage
		Me.cscViewer.Zoom = 1.0!
		'
		'repeatToFillTab
		'
		resources.ApplyResources(Me.repeatToFillTab, "repeatToFillTab")
		Me.repeatToFillTab.Controls.Add(Me.repeatToFillViewer)
		Me.repeatToFillTab.Name = "repeatToFillTab"
		Me.repeatToFillTab.UseVisualStyleBackColor = True
		'
		'repeatToFillViewer
		'
		resources.ApplyResources(Me.repeatToFillViewer, "repeatToFillViewer")
		Me.repeatToFillViewer.AnnotationDropDownVisible = False
		Me.repeatToFillViewer.BackColor = System.Drawing.SystemColors.Control
		Me.repeatToFillViewer.CurrentPage = 0
		Me.repeatToFillViewer.HyperlinkBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.repeatToFillViewer.HyperlinkForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
		Me.repeatToFillViewer.Name = "repeatToFillViewer"
		Me.repeatToFillViewer.PagesBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.repeatToFillViewer.PreviewPages = 0
		Me.repeatToFillViewer.SearchResultsBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
		Me.repeatToFillViewer.SearchResultsForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(139, Byte), Integer))
		'
		'
		'
		'
		'
		'
		Me.repeatToFillViewer.Sidebar.ParametersPanel.ContextMenu = Nothing
		Me.repeatToFillViewer.Sidebar.ParametersPanel.Width = 180
		'
		'
		'
		Me.repeatToFillViewer.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.repeatToFillViewer.Sidebar.SearchPanel.Width = 180
		Me.repeatToFillViewer.Sidebar.SelectedIndex = 0
		'
		'
		'
		Me.repeatToFillViewer.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.repeatToFillViewer.Sidebar.ThumbnailsPanel.Width = 180
		'
		'
		'
		Me.repeatToFillViewer.Sidebar.TocPanel.ContextMenu = Nothing
		Me.repeatToFillViewer.Sidebar.TocPanel.Width = 180
		Me.repeatToFillViewer.Sidebar.Width = 180
		Me.repeatToFillViewer.SplitView = False
		Me.repeatToFillViewer.ViewType = GrapeCity.Viewer.Common.Model.ViewType.SinglePage
		Me.repeatToFillViewer.Zoom = 1.0!
		'
		'printAtBottomTab
		'
		resources.ApplyResources(Me.printAtBottomTab, "printAtBottomTab")
		Me.printAtBottomTab.Controls.Add(Me.printAtBottomViewer)
		Me.printAtBottomTab.Name = "printAtBottomTab"
		Me.printAtBottomTab.UseVisualStyleBackColor = True
		'
		'printAtBottomViewer
		'
		resources.ApplyResources(Me.printAtBottomViewer, "printAtBottomViewer")
		Me.printAtBottomViewer.AnnotationDropDownVisible = False
		Me.printAtBottomViewer.BackColor = System.Drawing.SystemColors.Control
		Me.printAtBottomViewer.CurrentPage = 0
		Me.printAtBottomViewer.HyperlinkBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.printAtBottomViewer.HyperlinkForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
		Me.printAtBottomViewer.Name = "printAtBottomViewer"
		Me.printAtBottomViewer.PagesBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.printAtBottomViewer.PreviewPages = 0
		Me.printAtBottomViewer.SearchResultsBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
		Me.printAtBottomViewer.SearchResultsForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(139, Byte), Integer))
		'
		'
		'
		'
		'
		'
		Me.printAtBottomViewer.Sidebar.ParametersPanel.ContextMenu = Nothing
		Me.printAtBottomViewer.Sidebar.ParametersPanel.Width = 180
		'
		'
		'
		Me.printAtBottomViewer.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.printAtBottomViewer.Sidebar.SearchPanel.Width = 180
		Me.printAtBottomViewer.Sidebar.SelectedIndex = 0
		'
		'
		'
		Me.printAtBottomViewer.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.printAtBottomViewer.Sidebar.ThumbnailsPanel.Width = 180
		'
		'
		'
		Me.printAtBottomViewer.Sidebar.TocPanel.ContextMenu = Nothing
		Me.printAtBottomViewer.Sidebar.TocPanel.Width = 180
		Me.printAtBottomViewer.Sidebar.Width = 180
		Me.printAtBottomViewer.SplitView = False
		Me.printAtBottomViewer.ViewType = GrapeCity.Viewer.Common.Model.ViewType.SinglePage
		Me.printAtBottomViewer.Zoom = 1.0!
		'
		'ViewerForm
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.tabControl)
		Me.Name = "ViewerForm"
		Me.tabControl.ResumeLayout(False)
		Me.cscTab.ResumeLayout(False)
		Me.repeatToFillTab.ResumeLayout(False)
		Me.printAtBottomTab.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents tabControl As System.Windows.Forms.TabControl
	Friend WithEvents cscTab As System.Windows.Forms.TabPage
	Friend WithEvents cscViewer As GrapeCity.ActiveReports.Viewer.Win.Viewer 'Friend WithEvents cscViewer As GrapeCity.ActiveReports.Viewer.Viewer
	Friend WithEvents repeatToFillTab As System.Windows.Forms.TabPage
	Friend WithEvents repeatToFillViewer As GrapeCity.ActiveReports.Viewer.Win.Viewer 'Friend WithEvents repeatToFillViewer As GrapeCity.ActiveReports.Viewer.Viewer
	Friend WithEvents printAtBottomTab As System.Windows.Forms.TabPage
	Friend WithEvents printAtBottomViewer As GrapeCity.ActiveReports.Viewer.Win.Viewer 'Friend WithEvents printAtBottomViewer As GrapeCity.ActiveReports.Viewer.Viewer

End Class
