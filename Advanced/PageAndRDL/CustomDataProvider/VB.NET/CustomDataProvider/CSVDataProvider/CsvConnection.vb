Imports System.Data.Common
Imports System.Data
Imports System.Collections.Specialized
Imports System


''' <summary>
''' Provides the implementation of <see cref="IDbConnection"/> for the .NET Framework CSV Data Provider.
''' </summary>
Public NotInheritable Class CsvConnection
	Inherits DbConnection
	Private ReadOnly _localizedName As String

	''' <summary>
	''' Creates new instance of <see cref="CsvConnection"/> class.
	''' </summary>
	Public Sub New()
		_localizedName = "Csv"
	End Sub

	''' <summary>
	''' Creates new instance of <see cref="CsvConnection"/> class.
	''' </summary>
	''' <param name="localizeName">The localized name for the <see cref="CsvConnection"/> instance.</param>
	Public Sub New(localizeName As String)
		_localizedName = localizeName
	End Sub

	''' <summary>
	''' Gets or sets the string used to open the connection to the data source.
	''' </summary>
	''' <remarks>We don't use it for Csv Data Provider.</remarks>
	Public Overrides Property ConnectionString() As String
		Get
			Return String.Empty
		End Get
		Set
		End Set
	End Property

	''' <summary>
	''' Gets the time to wait while trying to establish a connection before terminating the attempt and generating an error.
	''' </summary>
	''' <remarks>We don't use it for Csv Data Provider.</remarks>
	Public Overrides ReadOnly Property ConnectionTimeout() As Integer
		Get
			Throw New NotImplementedException()
		End Get
	End Property

	''' <summary>
	''' Begins a data source transaction.
	''' </summary>
	''' <returns>An object representing the new transaction.</returns>
	''' <remarks>We don't use it for Csv Data Provider.</remarks>
	Protected Overrides Function BeginDbTransaction(isolationLevel As IsolationLevel) As DbTransaction
		Return Nothing
	End Function

	''' <summary>
	''' Opens a data source connection.
	''' </summary>
	''' <remarks>We don't use it for Csv Data Provider.</remarks>
	Public Overrides Sub Open()
	End Sub

	''' <summary>
	''' Closes the connection to the data source.
	''' </summary>
	Public Overrides Sub Close()
		Dispose()
	End Sub

	Protected Overrides Function CreateDbCommand() As DbCommand
		Return New CsvCommand(String.Empty)
	End Function

	''' <summary>
	''' Releases the resources used by the <see cref="CsvConnection"/>.
	''' </summary>
	Public Shadows Sub Dispose()
		MyBase.Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub

	''' <summary>
	''' Gets the localized name of the <see cref="CsvConnection"/>.
	''' </summary>
	Public ReadOnly Property LocalizedName() As String
		Get
			Return _localizedName
		End Get
	End Property

	Public Overrides ReadOnly Property State() As ConnectionState
		Get
			Return ConnectionState.Open
		End Get
	End Property

	Public Overrides ReadOnly Property Database() As String
		Get
			Return String.Empty
		End Get
	End Property

	Public Overrides ReadOnly Property DataSource() As String
		Get
			Throw New NotImplementedException()
		End Get
	End Property

	Public Overrides ReadOnly Property ServerVersion() As String
		Get
			Throw New NotImplementedException()
		End Get
	End Property

	''' <summary>
	''' Specifies any configuration information for this extension.
	''' </summary>
	''' <param name="configurationSettings">A <see cref="NameValueCollection"/> of the settings.</param>
	Public Sub SetConfiguration(configurationSettings As NameValueCollection)
	End Sub

	Public Shadows Function BeginTransaction(il As IsolationLevel) As IDbTransaction
		'do nothing
		Return Nothing
	End Function

	Public Overrides Sub ChangeDatabase(databaseName As String)
		'do nothing
	End Sub
End Class
