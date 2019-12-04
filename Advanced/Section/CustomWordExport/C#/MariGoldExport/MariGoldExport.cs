using System;
using System.IO;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Export;
using GrapeCity.ActiveReports.Export.Html;
using GrapeCity.ActiveReports.Export.Html.Section;
using MariGold.OpenXHTML;

namespace GrapeCity.ActiveReports.Samples.WordExport
{
	public class MariGoldExport : IDocumentExport
	{
		private const string _htmlPath = @"..\..\htmlTemp\";
		private const string _htmlFilePath = _htmlPath + "temp.html";

		private string HtmlExport(SectionDocument document, string pageRange)
		{
			var htmlDirectory = Path.GetDirectoryName(_htmlPath);
			if (!Directory.Exists(htmlDirectory))
			{
				Directory.CreateDirectory(htmlDirectory);
			}

			using (var htmlExport = new HtmlExport())
				htmlExport.Export(document, _htmlFilePath, pageRange);
			return GetHtmlString();
		}

		private void HtmlToWord(string htmlString, string destFilePath)
		{
			var doc = new WordDocument(destFilePath);
			doc.ImagePath = Path.GetFullPath (_htmlPath);
			doc.Process(new HtmlParser(htmlString));
			doc.Save();
		}

		private string GetHtmlString()
		{
			using (var stream = new StreamReader(_htmlFilePath))
			{
				return stream.ReadToEnd();
			}
		}

		private void ClearHtmlPath()
		{
			foreach (var file in new DirectoryInfo(Path.GetDirectoryName(_htmlPath)).GetFiles())
			{
				file.Delete();
			}
			Directory.Delete(Path.GetDirectoryName(_htmlPath));
		}

		void IDocumentExport.Export(SectionDocument document, string filePath, string pageRange)
		{
			var html = HtmlExport(document, pageRange);
			HtmlToWord(html, filePath);
			ClearHtmlPath();
		}

		void IDocumentExport.Export(SectionDocument document, string filePath)
		{
			((IDocumentExport)this).Export(document, filePath, string.Empty);
		}

		void IDocumentExport.Export(SectionDocument document, Stream outputStream)
		{
			((IDocumentExport)this).Export(document, outputStream, string.Empty);
		}

		void IDocumentExport.Export(SectionDocument document, Stream outputStream, string pageRange)
		{
			var html = HtmlExport(document, pageRange);
			HtmlToWord(html, ((FileStream)outputStream).Name);
			ClearHtmlPath();
		}

		void IDocumentExport.Export(SectionDocument document, IOutputHtml outputHandler, string pageRange)
		{
			throw new NotSupportedException("It's not allowed to use IOutputHtml");
		}
	}
}
