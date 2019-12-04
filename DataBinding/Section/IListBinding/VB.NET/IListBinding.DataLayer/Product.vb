'<summary>
' Summary description for Product.
'</summary>
Public Class Product
	Private _productID As Integer
	Private _productName As String
	Private _supplierID As Integer
	Private _categoryID As Integer
	Private _quantityPerUnit As String
	Private _unitPrice As Decimal
	Private _unitsInStock As Integer
	Private _unitsOnOrder As Integer
	Private _reorderLevel As Integer
	Private _discontinued As Boolean

	Friend Sub New()
	End Sub 'New

	'******
	'* The properties listed below define a product in the datalayer. 
	'******

	Public Property ProductID() As Integer
		Get
			Return _productID
		End Get
		Set(ByVal Value As Integer)
			_productID = Value
		End Set
	End Property

	Public Property ProductName() As String
		Get
			Return _productName
		End Get
		Set(ByVal Value As String)
			_productName = Value
		End Set
	End Property

	Public Property SupplierID() As Integer
		Get
			Return _supplierID
		End Get
		Set(ByVal Value As Integer)
			_supplierID = Value
		End Set
	End Property

	Public Property CategoryID() As Integer
		Get
			Return _categoryID
		End Get
		Set(ByVal Value As Integer)
			_categoryID = Value
		End Set
	End Property

	Public Property QuantityPerUnit() As String
		Get
			Return _quantityPerUnit
		End Get
		Set(ByVal Value As String)
			_quantityPerUnit = Value
		End Set
	End Property

	Public Property UnitPrice() As Decimal
		Get
			Return _unitPrice
		End Get
		Set(ByVal Value As Decimal)
			_unitPrice = Value
		End Set
	End Property

	Public Property UnitsInStock() As Integer
		Get
			Return _unitsInStock
		End Get
		Set(ByVal Value As Integer)
			_unitsInStock = Value
		End Set
	End Property

	Public Property UnitsOnOrder() As Integer
		Get
			Return _unitsOnOrder
		End Get
		Set(ByVal Value As Integer)
			_unitsOnOrder = Value
		End Set
	End Property

	Public Property ReorderLevel() As Integer
		Get
			Return _reorderLevel
		End Get
		Set(ByVal Value As Integer)
			_reorderLevel = Value
		End Set
	End Property

	Public Property Discontinued() As Boolean
		Get
			Return _discontinued
		End Get
		Set(ByVal Value As Boolean)
			_discontinued = Value
		End Set
	End Property

End Class 'Product
