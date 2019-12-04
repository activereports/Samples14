Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web.Script.Serialization

' Provides the data used in the sample.
Friend NotInheritable Class DataLayer

	Public Function CreateData() As String


		Dim source_url As String = "http://localhost:10395/WebService.asmx/GetJson"
		Dim responseText As String = Nothing

		Using webClient As WebClient = New WebClient()

			webClient.Headers(HttpRequestHeader.Authorization) = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("admin:1")) ' username:password 
			webClient.Headers(HttpRequestHeader.ContentType) = "application/json;"
			webClient.Encoding = Encoding.UTF8

			Dim responseJson As String = webClient.DownloadString(source_url)
			Dim values As Dictionary(Of String, String) = New JavaScriptSerializer().Deserialize(Of Dictionary(Of String, String))(responseJson)

			If (values.ContainsKey("d")) Then
				responseText = values("d")
			End If
		End Using

		Return responseText
	End Function

End Class
