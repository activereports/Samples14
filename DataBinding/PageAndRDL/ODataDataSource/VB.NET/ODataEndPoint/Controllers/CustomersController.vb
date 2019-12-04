Imports System.Collections.ObjectModel
Imports System.Data.OleDb
Imports System.Web.Http
Imports System.Web.OData
''' <summary>
''' Controller is based on article https://docs.microsoft.com/en-us/aspnet/web-api/overview/odata-support-in-aspnet-web-api/odata-v4/create-an-odata-v4-endpoint
''' </summary>
<EnableQuery>
Public Class CustomersController
	Inherits ODataController
	Public Function [Get]() As IHttpActionResult
		Dim connStr = UpdateConnectionString(My.Resources.Nwind)
		Dim conn As New OleDbConnection(connStr)
		conn.Open()
		Dim customers As New Collection(Of Customer)()
		Dim cmd As New OleDbCommand("select customers.CustomerID, customers.CompanyName, customers.ContactName, customers.Address from customers", conn)
		Dim dataReader As OleDbDataReader = cmd.ExecuteReader()
		While dataReader.Read()
			customers.Add(New Customer() With {
				.CustomerID = dataReader.GetValue(0).ToString(),
				.CompanyName = dataReader.GetValue(1).ToString(),
				.ContactName = dataReader.GetValue(2).ToString(),
				.Address = dataReader.GetValue(3).ToString()
			})
		End While
		conn.Close()
		Return Ok(customers.AsQueryable())
	End Function
End Class
