using GrapeCity.ActiveReports.Samples.CustomDataProvider.CsvDataProvider;
using System.Data.Common;

namespace GrapeCity.ActiveReports.Samples.CustomDataProvider.CSVDataProvider
{
	/// <summary>
	/// Implements <see cref="DataProviderFactory"/> for .NET Framework CSV Data Provider.
	/// </summary>
	public class CsvDataProviderFactory : DbProviderFactory
	{
		/// <summary>
		/// Returns a new instance of the <see cref="CsvCommand"/>.
		/// </summary>
		public override DbCommand CreateCommand()
		{
			return new CsvCommand();
		}

		/// <summary>
		/// Returns a new instance of the <see cref="CsvConnection"/>.
		/// </summary>
		public override DbConnection CreateConnection()
		{
			return new CsvConnection();
		}
	}
}
