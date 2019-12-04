<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PDFDigitalSignature
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
		Me.arvMain = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
		Me.splitContainer = New System.Windows.Forms.SplitContainer()
		Me.lblVisibilityType = New System.Windows.Forms.Label()
		Me.cmbVisibilityType = New System.Windows.Forms.ComboBox()
		Me.chkTimeStamp = New System.Windows.Forms.CheckBox()
		Me.pdfExportButton = New System.Windows.Forms.Button()
		CType(Me.splitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splitContainer.Panel1.SuspendLayout()
		Me.splitContainer.Panel2.SuspendLayout()
		Me.splitContainer.SuspendLayout()
		Me.SuspendLayout()
		'
		'arvMain
		'
		Me.arvMain.CurrentPage = 0
		Me.arvMain.Dock = System.Windows.Forms.DockStyle.Fill
		Me.arvMain.Location = New System.Drawing.Point(0, 0)
		Me.arvMain.Margin = New System.Windows.Forms.Padding(4)
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
		Me.arvMain.Size = New System.Drawing.Size(933, 613)
		Me.arvMain.TabIndex = 0
		'
		'splitContainer
		'
		Me.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill
		Me.splitContainer.Location = New System.Drawing.Point(0, 0)
		Me.splitContainer.Margin = New System.Windows.Forms.Padding(4)
		Me.splitContainer.Name = "splitContainer"
		Me.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'splitContainer.Panel1
		'
		Me.splitContainer.Panel1.Controls.Add(Me.lblVisibilityType)
		Me.splitContainer.Panel1.Controls.Add(Me.cmbVisibilityType)
		Me.splitContainer.Panel1.Controls.Add(Me.chkTimeStamp)
		Me.splitContainer.Panel1.Controls.Add(Me.pdfExportButton)
		'
		'splitContainer.Panel2
		'
		Me.splitContainer.Panel2.Controls.Add(Me.arvMain)
		Me.splitContainer.Size = New System.Drawing.Size(933, 690)
		Me.splitContainer.SplitterDistance = 72
		Me.splitContainer.SplitterWidth = 5
		Me.splitContainer.TabIndex = 1
		'
		'lblVisibilityType
		'
		Me.lblVisibilityType.AutoSize = True
		Me.lblVisibilityType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
		Me.lblVisibilityType.ImeMode = System.Windows.Forms.ImeMode.NoControl
		Me.lblVisibilityType.Location = New System.Drawing.Point(256, 38)
		Me.lblVisibilityType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.lblVisibilityType.Name = "lblVisibilityType"
		Me.lblVisibilityType.Size = New System.Drawing.Size(113, 16)
		Me.lblVisibilityType.TabIndex = 3
		Me.lblVisibilityType.Text = "Signature Format:"
		'
		'cmbVisibilityType
		'
		Me.cmbVisibilityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmbVisibilityType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
		Me.cmbVisibilityType.FormattingEnabled = True
		Me.cmbVisibilityType.Items.AddRange(New Object() {"Invisible", "Text", "Image", "ImageText"})
		Me.cmbVisibilityType.Location = New System.Drawing.Point(381, 34)
		Me.cmbVisibilityType.Margin = New System.Windows.Forms.Padding(4)
		Me.cmbVisibilityType.Name = "cmbVisibilityType"
		Me.cmbVisibilityType.Size = New System.Drawing.Size(160, 24)
		Me.cmbVisibilityType.TabIndex = 2
		'
		'chkTimeStamp
		'
		Me.chkTimeStamp.AutoSize = True
		Me.chkTimeStamp.Checked = True
		Me.chkTimeStamp.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkTimeStamp.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
		Me.chkTimeStamp.ImeMode = System.Windows.Forms.ImeMode.NoControl
		Me.chkTimeStamp.Location = New System.Drawing.Point(637, 36)
		Me.chkTimeStamp.Margin = New System.Windows.Forms.Padding(4)
		Me.chkTimeStamp.Name = "chkTimeStamp"
		Me.chkTimeStamp.Size = New System.Drawing.Size(127, 21)
		Me.chkTimeStamp.TabIndex = 1
		Me.chkTimeStamp.Text = " Set TimeStamp"
		Me.chkTimeStamp.UseVisualStyleBackColor = True
		'
		'pdfExportButton
		'
		Me.pdfExportButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
		Me.pdfExportButton.ImeMode = System.Windows.Forms.ImeMode.NoControl
		Me.pdfExportButton.Location = New System.Drawing.Point(24, 24)
		Me.pdfExportButton.Margin = New System.Windows.Forms.Padding(4)
		Me.pdfExportButton.Name = "pdfExportButton"
		Me.pdfExportButton.Size = New System.Drawing.Size(191, 44)
		Me.pdfExportButton.TabIndex = 0
		Me.pdfExportButton.Text = "Generate Digitally Signed PDF"
		Me.pdfExportButton.UseVisualStyleBackColor = True
		'
		'PDFDigitalSignature
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(933, 690)
		Me.Controls.Add(Me.splitContainer)
		Me.Name = "PDFDigitalSignature"
		Me.Text = "DigitalSignaturePro"
		Me.splitContainer.Panel1.ResumeLayout(False)
		Me.splitContainer.Panel1.PerformLayout()
		Me.splitContainer.Panel2.ResumeLayout(False)
		CType(Me.splitContainer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splitContainer.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	Private WithEvents arvMain As GrapeCity.ActiveReports.Viewer.Win.Viewer
	Private WithEvents splitContainer As SplitContainer
	Private WithEvents lblVisibilityType As Label
	Private WithEvents cmbVisibilityType As ComboBox
	Private WithEvents chkTimeStamp As CheckBox
	Private WithEvents pdfExportButton As Button
End Class
