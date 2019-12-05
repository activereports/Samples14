Imports System.Net
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.Text

Friend Class DataLayer

	Private Shared ReadOnly _sources As String() = {
		"http://localhost:55856/Customers?$format=application/json;odata.metadata=none",
		"https://services.odata.org/V4/Northwind/Northwind.svc/Customers?$select=CustomerID,%20CompanyName,%20ContactName,%20Address%20&%20$format=application/json;odata.metadata=none"
	}

	Public Shared Function CreateData(service As Service) As String
		Dim source_url = _sources(service)
		Try
			Using webClient As New WebClient()
				webClient.Encoding = Encoding.UTF8
				Dim json = webClient.DownloadString(source_url)
				Dim jObject = CType(JsonConvert.DeserializeObject(json), JObject)
				For Each obj As KeyValuePair(Of String, JToken)
					In jObject
					If obj.Key = "value" Then
						Return "{" + obj.Key + ":" + obj.Value.ToString + "}"
					End If
				Next
				Return ""
			End Using
		Catch ex As Exception
			Throw ex
		End Try
	End Function
End Class
