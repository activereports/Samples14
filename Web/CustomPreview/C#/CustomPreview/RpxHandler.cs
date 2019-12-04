using System;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Xml;
using GrapeCity.ActiveReports.Export.Html;
using GrapeCity.ActiveReports.Export.Html.Section;

namespace GrapeCity.ActiveReports.Samples.Web.CustomPreview
{
	public class RpxHandler : IHttpHandler
	{
		private const string HandlerExtension = ".rpx";
		private const string HandlerCacheExtension = ".rpxWeb";

		public void ProcessRequest(HttpContext context)
		{
			if (!context.Request.Url.AbsolutePath.EndsWith(HandlerExtension))
			{
				if (!context.Request.Url.AbsolutePath.EndsWith(HandlerCacheExtension))
					return;

				// return image
				var keyName = Path.GetFileName(context.Request.FilePath);
				var cacheItem = context.Cache[keyName];
				context.Response.BinaryWrite((byte[])cacheItem);
				return;
			}

			var rpxFile = context.Server.MapPath(context.Request.Url.LocalPath);
			var htmlHandler = new HtmlOutputHandler(context.Cache, Path.GetFileNameWithoutExtension(rpxFile));

			context.Response.ContentType = "text/html";
			try
			{
				using (var report = new SectionReport())
				using (var reader = XmlReader.Create(rpxFile))
				{
					report.ResourceLocator = new DefaultResourceLocator(new Uri(Path.GetDirectoryName(rpxFile) + @"\"));
					report.LoadLayout(reader);
					report.Run(false);
					using (var html = new HtmlExport { IncludeHtmlHeader = true })
						html.Export(report.Document, htmlHandler, "");
					report.Document.Dispose();
				}
			}
			catch (ReportException eRunReport)
			{
				// Failure running report, just report the error to the user.
				context.Response.Write(Properties.Resource.Error);
				context.Response.Write(eRunReport.ToString());
				return;
			}

			context.Response.BinaryWrite(htmlHandler.MainPageData);
		}

		public bool IsReusable
		{
			get { return true; }
		}

		private sealed class HtmlOutputHandler : IOutputHtml
		{
			private readonly Cache _cache;
			private readonly string _name;

			public HtmlOutputHandler(Cache cache, string name)
			{
				_cache = cache;
				_name = name;
			}

			public string OutputHtmlData(HtmlOutputInfoArgs info)
			{
				var stream = info.OutputStream;

				var dataLen = (int)stream.Length;
				if (dataLen <= 0)
					return string.Empty;

				var bytesData = new byte[dataLen];
				stream.Seek(0, SeekOrigin.Begin);
				stream.Read(bytesData, 0, dataLen);

				if (info.OutputKind == HtmlOutputKind.HtmlPage)
				{
					MainPageData = bytesData;
					return _name;
				}

				var keyName = Guid.NewGuid().ToString() + HandlerCacheExtension;
				// 30 seconds should be enough to load all images
				_cache.Insert(keyName, bytesData, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 30));
				return keyName;
			}

			public void Finish()
			{
			}

			public byte[] MainPageData { get; private set; }
		}
	}
}
