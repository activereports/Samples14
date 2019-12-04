using System;
using System.Windows.Forms;
using System.Xml;

namespace GrapeCity.ActiveReports.Samples.XML
{
	/// <summary>
	/// Demonstrates how to setup an XML data source through the customer order list.
	/// It also displays subreports and multi-level reports.
	/// </summary>
	public partial class StartForm : System.Windows.Forms.Form
	{
		
		public StartForm()
		{
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

		/// <summary>
		/// btnCustomers_Click - Checks the radio options and sets the report object's data
		/// </summary>
		private void btnCustomers_Click(object sender, EventArgs e)
		{
			try
			{
				var rpt = new SectionReport();
				rpt.LoadLayout(XmlReader.Create(Properties.Resources.CustomersOrders));

				Data.XMLDataSource ds = rpt.DataSource as Data.XMLDataSource;
				if (ds == null)
				{
					// Display the message when an error occurs.
					MessageBox.Show(Properties.Resources.DataSourceError, this.Text);
					return;
				}

				ds.FileURL = Properties.Resources.ConnectionString;

				// Set an XSL pattern to get the node (record) based on the report.
				if (radioAll.Checked)
				{
					// Show all data
					ds.RecordsetPattern = "//CUSTOMER";
				}
				else if (radioID.Checked)
				{
					// Show data where ID = 2301
					ds.RecordsetPattern = "//CUSTOMER[@id = " + @"""" + "2301" + @"""" + "]";
				}
				else if (radioEmail.Checked)
				{
					// Show data where Email is valid
					ds.RecordsetPattern = "//CUSTOMER[@email]";
				}
				
				ViewerForm frm = new ViewerForm();
				frm.Show();
				frm.LoadReport(rpt);
			}
			catch (ReportException ex)
			{
				MessageBox.Show(ex.ToString(), Text);
			}
		}

		/// <summary>
		/// btnCustomersLeveled_Click - Creates a OrdersLeveled report and sets
		/// the data source for it.
		/// </summary>
		private void btnCustomersLeveled_Click(object sender, EventArgs e)
		{
			try
			{
				//OrdersLeveled rpt = new OrdersLeveled();
				var rpt = new SectionReport();
				rpt.LoadLayout(XmlReader.Create(Properties.Resources.OrdersLeveled));

				Data.XMLDataSource ds = rpt.DataSource as Data.XMLDataSource;
				if (ds == null)
				{
					// Display the message when an error occurs.
					MessageBox.Show(Properties.Resources.DataSourceError, this.Text);
					return;
				}

				// Set the XML file name.
				ds.FileURL = Properties.Resources.ConnectionString;
				
				// Display the report.
				ViewerForm frm = new ViewerForm();
				frm.Show();
				frm.LoadReport(rpt);
			}
			catch (ReportException ex)
			{
				MessageBox.Show(ex.ToString(), Text);
			}
		}
	}
}
