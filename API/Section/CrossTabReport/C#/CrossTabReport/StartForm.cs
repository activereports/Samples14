using System;
using System.Windows.Forms;
using System.Xml;

namespace GrapeCity.ActiveReports.Samples.CrossTabReport
{
	/// <summary>
	/// Demonstrates unbound data, conditional highlighting, distributing data 
	/// across columns to create a cross tab view and data aggregation.
	/// </summary>
	public partial class StartForm : System.Windows.Forms.Form
	{
		public StartForm()
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
			Application.Run(new StartForm());
		}

		private void StartForm_Load(object sender, EventArgs e)
		{
			try
			{
				//Create a report and display it in the Viewer.
				var rpt = new SectionReport();
				rpt.LoadLayout(XmlReader.Create( Properties.Resources.ProductWeeklySales));
				arvMain.LoadDocument(rpt);
			}
			catch (Exception ex)
			{
				//Display a message when an error is thrown while creating a report.
				MessageBox.Show(ex.ToString(), Text);
			}
		}
	}
}
