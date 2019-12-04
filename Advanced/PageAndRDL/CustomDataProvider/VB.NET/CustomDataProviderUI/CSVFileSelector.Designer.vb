Partial Class CSVFileSelector
   

	Private components As System.ComponentModel.IContainer = Nothing
  

	Protected Overloads Overrides Sub Dispose(disposing As Boolean)
		If disposing AndAlso Not ((components) Is Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub
	

			Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CSVFileSelector))
		Me.btnSelectFile = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		'
		'btnSelectFile
		'
		resources.ApplyResources(Me.btnSelectFile, "btnSelectFile")
		Me.btnSelectFile.Name = "btnSelectFile"
		Me.btnSelectFile.UseVisualStyleBackColor = True
		'
		'CSVFileSelector
		'
		Me.BackColor = System.Drawing.Color.DeepSkyBlue
		Me.Controls.Add(Me.btnSelectFile)
		Me.Name = "CSVFileSelector"
		resources.ApplyResources(Me, "$this")
		Me.ResumeLayout(False)

	End Sub
	Private WithEvents btnSelectFile As System.Windows.Forms.Button
		End Class
