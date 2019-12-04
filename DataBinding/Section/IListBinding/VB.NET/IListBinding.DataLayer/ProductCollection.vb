Imports System
Imports System.ComponentModel
Imports System.Collections
Imports System.Data
Imports System.Data.OleDb

Public Class ProductCollection
	Inherits CollectionBase
	Implements IComponent

	Private _site As ISite

	Public Sub New()
		FillWithAllProducts()
		_site = Nothing
	End Sub 'New

	'<summary>
	' FillWithAllProducts - Used to populate the List with all the products from a DB
	'</summary>
	Public Sub FillWithAllProducts()
		'reset current list
		List.Clear()

		'open connection and read the data
		Dim cn As OleDbConnection = DataProvider.NewConnection
		Try
			cn.Open()
			Dim cmd As New OleDbCommand("SELECT * FROM Products", cn)
			Dim reader As IDataReader = cmd.ExecuteReader()

			While reader.Read()
				List.Add(ProductFromDataReader(reader))
			End While
		Finally
			cn.Dispose()
		End Try
	End Sub

	'<summary>
	' ProductFromDataReader - used to create a new product from the DB for the List
	'</summary>
	'<param name="reader">The valid datareader containing the data</param>
	'<returns>Newly created Product object from the data row</returns>
	Friend Shared Function ProductFromDataReader(ByVal reader As IDataReader) As Product
		Dim p As New Product
		p.CategoryID = Convert.ToInt32(reader("CategoryID"))
		p.Discontinued = Convert.ToBoolean(reader("Discontinued"))
		p.ProductID = Convert.ToInt32(reader("ProductID"))
		p.ProductName = Convert.ToString(reader("ProductName"))
		p.QuantityPerUnit = Convert.ToString(reader("QuantityPerUnit"))
		p.ReorderLevel = Convert.ToInt32(reader("ReorderLevel"))
		p.SupplierID = Convert.ToInt32(reader("SupplierID"))
		p.UnitPrice = Convert.ToDecimal(reader("UnitPrice"))
		p.UnitsInStock = Convert.ToInt32(reader("UnitsInStock"))
		p.UnitsOnOrder = Convert.ToInt32(reader("UnitsOnOrder"))
		Return p
	End Function 'ProductFromDataReader

	Public Event Disposed As EventHandler Implements IComponent.Disposed

	'Required for IComponent implementation
	Public Sub Dispose() Implements IComponent.Dispose
		'There is nothing to clean.
		RaiseEvent Disposed(Me, EventArgs.Empty)
	End Sub

	'Required for IComponent implementation
	Public Property Site() As ISite Implements IComponent.Site
		Get
			Return _site
		End Get
		Set(ByVal Value As ISite)
			_site = Value
		End Set
	End Property

End Class 'ProductCollection
