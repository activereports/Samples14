using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components;
using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.Rendering;
using GrapeCity.ActiveReports.Rendering.Export;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using GrapeCity.ActiveReports.Drawing.Gdi;

namespace GrapeCity.ActiveReports.Samples.Export.Rendering.PdfSharp
{
	/// <summary>
	/// PDF generator based on PDFsharp: http://www.pdfsharp.net/
	/// </summary>
	internal sealed class PdfGenerator : IGenerator, IDisposable
	{
		private static readonly ITextMetricsProvider GdiMetricsProvider = new TextMetricsProvider();
		private static readonly IRenderersFactory GdiRenderersFactory = new GdiRenderersFactory();

		private readonly Stream _outputStream;

		private PdfDocument _document;
		private readonly IList<XGraphics> _graphics = new List<XGraphics>();
		private readonly PdfImagesFactory _images;
		private readonly PdfFontsFactory _fonts;
		private readonly IDictionary<string, PdfOutline> _outlines = new Dictionary<string, PdfOutline>();

		public PdfGenerator(Stream outputStream, bool embedFonts)
		{
			_outputStream = outputStream;

			_images = new PdfImagesFactory();
			_fonts = new PdfFontsFactory(embedFonts);
		}

		void IDisposable.Dispose()
		{
			foreach (var g in _graphics)
			{
				Debug.Assert(g.GraphicsStateLevel == 0);
				g.Dispose();
			}
			_graphics.Clear();
			if (_document != null)
			{
				_document.Save(_outputStream);
				_document.Dispose();
			}
			_document = null;
			((IDisposable)_images).Dispose();
		}
		
		bool IGenerator.IsDelayedContentSupported
		{
			get { return true; }
		}

		public bool ProfessionalEdition
		{
			get { return false; }
		}

		public InteractivityType InteractivitySupport
		{
			get { return InteractivityType.BuiltinHyperlinks; }
		}

		public ITextMetricsProvider MetricsProvider
		{
			get { return GdiMetricsProvider; }
		}

		public IRenderersFactory RenderersFactory
		{
			get { return GdiRenderersFactory; }
		}

		void IGenerator.Init(IReport report)
		{
			_document = new PdfDocument { Version = 17 };
		}

		IDrawingCanvas IGenerator.NewPage(int pageNumber, SizeF pageSize)
		{
			var page = new PdfPage(_document);
			page.Width = new XUnit(pageSize.Width / PdfConverter.TwipsPerPoint, XGraphicsUnit.Point);
			page.Height = new XUnit(pageSize.Height / PdfConverter.TwipsPerPoint, XGraphicsUnit.Point);
			_document.AddPage(page);
			var graphics = XGraphics.FromPdfPage(page);
			_graphics.Add(graphics);
			return new PdfContentGenerator(graphics, _images, _fonts);
		}

		void IGenerator.StartOutline(string key, string parentKey, int targetPage, RectangleF targetArea, string name)
		{
			var outline = new PdfOutline(name, _document.Pages[targetPage - 1])
			{
				PageDestinationType = PdfPageDestinationType.Xyz,
				Left = targetArea.X / PdfConverter.TwipsPerPoint,
				Top = targetArea.Y / PdfConverter.TwipsPerPoint
			};
			PdfOutline parentOutline;
			if (_outlines.TryGetValue(parentKey, out parentOutline))
				parentOutline.Outlines.Add(outline);
			else
				_document.Outlines.Add(outline);
			_outlines[key] = outline;
		}
		
		void IGenerator.AddBookmark(string key, int sourcePage, RectangleF sourceArea, int targetPage, RectangleF targetArea)
		{
			var sourceRect = _graphics[sourcePage].Transformer.WorldToDefaultPage(XRect.FromLTRB(sourceArea.Left, sourceArea.Top, sourceArea.Right, sourceArea.Bottom));
			_document.Pages[sourcePage].AddDocumentLink(new PdfRectangle(sourceRect), targetPage);
		}
		
		void IGenerator.UrlGoTo(string link, int sourcePage, RectangleF sourceArea)
		{
			_document.Pages[sourcePage - 1].AddWebLink(new PdfRectangle(PdfConverter.Convert(sourceArea)), link);
		}
		
		void IGenerator.AddSorting(string key, int sourcePage, RectangleF sourceArea)
		{
		}

		void IGenerator.AddToggle(string key, int sourcePage, RectangleF sourceArea)
		{
		}

		void IGenerator.BookmarkGoTo(string key, int sourcePage, RectangleF sourceArea)
		{
		}

		void IGenerator.DrillthroughGoTo(string reportName, IDictionary<string, object> parameters, int sourcePage, RectangleF sourceArea)
		{
		}

		void IGenerator.EndOutline(string key)
		{
		}

		void IGenerator.FinishLinks()
		{
		}
	}
}
