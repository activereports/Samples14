Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Runtime.CompilerServices

Namespace GrapeCity.ActiveReports.Samples.OracleDataProvider
	Friend Module DataExtensions
		<Extension()>
		Iterator Function ReadAll(ByVal reader As System.Data.IDataReader) As IEnumerable(Of System.Data.IDataRecord)
			While reader.Read()
				Yield reader
			End While
		End Function

		<Extension()>
		Function GetString(ByVal record As System.Data.IDataRecord, ByVal name As String) As String
			Dim value = record.GetValue(record.GetOrdinal(name))
			Return Convert.ToString(value, CultureInfo.InvariantCulture)
		End Function

		<Extension()>
		Function GetInt32(ByVal record As System.Data.IDataRecord, ByVal name As String) As Integer
			Dim value = record.GetValue(record.GetOrdinal(name))
			Return Convert.ToInt32(value, CultureInfo.InvariantCulture)
		End Function

		<Extension()>
		Function GetBoolean(ByVal record As System.Data.IDataRecord, ByVal name As String) As Boolean
			Dim value = record.GetValue(record.GetOrdinal(name))
			Return Convert.ToBoolean(value, CultureInfo.InvariantCulture)
		End Function

		<Extension()>
		Function IsDBNull(ByVal record As System.Data.IDataRecord, ByVal name As String) As Boolean
			Dim value = record.GetValue(record.GetOrdinal(name))
			Return value = DBNull.Value
		End Function
	End Module
End Namespace
