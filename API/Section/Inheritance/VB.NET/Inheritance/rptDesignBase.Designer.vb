<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class rptDesignBase
	Inherits GrapeCity.ActiveReports.SectionReport

	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
		End If
		MyBase.Dispose(disposing)
	End Sub
#Region "ActiveReports Designer generated code"


	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptDesignBase))
		Me.Label1 = New GrapeCity.ActiveReports.SectionReportModel.Label()
		Me.riPageOf = New GrapeCity.ActiveReports.SectionReportModel.ReportInfo()
		Me.PageHeader = New GrapeCity.ActiveReports.SectionReportModel.PageHeader()
		Me.Detail = New GrapeCity.ActiveReports.SectionReportModel.Detail()
		Me.PageFooter = New GrapeCity.ActiveReports.SectionReportModel.PageFooter()
		Me.imgNW = New GrapeCity.ActiveReports.SectionReportModel.Picture()
		Me.imgLogo = New GrapeCity.ActiveReports.SectionReportModel.Picture()
		CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.riPageOf, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.imgNW, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.imgLogo, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
		'
		'Label1
		'
		resources.ApplyResources(Me.Label1, "Label1")
		Me.Label1.Name = "Label1"
		'
		'riPageOf
		'
		Me.riPageOf.FormatString = "{PageNumber} ／ {PageCount}"
		resources.ApplyResources(Me.riPageOf, "riPageOf")
		Me.riPageOf.Name = "riPageOf"
		'
		'PageHeader
		'
		Me.PageHeader.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.Label1, Me.imgNW, Me.imgLogo})
		Me.PageHeader.Height = 0.78125!
		Me.PageHeader.Name = "PageHeader"
		'
		'Detail
		'
		Me.Detail.Height = 0.2291667!
		Me.Detail.Name = "Detail"
		'
		'PageFooter
		'
		Me.PageFooter.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.riPageOf})
		Me.PageFooter.Height = 0.3333333!
		Me.PageFooter.Name = "PageFooter"
		'
		'imgNW
		'
		Me.imgNW.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
		resources.ApplyResources(Me.imgNW, "imgNW")
		Me.imgNW.ImageData = CType(resources.GetObject("imgNW.ImageData"), System.IO.Stream)
		Me.imgNW.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.imgNW.LineWeight = 1.0!
		Me.imgNW.Name = "imgNW"
		Me.imgNW.SizeMode = GrapeCity.ActiveReports.SectionReportModel.SizeModes.Zoom
		'
		'imgLogo
		'
		Me.imgLogo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
		resources.ApplyResources(Me.imgLogo, "imgLogo")
		Me.imgLogo.ImageData = CType(resources.GetObject("imgLogo.ImageData"), System.IO.Stream)
		Me.imgLogo.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.imgLogo.LineWeight = 1.0!
		Me.imgLogo.Name = "imgLogo"
		Me.imgLogo.SizeMode = GrapeCity.ActiveReports.SectionReportModel.SizeModes.Zoom
		'
		'rptDesignBase
		'
		Me.MasterReport = True
		Me.PageSettings.PaperHeight = 11.0!
		Me.PageSettings.PaperWidth = 8.5!
		Me.PrintWidth = 6.0!
		Me.Sections.Add(Me.PageHeader)
		Me.Sections.Add(Me.Detail)
		Me.Sections.Add(Me.PageFooter)
		Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" &
			"l; font-size: 10pt; color: Black; ddo-char-set: 186", "Normal"))
		CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.riPageOf, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.imgNW, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.imgLogo, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

	End Sub
#End Region
	Private WithEvents PageHeader As GrapeCity.ActiveReports.SectionReportModel.PageHeader
	Private WithEvents Detail As GrapeCity.ActiveReports.SectionReportModel.Detail
	Private WithEvents PageFooter As GrapeCity.ActiveReports.SectionReportModel.PageFooter
	Private WithEvents Label1 As GrapeCity.ActiveReports.SectionReportModel.Label
	Private WithEvents riPageOf As GrapeCity.ActiveReports.SectionReportModel.ReportInfo
	Private WithEvents imgNW As SectionReportModel.Picture
	Private WithEvents imgLogo As SectionReportModel.Picture
End Class
