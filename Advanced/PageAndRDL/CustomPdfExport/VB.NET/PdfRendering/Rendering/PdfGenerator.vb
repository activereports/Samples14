Imports PdfSharp.Pdf
Imports PdfSharp.Drawing
Imports GrapeCity.ActiveReports.Rendering.Export
Imports GrapeCity.ActiveReports.Rendering
Imports GrapeCity.ActiveReports.Drawing
Imports GrapeCity.ActiveReports.Extensibility.Rendering.Components
Imports GrapeCity.ActiveReports.Extensibility.Rendering
Imports System.IO
Imports System.Drawing
Imports GrapeCity.ActiveReports.Drawing.Gdi

''' <summary>
''' PDF generator based on PDFsharp: http://www.pdfsharp.net/
''' </summary>
Friend NotInheritable Class PdfGenerator
	Implements IGenerator
	Implements IDisposable
	Private Shared ReadOnly GdiMetricsProvider As ITextMetricsProvider = New TextMetricsProvider()
	Private Shared ReadOnly GdiRenderersFactory As IRenderersFactory = New GdiRenderersFactory()

	Private ReadOnly _outputStream As Stream

	Private _document As PdfDocument
	Private ReadOnly _graphics As IList(Of XGraphics) = New List(Of XGraphics)()
	Private ReadOnly _images As PdfImagesFactory
	Private ReadOnly _fonts As PdfFontsFactory
	Private ReadOnly _outlines As IDictionary(Of String, PdfOutline) = New Dictionary(Of String, PdfOutline)()

	Public Sub New(outputStream As Stream, embedFonts As Boolean)
		_outputStream = outputStream

		_images = New PdfImagesFactory()
		_fonts = New PdfFontsFactory(embedFonts)
	End Sub

	Sub Dispose() Implements IDisposable.Dispose
		For Each g As XGraphics In _graphics
			Debug.Assert(g.GraphicsStateLevel = 0)
			g.Dispose()
		Next
		_graphics.Clear()
		If _document IsNot Nothing Then
			_document.Save(_outputStream)
			_document.Dispose()
		End If
		_document = Nothing
		CType(_images, IDisposable).Dispose()
	End Sub

	ReadOnly Property IsDelayedContentSupported() As Boolean Implements IGenerator.IsDelayedContentSupported
		Get
			Return True
		End Get
	End Property

	Public ReadOnly Property ProfessionalEdition() As Boolean Implements IGenerator.ProfessionalEdition
		Get
			Return False
		End Get
	End Property

	Public ReadOnly Property InteractivitySupport() As InteractivityType Implements IGenerator.InteractivitySupport
		Get
			Return InteractivityType.BuiltinHyperlinks
		End Get
	End Property

	Public ReadOnly Property MetricsProvider() As ITextMetricsProvider Implements IGenerator.MetricsProvider

		Get
			Return GdiMetricsProvider
		End Get
	End Property

	Public ReadOnly Property RenderersFactory() As IRenderersFactory Implements IGenerator.RenderersFactory
		Get
			Return GdiRenderersFactory
		End Get
	End Property

	Sub Init(report As IReport) Implements IGenerator.Init
		_document = New PdfDocument() With {
				.Version = 17
			}
	End Sub

	Function NewPage(pageNumber As Integer, pageSize As SizeF) As IDrawingCanvas Implements IGenerator.NewPage
		Dim page As New PdfPage(_document)
		page.Width = New XUnit(pageSize.Width / PdfConverter.TwipsPerPoint, XGraphicsUnit.Point)
		page.Height = New XUnit(pageSize.Height / PdfConverter.TwipsPerPoint, XGraphicsUnit.Point)
		_document.AddPage(page)
		Dim graphics = XGraphics.FromPdfPage(page)
		_graphics.Add(graphics)
		Return New PdfContentGenerator(graphics, _images, _fonts)
	End Function

	Sub StartOutline(key As String, parentKey As String, targetPage As Integer, targetArea As RectangleF, name As String) Implements IGenerator.StartOutline
		Dim outline As New PdfOutline(name, _document.Pages(targetPage - 1)) With {
				.PageDestinationType = PdfPageDestinationType.Xyz,
				.Left = targetArea.X / PdfConverter.TwipsPerPoint,
				.Top = targetArea.Y / PdfConverter.TwipsPerPoint
			}
		Dim parentOutline As PdfOutline = Nothing
		If _outlines.TryGetValue(parentKey, parentOutline) Then
			parentOutline.Outlines.Add(outline)
		Else
			_document.Outlines.Add(outline)
		End If
		_outlines(key) = outline
	End Sub

	Sub AddBookmark(key As String, sourcePage As Integer, sourceArea As RectangleF, targetPage As Integer, targetArea As RectangleF) Implements IGenerator.AddBookmark
		Dim sourceRect = _graphics(sourcePage).Transformer.WorldToDefaultPage(sourceArea)
		_document.Pages(sourcePage).AddDocumentLink(New PdfRectangle(sourceRect), targetPage)
	End Sub

	Sub UrlGoTo(link As String, sourcePage As Integer, sourceArea As RectangleF) Implements IGenerator.UrlGoTo
		_document.Pages(sourcePage - 1).AddWebLink(New PdfRectangle(PdfConverter.Convert(sourceArea)), link)
	End Sub

	Sub AddSorting(key As String, sourcePage As Integer, sourceArea As RectangleF) Implements IGenerator.AddSorting
	End Sub

	Sub AddToggle(key As String, sourcePage As Integer, sourceArea As RectangleF) Implements IGenerator.AddToggle
	End Sub

	Sub BookmarkGoTo(key As String, sourcePage As Integer, sourceArea As RectangleF) Implements IGenerator.BookmarkGoTo
	End Sub

	Sub DrillthroughGoTo(reportName As String, parameters As IDictionary(Of String, Object), sourcePage As Integer, sourceArea As RectangleF) Implements IGenerator.DrillthroughGoTo
	End Sub

	Sub EndOutline(key As String) Implements IGenerator.EndOutline
	End Sub

	Sub FinishLinks() Implements IGenerator.FinishLinks
	End Sub
End Class
