using System;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Data.OleDb;

namespace GrapeCity.ActiveReports.Samples.IListBinding.DataLayer
{
	public class ProductCollection : CollectionBase, IComponent
	{
		private ISite _site;  //Required for the IComponent implementation
		public ProductCollection()
		{
			FillWithAllProducts();
			_site = null;
		}
	
		public void FillWithAllProducts()
		{
			// Reset current list
			List.Clear();
			
			using (OleDbConnection cn = DataProvider.NewConnection)
			{
				cn.Open();
				OleDbCommand cmd = new OleDbCommand("SELECT * FROM Products", cn);
				IDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					List.Add(ProductFromDataReader(reader));
				}
			}
		}

		/// <summary>
		/// ProductFromDataReader: Used to create a new product from the DB for the List.
		/// </summary>
		/// <param name="reader">A valid datareader containing data.</param>
		/// <returns>Newly created Product object from the data row.</returns>
		internal static Product ProductFromDataReader(IDataReader reader)
		{
			Product p = new Product();
			p.CategoryID = Convert.ToInt32(reader["CategoryID"]);
			p.Discontinued = Convert.ToBoolean(reader["Discontinued"]);
			p.ProductID  = Convert.ToInt32(reader["ProductID"]);
			p.ProductName  = Convert.ToString(reader["ProductName"]);
			p.QuantityPerUnit = Convert.ToString(reader["QuantityPerUnit"]);
			p.ReorderLevel = Convert.ToInt32(reader["ReorderLevel"]);
			p.SupplierID = Convert.ToInt32(reader["SupplierID"]);
			p.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
			p.UnitsInStock = Convert.ToInt32(reader["UnitsInStock"]);
			p.UnitsOnOrder = Convert.ToInt32(reader["UnitsOnOrder"]);
			return p;
		}


		public event EventHandler Disposed;

		public ISite Site
		{
			get
			{
				return _site;
			}
			set
			{
				_site = value;
			}
		}

		/// <summary>
		/// Required for IComponent implementation
		/// </summary>
		public void Dispose()
		{
			OnDisposed(EventArgs.Empty);
		}

		/// <summary>
		/// Required for IComponent implementation
		/// </summary>
		protected virtual void OnDisposed(EventArgs e)
		{
			if (Disposed != null)
				Disposed(this, e);
		}
	}
}
