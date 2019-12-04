Imports System.IO
Imports System.Web
Imports System.Web.Caching
Imports GrapeCity.ActiveReports.Export.Html.Page
Imports GrapeCity.ActiveReports.Extensibility.Rendering
Imports GrapeCity.ActiveReports.Extensibility.Rendering.IO
Imports GrapeCity.ActiveReports.Rendering.IO

Public Class RdlxHandler
	Implements IHttpHandler
	Private Const HandlerExtension As String = ".rdlx"
	Private Const HandlerCacheExtension As String = ".rdlxWeb"

	Public Sub ProcessRequest(context As HttpContext) Implements IHttpHandler.ProcessRequest
		If Not context.Request.Url.AbsolutePath.EndsWith(HandlerExtension) Then
			If Not context.Request.Url.AbsolutePath.EndsWith(HandlerCacheExtension) Then
				Return
			End If

			' return image
			Dim keyName As String = Path.GetFileName(context.Request.FilePath)
			Dim cacheItem As Byte() = DirectCast(context.Cache(keyName), Byte())
			context.Response.BinaryWrite(cacheItem)
			Return
		End If

		context.Response.ContentType = "text/html"
		Dim streams As HtmlStreamProvider = New HtmlStreamProvider()
		Try
			Using report As PageReport = New PageReport(New FileInfo(context.Server.MapPath(context.Request.Url.LocalPath)))
				Dim html As HtmlRenderingExtension = New HtmlRenderingExtension()
				Dim settings As Settings = DirectCast(html.GetSupportedSettings(), Settings)
				settings.StyleStream = False
				settings.OutputTOC = False
				settings.Mode = RenderMode.Paginated
				report.Document.Render(html, streams, DirectCast(settings, ISettings).GetSettings())
			End Using

			For Each secondaryStreamInfo As StreamInfo In streams.GetSecondaryStreams()
				Dim secondaryStream As MemoryStream = DirectCast(secondaryStreamInfo.OpenStream(), MemoryStream)
				' 30 seconds should be enough to load all images
				context.Cache.Insert(secondaryStreamInfo.Uri.OriginalString, secondaryStream.ToArray(), Nothing, Cache.NoAbsoluteExpiration, New TimeSpan(0, 0, 30))
			Next
		Catch eRunReport As ReportException
			' Failure running report, just report the error to the user.
			context.Response.Write(My.Resources.Resource.RunningError)
			context.Response.Write(eRunReport.ToString())
			Return
		End Try

		Dim primaryStream As MemoryStream = DirectCast(streams.GetPrimaryStream().OpenStream(), MemoryStream)
		context.Response.BinaryWrite(primaryStream.ToArray())
	End Sub

	Public ReadOnly Property IsReusable As Boolean Implements IHttpHandler.IsReusable
		Get
			Return True
		End Get
	End Property

	Private NotInheritable Class HtmlStreamProvider
		Inherits MemoryStreamProvider
		Protected Overrides Function GetStreamUri(extension As String) As Uri
			Dim uri As Uri = MyBase.GetStreamUri(extension)
			Return New Uri(uri.OriginalString + HandlerCacheExtension, UriKind.Relative)
		End Function
	End Class
End Class
