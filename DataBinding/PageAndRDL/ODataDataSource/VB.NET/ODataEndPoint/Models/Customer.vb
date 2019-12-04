Imports System.ComponentModel.DataAnnotations

Public Class Customer
	<Key>
	Public Property CustomerID() As String
	Public Property CompanyName() As String
	Public Property ContactName() As String
	Public Property Address() As String
End Class
