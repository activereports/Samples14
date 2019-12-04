
namespace GrapeCity.ActiveReports.Samples.IListBinding.DataLayer
{
	/// <summary>
	/// Summary description for Product.
	/// </summary>
	public class Product
	{
		int _productID;
		string _productName;
		int _supplierID;
		int _categoryID;
		string _quantityPerUnit;
		decimal _unitPrice;
		int _unitsInStock;
		int _unitsOnOrder;
		int _reorderLevel;
		bool _discontinued;

		internal Product()
		{
		}

		//******
		//The properties listed below define a product in the datalayer. 
		//******

		public int ProductID
		{
			get
			{
				return _productID;
			}
			set
			{
				_productID = value;
			}
		}

		public string ProductName
		{
			get
			{
				return _productName;
			}
			set
			{
				_productName = value;
			}
		}

		public int SupplierID
		{
			get
			{
				return _supplierID;
			}
			set
			{
				_supplierID = value;
			}
		}

		public int CategoryID
		{
			get
			{
				return _categoryID;
			}
			set
			{
				_categoryID = value;
			}
		}


		public string QuantityPerUnit
		{
			get
			{
				return _quantityPerUnit;
			}
			set
			{
				_quantityPerUnit = value;
			}
		}


		public decimal UnitPrice
		{
			get
			{
				return _unitPrice;
			}
			set
			{
				_unitPrice = value;
			}
		}

		public int UnitsInStock
		{
			get
			{
				return _unitsInStock;
			}
			set
			{
				_unitsInStock = value;
			}
		}

		public int UnitsOnOrder
		{
			get
			{
				return _unitsOnOrder;
			}
			set
			{
				_unitsOnOrder = value;
			}
		}

		public int ReorderLevel
		{
			get
			{
				return _reorderLevel;
			}
			set
			{
				_reorderLevel = value;
			}
		}

		public bool Discontinued
		{
			get
			{
				return _discontinued;
			}
			set
			{
				_discontinued = value;
			}
		}
	}
}
