using System.Data.OleDb;

namespace GrapeCity.ActiveReports.Samples.IListBinding.DataLayer
{
	internal class DataProvider
	{
		/// <summary>
		/// Returns a new connection object for reading the data in the ProductCollection
		/// </summary>
		internal static OleDbConnection NewConnection
		{
			get
			{
				return new OleDbConnection(GrapeCity.ActiveReports.IListBinding.DataLayer.Properties.Resources.ConnectionString);
			}
		}
	}
}
