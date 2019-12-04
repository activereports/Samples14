using System;
using System.Drawing;
using System.Resources;
using System.Xml;

namespace GrapeCity.ActiveReports.Samples.IListBinding
{
	/// <summary>
	/// Summary description for BindIListToDataGridSample.
	/// </summary>>
	public partial class BindIListToDataGridSample : System.Windows.Forms.Form
	{
		private DataLayer.ProductCollection _productCollection;
		private System.ComponentModel.IContainer components = null;

		public BindIListToDataGridSample()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();																										
		}

		/// <summary>
		/// btnGenerateReport_Click - Opens a new viewer form to display the
		/// report bound to the DataLayer object 
		/// </summary>
		private void btnGenerateReport_Click(object sender, System.EventArgs e)
		{
			// Create new report object
			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create("IlistReportSample.rpx"));
			rpt.DataSource = _productCollection;
			
			// Pass the document to show in the viewer form
			ViewerForm frmViewer = new ViewerForm();
			frmViewer.Show();
			frmViewer.LoadDocument(rpt);
		}

		private void BindIListToDataGridSample_Load(object sender, EventArgs e)
		{
			dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(Resource.DefaultFontName, 10);
		}
	}
}
