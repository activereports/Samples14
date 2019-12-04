Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Namespace MetaData
	Public Class FieldMetaData

		Private _title As String
		Public Sub New()
			Name = ""
			Title = ""
			IsNumeric = False
			AllowTotaling = False
			FormatString = ""
			PreferredWidth = "1cm"
			SummaryFunction = ""
		End Sub

		Public Name As String

		'property for data binding purposes
		Public Property Title() As String
			Get
				Return _title
			End Get
			Set(ByVal value As String)
				_title = value
			End Set
		End Property
		Public IsNumeric As Boolean
		Public AllowTotaling As Boolean
		Public FormatString As String
		Public PreferredWidth As String
		Public SummaryFunction As String
	End Class
End Namespace
