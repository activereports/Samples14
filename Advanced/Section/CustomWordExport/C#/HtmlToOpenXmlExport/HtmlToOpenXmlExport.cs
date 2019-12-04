using System;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Export;
using GrapeCity.ActiveReports.Export.Html;
using GrapeCity.ActiveReports.Export.Html.Section;
using HtmlToOpenXml;

namespace GrapeCity.ActiveReports.Samples.WordExport
{
	public class HtmlToOpenXmlExport : IDocumentExport
	{
		private const string _htmlPath = @"..\..\htmlTemp\";
		private const string _htmlFilePath = _htmlPath + "temp.html";

		private void HtmlToOpenXml(string html, MemoryStream generatedDocument)
		{
			using (var buffer = ResourceHelper.GetStream("Resources.template.docx"))
				buffer.CopyToAsync(generatedDocument);

			generatedDocument.Position = 0L;
			using (var package = WordprocessingDocument.Open(generatedDocument, true))
			{
				MainDocumentPart mainPart = package.MainDocumentPart;
				if (mainPart == null)
				{
					mainPart = package.AddMainDocumentPart();
					new DocumentFormat.OpenXml.Wordprocessing.Document(new Body()).Save(mainPart);
				}

				var converter = new HtmlConverter(mainPart);
				converter.ImageProcessing = ImageProcessing.ManualProvisioning;
				converter.ProvisionImage += OnProvisionImage;

				var paragraphs = converter.Parse(html);

				Body body = mainPart.Document.Body;
				SectionProperties sectionProperties = GetLastChild<SectionProperties>(mainPart.Document.Body);

				for (int i = 0; i < paragraphs.Count; i++)
					body.Append(paragraphs[i]);

				if (sectionProperties != null)
				{
					sectionProperties.Remove();
					body.Append(sectionProperties);
				}

				mainPart.Document.Save();
			}
		}

		private T GetLastChild<T>(OpenXmlElement element) where T : OpenXmlElement
		{
			if (element == null) return null;

			for (int i = element.ChildElements.Count - 1; i >= 0; i--)
			{
				if (element.ChildElements[i] is T)
					return element.ChildElements[i] as T;
			}

			return null;
		}

		private void WriteAllBytes(Stream stream, MemoryStream generatedDocument)
		{
			var bytes = generatedDocument.ToArray();
			stream.Write(bytes, 0, bytes.Length);
		}

		private void WriteAllBytes(string destFilePath, MemoryStream generatedDocument)
		{
			File.WriteAllBytes(destFilePath, generatedDocument.ToArray());
		}

		private string HtmlExport(SectionDocument document, string pageRange)
		{
			if (!Directory.Exists(_htmlPath))
			{
				Directory.CreateDirectory(_htmlPath);
			}

			using (var htmlExport = new HtmlExport())
				htmlExport.Export(document, _htmlFilePath, pageRange);
			return GetHtmlString();
		}

		private string GetHtmlString()
		{
			using (var stream = new StreamReader(_htmlFilePath))
			{
				return stream.ReadToEnd();
			}
		}

		private void OnProvisionImage(object sender, ProvisionImageEventArgs e)
		{
			string filename = Path.GetFileName(e.ImageUrl.OriginalString);
			var imagePath = Path.Combine(_htmlPath, filename);
			if (!File.Exists(imagePath))
			{
				//e.Cancel = true;
				return;
			}

			e.Provision(File.ReadAllBytes(imagePath));
		}

		private void ClearHtmlPath()
		{
			foreach (var file in new DirectoryInfo(_htmlPath).GetFiles())
			{
				file.Delete();
			}
			Directory.Delete(_htmlPath);
		}

		void IDocumentExport.Export(SectionDocument document, string filePath, string pageRange)
		{
			var html = HtmlExport(document, pageRange);
			using (var generatedDocument = new MemoryStream())
			{
				HtmlToOpenXml(html, generatedDocument);
				WriteAllBytes(filePath, generatedDocument);
			}
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
			using (var generatedDocument = new MemoryStream())
			{
				HtmlToOpenXml(html, generatedDocument);
				WriteAllBytes(outputStream, generatedDocument);
			}
			ClearHtmlPath();
		}

		void IDocumentExport.Export(SectionDocument document, IOutputHtml outputHandler, string pageRange)
		{
			throw new NotSupportedException("It's not allowed to use IOutputHtml");
		}
	}
}
