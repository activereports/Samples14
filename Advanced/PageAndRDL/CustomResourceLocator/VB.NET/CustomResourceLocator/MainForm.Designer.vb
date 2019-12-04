<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso components IsNot Nothing Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.richTextBox = New System.Windows.Forms.RichTextBox()
		Me.listView = New System.Windows.Forms.ListView()
		Me.imageList = New System.Windows.Forms.ImageList(Me.components)
		Me.showReport = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		'
		'richTextBox
		'
		resources.ApplyResources(Me.richTextBox, "richTextBox")
		Me.richTextBox.BackColor = System.Drawing.SystemColors.Window
		Me.richTextBox.Name = "richTextBox"
		Me.richTextBox.ReadOnly = True
		'
		'listView
		'
		resources.ApplyResources(Me.listView, "listView")
		Me.listView.HideSelection = False
		Me.listView.LargeImageList = Me.imageList
		Me.listView.MultiSelect = False
		Me.listView.Name = "listView"
		Me.listView.UseCompatibleStateImageBehavior = False
		'
		'imageList
		'
		Me.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
		resources.ApplyResources(Me.imageList, "imageList")
		Me.imageList.TransparentColor = System.Drawing.Color.Transparent
		'
		'showReport
		'
		resources.ApplyResources(Me.showReport, "showReport")
		Me.showReport.Name = "showReport"
		Me.showReport.UseVisualStyleBackColor = True
		'
		'MainForm
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.showReport)
		Me.Controls.Add(Me.listView)
		Me.Controls.Add(Me.richTextBox)
		Me.Name = "MainForm"
		Me.ResumeLayout(False)

	End Sub
	Private WithEvents richTextBox As System.Windows.Forms.RichTextBox
	Private WithEvents listView As System.Windows.Forms.ListView
	Private WithEvents showReport As System.Windows.Forms.Button
	Friend WithEvents imageList As System.Windows.Forms.ImageList

End Class
