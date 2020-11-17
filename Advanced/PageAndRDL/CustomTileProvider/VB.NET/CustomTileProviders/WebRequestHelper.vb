Imports System.IO
Imports System.Net
Imports System.Runtime.CompilerServices

Namespace GrapeCity.ActiveReports.Samples.CustomTileProviders

	Module StringExtensions
		<Extension()> _
		Public Sub CopyTo(ByVal input As Stream, ByVal output As Stream)
			Dim buffer = New Byte(16 * 1024 - 1) {}
			Dim read As Integer
			While (InlineAssignHelper(read, input.Read(buffer, 0, buffer.Length))) > 0
				output.Write(buffer, 0, read)
			End While
		End Sub

		Private Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
			target = value
			Return value
		End Function
	End Module
	Friend NotInheritable Class WebRequestHelper
		Private Sub New()
		End Sub
		''' <summary>
		''' Load raw data into MemoryStream from specified Url.
		''' </summary>
		''' <param name="url">Data source Url</param>
		''' <param name="timeoutMilliseconds">Timeout in milliseconds. If -1 the default timeout will  used.</param>
		''' <param name="userAgent">User-Agent for request.</param>
		''' <returns></returns>
		Public Shared Function DownloadData(url As String, timeoutMilliseconds As Integer, [userAgent] As String) As Stream
			Dim request = WebRequest.CreateHttp(url)

			If Not String.IsNullOrEmpty(userAgent) Then
				request.UserAgent = userAgent
			End If

			If timeoutMilliseconds > 0 Then
				request.Timeout = timeoutMilliseconds
			End If

			Dim response = request.GetResponse()

			'Copy data from buffer (It must be done, otherwise the buffer overflow and we stop to get repsonses).
			Dim stream = New MemoryStream()
			response.GetResponseStream().CopyTo(stream)
			Return stream
		End Function

		''' <summary>
		''' Load raw data into MemoryStream from specified Url.
		''' </summary>
		''' <param name="url">Data source Url</param>
		''' <param name="timeoutMilliseconds">Timeout in milliseconds. If -1 the default timeout will  used.</param>
		''' <param name="success">Success callback handler.</param>
		''' <param name="error">Error callback handler.</param>
		''' <param name="userAgent">User-Agent for request.</param>

		Public Shared Sub DownloadDataAsync(url As String, timeoutMilliseconds As Integer, success As Action(Of MemoryStream), [error] As Action(Of Exception), Optional userAgent As String = Nothing)
			Dim request = WebRequest.CreateHttp(url)

			If Not String.IsNullOrEmpty(userAgent) Then
				request.UserAgent = userAgent
			End If

			If timeoutMilliseconds > 0 Then
				request.Timeout = timeoutMilliseconds
			End If

			request.BeginGetResponse(Sub(ar)
										 Try
											 Dim response = request.GetResponse()

											 'Copy data from buffer (It must be done, otherwise the buffer overflow and we stop to get repsonses).
											 Dim stream = New MemoryStream()
											 response.GetResponseStream().CopyTo(stream)
											 success(stream)
										 Catch exception As Exception
											 [error](exception)
										 End Try

									 End Sub, Nothing)
		End Sub

	End Class
End Namespace
