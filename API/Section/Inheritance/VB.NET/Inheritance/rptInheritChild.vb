Public Class rptInheritChild
	Inherits GrapeCity.ActiveReports.Sample.Inheritance.rptInheritBase
	
	Public Sub New()

		' This call is required by the ActiveReports designer.
		InitializeComponent()

		'Add any initialization after the call () InitializeComponent.
		AddHandler DataInitialize, AddressOf BaseReport_DataInitialize

		CsvPath = "..\..\Customers.csv"

	End Sub

#Region "ActiveReports Designer generated code"

	'ActiveReport overrides dispose to clean up for the list of components.
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
		End If
		MyBase.Dispose(disposing)
	End Sub

	'Note: The following procedure is required by the ActiveReport designer.
	'ActiveReport can be changed by using the designer
	'Please do not modify it using the code editor.

#End Region
  
End Class
