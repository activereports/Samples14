using System;
using System.Data;
using System.Data.Common;
using System.IO;

namespace GrapeCity.ActiveReports.Samples.CustomDataProvider.CsvDataProvider
{
	/// <summary>
	/// Provides the implementation of <see cref="IDbCommand"/> for the .NET Framework CSV Data Provider.
	/// </summary>
	public sealed class CsvCommand : DbCommand
	{
		private string _commandText;
		private DbConnection _connection;
		private int _commandTimeout;
		private CommandType _commandType;

		/// <summary>
		/// Creates new instance of <see cref="CsvCommand"/> class.
		/// </summary>
		public CsvCommand()
			: this(string.Empty)
		{
		}

		/// <summary>
		/// Creates new instance of <see cref="CsvCommand"/> class with the text of the command.
		/// </summary>
		/// <param name="commandText">The text of the command.</param>
		public CsvCommand(string commandText)
			: this(commandText, null)
		{
		}

		/// <summary>
		/// Creates a new instance of <see cref="CsvCommand"/> class with the text of the command.
		/// </summary>
		/// <param name="commandText">The text of the command.</param>
		/// <param name="connection">A <see cref="CsvConnection"/> that represents the connection to a data source.</param>
		public CsvCommand(string commandText, CsvConnection connection)
		{
			_commandText = commandText;
			_connection = connection;
		}

		/// <summary>
		/// Gets or sets the command to execute at the data source.
		/// </summary>
		public override string CommandText
		{
			get { return _commandText; }
			set { _commandText = value; }
		}

		/// <summary>
		/// Gets or sets the wait time before terminating an attempt to execute a command and generating an error.
		/// </summary>
		public override int CommandTimeout
		{
			get { return _commandTimeout; }
			set { _commandTimeout = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating how the <see cref="CommandText"/> property is interpreted.
		/// </summary>
		/// <remarks>We don't use it for Csv Data Provider.</remarks> 
		public override CommandType CommandType
		{
			get { return _commandType; }
			set { _commandType = value; }
		}

		/// <summary>
		/// Sends the <see cref="CommandText"/> to the <see cref="CsvConnection"/>, and builds an <see cref="CsvDataReader"/> using one of the <see cref="CommandBehavior"/> values.
		/// </summary>
		/// <param name="behavior">One of the <see cref="CommandBehavior"/> values.</param>
		/// <returns>A <see cref="CsvDataReader"/> object.</returns>
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return new CsvDataReader(new StringReader(_commandText));
		}

		/// <summary>
		/// Returns the command text with the parameters expanded into constants.
		/// </summary>
		/// <returns>The string represents the command text with the parameters expanded into constants.</returns>
		public string GenerateRewrittenCommandText()
		{
			return _commandText;
		}

		/// <summary>
		/// Sends the <see cref="CommandText"/> to the <see cref="CsvConnection"/> and builds an <see cref="CsvDataReader"/>.
		/// </summary>
		/// <returns>A <see cref="CsvDataReader"/> object.</returns>
		public new IDataReader ExecuteReader()
		{
			return ExecuteReader(CommandBehavior.SchemaOnly);
		}

		public override UpdateRowSource UpdatedRowSource
		{
			get { return UpdateRowSource.None; }
			set { }
		}

		protected override DbConnection DbConnection
		{
			get { return _connection; }
			set { _connection = value; }
		}

		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		protected override DbTransaction DbTransaction
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override bool DesignTimeVisible
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override void Cancel()
		{
			//do nothing
		}

		public new IDbDataParameter CreateParameter()
		{
			//do nothing
			return null;
		}


		/// <summary>
		/// Releases the resources used by the <see cref="CsvCommand"/>.
		/// </summary>
		public new void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_connection != null)
				{
					_connection.Dispose();
					_connection = null;
				}
			}
		}

		public override void Prepare()
		{
			//do nothing
		}

		public override int ExecuteNonQuery()
		{
			return 0;
		}

		public override object ExecuteScalar()
		{
			//do nothing
			return null;
		}

		protected override DbParameter CreateDbParameter()
		{
			throw new NotImplementedException();
		}
	}
}
