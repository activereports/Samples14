Public Module Utility
	Public Function UpdateConnectionString(connectionString As String) As String
		Return connectionString.Replace("$appPath$", HttpContext.Current.Server.MapPath("~"))
	End Function
End Module
