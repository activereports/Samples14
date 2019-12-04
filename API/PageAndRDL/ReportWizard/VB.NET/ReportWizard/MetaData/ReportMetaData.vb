Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Namespace MetaData
	Public Class ReportMetaData
		Private _title As String
		Public Sub New()
			Title = ""
			MasterReportFile = ""
			Fields = New Dictionary(Of String, FieldMetaData)()
		End Sub
		' property needed for data binding purposes
		Public Property Title() As String
			Get
				Return _title
			End Get
			Set(ByVal value As String)
				_title = value
			End Set
		End Property

		Public MasterReportFile As String
		Public ReadOnly Fields As Dictionary(Of String, FieldMetaData)
	End Class
End Namespace
