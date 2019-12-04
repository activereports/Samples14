<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LayersForm
	Inherits System.Windows.Forms.Form

	'Dispose to clean up the components
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

	'Required designer variable.
	Private components As System.ComponentModel.IContainer

	'The following procedure Is required by Windows Form Designer
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LayersForm))
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog()
		Me.bottomPanel = New System.Windows.Forms.Panel()
		Me.reportViewer = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.topPanel = New System.Windows.Forms.Panel()
		Me.checkBoxPDF = New System.Windows.Forms.CheckBox()
		Me.checkBoxPaper = New System.Windows.Forms.CheckBox()
		Me.checkBoxScreen = New System.Windows.Forms.CheckBox()
		Me.rtbDescription = New System.Windows.Forms.RichTextBox()
		Me.panelButtonContainer = New System.Windows.Forms.Panel()
		Me.btnPdfExport = New System.Windows.Forms.Button()
		Me.btnPreview = New System.Windows.Forms.Button()
		Me.bottomPanel.SuspendLayout
		Me.topPanel.SuspendLayout
		Me.panelButtonContainer.SuspendLayout
		Me.SuspendLayout
		'
		'bottomPanel
		'
		Me.bottomPanel.Controls.Add(Me.reportViewer)
		Me.bottomPanel.Controls.Add(Me.topPanel)
		resources.ApplyResources(Me.bottomPanel, "bottomPanel")
		Me.bottomPanel.Name = "bottomPanel"
		'
		'reportViewer
		'
		Me.reportViewer.CurrentPage = 0
		resources.ApplyResources(Me.reportViewer, "reportViewer")
		Me.reportViewer.Name = "reportViewer"
		Me.reportViewer.PreviewPages = 0
		'
		'
		'
		'
		'
		'
		Me.reportViewer.Sidebar.ParametersPanel.ContextMenu = Nothing
		Me.reportViewer.Sidebar.ParametersPanel.Width = 200
		'
		'
		'
		Me.reportViewer.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.reportViewer.Sidebar.SearchPanel.Width = 200
		'
		'
		'
		Me.reportViewer.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.reportViewer.Sidebar.ThumbnailsPanel.Width = 200
		Me.reportViewer.Sidebar.ThumbnailsPanel.Zoom = 0.1R
		'
		'
		'
		Me.reportViewer.Sidebar.TocPanel.ContextMenu = Nothing
		Me.reportViewer.Sidebar.TocPanel.Expanded = true
		Me.reportViewer.Sidebar.TocPanel.Width = 200
		Me.reportViewer.Sidebar.Width = 200
		'
		'topPanel
		'
		Me.topPanel.Controls.Add(Me.checkBoxPDF)
		Me.topPanel.Controls.Add(Me.checkBoxPaper)
		Me.topPanel.Controls.Add(Me.checkBoxScreen)
		Me.topPanel.Controls.Add(Me.rtbDescription)
		Me.topPanel.Controls.Add(Me.panelButtonContainer)
		resources.ApplyResources(Me.topPanel, "topPanel")
		Me.topPanel.Name = "topPanel"
		'
		'checkBoxPDF
		'
		resources.ApplyResources(Me.checkBoxPDF, "checkBoxPDF")
		Me.checkBoxPDF.Checked = true
		Me.checkBoxPDF.CheckState = System.Windows.Forms.CheckState.Checked
		Me.checkBoxPDF.Name = "checkBoxPDF"
		Me.checkBoxPDF.UseVisualStyleBackColor = true
		'
		'checkBoxPaper
		'
		resources.ApplyResources(Me.checkBoxPaper, "checkBoxPaper")
		Me.checkBoxPaper.Checked = true
		Me.checkBoxPaper.CheckState = System.Windows.Forms.CheckState.Checked
		Me.checkBoxPaper.Name = "checkBoxPaper"
		Me.checkBoxPaper.UseVisualStyleBackColor = true
		'
		'checkBoxScreen
		'
		resources.ApplyResources(Me.checkBoxScreen, "checkBoxScreen")
		Me.checkBoxScreen.Checked = true
		Me.checkBoxScreen.CheckState = System.Windows.Forms.CheckState.Checked
		Me.checkBoxScreen.Name = "checkBoxScreen"
		Me.checkBoxScreen.UseVisualStyleBackColor = true
		'
		'rtbDescription
		'
		Me.rtbDescription.BackColor = System.Drawing.Color.White
		resources.ApplyResources(Me.rtbDescription, "rtbDescription")
		Me.rtbDescription.Name = "rtbDescription"
		Me.rtbDescription.ReadOnly = true
		'
		'panelButtonContainer
		'
		Me.panelButtonContainer.Controls.Add(Me.btnPdfExport)
		Me.panelButtonContainer.Controls.Add(Me.btnPreview)
		resources.ApplyResources(Me.panelButtonContainer, "panelButtonContainer")
		Me.panelButtonContainer.Name = "panelButtonContainer"
		'
		'btnPdfExport
		'
		resources.ApplyResources(Me.btnPdfExport, "btnPdfExport")
		Me.btnPdfExport.Name = "btnPdfExport"
		Me.btnPdfExport.UseVisualStyleBackColor = true
		'
		'btnPreview
		'
		resources.ApplyResources(Me.btnPreview, "btnPreview")
		Me.btnPreview.Name = "btnPreview"
		Me.btnPreview.UseVisualStyleBackColor = true
		'
		'LayersForm
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.bottomPanel)
		Me.Name = "LayersForm"
		Me.bottomPanel.ResumeLayout(false)
		Me.topPanel.ResumeLayout(false)
		Me.topPanel.PerformLayout
		Me.panelButtonContainer.ResumeLayout(false)
		Me.ResumeLayout(false)

End Sub
	Private WithEvents saveFileDialog As System.Windows.Forms.SaveFileDialog
	Private WithEvents bottomPanel As System.Windows.Forms.Panel
	Private WithEvents panelButtonContainer As System.Windows.Forms.Panel
	Private WithEvents btnPdfExport As System.Windows.Forms.Button
	Private WithEvents btnPreview As System.Windows.Forms.Button

	Private WithEvents topPanel As System.Windows.Forms.Panel
	Private WithEvents checkBoxPDF As System.Windows.Forms.CheckBox
	Private WithEvents checkBoxPaper As System.Windows.Forms.CheckBox
	Private WithEvents checkBoxScreen As System.Windows.Forms.CheckBox
	Private WithEvents rtbDescription As System.Windows.Forms.RichTextBox
	Friend WithEvents reportViewer As GrapeCity.ActiveReports.Viewer.Win.Viewer

End Class
