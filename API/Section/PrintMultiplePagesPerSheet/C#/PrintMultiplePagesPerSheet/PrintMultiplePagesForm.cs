using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using GrapeCity.ActiveReports.Document.Section;

namespace GrapeCity.ActiveReports.Samples.PrintMultiplePagesPerSheet
{
	/// <summary>
	/// Summary description for PrintMultiplePagesPerSheetForm.
	/// </summary>
	public partial class PrintMultiplePagesForm : System.Windows.Forms.Form
	{
		private const float SpaceBetweenPages = 50.0f; 
		
		private SizeF _maxPageSize = SizeF.Empty;
		private int _pageCount;
		private int _currentPageIndex;
		private int _numberOfPagesPerPrinterPage = 6; 
		private int _currentNumberOfPagesPrinted;
		private int _numberOfPagesToPrint;
		private SizeF _pageScaledSize = SizeF.Empty;
		private int _pagesAcross;
		private int _pagesDown;
		private float _scaleFactor;
		private System.Drawing.Printing.PrintDocument _printDocument;
		
		public PrintMultiplePagesForm()
		{
			// Required for Windows Form Designer support
			InitializeComponent();
		}

		/// <summary>
		/// The main entry point for the application.
		/// 
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new PrintMultiplePagesForm());
		}

		/// <summary>
		/// PrintDocument_PrintPage: Controls page printing of the report based on the number of pages selected per sheet.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			if (_currentPageIndex < _pageCount)
			{
				RectangleF bounds = e.PageBounds;
				bounds.Width = e.Graphics.VisibleClipBounds.Width;
				bounds.Height = e.Graphics.VisibleClipBounds.Height;
				if (_currentPageIndex == 0)
				{
					_pagesAcross = _numberOfPagesPerPrinterPage / 2;
					_pagesDown = _numberOfPagesPerPrinterPage / _pagesAcross;
					_pageScaledSize.Width = (bounds.Width - (SpaceBetweenPages * (_pagesAcross - 1))) / _pagesAcross;
					_pageScaledSize.Height = (bounds.Height - (SpaceBetweenPages * (_pagesDown - 1))) / _pagesDown;
					_scaleFactor = _pageScaledSize.Width / _maxPageSize.Width;
					if (_scaleFactor > _pageScaledSize.Height / _maxPageSize.Height)
						_scaleFactor = _pageScaledSize.Height / _maxPageSize.Height;
				}
				RectangleF printRectangle = bounds;
				printRectangle.Width = _pageScaledSize.Width;
				printRectangle.Height = _pageScaledSize.Height;
				RectangleF pageRectangleInches = RectangleF.Empty;
				RectangleF pageRectangle = RectangleF.Empty;
				int column = 0;
				int startIndex = _currentPageIndex;
				PagesCollection pages = arViewer.Document.Pages;
				for (; _currentPageIndex < _pageCount && (startIndex == _currentPageIndex || _currentPageIndex % _numberOfPagesPerPrinterPage != 0); this._currentPageIndex++)
				{
					Page page = pages[_currentPageIndex];

					pageRectangleInches.X = printRectangle.X / 100;
					pageRectangleInches.Y = printRectangle.Y / 100;
					pageRectangleInches.Width = page.Width * _scaleFactor;
					pageRectangleInches.Height = page.Height * _scaleFactor;

					pageRectangle = printRectangle;
					pageRectangle.Width = page.Width * 100 * _scaleFactor;
					pageRectangle.Height = page.Height * 100 * _scaleFactor;
					e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(pageRectangle));
					page.Draw(e.Graphics, pageRectangleInches, _scaleFactor, _scaleFactor, true);

					column++;
					if (column >= _pagesAcross)
					{
						column = 0;
						printRectangle.X = bounds.X;
						printRectangle.Y += printRectangle.Height + SpaceBetweenPages;
					}
					else
						printRectangle.X += printRectangle.Width + SpaceBetweenPages;
				}
			}
			_currentNumberOfPagesPrinted++;
			e.HasMorePages = _currentNumberOfPagesPrinted < _numberOfPagesToPrint;
			if (!e.HasMorePages)
				_currentPageIndex = 0;
		}

		/// <summary>
		/// PrintMultiplePagesPerSheetForm_Load: Run the sample report and load it into the viewer control (first 20 pages - report normally runs to 100 pages).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PrintMultiplePagesPerSheetForm_Load(object sender, EventArgs e)
		{
			//Define and run the Invoice report
			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(Properties.Resources.Invoice));
			((Data.OleDBDataSource)rpt.DataSource).ConnectionString = Properties.Resources.ConnectionString;						
			rpt.Run(false);

			//Remove all but the first 20 pages from the generated report.
			while(rpt.Document.Pages.Count > 20)
				rpt.Document.Pages.RemoveAt(rpt.Document.Pages.Count - 1);

			arViewer.Document = rpt.Document;
			apiViewer.Document = rpt.Document;

			// Find the maximum page size.
			PagesCollection pages = arViewer.Document.Pages;
			Page page;
			_pageCount = pages.Count;
			for (int pageIndex = 0; pageIndex < _pageCount; pageIndex++)
			{
				page = pages[pageIndex];
				if (_maxPageSize.Width < page.Size.Width)
					_maxPageSize.Width = page.Size.Width;

				if (_maxPageSize.Height < page.Size.Height)
					_maxPageSize.Height = page.Size.Height;
			}

			// Convert to document units.
			_maxPageSize.Width *= 100;
			_maxPageSize.Height *= 100;

			cmbPageCount.SelectedIndex = cmbPageCountAPI.SelectedIndex = 0;
		}

		//btnPrint_Click: Set up the defaults and call the print document to print the report.
		private void btnPrint_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == dlgPrint.ShowDialog(this))
			{
				_numberOfPagesPerPrinterPage = Convert.ToInt32(cmbPageCount.SelectedItem.ToString());
				if (_numberOfPagesPerPrinterPage % 2 > 0)
					_numberOfPagesPerPrinterPage = (_numberOfPagesPerPrinterPage / 2) * 2;
				// Setup defaults.
				_currentNumberOfPagesPrinted = 0;
				_numberOfPagesToPrint = _pageCount / _numberOfPagesPerPrinterPage;
				_numberOfPagesToPrint += (_pageCount % _numberOfPagesPerPrinterPage) > 0 ? 1 : 0;
				_printDocument.Print();
			}
		}

		//Printing multiple Pages on one sheet using PrintOptions API 
		private void btnAPIprint_Click(object sender, EventArgs e)
		{			
			apiViewer.Document.PrintOptions.PageScaling = Extensibility.Printing.PageScaling.MultiplePages;
			apiViewer.Document.PrintOptions.PagesPerSheet = Convert.ToInt32(cmbPageCountAPI.SelectedItem.ToString());
			apiViewer.Document.PrintOptions.AutoRotate = true;
			apiViewer.Document.PrintOptions.PrintPageBorder = true;
			apiViewer.Document.Print();
		}
	}
}
