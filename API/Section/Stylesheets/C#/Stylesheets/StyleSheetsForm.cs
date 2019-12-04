using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace GrapeCity.ActiveReports.Samples.StyleSheets
{
	public partial class StyleSheetsForm : Form
	{
		private string _externalStyleSheet = string.Empty;

		public StyleSheetsForm()
		{
			InitializeComponent();
		}

		private void buttonRunReport_Click(object sender, EventArgs e)
		{
			XmlReader xmlReader = null;
			// Select the report in the Viewer.
			// 
			if (radioButtonProductListReport.Checked)
				xmlReader = XmlReader.Create(Properties.Resources.ProductsReport);
			else if (radioButtonCategoriesReport.Checked)
				xmlReader = XmlReader.Create(Properties.Resources.CategoryReport);

			var report = new SectionReport();
			report.LoadLayout(xmlReader);
			// Apply stylesheet on the report.
			// 
			string outputFolder = new FileInfo(GetType().Assembly.Location).DirectoryName + "\\";

			string styleSheet = "";
			if (radioButtonClassicStyle.Checked)
				styleSheet = outputFolder + "Classic.reportstyle";
			else if (radioButtonColoredStyle.Checked)
				styleSheet = outputFolder + "Colored.reportstyle";
			else if (_externalStyleSheet != "")
				styleSheet = _externalStyleSheet;

			if (styleSheet != "")
			{
				report.LoadStyles(styleSheet);
			}
			reportViewer.LoadDocument(report);
		}

		private void buttonChooseExtStyle_Click(object sender, EventArgs e)
		{
			// Select the external stylesheet to apply on the report.
			// 
			FileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = Properties.Resources.Filter;
			openFileDialog.InitialDirectory = new FileInfo(GetType().Assembly.Location).DirectoryName;
			openFileDialog.CheckFileExists = true;

			if (openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				FileInfo styleSheetFile = new FileInfo(openFileDialog.FileName);
				_externalStyleSheet = styleSheetFile.FullName;
				radioButtonExternalStyleSheet.Text = Properties.Resources.ExternalSstylesheet + styleSheetFile.Name;
				radioButtonExternalStyleSheet.Checked = true;
			}
		}
	}
}
