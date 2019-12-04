 _
Partial Class rptInheritChild
	Inherits GrapeCity.ActiveReports.Sample.Inheritance.rptInheritBase





<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptInheritChild))
		Me.TextBox = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
		Me.TextBox1 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
		Me.TextBox2 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
		Me.TextBox3 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
		Me.TextBox4 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
		Me.TextBox7 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
		Me.TextBox8 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
		Me.Detail = New GrapeCity.ActiveReports.SectionReportModel.Detail()
		CType(Me.TextBox, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.TextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.TextBox2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.TextBox3, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.TextBox4, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.TextBox7, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.TextBox8, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
		'
		'TextBox
		'
		Me.TextBox.CanShrink = True
		Me.TextBox.DataField = "CustomerID"
		resources.ApplyResources(Me.TextBox, "TextBox")
		Me.TextBox.Name = "TextBox"
		'
		'TextBox1
		'
		Me.TextBox1.CanShrink = True
		Me.TextBox1.DataField = "CompanyName"
		resources.ApplyResources(Me.TextBox1, "TextBox1")
		Me.TextBox1.Name = "TextBox1"
		'
		'TextBox2
		'
		Me.TextBox2.CanShrink = True
		Me.TextBox2.DataField = "ContactName"
		resources.ApplyResources(Me.TextBox2, "TextBox2")
		Me.TextBox2.Name = "TextBox2"
		'
		'TextBox3
		'
		Me.TextBox3.CanShrink = True
		Me.TextBox3.DataField = "ContactTitle"
		resources.ApplyResources(Me.TextBox3, "TextBox3")
		Me.TextBox3.Name = "TextBox3"
		'
		'TextBox4
		'
		Me.TextBox4.CanShrink = True
		Me.TextBox4.DataField = "Address"
		resources.ApplyResources(Me.TextBox4, "TextBox4")
		Me.TextBox4.Name = "TextBox4"
		'
		'TextBox7
		'
		Me.TextBox7.CanShrink = True
		Me.TextBox7.DataField = "PostalCode"
		resources.ApplyResources(Me.TextBox7, "TextBox7")
		Me.TextBox7.Name = "TextBox7"
		'
		'TextBox8
		'
		Me.TextBox8.CanShrink = True
		Me.TextBox8.DataField = "Country"
		resources.ApplyResources(Me.TextBox8, "TextBox8")
		Me.TextBox8.Name = "TextBox8"
		'
		'Detail
		'
		Me.Detail.CanShrink = True
		Me.Detail.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.TextBox, Me.TextBox1, Me.TextBox2, Me.TextBox3, Me.TextBox4, Me.TextBox7, Me.TextBox8})
		Me.Detail.Height = 0.656!
		Me.Detail.KeepTogether = True
		Me.Detail.Name = "Detail"
		'
		'rptInheritChild
		'
		Me.MasterReport = False
		Me.PageSettings.PaperHeight = 11.0!
		Me.PageSettings.PaperWidth = 8.5!
		Me.PrintWidth = 6.0!
		Me.Sections.Add(Me.Detail)
		Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" & _
					"l; font-size: 10pt; color: Black; ddo-char-set: 186", "Normal"))
		CType(Me.TextBox, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.TextBox1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.TextBox2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.TextBox3, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.TextBox4, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.TextBox7, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.TextBox8, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

	End Sub
Private WithEvents Detail As GrapeCity.ActiveReports.SectionReportModel.Detail
Private WithEvents TextBox As GrapeCity.ActiveReports.SectionReportModel.TextBox
Private WithEvents TextBox1 As GrapeCity.ActiveReports.SectionReportModel.TextBox
Private WithEvents TextBox2 As GrapeCity.ActiveReports.SectionReportModel.TextBox
Private WithEvents TextBox3 As GrapeCity.ActiveReports.SectionReportModel.TextBox
Private WithEvents TextBox4 As GrapeCity.ActiveReports.SectionReportModel.TextBox
Private WithEvents TextBox7 As GrapeCity.ActiveReports.SectionReportModel.TextBox
Private WithEvents TextBox8 As GrapeCity.ActiveReports.SectionReportModel.TextBox

End Class
