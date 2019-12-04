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
		Me.radioButtonCategoriesReport = New System.Windows.Forms.RadioButton()
		Me.radioButtonProductListReport = New System.Windows.Forms.RadioButton()
		Me.buttonChooseExtStyle = New System.Windows.Forms.Button()
		Me.groupBoxChooseStyle = New System.Windows.Forms.GroupBox()
		Me.radioButtonExternalStyleSheet = New System.Windows.Forms.RadioButton()
		Me.radioButtonColoredStyle = New System.Windows.Forms.RadioButton()
		Me.radioButtonClassicStyle = New System.Windows.Forms.RadioButton()
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
		Me.reportViewer.HyperlinkBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.reportViewer.HyperlinkForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
		Me.reportViewer.Name = "reportViewer"
		Me.reportViewer.PagesBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.reportViewer.PreviewPages = 0
		Me.reportViewer.SearchResultsBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
		Me.reportViewer.SearchResultsForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(139, Byte), Integer))
		'
		'
		'
		'
		'
		'
		Me.reportViewer.Sidebar.ParametersPanel.ContextMenu = Nothing
		Me.reportViewer.Sidebar.ParametersPanel.Width = 180
		'
		'
		'
		Me.reportViewer.Sidebar.SearchPanel.ContextMenu = Nothing
		Me.reportViewer.Sidebar.SearchPanel.Width = 180
		Me.reportViewer.Sidebar.SelectedIndex = 0
		'
		'
		'
		Me.reportViewer.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
		Me.reportViewer.Sidebar.ThumbnailsPanel.Width = 180
		'
		'
		'
		Me.reportViewer.Sidebar.TocPanel.ContextMenu = Nothing
		Me.reportViewer.Sidebar.TocPanel.Width = 180
		Me.reportViewer.Sidebar.Width = 180
		Me.reportViewer.SplitView = False
		Me.reportViewer.ViewType = GrapeCity.Viewer.Common.Model.ViewType.SinglePage
		Me.reportViewer.Zoom = 1.0!
		'
		'groupBoxChooseReport
		'
		resources.ApplyResources(Me.groupBoxChooseReport, "groupBoxChooseReport")
		Me.groupBoxChooseReport.Controls.Add(Me.radioButtonCategoriesReport)
		Me.groupBoxChooseReport.Controls.Add(Me.radioButtonProductListReport)
		Me.groupBoxChooseReport.Name = "groupBoxChooseReport"
		Me.groupBoxChooseReport.TabStop = False
		'
		'radioButtonCategoriesReport
		'
		resources.ApplyResources(Me.radioButtonCategoriesReport, "radioButtonCategoriesReport")
		Me.radioButtonCategoriesReport.Checked = True
		Me.radioButtonCategoriesReport.Name = "radioButtonCategoriesReport"
		Me.radioButtonCategoriesReport.TabStop = True
		Me.radioButtonCategoriesReport.UseVisualStyleBackColor = True
		'
		'radioButtonProductListReport
		'
		resources.ApplyResources(Me.radioButtonProductListReport, "radioButtonProductListReport")
		Me.radioButtonProductListReport.Name = "radioButtonProductListReport"
		Me.radioButtonProductListReport.UseVisualStyleBackColor = True
		'
		'buttonChooseExtStyle
		'
		resources.ApplyResources(Me.buttonChooseExtStyle, "buttonChooseExtStyle")
		Me.buttonChooseExtStyle.Name = "buttonChooseExtStyle"
		Me.buttonChooseExtStyle.UseVisualStyleBackColor = True
		'
		'groupBoxChooseStyle
		'
		resources.ApplyResources(Me.groupBoxChooseStyle, "groupBoxChooseStyle")
		Me.groupBoxChooseStyle.Controls.Add(Me.buttonChooseExtStyle)
		Me.groupBoxChooseStyle.Controls.Add(Me.radioButtonExternalStyleSheet)
		Me.groupBoxChooseStyle.Controls.Add(Me.radioButtonColoredStyle)
		Me.groupBoxChooseStyle.Controls.Add(Me.radioButtonClassicStyle)
		Me.groupBoxChooseStyle.Name = "groupBoxChooseStyle"
		Me.groupBoxChooseStyle.TabStop = False
		'
		'radioButtonExternalStyleSheet
		'
		resources.ApplyResources(Me.radioButtonExternalStyleSheet, "radioButtonExternalStyleSheet")
		Me.radioButtonExternalStyleSheet.Name = "radioButtonExternalStyleSheet"
		Me.radioButtonExternalStyleSheet.UseVisualStyleBackColor = True
		'
		'radioButtonColoredStyle
		'
		resources.ApplyResources(Me.radioButtonColoredStyle, "radioButtonColoredStyle")
		Me.radioButtonColoredStyle.Name = "radioButtonColoredStyle"
		Me.radioButtonColoredStyle.UseVisualStyleBackColor = True
		'
		'radioButtonClassicStyle
		'
		resources.ApplyResources(Me.radioButtonClassicStyle, "radioButtonClassicStyle")
		Me.radioButtonClassicStyle.Checked = True
		Me.radioButtonClassicStyle.Name = "radioButtonClassicStyle"
		Me.radioButtonClassicStyle.TabStop = True
		Me.radioButtonClassicStyle.UseVisualStyleBackColor = True
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
		Me.groupBoxChooseStyle.ResumeLayout(False)
		Me.groupBoxChooseStyle.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Private WithEvents reportViewer As GrapeCity.ActiveReports.Viewer.Win.Viewer
	Private WithEvents groupBoxChooseReport As System.Windows.Forms.GroupBox
	Private WithEvents radioButtonCategoriesReport As System.Windows.Forms.RadioButton
	Private WithEvents radioButtonProductListReport As System.Windows.Forms.RadioButton
	Private WithEvents buttonChooseExtStyle As System.Windows.Forms.Button
	Private WithEvents groupBoxChooseStyle As System.Windows.Forms.GroupBox
	Private WithEvents radioButtonExternalStyleSheet As System.Windows.Forms.RadioButton
	Private WithEvents radioButtonColoredStyle As System.Windows.Forms.RadioButton
	Private WithEvents radioButtonClassicStyle As System.Windows.Forms.RadioButton
	Private WithEvents buttonRunReport As System.Windows.Forms.Button

End Class
