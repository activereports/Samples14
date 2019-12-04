Imports System.IO
Imports System.Web
Imports System.Web.Caching
Imports System.Xml
Imports GrapeCity.ActiveReports.Export.Html
Imports GrapeCity.ActiveReports.Export.Html.Section

Public Class RpxHandler
	Implements IHttpHandler
	Private Const HandlerExtension As String = ".rpx"
	Private Const HandlerCacheExtension As String = ".rpxWeb"

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

		Dim rpxFile As String = context.Server.MapPath(context.Request.Url.LocalPath)
		Dim htmlHandler As HtmlOutputHandler = New HtmlOutputHandler(context.Cache, Path.GetFileNameWithoutExtension(rpxFile))

		context.Response.ContentType = "text/html"
		Try
			Using report As SectionReport = New SectionReport()
				Using reader As XmlReader = XmlReader.Create(rpxFile)
					report.ResourceLocator = New DefaultResourceLocator(New Uri(Path.GetDirectoryName(rpxFile) + "\"))
					report.LoadLayout(reader)
					report.Run(False)
					Using html As HtmlExport = New HtmlExport()
						html.IncludeHtmlHeader = True
						html.Export(report.Document, htmlHandler, "")
					End Using
					report.Document.Dispose()
				End Using
			End Using
		Catch eRunReport As ReportException
			' Failure running report, just report the error to the user.
			context.Response.Write(My.Resources.Resource.RunningError)
			context.Response.Write(eRunReport.ToString())
			Return
		End Try

		context.Response.BinaryWrite(htmlHandler.MainPageData)
	End Sub

	Public ReadOnly Property IsReusable As Boolean Implements IHttpHandler.IsReusable
		Get
			Return True
		End Get
	End Property

	Private NotInheritable Class HtmlOutputHandler
		Implements IOutputHtml
		Private ReadOnly _cache As Cache
		Private ReadOnly _name As String
		Private _mainPageData As Byte()

		Public Sub New(cache As Cache, name As String)
			_cache = cache
			_name = name
		End Sub

		Public Function OutputHtmlData(info As HtmlOutputInfoArgs) As String Implements IOutputHtml.OutputHtmlData
			Dim stream As Stream = info.OutputStream

			Dim dataLen As Int32 = CInt(stream.Length)
			If dataLen <= 0 Then
				Return String.Empty
			End If

			Dim bytesData As Byte() = New Byte(dataLen - 1) {}
			stream.Seek(0, SeekOrigin.Begin)
			stream.Read(bytesData, 0, dataLen)

			If info.OutputKind = HtmlOutputKind.HtmlPage Then
				MainPageData = bytesData
				Return _name
			End If

			Dim keyName As String = Guid.NewGuid().ToString() & HandlerCacheExtension
			' 30 seconds should be enough to load all images
			_cache.Insert(keyName, bytesData, Nothing, Cache.NoAbsoluteExpiration, New TimeSpan(0, 0, 30))
			Return keyName
		End Function

		Public Sub Finish() Implements IOutputHtml.Finish
		End Sub

		Public Property MainPageData As Byte()
			Get
				Return _mainPageData
			End Get
			Private Set(value As Byte())
				_mainPageData = value
			End Set
		End Property
	End Class
End Class
