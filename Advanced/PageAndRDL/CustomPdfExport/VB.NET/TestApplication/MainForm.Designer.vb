Imports GrapeCity.ActiveReports.Export.Pdf.Page

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
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.saveAsButton = New System.Windows.Forms.Button()
		Me.reports = New System.Windows.Forms.ComboBox()
		Me.exportButton = New System.Windows.Forms.Button()
		Me.propertyGrid = New System.Windows.Forms.PropertyGrid()
		Me.splitContainer2 = New System.Windows.Forms.SplitContainer()
		Me.pdfTime = New System.Windows.Forms.Label()
		Me.arTime = New System.Windows.Forms.Label()
		Me.RadioButton2 = New RenderingExtensionWrapper(New PdfSharpRenderingExtension(), My.Resources.PdfSharpName)
		Me.RadioButton1 = New RenderingExtensionWrapper(New PdfRenderingExtension(), My.Resources.PdfFromArName)
		Me.pdfLabel = New System.Windows.Forms.Label()
		Me.arLabel = New System.Windows.Forms.Label()
		Me.splitContainer3 = New System.Windows.Forms.SplitContainer()
		Me.arViewer = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.pdfViewer = New PdfiumViewer.PdfViewer()
		Me.saveAsPdf = New System.Windows.Forms.SaveFileDialog()
		CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splitContainer1.Panel1.SuspendLayout()
		Me.splitContainer1.Panel2.SuspendLayout()
		Me.splitContainer1.SuspendLayout()
		CType(Me.splitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splitContainer2.Panel1.SuspendLayout()
		Me.splitContainer2.Panel2.SuspendLayout()
		Me.splitContainer2.SuspendLayout()
		CType(Me.splitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splitContainer3.Panel1.SuspendLayout()
		Me.splitContainer3.Panel2.SuspendLayout()
		Me.splitContainer3.SuspendLayout()
		Me.SuspendLayout()
		'
		'splitContainer1
		'
		Me.splitContainer1.BackColor = System.Drawing.SystemColors.Control
		resources.ApplyResources(Me.splitContainer1, "splitContainer1")
		Me.splitContainer1.Name = "splitContainer1"
		'
		'splitContainer1.Panel1
		'
		Me.splitContainer1.Panel1.Controls.Add(Me.RadioButton2)
		Me.splitContainer1.Panel1.Controls.Add(Me.RadioButton1)
		Me.splitContainer1.Panel1.Controls.Add(Me.saveAsButton)
		Me.splitContainer1.Panel1.Controls.Add(Me.reports)
		Me.splitContainer1.Panel1.Controls.Add(Me.exportButton)
		Me.splitContainer1.Panel1.Controls.Add(Me.propertyGrid)
		'
		'splitContainer1.Panel2
		'
		Me.splitContainer1.Panel2.Controls.Add(Me.splitContainer2)
		'
		'RadioButton2
		'
		resources.ApplyResources(Me.RadioButton2, "RadioButton2")
		Me.RadioButton2.Name = "RadioButton2"
		Me.RadioButton2.Checked = True
		Me.RadioButton2.TabStop = True
		Me.RadioButton2.Text = Global.GrapeCity.ActiveReports.Samples.Export.Rendering.My.Resources.Resources.PdfSharpName
		Me.RadioButton2.UseVisualStyleBackColor = True
		'
		'RadioButton1
		'
		resources.ApplyResources(Me.RadioButton1, "RadioButton1")
		Me.RadioButton1.Name = "RadioButton1"
		Me.RadioButton1.TabStop = True
		Me.RadioButton1.Text = Global.GrapeCity.ActiveReports.Samples.Export.Rendering.My.Resources.Resources.PdfFromArName
		Me.RadioButton1.UseVisualStyleBackColor = True
		'
		'saveAsButton
		'
		resources.ApplyResources(Me.saveAsButton, "saveAsButton")
		Me.saveAsButton.Name = "saveAsButton"
		Me.saveAsButton.UseVisualStyleBackColor = True
		'
		'reports
		'
		resources.ApplyResources(Me.reports, "reports")
		Me.reports.FormattingEnabled = True
		Me.reports.Name = "reports"
		'
		'exportButton
		'
		resources.ApplyResources(Me.exportButton, "exportButton")
		Me.exportButton.Name = "exportButton"
		Me.exportButton.UseVisualStyleBackColor = True
		'
		'propertyGrid
		'
		resources.ApplyResources(Me.propertyGrid, "propertyGrid")
		Me.propertyGrid.LineColor = System.Drawing.SystemColors.ControlDark
		Me.propertyGrid.Name = "propertyGrid"
		'
		'splitContainer2
		'
		resources.ApplyResources(Me.splitContainer2, "splitContainer2")
		Me.splitContainer2.Name = "splitContainer2"
		'
		'splitContainer2.Panel1
		'
		Me.splitContainer2.Panel1.Controls.Add(Me.pdfTime)
		Me.splitContainer2.Panel1.Controls.Add(Me.arTime)
		Me.splitContainer2.Panel1.Controls.Add(Me.pdfLabel)
		Me.splitContainer2.Panel1.Controls.Add(Me.arLabel)
		'
		'splitContainer2.Panel2
		'
		Me.splitContainer2.Panel2.Controls.Add(Me.splitContainer3)
		'
		'pdfTime
		'
		resources.ApplyResources(Me.pdfTime, "pdfTime")
		Me.pdfTime.Name = "pdfTime"
		'
		'arTime
		'
		resources.ApplyResources(Me.arTime, "arTime")
		Me.arTime.Name = "arTime"
		'
		'pdfLabel
		'
		resources.ApplyResources(Me.pdfLabel, "pdfLabel")
		Me.pdfLabel.Name = "pdfLabel"
		'
		'arLabel
		'
		resources.ApplyResources(Me.arLabel, "arLabel")
		Me.arLabel.Name = "arLabel"
		'
		'splitContainer3
		'
		resources.ApplyResources(Me.splitContainer3, "splitContainer3")
		Me.splitContainer3.Name = "splitContainer3"
		'
		'splitContainer3.Panel1
		'
		Me.splitContainer3.Panel1.Controls.Add(Me.arViewer)
		'
		'splitContainer3.Panel2
		'
		Me.splitContainer3.Panel2.Controls.Add(Me.pdfViewer)
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
		Me.arViewer.Sidebar.ParametersPanel.Text = "Parameters"
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
		Me.arViewer.Sidebar.TocPanel.Expanded = True
		Me.arViewer.Sidebar.TocPanel.Text = "Document map"
		Me.arViewer.Sidebar.TocPanel.Width = 200
		Me.arViewer.Sidebar.Width = 200
		Me.arViewer.ViewType = GrapeCity.Viewer.Common.Model.ViewType.Continuous
		Me.arViewer.Zoom = -1.0!
		'
		'pdfViewer
		'
		resources.ApplyResources(Me.pdfViewer, "pdfViewer")
		Me.pdfViewer.Name = "pdfViewer"
		Me.pdfViewer.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitWidth
		'
		'saveAsPdf
		'
		resources.ApplyResources(Me.saveAsPdf, "saveAsPdf")
		Me.saveAsPdf.RestoreDirectory = True
		'
		'MainForm
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.splitContainer1)
		Me.Name = "MainForm"
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
		Me.splitContainer3.Panel1.ResumeLayout(False)
		Me.splitContainer3.Panel2.ResumeLayout(False)
		CType(Me.splitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splitContainer3.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	Private WithEvents splitContainer1 As SplitContainer
	Private WithEvents saveAsButton As Button
	Private WithEvents reports As ComboBox
	Private WithEvents exportButton As Button
	Private WithEvents propertyGrid As PropertyGrid
	Private WithEvents splitContainer2 As SplitContainer
	Private WithEvents pdfTime As Label
	Private WithEvents arTime As Label
	Private WithEvents pdfLabel As Label
	Private WithEvents arLabel As Label
	Private WithEvents splitContainer3 As SplitContainer
	Private WithEvents arViewer As Viewer.Win.Viewer
	Private WithEvents pdfViewer As PdfiumViewer.PdfViewer
	Private WithEvents saveAsPdf As SaveFileDialog
	Private WithEvents RadioButton2 As RenderingExtensionWrapper
	Private WithEvents RadioButton1 As RenderingExtensionWrapper
End Class
