using System;
using System.IO;
using System.Web;
using System.Web.Caching;
using GrapeCity.ActiveReports.Export.Html.Page;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Rendering.IO;

namespace GrapeCity.ActiveReports.Samples.Web.CustomPreview
{
	public class RdlxHandler : IHttpHandler
	{
		private const string HandlerExtension = ".rdlx";
		private const string HandlerCacheExtension = ".rdlxWeb";

		public void ProcessRequest(HttpContext context)
		{
			if (!context.Request.Url.AbsolutePath.EndsWith(HandlerExtension))
			{
				if (!context.Request.Url.AbsolutePath.EndsWith(HandlerCacheExtension))
					return;

				// return image
				var keyName = Path.GetFileName(context.Request.FilePath);
				var cacheItem = context.Cache[keyName];
				context.Response.BinaryWrite((byte[]) cacheItem);
				return;
			}

			context.Response.ContentType = "text/html";
			var streams = new HtmlStreamProvider();
			try
			{
				using (var report = new PageReport(new FileInfo(context.Server.MapPath(context.Request.Url.LocalPath))))
				{
					var html = new HtmlRenderingExtension();
					var settings = (Settings)html.GetSupportedSettings();
					settings.StyleStream = false;
					settings.OutputTOC = false;
					settings.Mode = RenderMode.Paginated;
					report.Document.Render(html, streams, ((ISettings)settings).GetSettings());
				}

				foreach (var secondaryStreamInfo in streams.GetSecondaryStreams())
				{
					var secondaryStream = (MemoryStream) secondaryStreamInfo.OpenStream();
					// 30 seconds should be enough to load all images
					context.Cache.Insert(secondaryStreamInfo.Uri.OriginalString, secondaryStream.ToArray(), null,
						Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 30));
				}
			}
			catch (ReportException eRunReport)
			{
				// Failure running report, just report the error to the user.
				context.Response.Write(Properties.Resource.Error);
				context.Response.Write(eRunReport.ToString());
				return;
			}

			var primaryStream = (MemoryStream) streams.GetPrimaryStream().OpenStream();
			context.Response.BinaryWrite(primaryStream.ToArray());
		}

		public bool IsReusable
		{
			get { return true; }
		}

		private sealed class HtmlStreamProvider : MemoryStreamProvider
		{
			protected override Uri GetStreamUri(string extension)
			{
				Uri uri = base.GetStreamUri(extension);
				return new Uri(uri.OriginalString + HandlerCacheExtension, UriKind.Relative);
			}
		}
	}
}
