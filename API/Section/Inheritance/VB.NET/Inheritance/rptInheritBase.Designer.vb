 _
Partial Class rptInheritBase
	Inherits GrapeCity.ActiveReports.SectionReport





	Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(rptInheritBase))
		CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
		'
		'rptInheritBase
		'
		Me.MasterReport = False
		Me.PageSettings.PaperHeight = 11.0!
		Me.PageSettings.PaperWidth = 8.5!
		Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" &
					"l; font-size: 10pt; color: Black; ddo-char-set: 186", "Normal"))
		CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

	End Sub

End Class
