<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PDFDigitalSignature
	Inherits System.Windows.Forms.Form


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


	Private components As System.ComponentModel.IContainer

	
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PDFDigitalSignature))
		Me.splitContainer = New System.Windows.Forms.SplitContainer()
		Me.chkTimeStamp = New System.Windows.Forms.CheckBox()
		Me.cmbVisibilityType = New System.Windows.Forms.ComboBox()
		Me.lblVisibilityType = New System.Windows.Forms.Label()
		Me.btnExport = New System.Windows.Forms.Button()
		Me.arvMain = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		CType(Me.splitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splitContainer.Panel1.SuspendLayout()
		Me.splitContainer.Panel2.SuspendLayout()
		Me.splitContainer.SuspendLayout()
		Me.SuspendLayout()
		'
		'splitContainer
		'
		resources.ApplyResources(Me.splitContainer, "splitContainer")
		Me.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.splitContainer.Name = "splitContainer"
		'
		'splitContainer.Panel1
		'
		Me.splitContainer.Panel1.Controls.Add(Me.chkTimeStamp)
		Me.splitContainer.Panel1.Controls.Add(Me.cmbVisibilityType)
		Me.splitContainer.Panel1.Controls.Add(Me.lblVisibilityType)
		Me.splitContainer.Panel1.Controls.Add(Me.btnExport)
		'
		'splitContainer.Panel2
		'
		Me.splitContainer.Panel2.Controls.Add(Me.arvMain)
		'
		'chkTimeStamp
		'
		resources.ApplyResources(Me.chkTimeStamp, "chkTimeStamp")
		Me.chkTimeStamp.Checked = True
		Me.chkTimeStamp.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkTimeStamp.Name = "chkTimeStamp"
		Me.chkTimeStamp.UseVisualStyleBackColor = True
		'
		'cmbVisibilityType
		'
		Me.cmbVisibilityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmbVisibilityType.FormattingEnabled = True
		Me.cmbVisibilityType.Items.AddRange(New Object() {resources.GetString("cmbVisibilityType.Items"), resources.GetString("cmbVisibilityType.Items1"), resources.GetString("cmbVisibilityType.Items2"), resources.GetString("cmbVisibilityType.Items3")})
		resources.ApplyResources(Me.cmbVisibilityType, "cmbVisibilityType")
		Me.cmbVisibilityType.Name = "cmbVisibilityType"
		'
		'lblVisibilityType
		'
		resources.ApplyResources(Me.lblVisibilityType, "lblVisibilityType")
		Me.lblVisibilityType.Name = "lblVisibilityType"
		'
		'btnExport
		'
		resources.ApplyResources(Me.btnExport, "btnExport")
		Me.btnExport.Name = "btnExport"
		Me.btnExport.UseVisualStyleBackColor = True
		'
		'arvMain
		'
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
		'PDFDigitalSignature
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.splitContainer)
		Me.Name = "PDFDigitalSignature"
		Me.splitContainer.Panel1.ResumeLayout(False)
		Me.splitContainer.Panel1.PerformLayout()
		Me.splitContainer.Panel2.ResumeLayout(False)
		CType(Me.splitContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splitContainer.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents splitContainer As System.Windows.Forms.SplitContainer
	Friend WithEvents chkTimeStamp As System.Windows.Forms.CheckBox
	Friend WithEvents cmbVisibilityType As System.Windows.Forms.ComboBox
	Friend WithEvents lblVisibilityType As System.Windows.Forms.Label
	Friend WithEvents btnExport As System.Windows.Forms.Button
	Friend WithEvents arvMain As Viewer.Win.Viewer
End Class
