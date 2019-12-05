Imports System.Drawing
Imports System.Windows.Forms
Imports System.Xml
Imports GrapeCity.ActiveReports.Document


Public Class PrintMultiplePages


	Private Const SpaceBetweenPages As Single = 50.0F ' 50 document units

	Private _maxPageSize As SizeF = SizeF.Empty
	Private _pageCount As Integer = 0
	Private _currentPageIndex As Integer = 0
	Private _numberOfPagesPerPrinterPage As Integer = 6 ' needs to be an even number
	Private _currentNumberOfPagesPrinted As Integer = 0
	Private _numberOfPagesToPrint As Integer = 0
	Private _pageScaledSize As SizeF = SizeF.Empty
	Private _pagesAcross As Integer = 0
	Private _pagesDown As Integer = 0
	Private _scaleFactor As Single = 0.0F


	Public Sub New()
		MyBase.New()

		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub

	'Form overrides dispose to clean up the component list.
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	Private Sub PrintMultiplePage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Define and run the Invoice Report
		Dim rpt As New SectionReport
		rpt.LoadLayout(XmlReader.Create(My.Resources.Invoice))
		CType(rpt.DataSource, Data.OleDBDataSource).ConnectionString = My.Resources.ConnectionString
		rpt.Run(False)

		'Remove all but the first 20 pages from the generated report.
		While rpt.Document.Pages.Count > 20
			rpt.Document.Pages.RemoveAt((rpt.Document.Pages.Count - 1))
		End While
		arViewer.Document = rpt.Document
		apiViewer.Document = rpt.Document

		'Find the maximum page size. 
		Dim pages As Section.PagesCollection = arViewer.Document.Pages
		Dim page As Section.Page
		_pageCount = pages.Count
		Dim pageIndex As Integer
		For pageIndex = 0 To (_pageCount) - 1
			page = pages(pageIndex)
			If _maxPageSize.Width < page.Size.Width Then
				_maxPageSize.Width = page.Size.Width
			End If
			If _maxPageSize.Height < page.Size.Height Then
				_maxPageSize.Height = page.Size.Height
			End If
		Next pageIndex

		_maxPageSize.Width *= 100
		_maxPageSize.Height *= 100

		cmbPageCountAPI.SelectedIndex = 0
		cmbPageCount.SelectedIndex = 0
	End Sub

	Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
		If DialogResult.OK = dlgPrint.ShowDialog(Me) Then
			_numberOfPagesPerPrinterPage = CType(cmbPageCount.SelectedItem.ToString(), Integer)
			If _numberOfPagesPerPrinterPage Mod 2 > 0 Then
				_numberOfPagesPerPrinterPage = CInt(_numberOfPagesPerPrinterPage / 2 * 2)
			End If 'Setup defaults.
			_currentNumberOfPagesPrinted = 0
			_numberOfPagesToPrint = CInt(_pageCount / _numberOfPagesPerPrinterPage)
			_numberOfPagesToPrint += CInt(IIf(_pageCount Mod _numberOfPagesPerPrinterPage > 0, 1, 0))
			PrintDocument.Print()
		End If
	End Sub

	Private Sub btnAPIprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAPIprint.Click
		apiViewer.Document.PrintOptions.PageScaling = Extensibility.Printing.PageScaling.MultiplePages
		apiViewer.Document.PrintOptions.PagesPerSheet = CType(cmbPageCountAPI.SelectedItem.ToString(), Integer)
		apiViewer.Document.PrintOptions.AutoRotate = True
		apiViewer.Document.PrintOptions.PrintPageBorder = True
		apiViewer.Document.Print()
	End Sub

	'PrintDocument_PrintPage: Controls page printing of the report based on the number of pages selected per sheet.
	Private Sub PrintDocument_PrintPage(ByVal sender As Object, ByVal e As Printing.PrintPageEventArgs) Handles PrintDocument.PrintPage
		If _currentPageIndex < _pageCount Then
			Dim bounds As RectangleF
			bounds.Width = e.Graphics.VisibleClipBounds.Width
			bounds.Height = e.Graphics.VisibleClipBounds.Height
			If _currentPageIndex = 0 Then
				_pagesAcross = CInt(_numberOfPagesPerPrinterPage / 2)
				_pagesDown = CInt(_numberOfPagesPerPrinterPage / _pagesAcross)
				_pageScaledSize.Width = (bounds.Width - SpaceBetweenPages * (_pagesAcross - 1)) / _pagesAcross
				_pageScaledSize.Height = (bounds.Height - SpaceBetweenPages * (_pagesDown - 1)) / _pagesDown
				_scaleFactor = _pageScaledSize.Width / _maxPageSize.Width
				If _scaleFactor > _pageScaledSize.Height / _maxPageSize.Height Then
					_scaleFactor = _pageScaledSize.Height / _maxPageSize.Height
				End If
			End If
			Dim printRectangle As RectangleF = bounds
			printRectangle.Width = _pageScaledSize.Width
			printRectangle.Height = _pageScaledSize.Height
			Dim pageRectangleInches As RectangleF = RectangleF.Empty
			Dim pageRectangle As RectangleF = RectangleF.Empty
			Dim page As Section.Page
			Dim column As Integer = 0
			Dim startIndex As Integer = _currentPageIndex
			Dim pages As Section.PagesCollection = arViewer.Document.Pages

			While _currentPageIndex < _pageCount AndAlso (startIndex = _currentPageIndex OrElse Me._currentPageIndex Mod _numberOfPagesPerPrinterPage <> 0)
				page = pages(_currentPageIndex)

				pageRectangleInches.X = printRectangle.X / 100
				pageRectangleInches.Y = printRectangle.Y / 100
				pageRectangleInches.Width = page.Width * _scaleFactor
				pageRectangleInches.Height = page.Height * _scaleFactor

				pageRectangle = printRectangle
				pageRectangle.Width = page.Width * 100 * _scaleFactor
				pageRectangle.Height = page.Height * 100 * _scaleFactor
				e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(pageRectangle))
				page.Draw(e.Graphics, pageRectangleInches, _scaleFactor, _scaleFactor, True)

				column += 1
				If column >= _pagesAcross Then
					column = 0
					printRectangle.X = bounds.X
					printRectangle.Y += printRectangle.Height + SpaceBetweenPages
				Else
					printRectangle.X += printRectangle.Width + SpaceBetweenPages
				End If
				_currentPageIndex = _currentPageIndex + 1
			End While
		End If
		_currentNumberOfPagesPrinted = _currentNumberOfPagesPrinted + 1
		e.HasMorePages = _currentNumberOfPagesPrinted < Me._numberOfPagesToPrint
		If Not e.HasMorePages Then
			_currentPageIndex = 0
		End If
	End Sub 'PrintDocument_PrintPage


End Class
