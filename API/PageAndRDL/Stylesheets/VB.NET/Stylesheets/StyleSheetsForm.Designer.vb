<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StyleSheetsForm
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
	'Required method for Designer support - do not modify
	'the contents of this method with the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StyleSheetsForm))
		Me.reportViewer = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.groupBoxChooseReport = New System.Windows.Forms.GroupBox()
		Me.radioButtonPageReport = New System.Windows.Forms.RadioButton()
		Me.radioButtonRDLReport = New System.Windows.Forms.RadioButton()
		Me.buttonChooseExtStyle = New System.Windows.Forms.Button()
		Me.groupBoxChooseStyle = New System.Windows.Forms.GroupBox()
		Me.radioButtonBlueStyle = New System.Windows.Forms.RadioButton()
		Me.radioButtonGreenStyle = New System.Windows.Forms.RadioButton()
		Me.radioButtonCustomStyle = New System.Windows.Forms.RadioButton()
		Me.radioButtonOrangeStyle = New System.Windows.Forms.RadioButton()
		Me.radioButtonEmbeddedStyle = New System.Windows.Forms.RadioButton()
		Me.buttonRunReport = New System.Windows.Forms.Button()
		Me.groupBoxChooseReport.SuspendLayout()
		Me.groupBoxChooseStyle.SuspendLayout()
		Me.SuspendLayout()
		'
		'reportViewer
		'
		resources.ApplyResources(Me.reportViewer, "reportViewer")
		Me.reportViewer.BackColor = System.Drawing.SystemColors.Control
		Me.reportViewer.CurrentPage = 0
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
		Me.reportViewer.Sidebar.TocPanel.Expanded = True
		Me.reportViewer.Sidebar.TocPanel.Width = 200
		Me.reportViewer.Sidebar.Width = 200
		'
		'groupBoxChooseReport
		'
		Me.groupBoxChooseReport.Controls.Add(Me.radioButtonPageReport)
		Me.groupBoxChooseReport.Controls.Add(Me.radioButtonRDLReport)
		resources.ApplyResources(Me.groupBoxChooseReport, "groupBoxChooseReport")
		Me.groupBoxChooseReport.Name = "groupBoxChooseReport"
		Me.groupBoxChooseReport.TabStop = False
		'
		'radioButtonPageReport
		'
		resources.ApplyResources(Me.radioButtonPageReport, "radioButtonPageReport")
		Me.radioButtonPageReport.Checked = True
		Me.radioButtonPageReport.Name = "radioButtonPageReport"
		Me.radioButtonPageReport.TabStop = True
		Me.radioButtonPageReport.UseVisualStyleBackColor = True
		'
		'radioButtonRDLReport
		'
		resources.ApplyResources(Me.radioButtonRDLReport, "radioButtonRDLReport")
		Me.radioButtonRDLReport.Name = "radioButtonRDLReport"
		Me.radioButtonRDLReport.UseVisualStyleBackColor = True
		'
		'buttonChooseExtStyle
		'
		resources.ApplyResources(Me.buttonChooseExtStyle, "buttonChooseExtStyle")
		Me.buttonChooseExtStyle.Name = "buttonChooseExtStyle"
		Me.buttonChooseExtStyle.UseVisualStyleBackColor = True
		'
		'groupBoxChooseStyle
		'
		Me.groupBoxChooseStyle.Controls.Add(Me.radioButtonBlueStyle)
		Me.groupBoxChooseStyle.Controls.Add(Me.radioButtonGreenStyle)
		Me.groupBoxChooseStyle.Controls.Add(Me.buttonChooseExtStyle)
		Me.groupBoxChooseStyle.Controls.Add(Me.radioButtonCustomStyle)
		Me.groupBoxChooseStyle.Controls.Add(Me.radioButtonOrangeStyle)
		Me.groupBoxChooseStyle.Controls.Add(Me.radioButtonEmbeddedStyle)
		resources.ApplyResources(Me.groupBoxChooseStyle, "groupBoxChooseStyle")
		Me.groupBoxChooseStyle.Name = "groupBoxChooseStyle"
		Me.groupBoxChooseStyle.TabStop = False
		'
		'radioButtonBlueStyle
		'
		resources.ApplyResources(Me.radioButtonBlueStyle, "radioButtonBlueStyle")
		Me.radioButtonBlueStyle.Name = "radioButtonBlueStyle"
		Me.radioButtonBlueStyle.UseVisualStyleBackColor = True
		'
		'radioButtonGreenStyle
		'
		resources.ApplyResources(Me.radioButtonGreenStyle, "radioButtonGreenStyle")
		Me.radioButtonGreenStyle.Name = "radioButtonGreenStyle"
		Me.radioButtonGreenStyle.UseVisualStyleBackColor = True
		'
		'radioButtonCustomStyle
		'
		resources.ApplyResources(Me.radioButtonCustomStyle, "radioButtonCustomStyle")
		Me.radioButtonCustomStyle.Name = "radioButtonCustomStyle"
		Me.radioButtonCustomStyle.UseVisualStyleBackColor = True
		'
		'radioButtonOrangeStyle
		'
		resources.ApplyResources(Me.radioButtonOrangeStyle, "radioButtonOrangeStyle")
		Me.radioButtonOrangeStyle.Name = "radioButtonOrangeStyle"
		Me.radioButtonOrangeStyle.UseVisualStyleBackColor = True
		'
		'radioButtonEmbeddedStyle
		'
		Me.radioButtonEmbeddedStyle.Checked = True
		resources.ApplyResources(Me.radioButtonEmbeddedStyle, "radioButtonEmbeddedStyle")
		Me.radioButtonEmbeddedStyle.Name = "radioButtonEmbeddedStyle"
		Me.radioButtonEmbeddedStyle.TabStop = True
		Me.radioButtonEmbeddedStyle.UseVisualStyleBackColor = True
		'
		'buttonRunReport
		'
		resources.ApplyResources(Me.buttonRunReport, "buttonRunReport")
		Me.buttonRunReport.Name = "buttonRunReport"
		Me.buttonRunReport.UseVisualStyleBackColor = True
		'
		'StyleSheetsForm
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.reportViewer)
		Me.Controls.Add(Me.groupBoxChooseReport)
		Me.Controls.Add(Me.groupBoxChooseStyle)
		Me.Controls.Add(Me.buttonRunReport)
		Me.Name = "StyleSheetsForm"
		Me.groupBoxChooseReport.ResumeLayout(False)
		Me.groupBoxChooseReport.PerformLayout()
		Me.groupBoxChooseStyle.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Private WithEvents reportViewer As GrapeCity.ActiveReports.Viewer.Win.Viewer
	Private WithEvents groupBoxChooseReport As System.Windows.Forms.GroupBox
	Private WithEvents radioButtonPageReport As System.Windows.Forms.RadioButton
	Private WithEvents radioButtonRDLReport As System.Windows.Forms.RadioButton
	Private WithEvents buttonChooseExtStyle As System.Windows.Forms.Button
	Private WithEvents groupBoxChooseStyle As System.Windows.Forms.GroupBox
	Private WithEvents radioButtonCustomStyle As System.Windows.Forms.RadioButton
	Private WithEvents radioButtonOrangeStyle As System.Windows.Forms.RadioButton
	Private WithEvents radioButtonEmbeddedStyle As System.Windows.Forms.RadioButton
	Private WithEvents buttonRunReport As System.Windows.Forms.Button
	Private WithEvents radioButtonBlueStyle As System.Windows.Forms.RadioButton
	Private WithEvents radioButtonGreenStyle As System.Windows.Forms.RadioButton

End Class
