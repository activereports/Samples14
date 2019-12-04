Imports System.Data.Common

''' <summary>
''' Implements <see cref="DataProviderFactory"/> for .NET Framework CSV Data Provider.
''' </summary>
Public Class CsvDataProviderFactory
	Inherits DbProviderFactory

	''' <summary>
	''' Returns a new instance of the <see cref="CsvCommand"/>.
	''' </summary>
	Public Overrides Function CreateCommand() As DbCommand
		Return New CsvCommand()
	End Function

	''' <summary>
	''' Returns a new instance of the <see cref="CsvConnection"/>.
	''' </summary>
	Public Overrides Function CreateConnection() As DbConnection
		Return New CsvConnection()
	End Function
End Class
