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
		Me.pnlOptions = New System.Windows.Forms.Panel()
		Me.btnCSV = New System.Windows.Forms.Button()
		Me.rbtnHeader = New System.Windows.Forms.RadioButton()
		Me.rbtnNoHeader = New System.Windows.Forms.RadioButton()
		Me.rbtnHeaderTab = New System.Windows.Forms.RadioButton()
		Me.rbtnNoHeaderComma = New System.Windows.Forms.RadioButton()
		Me.lblFixWData = New System.Windows.Forms.Label()
		Me.lblCSVDelData = New System.Windows.Forms.Label()
		Me.lblCSV = New System.Windows.Forms.Label()
		Me.arvMain = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.pnlOptions.SuspendLayout()
		Me.SuspendLayout()
		'
		'pnlOptions
		'
		Me.pnlOptions.Controls.Add(Me.btnCSV)
		Me.pnlOptions.Controls.Add(Me.rbtnHeader)
		Me.pnlOptions.Controls.Add(Me.rbtnNoHeader)
		Me.pnlOptions.Controls.Add(Me.rbtnHeaderTab)
		Me.pnlOptions.Controls.Add(Me.rbtnNoHeaderComma)
		Me.pnlOptions.Controls.Add(Me.lblFixWData)
		Me.pnlOptions.Controls.Add(Me.lblCSVDelData)
		Me.pnlOptions.Controls.Add(Me.lblCSV)
		resources.ApplyResources(Me.pnlOptions, "pnlOptions")
		Me.pnlOptions.Name = "pnlOptions"
		'
		'btnCSV
		'
		resources.ApplyResources(Me.btnCSV, "btnCSV")
		Me.btnCSV.Name = "btnCSV"
		Me.btnCSV.UseVisualStyleBackColor = True
		'
		'rbtnHeader
		'
		resources.ApplyResources(Me.rbtnHeader, "rbtnHeader")
		Me.rbtnHeader.Name = "rbtnHeader"
		Me.rbtnHeader.UseVisualStyleBackColor = True
		'
		'rbtnNoHeader
		'
		resources.ApplyResources(Me.rbtnNoHeader, "rbtnNoHeader")
		Me.rbtnNoHeader.Name = "rbtnNoHeader"
		Me.rbtnNoHeader.UseVisualStyleBackColor = True
		'
		'rbtnHeaderTab
		'
		resources.ApplyResources(Me.rbtnHeaderTab, "rbtnHeaderTab")
		Me.rbtnHeaderTab.Name = "rbtnHeaderTab"
		Me.rbtnHeaderTab.UseVisualStyleBackColor = True
		'
		'rbtnNoHeaderComma
		'
		resources.ApplyResources(Me.rbtnNoHeaderComma, "rbtnNoHeaderComma")
		Me.rbtnNoHeaderComma.Checked = True
		Me.rbtnNoHeaderComma.Name = "rbtnNoHeaderComma"
		Me.rbtnNoHeaderComma.TabStop = True
		Me.rbtnNoHeaderComma.UseVisualStyleBackColor = True
		'
		'lblFixWData
		'
		resources.ApplyResources(Me.lblFixWData, "lblFixWData")
		Me.lblFixWData.Name = "lblFixWData"
		'
		'lblCSVDelData
		'
		resources.ApplyResources(Me.lblCSVDelData, "lblCSVDelData")
		Me.lblCSVDelData.Name = "lblCSVDelData"
		'
		'lblCSV
		'
		resources.ApplyResources(Me.lblCSV, "lblCSV")
		Me.lblCSV.Name = "lblCSV"
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
		Me.arvMain.Sidebar.ParametersPanel.Enabled = False
		Me.arvMain.Sidebar.ParametersPanel.Visible = False
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
		Me.arvMain.Sidebar.TocPanel.Enabled = False
		Me.arvMain.Sidebar.TocPanel.Expanded = True
		Me.arvMain.Sidebar.TocPanel.Visible = False
		Me.arvMain.Sidebar.TocPanel.Width = 200
		Me.arvMain.Sidebar.Width = 200
		'
		'MainForm
		'
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
		resources.ApplyResources(Me, "$this")
		Me.Controls.Add(Me.arvMain)
		Me.Controls.Add(Me.pnlOptions)
		Me.Name = "MainForm"
		Me.pnlOptions.ResumeLayout(False)
		Me.pnlOptions.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Private WithEvents pnlOptions As System.Windows.Forms.Panel
	Private WithEvents btnCSV As System.Windows.Forms.Button
	Private WithEvents rbtnHeader As System.Windows.Forms.RadioButton
	Private WithEvents rbtnNoHeader As System.Windows.Forms.RadioButton
	Private WithEvents rbtnHeaderTab As System.Windows.Forms.RadioButton
	Private WithEvents rbtnNoHeaderComma As System.Windows.Forms.RadioButton
	Private WithEvents lblFixWData As System.Windows.Forms.Label
	Private WithEvents lblCSVDelData As System.Windows.Forms.Label
	Private WithEvents lblCSV As System.Windows.Forms.Label
	Private WithEvents arvMain As GrapeCity.ActiveReports.Viewer.Win.Viewer

End Class
