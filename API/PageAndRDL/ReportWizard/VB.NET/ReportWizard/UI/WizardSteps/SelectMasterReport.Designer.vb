Imports System.Resources
Imports GrapeCity.ActiveReports.Samples.ReportWizard.UI
Namespace UI.WizardSteps
	Partial Class SelectMasterReport
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
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectMasterReport))
			Me.label1 = New System.Windows.Forms.Label()
			Me.reportList = New System.Windows.Forms.ListBox()
			Me.tipControl1 = New GrapeCity.ActiveReports.Samples.ReportWizard.UI.TipControl()
			Me.SuspendLayout()
			'
			'label1
			'
			resources.ApplyResources(Me.label1, "label1")
			Me.label1.Name = "label1"
			'
			'reportList
			'
			Me.reportList.DisplayMember = "Title"
			Me.reportList.FormattingEnabled = True
			resources.ApplyResources(Me.reportList, "reportList")
			Me.reportList.Name = "reportList"
			'
			'tipControl1
			'
			resources.ApplyResources(Me.tipControl1, "tipControl1")
			Me.tipControl1.Name = "tipControl1"
			'
			'SelectMasterReport
			'
			resources.ApplyResources(Me, "$this")
			Me.Controls.Add(Me.tipControl1)
			Me.Controls.Add(Me.reportList)
			Me.Controls.Add(Me.label1)

			Me.Description = resources.GetString("Description")
			Me.Name = "SelectMasterReport"
			Me.Title = resources.GetString("Title")

			Me.ResumeLayout(False)
			Me.PerformLayout()
		End Sub
		Private label1 As System.Windows.Forms.Label
		Private WithEvents reportList As System.Windows.Forms.ListBox
		Private tipControl1 As TipControl
	End Class
End Namespace
