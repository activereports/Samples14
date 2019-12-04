using System;
using System.Windows.Forms;
using System.Xml;

namespace GrapeCity.ActiveReports.Samples.UnboundData
{
	/// <summary>
	/// UnboundDataSample - Illustrates the use of unbound data in ActiveReports.
	/// </summary>
	public partial class MainForm : System.Windows.Forms.Form
	{
		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		/// <summary>
		/// tabDataBinding_SelectedIndexChanged - clears the viewer out when switching
	   /// </summary>
		private void tabDataBinding_SelectedIndexChanged(object sender, EventArgs e)
		{
		  //Clear existing pages
		  //
			arvMain.Document = new Document.SectionDocument();
		}

		//#region Unbound Data Code
		//

		/// <summary>
		/// btnDataSet_Click -  Illustrates using a DataSet as a data source 
		/// </summary>
		private void btnDataSet_Click(object sender, EventArgs e)
		{
			//Create the report.
			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(Properties.Resources.UnboundDSInvoice));
			//Run and view the report.
			arvMain.LoadDocument(rpt);
		}

		/// <summary>
		/// btnDataReader_Click - Illustrates using a DataReader as a data source 
		/// </summary>
		private void btnDataReader_Click(object sender, System.EventArgs e)
		{
			//Create the report
			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(Properties.Resources.UnboundDRInvoice));
			//Run and view the report
			arvMain.LoadDocument(rpt);
		}

		/// <summary>
		/// btnTextFile_Click - Illustrates using a TextFile as a data source 
		/// </summary>
		private void btnTextFile_Click(object sender, System.EventArgs e)
		{
			//Create the report
			//UnboundTFInvoice rpt = new UnboundTFInvoice();
			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(Properties.Resources.UnboundTFInvoice));
			//Run and view the report
			arvMain.LoadDocument(rpt);		
		}

		/// <summary>
		/// btnArray_Click - Illustrates using a Array as a data source 
		/// </summary>
		private void btnArray_Click(object sender, System.EventArgs e)
		{
			//Create the report
			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create(Properties.Resources.UnboundDAInvoice));
			//Run and view the report
			arvMain.LoadDocument(rpt);
		}

		//#endregion
	}
}
