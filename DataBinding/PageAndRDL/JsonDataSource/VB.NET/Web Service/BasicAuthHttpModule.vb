Imports Microsoft.VisualBasic
Imports System
Imports System.Security.Principal
Imports System.Text
Imports System.Threading
Imports System.Web



Public Class BasicAuthHttpModule : Implements IHttpModule

	Private Const Realm As String = "My Realm"

	Public Sub Init(ByVal context As HttpApplication) Implements IHttpModule.Init
		'Register event handlers
		AddHandler context.AuthenticateRequest, AddressOf OnApplicationAuthenticateRequest
		AddHandler context.EndRequest, AddressOf OnApplicationEndRequest
	End Sub

	Private Shared Sub SetPrincipal(principal As IPrincipal)
		Thread.CurrentPrincipal = principal
		If Not IsNothing(HttpContext.Current) Then
			HttpContext.Current.User = principal
		End If
	End Sub

	' TODO: Here is where you would validate the username and password.
	Private Shared Function CheckPassword(username As String, password As String) As Boolean
		Return username = "admin" And password = "1"
	End Function

	Private Shared Sub AuthenticateUser(credentials As String)
		Try
			Dim encoding As Encoding = encoding.GetEncoding("iso-8859-1")
			credentials = encoding.GetString(Convert.FromBase64String(credentials))

			Dim separator As Integer = credentials.IndexOf(":")
			Dim name As String = credentials.Substring(0, separator)
			Dim password As String = credentials.Substring(separator + 1)

			If (CheckPassword(name, password)) Then
				Dim identity = New GenericIdentity(name)
				SetPrincipal(New GenericPrincipal(identity, Nothing))
			Else
				' Invalid username or password.
				HttpContext.Current.Response.StatusCode = 403
			End If
		Catch ex As Exception
			' Credentials were not formatted correctly.
			HttpContext.Current.Response.StatusCode = 401
		End Try
	End Sub
	' http://cacheandquery.com/blog/2011/03/customizing-asp-net-mvc-basic-authentication/
	Private Sub OnApplicationAuthenticateRequest(sender As Object, e As EventArgs)
		Dim request = HttpContext.Current.Request
		Dim info As System.IO.FileInfo = New System.IO.FileInfo(request.Url.AbsolutePath)
		If not info.Name.Equals("GetJson") Then Return

		Dim authHeader As String = request.Headers("Authorization")
		' RFC 2617 sec 1.2, "scheme" name is case-insensitive
		If ((Not (authHeader) Is Nothing) _
			AndAlso (authHeader.StartsWith("basic ", StringComparison.OrdinalIgnoreCase) _
			AndAlso (authHeader.Length > 6))) Then
			AuthenticateUser(authHeader.Substring(6))
		Else
			HttpContext.Current.Response.StatusCode = 401
		End If
	End Sub

	' If the request was unauthorized, add the WWW-Authenticate header 
	' to the response.
	Private Sub OnApplicationEndRequest(sender As Object, e As EventArgs)
		Dim response = HttpContext.Current.Response
		If (response.StatusCode = 401) Then
			response.Headers.Add("WWW-Authenticate", String.Format("Basic realm=""{0}""", Realm))
		End If
	End Sub

	Public Sub Dispose() Implements IHttpModule.Dispose
	End Sub

End Class
