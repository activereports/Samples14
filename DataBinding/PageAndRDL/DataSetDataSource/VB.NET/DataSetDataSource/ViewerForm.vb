Imports System.Data
Imports System.IO

Public Class ViewerForm

	' Fill DataSet with Data
	Private Function LoadData() As DataTable
		Dim InvoiceTable As New DataTable("Invoice")

		InvoiceTable.Columns.Add("CustomerID", GetType(String))
		InvoiceTable.Columns.Add("CompanyName", GetType(String))
		InvoiceTable.Columns.Add("Address", GetType(String))
		InvoiceTable.Columns.Add("PostalCode", GetType(Integer))
		InvoiceTable.Columns.Add("ProductID", GetType(Integer))
		InvoiceTable.Columns.Add("ProductName", GetType(String))
		InvoiceTable.Columns.Add("QuantityPerUnit", GetType(Integer))
		InvoiceTable.Columns.Add("Price", GetType(Integer))
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 89, "Biscuit", 1, 10)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 12, "Coffee", 5, 20)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 567, "Meat", 8, 43)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 687, "Sushi", 12, 23)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 987, "Eggs", 23, 13)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 981, "Flakes", 1, 10)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 982, "Card", 2, 10)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 112, "Pins", 10, 1)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 129, "FootBall", 2, 17)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 123, "Matchstick", 22, 12)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 127, "Lighter", 1, 10)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 221, "Wine", 1, 130)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 132, "Apples", 10, 130)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 133, "Energy Drink", 2, 130)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 332, "Mapple Syrup", 8, 11)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 126, "Box Set", 2, 90)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 11, "DVD", 1, 13)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 112, "CD", 2, 12)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 34, "MP3 Player", 1, 1300)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 134, "Needle", 2, 89)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 456, "SoftDrink", 3, 23)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 83, "Jam", 1, 34)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 100, "Olives", 1, 78)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 189, "Oil", 3, 68)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 102, "Grapes", 3, 56)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 103, "Chair", 1, 900)
		InvoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 107, "Bottle", 1, 67)

		Return InvoiceTable
	End Function

	' Loads and shows the report.
	Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
		Dim rptPath As New FileInfo("..\..\Invoice2.rdlx")
		Dim definition As New PageReport(rptPath)
		AddHandler definition.Document.LocateDataSource, AddressOf OnLocateDataSource

		arvMain.LoadDocument(definition.Document)
	End Sub

	' To connect to unbound data sources at run time,  the DataSet provider can be used with the LocateDataSource event. The reporting engine raises the LocateDataSource event when it needs input on the data to use.
	Private Sub OnLocateDataSource(ByVal sender As Object, ByVal args As LocateDataSourceEventArgs)
		args.Data = LoadData()
	End Sub

End Class
