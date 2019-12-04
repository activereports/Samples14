Imports System.IO
Imports System.Data.Common

''' <summary>
''' Provides the implementation of <see cref="IDbCommand"/> for the .NET Framework CSV Data Provider.
''' </summary>
Public NotInheritable Class CsvCommand
	Inherits DbCommand
	Private _commandText As String
	Private _connection As DbConnection
	Private _commandTimeout As Integer
	Private _commandType As CommandType

	''' <summary>
	''' Creates new instance of <see cref="CsvCommand"/> class.
	''' </summary>
	Public Sub New()
		Me.New(String.Empty)
	End Sub

	''' <summary>
	''' Creates new instance of <see cref="CsvCommand"/> class with the text of the command.
	''' </summary>
	''' <param name="commandText">The text of the command.</param>
	Public Sub New(commandText As String)
		Me.New(commandText, Nothing)
	End Sub

	''' <summary>
	''' Creates a new instance of <see cref="CsvCommand"/> class with the text of the command.
	''' </summary>
	''' <param name="commandText">The text of the command.</param>
	''' <param name="connection">A <see cref="CsvConnection"/> that represents the connection to a data source.</param>
	Public Sub New(commandText As String, connection As CsvConnection)
		_commandText = commandText
		_connection = connection
	End Sub

	''' <summary>
	''' Gets or sets the command to execute at the data source.
	''' </summary>
	Public Overrides Property CommandText() As String
		Get
			Return _commandText
		End Get
		Set
			_commandText = Value
		End Set
	End Property

	''' <summary>
	''' Gets or sets the wait time before terminating an attempt to execute a command and generating an error.
	''' </summary>
	Public Overrides Property CommandTimeout() As Integer
		Get
			Return _commandTimeout
		End Get
		Set
			_commandTimeout = Value
		End Set
	End Property

	''' <summary>
	''' Gets or sets a value indicating how the <see cref="CommandText"/> property is interpreted.
	''' </summary>
	''' <remarks>We don't use it for Csv Data Provider.</remarks> 
	Public Overrides Property CommandType() As CommandType
		Get
			Return _commandType
		End Get
		Set
			_commandType = Value
		End Set
	End Property

	''' <summary>
	''' Sends the <see cref="CommandText"/> to the <see cref="CsvConnection"/>, and builds an <see cref="CsvDataReader"/> using one of the <see cref="CommandBehavior"/> values.
	''' </summary>
	''' <param name="behavior">One of the <see cref="CommandBehavior"/> values.</param>
	''' <returns>A <see cref="CsvDataReader"/> object.</returns>
	Protected Overrides Function ExecuteDbDataReader(behavior As CommandBehavior) As DbDataReader
		Return New CsvDataReader(New StringReader(_commandText))
	End Function

	''' <summary>
	''' Returns the command text with the parameters expanded into constants.
	''' </summary>
	''' <returns>The string represents the command text with the parameters expanded into constants.</returns>
	Public Function GenerateRewrittenCommandText() As String
		Return _commandText
	End Function

	''' <summary>
	''' Sends the <see cref="CommandText"/> to the <see cref="CsvConnection"/> and builds an <see cref="CsvDataReader"/>.
	''' </summary>
	''' <returns>A <see cref="CsvDataReader"/> object.</returns>
	Public Shadows Function ExecuteReader() As IDataReader
		Return mybase.ExecuteReader(CommandBehavior.SchemaOnly)
	End Function

	Public Overrides Property UpdatedRowSource() As UpdateRowSource
		Get
			Return UpdateRowSource.None
		End Get
		Set
		End Set
	End Property

	Protected Overrides Property DbConnection As DbConnection
		Get
			Return _connection
		End Get
		Set(value As DbConnection)
			_connection = value
		End Set
	End Property

	Protected Overrides ReadOnly Property DbParameterCollection As DbParameterCollection
		Get
			Throw New NotImplementedException()
		End Get
	End Property

	Protected Overrides Property DbTransaction As DbTransaction
		Get
			Throw New NotImplementedException()
		End Get
		Set(value As DbTransaction)
			Throw New NotImplementedException()
		End Set
	End Property

	Public Overrides Property DesignTimeVisible As Boolean
		Get
			Throw New NotImplementedException()
		End Get
		Set(value As Boolean)
			Throw New NotImplementedException()
		End Set
	End Property

	Public Overrides Sub Cancel()
		'do nothing
	End Sub

	Public Shadows Function CreateParameter() As IDbDataParameter
		'do nothing
		Return Nothing
	End Function


	''' <summary>
	''' Releases the resources used by the <see cref="CsvCommand"/>.
	''' </summary>
	Public Shadows Sub Dispose()
		Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub

	Protected Shadows Sub Dispose(disposing As Boolean)
		If disposing Then
			If _connection IsNot Nothing Then
				_connection.Dispose()
				_connection = Nothing
			End If
		End If
	End Sub

	Public Overrides Sub Prepare()
		'do nothing
	End Sub

	Public Overrides Function ExecuteNonQuery() As Integer
		Return 0
	End Function

	Public Overrides Function ExecuteScalar() As Object
		'do nothing
		Return Nothing
	End Function

	Protected Overrides Function CreateDbParameter() As DbParameter
		Throw New NotImplementedException()
	End Function
End Class
