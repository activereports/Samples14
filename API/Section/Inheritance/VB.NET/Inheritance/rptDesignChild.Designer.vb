Imports System.Resources
Imports GrapeCity.ActiveReports.SectionReportModel

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptDesignChild
	Inherits GrapeCity.ActiveReports.Sample.Inheritance.rptDesignBase
	Private _resource As New ResourceManager(GetType(rptDesignChild))

	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
		End If
		MyBase.Dispose(disposing)
	End Sub

#Region "ActiveReports Designer generated code"
	Private WithEvents PageHeader As GrapeCity.ActiveReports.SectionReportModel.PageHeader
	Private WithEvents Detail As GrapeCity.ActiveReports.SectionReportModel.Detail
	Private WithEvents PageFooter As GrapeCity.ActiveReports.SectionReportModel.PageFooter
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptDesignChild))
		Dim OleDBDataSource1 As GrapeCity.ActiveReports.Data.OleDBDataSource = New GrapeCity.ActiveReports.Data.OleDBDataSource()
		Me.txtCustomerID1 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
		Me.txtCompanyName1 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
		Me.txtContactName1 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
		Me.txtCountry1 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
		Me.txtAddress1 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
		Me.Detail = New GrapeCity.ActiveReports.SectionReportModel.Detail()
		CType(Me.txtCustomerID1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txtCompanyName1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txtContactName1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txtCountry1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txtAddress1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
		'
		'txtCustomerID1
		'
		Me.txtCustomerID1.CanShrink = True
		resources.ApplyResources(Me.txtCustomerID1, "txtCustomerID1")
		Me.txtCustomerID1.DataField = "CustomerID"
		Me.txtCustomerID1.Name = "txtCustomerID1"
		'
		'txtCompanyName1
		'
		Me.txtCompanyName1.CanShrink = True
		resources.ApplyResources(Me.txtCompanyName1, "txtCompanyName1")
		Me.txtCompanyName1.DataField = "CompanyName"
		Me.txtCompanyName1.Name = "txtCompanyName1"
		'
		'txtContactName1
		'
		Me.txtContactName1.CanShrink = True
		resources.ApplyResources(Me.txtContactName1, "txtContactName1")
		Me.txtContactName1.DataField = "ContactName"
		Me.txtContactName1.Name = "txtContactName1"
		'
		'txtCountry1
		'
		Me.txtCountry1.CanShrink = True
		resources.ApplyResources(Me.txtCountry1, "txtCountry1")
		Me.txtCountry1.DataField = "Country"
		Me.txtCountry1.Name = "txtCountry1"
		'
		'txtAddress1
		'
		Me.txtAddress1.CanShrink = True
		resources.ApplyResources(Me.txtAddress1, "txtAddress1")
		Me.txtAddress1.DataField = "Address"
		Me.txtAddress1.Name = "txtAddress1"
		'
		'Detail
		'
		Me.Detail.CanShrink = True
		Me.Detail.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txtCustomerID1, Me.txtCompanyName1, Me.txtContactName1, Me.txtCountry1, Me.txtAddress1})
		Me.Detail.Height = 0.375!
		Me.Detail.KeepTogether = True
		Me.Detail.Name = "Detail"
		'
		'rptDesignChild
		'
		resources.ApplyResources(Me, "$this")
		OleDBDataSource1.ConnectionString = _resource.GetString("ConnectionString")
		OleDBDataSource1.SQL = "Select * from Customers Order By Val(CustomerID)"
		Me.DataSource = OleDBDataSource1
		Me.MasterReport = False
		Me.PageSettings.PaperHeight = 11.0!
		Me.PageSettings.PaperWidth = 8.5!
		Me.Sections.Add(Me.Detail)
		Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-style: normal; text-decoration: none; font-weight: normal; font-size: 10pt; " & _
					"color: Black; font-family: Arial; ddo-char-set: 186", "Normal"))
		CType(Me.txtCustomerID1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txtCompanyName1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txtContactName1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txtCountry1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txtAddress1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

	End Sub
#End Region
	Friend WithEvents txtCustomerID1 As TextBox
	Friend WithEvents txtCompanyName1 As TextBox
	Friend WithEvents txtContactName1 As TextBox
	Friend WithEvents txtCountry1 As TextBox
	Friend WithEvents txtAddress1 As TextBox
End Class
