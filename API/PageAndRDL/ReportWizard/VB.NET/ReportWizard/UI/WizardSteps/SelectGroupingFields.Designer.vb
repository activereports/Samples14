Imports Microsoft.VisualBasic
Imports System.Resources
Imports System.Windows.Forms
Namespace UI.WizardSteps
	Partial Class SelectGroupingFields
		' <summary>
		'Required designer variable.
		' </summary>
		Private components As System.ComponentModel.IContainer = Nothing
		' <summary>
		'Clean up any resources being used.
		' </summary>
		'<param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso Not ((components) Is Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
		' <summary>
		'Required method for Designer support - do not modify
		'the contents of this method with the code editor.
		' </summary>
		Private Sub InitializeComponent()
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectGroupingFields))
			Me.availableGroupsLabel = New System.Windows.Forms.Label()
			Me.addGroup = New System.Windows.Forms.Button()
			Me.removeGroup = New System.Windows.Forms.Button()
			Me.label2 = New System.Windows.Forms.Label()
			Me.isLastGroupDetail = New System.Windows.Forms.CheckBox()
			Me.selectedGroups = New System.Windows.Forms.TreeView()
			Me.availableGroups = New GrapeCity.ActiveReports.Samples.ReportWizard.UI.DragDropListBox()
			Me.SuspendLayout()
			'
			'availableGroupsLabel
			'
			resources.ApplyResources(Me.availableGroupsLabel, "availableGroupsLabel")
			Me.availableGroupsLabel.Name = "availableGroupsLabel"
			'
			'addGroup
			'
			resources.ApplyResources(Me.addGroup, "addGroup")
			Me.addGroup.Name = "addGroup"
			Me.addGroup.UseVisualStyleBackColor = True
			'
			'removeGroup
			'
			resources.ApplyResources(Me.removeGroup, "removeGroup")
			Me.removeGroup.Name = "removeGroup"
			Me.removeGroup.UseVisualStyleBackColor = True
			'
			'label2
			'
			resources.ApplyResources(Me.label2, "label2")
			Me.label2.Name = "label2"
			'
			'isLastGroupDetail
			'
			resources.ApplyResources(Me.isLastGroupDetail, "isLastGroupDetail")
			Me.isLastGroupDetail.Name = "isLastGroupDetail"
			Me.isLastGroupDetail.UseVisualStyleBackColor = True
			'
			'selectedGroups
			'
			Me.selectedGroups.AllowDrop = True
			resources.ApplyResources(Me.selectedGroups, "selectedGroups")
			Me.selectedGroups.Name = "selectedGroups"
			'
			'availableGroups
			'
			resources.ApplyResources(Me.availableGroups, "availableGroups")
			Me.availableGroups.Name = "availableGroups"
			'
			'SelectGroupingFields
			'
			resources.ApplyResources(Me, "$this")
			Me.Controls.Add(Me.availableGroups)
			Me.Controls.Add(Me.selectedGroups)
			Me.Controls.Add(Me.isLastGroupDetail)
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.removeGroup)
			Me.Controls.Add(Me.addGroup)
			Me.Controls.Add(Me.availableGroupsLabel)

			Me.Description = resources.GetString("Description")
			Me.Name = "SelectGroupingFields"
			Me.Title = resources.GetString("Title")

			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub
		Private availableGroupsLabel As System.Windows.Forms.Label
		Friend WithEvents addGroup As System.Windows.Forms.Button
		Friend WithEvents removeGroup As System.Windows.Forms.Button
		Private label2 As System.Windows.Forms.Label
		Private isLastGroupDetail As System.Windows.Forms.CheckBox
		Friend WithEvents selectedGroups As System.Windows.Forms.TreeView
		Friend WithEvents availableGroups As GrapeCity.ActiveReports.Samples.ReportWizard.UI.DragDropListBox
	End Class
End Namespace
