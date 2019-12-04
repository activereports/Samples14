Imports GrapeCity.BI.Data.DataProviders
Imports System
Imports System.Globalization
Imports System.Linq

Namespace GrapeCity.ActiveReports.Samples.OracleDataProvider
	Public NotInheritable Class OracleConnectionAdapter
		Inherits DbConnectionAdapter

		Public Shared Instance As OracleConnectionAdapter = New OracleConnectionAdapter()

		Protected Overrides Function MultivalueParameterValueToString(ByVal parameterArrayValue As Object()) As String
			Return String.Join(",", parameterArrayValue.[Select](Function(parameterValue) "'" & Convert.ToString(parameterValue, CultureInfo.InvariantCulture) & "'"))
		End Function

		Protected Overrides Function CreateParameterNamePattern(ByVal name As String) As String
			If Not name.StartsWith(":") Then name = String.Format(":{0}", name)
			Return MyBase.CreateParameterNamePattern(name)
		End Function
	End Class
End Namespace
