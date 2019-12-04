<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ExportForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExportForm))
		Me.exportButton = New System.Windows.Forms.Button()
		Me.exportTypes = New System.Windows.Forms.ComboBox()
		Me.label2 = New System.Windows.Forms.Label()
		Me.label1 = New System.Windows.Forms.Label()
		Me.reportsNames = New System.Windows.Forms.ComboBox()
		Me.SuspendLayout
		'
		'exportButton
		'
		resources.ApplyResources(Me.exportButton, "exportButton")
		Me.exportButton.Name = "exportButton"
		Me.exportButton.UseVisualStyleBackColor = true
		'
		'exportTypes
		'
		resources.ApplyResources(Me.exportTypes, "exportTypes")
		Me.exportTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.exportTypes.FormattingEnabled = true
		Me.exportTypes.Items.AddRange(New Object() {resources.GetString("exportTypes.Items"), resources.GetString("exportTypes.Items1"), resources.GetString("exportTypes.Items2"), resources.GetString("exportTypes.Items3"), resources.GetString("exportTypes.Items4")})
		Me.exportTypes.Name = "exportTypes"
		'
		'label2
		'
		resources.ApplyResources(Me.label2, "label2")
		Me.label2.Name = "label2"
		'
		'label1
		'
		resources.ApplyResources(Me.label1, "label1")
		Me.label1.Name = "label1"
		'
		'reportsNames
		'
		Me.reportsNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.reportsNames.FormattingEnabled = true
		Me.reportsNames.Items.AddRange(New Object() {resources.GetString("reportsNames.Items"), resources.GetString("reportsNames.Items1")})
		resources.ApplyResources(Me.reportsNames, "reportsNames")
		Me.reportsNames.Name = "reportsNames"
		'
		'ExportForm
		'
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.exportButton)
		Me.Controls.Add(Me.exportTypes)
		Me.Controls.Add(Me.label2)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.reportsNames)
		Me.Name = "ExportForm"
		Me.ResumeLayout(false)
		Me.PerformLayout

End Sub

	Private WithEvents exportButton As Button
	Private WithEvents exportTypes As ComboBox
	Private WithEvents label2 As Label
	Private WithEvents label1 As Label
	Private WithEvents reportsNames As ComboBox
End Class
