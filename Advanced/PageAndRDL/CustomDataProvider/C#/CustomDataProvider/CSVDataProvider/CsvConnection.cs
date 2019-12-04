using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;

namespace GrapeCity.ActiveReports.Samples.CustomDataProvider.CsvDataProvider
{
	/// <summary>
	/// Provides the implementation of <see cref="IDbConnection"/> for the .NET Framework CSV Data Provider.
	/// </summary>
	public sealed class CsvConnection : DbConnection
	{
		private readonly string _localizedName;

		/// <summary>
		/// Creates new instance of <see cref="CsvConnection"/> class.
		/// </summary>
		public CsvConnection()
		{
			_localizedName = "Csv";
		}

		/// <summary>
		/// Creates new instance of <see cref="CsvConnection"/> class.
		/// </summary>
		/// <param name="localizeName">The localized name for the <see cref="CsvConnection"/> instance.</param>
		public CsvConnection(string localizeName)
		{
			_localizedName = localizeName;
		}

		/// <summary>
		/// Gets or sets the string used to open the connection to the data source.
		/// </summary>
		/// <remarks>We don't use it for Csv Data Provider.</remarks>
		public override string ConnectionString
		{
			get { return string.Empty; }
			set { }
		}

		/// <summary>
		/// Gets the time to wait while trying to establish a connection before terminating the attempt and generating an error.
		/// </summary>
		/// <remarks>We don't use it for Csv Data Provider.</remarks>
		public override int ConnectionTimeout
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>
		/// Begins a data source transaction.
		/// </summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <remarks>We don't use it for Csv Data Provider.</remarks>
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			return null;
		}

		/// <summary>
		/// Opens a data source connection.
		/// </summary>
		/// <remarks>We don't use it for Csv Data Provider.</remarks>
		public override void Open()
		{
		}

		/// <summary>
		/// Closes the connection to the data source.
		/// </summary>
		public override void Close()
		{
			Dispose();
		}

		protected override DbCommand CreateDbCommand()
		{
			return new CsvCommand(string.Empty);
		}

		/// <summary>
		/// Releases the resources used by the <see cref="CsvConnection"/>.
		/// </summary>
		public new void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Gets the localized name of the <see cref="CsvConnection"/>.
		/// </summary>
		public string LocalizedName
		{
			get { return _localizedName; }
		}

		public override ConnectionState State
		{
			get { return ConnectionState.Open; }
		}

		public override string Database
		{
			get
			{
				return string.Empty;
			}
		}

		public override string DataSource
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override string ServerVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Specifies any configuration information for this extension.
		/// </summary>
		/// <param name="configurationSettings">A <see cref="NameValueCollection"/> of the settings.</param>
		public void SetConfiguration(NameValueCollection configurationSettings)
		{
		}

		public new IDbTransaction BeginTransaction(IsolationLevel il)
		{
			//do nothing
			return null;
		}

		public override void ChangeDatabase(string databaseName)
		{
			//do nothing
		}
	}
}
