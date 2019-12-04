using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using ODataDataSource.Models;

namespace ODataDataSource.Controllers
{
	/// <summary>
	/// Controller is based on article https://docs.microsoft.com/en-us/aspnet/web-api/overview/odata-support-in-aspnet-web-api/odata-v4/create-an-odata-v4-endpoint
	/// </summary>
	[EnableQuery]
	public class CustomersController : ODataController
	{
		public IHttpActionResult Get()
		{
			var connStr = Utility.UpdateConnectionString(Properties.Resource.Nwind);
			var conn = new OleDbConnection(connStr);
			conn.Open();
			var customers = new Collection<Customer>();
			var cmd = new OleDbCommand("select customers.Customerid, customers.CompanyName, customers.ContactName, customers.Address from customers", conn);
			var dataReader = cmd.ExecuteReader();
			while (dataReader.Read())
			{
				customers.Add(new Customer()
				{
					CustomerID = dataReader.GetValue(0).ToString(),
					CompanyName = dataReader.GetValue(1).ToString(),
					ContactName = dataReader.GetValue(2).ToString(),
					Address = dataReader.GetValue(3).ToString(),
				});
			}
			conn.Close();
			return Ok(customers.AsQueryable());
		}
	}
}
