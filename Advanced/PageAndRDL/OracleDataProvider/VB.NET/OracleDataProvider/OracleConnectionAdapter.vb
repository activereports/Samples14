Imports GrapeCity.BI.Data.DataProviders
Imports Oracle.ManagedDataAccess.Client
Imports System.Data.Common
Imports System.Globalization

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

		Public Overrides Sub SetParameters(queryParameters As IEnumerable(Of DbCommandParameter), command As DbCommand)
			If TypeOf command Is OracleCommand Then
				Dim oracleCommand As OracleCommand = command
				oracleCommand.BindByName = True
			End If

			MyBase.SetParameters(queryParameters, command)
		End Sub
	End Class
End Namespace
