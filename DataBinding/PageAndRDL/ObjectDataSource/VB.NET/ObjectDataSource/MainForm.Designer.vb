<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	'
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
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer. 
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.reportPreview = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.SuspendLayout()
		'
		'reportPreview
		'
		Me.reportPreview.AnnotationDropDownVisible = False
		Me.reportPreview.CurrentPage = 0
		Me.reportPreview.Dock = System.Windows.Forms.DockStyle.Fill
		Me.reportPreview.HyperlinkBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.reportPreview.HyperlinkForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
		Me.reportPreview.Location = New System.Drawing.Point(0, 0)
		Me.reportPreview.Name = "reportPreview"
		Me.reportPreview.PagesBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.reportPreview.PreviewPages = 0
		Me.reportPreview.SearchResultsBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
		Me.reportPreview.SearchResultsForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(139, Byte), Integer))
		Me.reportPreview.Size = New System.Drawing.Size(638, 394)
		Me.reportPreview.SplitView = False
		Me.reportPreview.TabIndex = 0
		Me.reportPreview.ViewType = GrapeCity.Viewer.Common.Model.ViewType.SinglePage
		Me.reportPreview.Zoom = 1.0!
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(638, 394)
		Me.Controls.Add(Me.reportPreview)
		Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
		Me.Name = "MainForm"
		Me.Text = My.Resources.ObjectDataSourceText
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents reportPreview As GrapeCity.ActiveReports.Viewer.Win.Viewer

End Class
