<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PrintMultiplePages
	Inherits System.Windows.Forms.Form



	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PrintMultiplePages))
		Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.lblPagenumAPI = New System.Windows.Forms.Label()
		Me.cmbPageCountAPI = New System.Windows.Forms.ComboBox()
		Me.btnAPIprint = New System.Windows.Forms.Button()
		Me.apiViewer = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.splitContainer2 = New System.Windows.Forms.SplitContainer()
		Me.lblPagenum = New System.Windows.Forms.Label()
		Me.cmbPageCount = New System.Windows.Forms.ComboBox()
		Me.btnPrint = New System.Windows.Forms.Button()
		Me.arViewer = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.PrintDocument = New System.Drawing.Printing.PrintDocument()
		Me.dlgPrint = New System.Windows.Forms.PrintDialog()
		Me.tabControl = New System.Windows.Forms.TabControl()
		Me.tbAPIPrint = New System.Windows.Forms.TabPage()
		Me.tbClassicPrint = New System.Windows.Forms.TabPage()
		CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splitContainer1.Panel1.SuspendLayout()
		Me.splitContainer1.Panel2.SuspendLayout()
		Me.splitContainer1.SuspendLayout()
		CType(Me.splitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splitContainer2.Panel1.SuspendLayout()
		Me.splitContainer2.Panel2.SuspendLayout()
		Me.splitContainer2.SuspendLayout()
		Me.tabControl.SuspendLayout()
		Me.tbAPIPrint.SuspendLayout()
		Me.tbClassicPrint.SuspendLayout()
		Me.SuspendLayout()
		'
		'splitContainer1
		'
		resources.ApplyResources(Me.splitContainer1, "splitContainer1")
		Me.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.splitContainer1.Name = "splitContainer1"
		'
		'splitContainer1.Panel1
		'
		Me.splitContainer1.Panel1.Controls.Add(Me.lblPagenumAPI)
		Me.splitContainer1.Panel1.Controls.Add(Me.cmbPageCountAPI)
		Me.splitContainer1.Panel1.Controls.Add(Me.btnAPIprint)
		'
		'splitContainer1.Panel2
		'
		Me.splitContainer1.Panel2.Controls.Add(Me.apiViewer)
		'
		'lblPagenumAPI
		'
		resources.ApplyResources(Me.lblPagenumAPI, "lblPagenumAPI")
		Me.lblPagenumAPI.BackColor = System.Drawing.SystemColors.Control
		Me.lblPagenumAPI.Name = "lblPagenumAPI"
		'
		'cmbPageCountAPI
		'
		Me.cmbPageCountAPI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		resources.ApplyResources(Me.cmbPageCountAPI, "cmbPageCountAPI")
		Me.cmbPageCountAPI.FormattingEnabled = True
		Me.cmbPageCountAPI.Items.AddRange(New Object() {resources.GetString("cmbPageCountAPI.Items"), resources.GetString("cmbPageCountAPI.Items1"), resources.GetString("cmbPageCountAPI.Items2")})
		Me.cmbPageCountAPI.Name = "cmbPageCountAPI"
		'
		'btnAPIprint
		'
		Me.btnAPIprint.BackColor = System.Drawing.SystemColors.ButtonHighlight
		resources.ApplyResources(Me.btnAPIprint, "btnAPIprint")
		Me.btnAPIprint.Name = "btnAPIprint"
		Me.btnAPIprint.UseVisualStyleBackColor = False
		'
		'apiViewer
		'
		Me.apiViewer.CurrentPage = 0
		resources.ApplyResources(Me.apiViewer, "apiViewer")
		Me.apiViewer.Name = "apiViewer"
		Me.apiViewer.PreviewPages = 0
		'
		'
		'
		'
		'
		'
		Me.apiViewer.Sidebar.ParametersPanel.ContextMenu = Nothing
		Me.apiViewer.Sidebar.ParametersPanel.Enabled = False
		Me.apiViewer.Sidebar.ParametersPanel.Text = "Parameters"
		Me.apiViewer.Sidebar.ParametersPanel.Visible = False
		Me.apiViewer.Sidebar.ParametersPanel.Width = 200
		'
		'
		'
		Me.apiViewer.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.apiViewer.Sidebar.SearchPanel.Text = "Search results"
		Me.apiViewer.Sidebar.SearchPanel.Width = 200
		'
		'
		'
		Me.apiViewer.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.apiViewer.Sidebar.ThumbnailsPanel.Text = "Page thumbnails"
		Me.apiViewer.Sidebar.ThumbnailsPanel.Width = 200
		Me.apiViewer.Sidebar.ThumbnailsPanel.Zoom = 0.1R
		'
		'
		'
		Me.apiViewer.Sidebar.TocPanel.ContextMenu = Nothing
		Me.apiViewer.Sidebar.TocPanel.Enabled = False
		Me.apiViewer.Sidebar.TocPanel.Expanded = True
		Me.apiViewer.Sidebar.TocPanel.Text = "Document map"
		Me.apiViewer.Sidebar.TocPanel.Visible = False
		Me.apiViewer.Sidebar.TocPanel.Width = 200
		Me.apiViewer.Sidebar.Width = 200
		'
		'splitContainer2
		'
		resources.ApplyResources(Me.splitContainer2, "splitContainer2")
		Me.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.splitContainer2.Name = "splitContainer2"
		'
		'splitContainer2.Panel1
		'
		Me.splitContainer2.Panel1.Controls.Add(Me.lblPagenum)
		Me.splitContainer2.Panel1.Controls.Add(Me.cmbPageCount)
		Me.splitContainer2.Panel1.Controls.Add(Me.btnPrint)
		'
		'splitContainer2.Panel2
		'
		Me.splitContainer2.Panel2.Controls.Add(Me.arViewer)
		'
		'lblPagenum
		'
		resources.ApplyResources(Me.lblPagenum, "lblPagenum")
		Me.lblPagenum.BackColor = System.Drawing.SystemColors.Control
		Me.lblPagenum.Name = "lblPagenum"
		'
		'cmbPageCount
		'
		Me.cmbPageCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		resources.ApplyResources(Me.cmbPageCount, "cmbPageCount")
		Me.cmbPageCount.FormattingEnabled = True
		Me.cmbPageCount.Items.AddRange(New Object() {resources.GetString("cmbPageCount.Items"), resources.GetString("cmbPageCount.Items1"), resources.GetString("cmbPageCount.Items2"), resources.GetString("cmbPageCount.Items3"), resources.GetString("cmbPageCount.Items4")})
		Me.cmbPageCount.Name = "cmbPageCount"
		'
		'btnPrint
		'
		Me.btnPrint.BackColor = System.Drawing.SystemColors.ButtonHighlight
		resources.ApplyResources(Me.btnPrint, "btnPrint")
		Me.btnPrint.Name = "btnPrint"
		Me.btnPrint.UseVisualStyleBackColor = False
		'
		'arViewer
		'
		Me.arViewer.CurrentPage = 0
		resources.ApplyResources(Me.arViewer, "arViewer")
		Me.arViewer.Name = "arViewer"
		Me.arViewer.PreviewPages = 0
		'
		'
		'
		'
		'
		'
		Me.arViewer.Sidebar.ParametersPanel.ContextMenu = Nothing
		Me.arViewer.Sidebar.ParametersPanel.Enabled = False
		Me.arViewer.Sidebar.ParametersPanel.Text = "Parameters"
		Me.arViewer.Sidebar.ParametersPanel.Visible = False
		Me.arViewer.Sidebar.ParametersPanel.Width = 200
		'
		'
		'
		Me.arViewer.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.arViewer.Sidebar.SearchPanel.Text = "Search results"
		Me.arViewer.Sidebar.SearchPanel.Width = 200
		'
		'
		'
		Me.arViewer.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.arViewer.Sidebar.ThumbnailsPanel.Text = "Page thumbnails"
		Me.arViewer.Sidebar.ThumbnailsPanel.Width = 200
		Me.arViewer.Sidebar.ThumbnailsPanel.Zoom = 0.1R
		'
		'
		'
		Me.arViewer.Sidebar.TocPanel.ContextMenu = Nothing
		Me.arViewer.Sidebar.TocPanel.Enabled = False
		Me.arViewer.Sidebar.TocPanel.Expanded = True
		Me.arViewer.Sidebar.TocPanel.Text = "Document map"
		Me.arViewer.Sidebar.TocPanel.Visible = False
		Me.arViewer.Sidebar.TocPanel.Width = 200
		Me.arViewer.Sidebar.Width = 200
		'
		'PrintDocument
		'
		'
		'dlgPrint
		'
		Me.dlgPrint.Document = Me.PrintDocument
		'
		'tabControl
		'
		Me.tabControl.Controls.Add(Me.tbAPIPrint)
		Me.tabControl.Controls.Add(Me.tbClassicPrint)
		resources.ApplyResources(Me.tabControl, "tabControl")
		Me.tabControl.Name = "tabControl"
		Me.tabControl.SelectedIndex = 0
		'
		'tbAPIPrint
		'
		Me.tbAPIPrint.Controls.Add(Me.splitContainer1)
		resources.ApplyResources(Me.tbAPIPrint, "tbAPIPrint")
		Me.tbAPIPrint.Name = "tbAPIPrint"
		Me.tbAPIPrint.UseVisualStyleBackColor = True
		'
		'tbClassicPrint
		'
		Me.tbClassicPrint.Controls.Add(Me.splitContainer2)
		resources.ApplyResources(Me.tbClassicPrint, "tbClassicPrint")
		Me.tbClassicPrint.Name = "tbClassicPrint"
		Me.tbClassicPrint.UseVisualStyleBackColor = True
		'
		'PrintMultiplePages
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.tabControl)
		Me.Name = "PrintMultiplePages"
		Me.splitContainer1.Panel1.ResumeLayout(False)
		Me.splitContainer1.Panel1.PerformLayout()
		Me.splitContainer1.Panel2.ResumeLayout(False)
		CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splitContainer1.ResumeLayout(False)
		Me.splitContainer2.Panel1.ResumeLayout(False)
		Me.splitContainer2.Panel1.PerformLayout()
		Me.splitContainer2.Panel2.ResumeLayout(False)
		CType(Me.splitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splitContainer2.ResumeLayout(False)
		Me.tabControl.ResumeLayout(False)
		Me.tbAPIPrint.ResumeLayout(False)
		Me.tbClassicPrint.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents apiViewer As GrapeCity.ActiveReports.Viewer.Win.Viewer
	Friend WithEvents arViewer As GrapeCity.ActiveReports.Viewer.Win.Viewer
	Private WithEvents PrintDocument As System.Drawing.Printing.PrintDocument
	Private WithEvents dlgPrint As System.Windows.Forms.PrintDialog
	Friend WithEvents tabControl As System.Windows.Forms.TabControl
	Friend WithEvents tbAPIPrint As System.Windows.Forms.TabPage
	Friend WithEvents splitContainer1 As System.Windows.Forms.SplitContainer
	Private WithEvents lblPagenumAPI As System.Windows.Forms.Label
	Private WithEvents cmbPageCountAPI As System.Windows.Forms.ComboBox
	Private WithEvents btnAPIprint As System.Windows.Forms.Button
	Friend WithEvents tbClassicPrint As System.Windows.Forms.TabPage
	Friend WithEvents splitContainer2 As System.Windows.Forms.SplitContainer
	Private WithEvents lblPagenum As System.Windows.Forms.Label
	Private WithEvents cmbPageCount As System.Windows.Forms.ComboBox
	Private WithEvents btnPrint As System.Windows.Forms.Button
End Class
