Namespace UI.WizardSteps
	Partial Class BaseStep
		' <summary>
		'Required designer variable.
		' </summary>
		Private components As System.ComponentModel.IContainer = Nothing
		' <summary>
		' Clean up any resources being used.
		' </summary>
		' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso Not ((components) Is Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
		' <summary>
		'Required method for Designer support - do not modify
		'the contents of this method with the code editor.
		Private Sub InitializeComponent()
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BaseStep))
			Me.SuspendLayout()
			'
			'BaseStep
			'
			resources.ApplyResources(Me, "$this")
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.MaximumSize = New System.Drawing.Size(638, 270)
			Me.MinimumSize = New System.Drawing.Size(638, 270)
			Me.Name = "BaseStep"
			Me.ResumeLayout(False)

		End Sub
	End Class
End Namespace
