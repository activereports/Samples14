using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.IO;

namespace GrapeCity.ActiveReports.Samples.BoundData
{
	/// <summary>
	/// BoundDataSample - Illustrates how to bind data in ActiveReports.
	/// </summary>
	public partial class MainForm : System.Windows.Forms.Form
	{
		readonly string _settingForNoHeaderFixed;
		readonly string _settingForHeaderExistsFixed;
		public MainForm()
		{
			_settingForNoHeaderFixed = Properties.Resources.NoHeaderFixed;
			_settingForHeaderExistsFixed = Properties.Resources.HeaderExistsFixed;
			// Required for Windows Form Designer support
			InitializeComponent();
		}

		/// <summary>
		/// The main entry point for the application.>
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			//Clear drop down lists.
			cbCompanyName.Items.Clear();
			cbSqlServerList.Items.Clear();
		}

		/// <summary>
		/// tabDataBinding_SelectedIndexChanged - Clear the viewer out when switching tabs.
		/// </summary>
		/// 
		private void tabDataBinding_SelectedIndexChanged(object sender, EventArgs e)
		{
			//Clear existing pages
			arvMain.Document = new Document.SectionDocument();
		}

		/// <summary>
		/// Populate the DropDown list with company names from the Northwind access database.
		/// </summary>
		private void cbCompanyName_DropDown(object sender, EventArgs e)
		{
			//Populate the combo box if no items exist.
			if (cbCompanyName.Items.Count == 0)
			{
				Cursor = Cursors.WaitCursor;

				//Set up database connection.
				OleDbConnection nwindConn = new OleDbConnection();
				nwindConn.ConnectionString = Properties.Resources.ConnectionString;
				//SQL Select statement used to get the Company Names
				OleDbCommand selectCMD = new OleDbCommand("SELECT DISTINCT Customers.CompanyName from Invoices", nwindConn);
				nwindConn.Open();	
				OleDbDataReader companyNamesDR = selectCMD.ExecuteReader();
				 //While the reader has data add a new Company Name to the list.
				while (companyNamesDR.Read())
				{
					cbCompanyName.Items.Add(companyNamesDR[0].ToString());
				}
				nwindConn.Close();
				//Set selection to first item in the list.
				cbCompanyName.SelectedIndex = 0;
				Cursor = Cursors.Arrow;
			}		
		}

		/// <summary>
		/// Used to populate the DropDown list with the existing SQL Servers on the network.
		/// </summary>
		private void cbSqlServerList_DropDown(object sender, EventArgs e)
		{
			//Populate the combo box if no items exist.
			if(cbSqlServerList.Items.Count == 0)
			{
				try
				{
					Cursor = Cursors.WaitCursor;

					DataTable table = System.Data.Sql.SqlDataSourceEnumerator.Instance.GetDataSources();
					foreach (DataRow server in table.Rows)
					{
						if (string.IsNullOrEmpty(server[table.Columns["ServerName"]] as string) ||
							string.IsNullOrEmpty(server[table.Columns["InstanceName"]] as string))
							continue;
						cbSqlServerList.Items.Add(server[table.Columns["ServerName"]] + "\\" + server[table.Columns["InstanceName"]]);
					}

					//Set the selection to the first item in the list.
					if (cbSqlServerList.Items.Count > 0)
						cbSqlServerList.SelectedIndex = 0;
				}
				catch (InvalidCastException ex)
				{
					MessageBox.Show(ex.Message);
				}
				catch (ArgumentNullException)
				{
					//SQLDMO object not found installed.
					MessageBox.Show(Properties.Resources.SQLServerProblem);
				}
				finally
				{
					Cursor = Cursors.Arrow;
				}
			}	
		}

		/// <summary>
		/// Illustrates using a DataSet as a data source.
		/// 
		/// </summary>
		private void btnDataSet_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;

			//Dataset to hold data.
			//
			DataSet invoiceData = new DataSet();
			invoiceData.Locale = CultureInfo.InvariantCulture;

			//Database connection populated from the sample Northwind access database
			//
			OleDbConnection nwindConn = new OleDbConnection();			
			nwindConn.ConnectionString = Properties.Resources.ConnectionString;			
			//Run the SQL command.
			//
			OleDbCommand selectCMD = new OleDbCommand("SELECT * FROM Invoices ORDER BY Customers.CompanyName, OrderID", nwindConn);
			selectCMD.CommandTimeout = 30;

			//Data adapter used to run the select command
			//
			OleDbDataAdapter invoicesDA = new OleDbDataAdapter();
			invoicesDA.SelectCommand = selectCMD;

			//Fill the DataSet.
			//
			invoicesDA.Fill(invoiceData, "Invoices");
			nwindConn.Close();

			//Create the report and assign the data source.
			//
			var rpt = new ActiveReports.SectionReport();
			rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\",Properties.Resources.ReportName)));
			rpt.DataSource = invoiceData;
			rpt.DataMember = invoiceData.Tables[0].TableName;
			//Run and view the report.
			//
			arvMain.LoadDocument(rpt);

			Cursor = Cursors.Arrow;
		}

		/// <summary>
		/// Illustrates using a DataTable as a data source
		/// 
		/// </summary>
		private void btnDataTable_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;

			//DataTable to hold the data.
			//
			DataTable invoiceData = new DataTable("Invoices");
			invoiceData.Locale = CultureInfo.InvariantCulture;

			//Database connection populated from the sample Northwind access database
			//
			OleDbConnection nwindConn = new OleDbConnection();			
			nwindConn.ConnectionString = Properties.Resources.ConnectionString;			

			//Run the SQL command.
			//
			OleDbCommand selectCMD = new OleDbCommand("SELECT * FROM Invoices ORDER BY Customers.CompanyName, OrderID", nwindConn);
			selectCMD.CommandTimeout = 30;

			//Data adapter used to run the select command
			//
			OleDbDataAdapter invoicesDA = new OleDbDataAdapter();
			invoicesDA.SelectCommand = selectCMD;

			//Fill the DataSet.
			//
			invoicesDA.Fill(invoiceData);
			nwindConn.Close();

			//Create the report and assign the data source.
			//
			var rpt = new ActiveReports.SectionReport();
			rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\", Properties.Resources.ReportName)));
			rpt.DataSource = invoiceData;
			//Run and view the report.
			//
			arvMain.LoadDocument(rpt);		

			Cursor = Cursors.Arrow;
		}

		/// <summary>
		/// Illustrates using a DataView as a data source
		/// 
		/// </summary>
		private void btnDataView_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;

			//Verify that a value Company Name is selected
			//
			if (cbCompanyName.SelectedItem == null)
			{
				MessageBox.Show(Properties.Resources.SelectCompanyName);
				Cursor = Cursors.Arrow;
				return;
			}

			//DataTable to hold the data.
			//
			DataTable invoiceData = new DataTable("Invoices");
			invoiceData.Locale = CultureInfo.InvariantCulture;

			//Database connection populated from the sample Northwind access database
			//
			OleDbConnection nwindConn = new OleDbConnection();			
			nwindConn.ConnectionString = Properties.Resources.ConnectionString;			

			//Run the SQL command.
			//
			OleDbCommand selectCMD = new OleDbCommand("SELECT * FROM Invoices ORDER BY Customers.CompanyName, OrderID", nwindConn);
			selectCMD.CommandTimeout = 30;

			//Data adapter used to run the select command
			//
			OleDbDataAdapter invoicesDA = new OleDbDataAdapter();
			invoicesDA.SelectCommand = selectCMD;

			//Fill the DataSet.
			//
			invoicesDA.Fill(invoiceData);
			nwindConn.Close();

			//Create a DataView and assign the selected CompanyName RowFilter.
			//  
			DataView invoiceDataView = new DataView(invoiceData);
			invoiceDataView.RowFilter = "Customers.CompanyName='" + Convert.ToString(cbCompanyName.SelectedItem).Replace("'", "''") + "'";

			//Create the report and assign the data source
			//
			var rpt = new ActiveReports.SectionReport();
			rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\",Properties.Resources.ReportName)));
			rpt.DataSource = invoiceDataView;
			//Run and view the report
			//
			arvMain.LoadDocument(rpt);		

			Cursor = Cursors.Arrow;
		}

		/// <summary>
		/// Illustrates using a DataReader as a data source.
		/// 
		/// </summary>
		private void btnDataReader_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;

			//Database connection populated from the sample Northwind access database
			//
			OleDbConnection nwindConn = new OleDbConnection();
			nwindConn.ConnectionString = Properties.Resources.ConnectionString;

			//Run the SQL command.
			//
			OleDbCommand selectCMD = new OleDbCommand("SELECT * FROM Invoices ORDER BY Customers.CompanyName, OrderID", nwindConn);
			selectCMD.CommandTimeout = 30;

			//DataReader used to read the data
			//
			OleDbDataReader invoiceDataReader;

			//Open the database connection and execute the reader
			//
			nwindConn.Open();
			invoiceDataReader = selectCMD.ExecuteReader();

			//Create the report and assign the data source.
			//
			var rpt = new ActiveReports.SectionReport();
			rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\",Properties.Resources.ReportName)));
			rpt.DataSource = invoiceDataReader;
			//Run and view the report
			//
			arvMain.Document = rpt.Document;
						rpt.Run(false);
			
			//Close the database connection
			//
			nwindConn.Close();

			Cursor = Cursors.Arrow;
		}

		/// <summary>
		/// Illustrates using a GrapeCity OleDb object as a data source
		/// 
		/// </summary>
		private void btnOleDb_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;

			//OleDb Data Source object to use.
			//
			Data.OleDBDataSource oleDb = new Data.OleDBDataSource();
			
			//Assign the database connection string from the sample Northwind access database
			//
			oleDb.ConnectionString = Properties.Resources.ConnectionString;
			
			//Run the SQL command.
			//
			oleDb.SQL = "SELECT * FROM Invoices ORDER BY Customers.CompanyName, OrderID";

			//Create the report and assign the data source
			//
			var rpt = new ActiveReports.SectionReport();
			rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\",Properties.Resources.ReportName)));
			rpt.DataSource = oleDb;
			//Run and view the report
			//
			arvMain.LoadDocument(rpt);

			Cursor = Cursors.Arrow;
		}

		/// <summary>
		/// Illustrates using a GrapeCity SqlServer object as a data source.
		/// 
		/// </summary>
		private void btnSqlServer_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;

			//Verify that a SQL Server has been selected
			//
			if (cbSqlServerList.SelectedItem == null)
			{
				MessageBox.Show(Properties.Resources.SelectSQLServer);
				Cursor = Cursors.Arrow;
				return;
			}

			//SqlServer Data Source object to use
			//
			Data.SqlDBDataSource sql = new Data.SqlDBDataSource();
			
			//Assign the database connection string based on the SQL Server selected
			//
			sql.ConnectionString = "Data Source=" + cbSqlServerList.SelectedItem + ";Initial Catalog=Northwind;Integrated Security=SSPI";
			//Run the SQL command.
			//
			sql.SQL = "SELECT * FROM invoices ORDER BY CustomerID, OrderID";

			//Create the report and assign the data source.
			//
			var rpt = new ActiveReports.SectionReport();
			rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\",Properties.Resources.ReportName)));
			rpt.DataSource = sql;
			//Run and view the report
			//
			try
			{
				arvMain.LoadDocument(rpt);
			} 
			catch (System.Data.SqlClient.SqlException ex)
			{
				MessageBox.Show(ex.Message);
			}

			Cursor = Cursors.Arrow;		
		}

		/// <summary>
		/// Generates a DataSet and saves it as an XML data file.
		/// 
		/// </summary>
		private void btnGenerateXML_Click(object sender, EventArgs e)
		{
			//DataSet used to hold the Data.
			//
			DataSet invoiceData = new DataSet("Northwind");
			invoiceData.Locale = CultureInfo.InvariantCulture;

			//Database connection populated from the sample Northwind access database
			//
			OleDbConnection nwindConn = new OleDbConnection();
			nwindConn.ConnectionString = Properties.Resources.ConnectionString;			

			//SQL Select command to run against the database
			//
			OleDbCommand selectCMD = new OleDbCommand("SELECT * FROM Invoices ORDER BY Customers.CompanyName, OrderID", nwindConn);
			selectCMD.CommandTimeout = 30;

			//Data adapter used to run the select command
			//
			OleDbDataAdapter invoicesDA = new OleDbDataAdapter();
			invoicesDA.SelectCommand = selectCMD;

			//Fill the DataSet
			//
			invoicesDA.FillSchema(invoiceData, SchemaType.Source, "Invoices");
			invoiceData.Tables["Invoices"].Columns["OrderDate"].DataType = Type.GetType("System.String");
			invoiceData.Tables["Invoices"].Columns["ShippedDate"].DataType = Type.GetType("System.String");
			invoicesDA.Fill(invoiceData, "Invoices");

			//Initalize the Save Dialog Box
			//
			dlgSave.Title =  Properties.Resources.SaveDataAs;
			dlgSave.FileName = "Invoices.xml";
			dlgSave.Filter = Properties.Resources.Filter;
			if (dlgSave.ShowDialog() == DialogResult.OK)
			{
				btnXML.Enabled = false;
				//If valid name is returned, save out the DataSet to the specified filename
				//
				if (dlgSave.FileName.Length != 0)
				{
					//Format all date fields in the XML for the report
					//
					for (int x = 0; x < invoiceData.Tables["Invoices"].Rows.Count; x++)
					{
						invoiceData.Tables["Invoices"].Rows[x]["OrderDate"] = Convert.ToDateTime(invoiceData.Tables["Invoices"].Rows[x]["OrderDate"]).ToShortDateString();
						if (invoiceData.Tables["Invoices"].Rows[x]["ShippedDate"].GetType() != Type.GetType("System.DBNull"))
						{
							invoiceData.Tables["Invoices"].Rows[x]["ShippedDate"] = Convert.ToDateTime(invoiceData.Tables["Invoices"].Rows[x]["ShippedDate"]).ToShortDateString();
						}
					}
					invoiceData.WriteXml(dlgSave.FileName);
				}
				btnXML.Enabled = true;
			}
		}

		/// <summary>
		/// Illustrates using a GrapeCity XML object as a data source.
		/// 
		/// </summary>
		private void btnXML_Click(object sender, EventArgs e)
		{
			//Initialize the Open Dialog Box
			//
			
			dlgOpen.Title = Properties.Resources.OpenDataFile;
			dlgOpen.FileName = dlgSave.FileName;
			dlgOpen.Filter = Properties.Resources.Filter;

			if (dlgOpen.ShowDialog() == DialogResult.OK)
			{
				//If valid name is returned, open the data and run the report
				//
				if (dlgOpen.FileName.Length != 0)
				{
					Cursor = Cursors.WaitCursor;

					//XML Data Source object to use
					Data.XMLDataSource xml = new Data.XMLDataSource();

					//Assign the FileName for the selected XML data file
					xml.FileURL = dlgOpen.FileName;
					//Assign the Recordset Pattern
					xml.RecordsetPattern = @"//Northwind/Invoices";

					//Create the report and assign the data source
					var rpt = new ActiveReports.SectionReport();
					rpt.LoadLayout(XmlReader.Create(System.IO.Path.Combine("..\\..\\",Properties.Resources.ReportName)));
					rpt.DataSource = xml;
					//Run and view the report
					arvMain.LoadDocument(rpt);

					Cursor = Cursors.Arrow;
				}
			}
		}

		private void btnCSV_Click(object sender, EventArgs e)
		{
			const string settingForNoHeaderDelimited = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued";

			Cursor = Cursors.WaitCursor;
			//CSV Data Source object to use.
			Data.CsvDataSource csv = new Data.CsvDataSource();


			//Dataset encoding.
			string encoding = Properties.Resources.CSVEncoding;

			//Configure connection string by selected dataset type
			string connectionString = string.Empty;

			//Delimited Data: No header, column separator is comma
			if (rbtnNoHeaderComma.Checked)
			{
				connectionString = @"Path=" + Properties.Resources.CSVDataSetPathComma + ";" +
								   "Encoding=" + encoding + ";" +
								   "TextQualifier=\";" +
								   "ColumnsSeparator=,;" +
								   "RowsSeparator=\\r\\n;" +
								   "Columns=" + settingForNoHeaderDelimited;
			}
			//Delimited Data: Header exists, column separator is Tab
			else if (rbtnHeaderTab.Checked)
			{
				connectionString = @"Path=" + Properties.Resources.CSVDataSetPathHeaderTab + ";" +
								   "Encoding=" + encoding + ";" +
								   "TextQualifier=\";" +
								   "ColumnsSeparator=\t;" +
								   "RowsSeparator=\\r\\n;" +
								   "HasHeaders=True";

			}
			//Fixed width Data: Header exists
			else if (rbtnHeader.Checked)
			{
				connectionString = @"Path=" + Properties.Resources.CSVDataSetPathHeaderFixed + ";" +
								   "Encoding=" + encoding + ";" +
								   "Columns=" + _settingForHeaderExistsFixed + ";" +
								   "HasHeaders=True";
			}
			//Fixed width Data: No header
			else if (rbtnNoHeader.Checked)
			{
				connectionString = @"Path=" + Properties.Resources.CSVDataSetPathFixed + ";" +
								   "Encoding=" + encoding + ";" +
								   "Columns=" + _settingForNoHeaderFixed;
			}
			//Applying specified connection string to data source
			csv.ConnectionString = connectionString;

			//Create the report and assign the data source.
			ProductList productList = new ProductList
			{
				ResourceLocator = new DefaultResourceLocator(new Uri(Path.GetDirectoryName(Application.ExecutablePath) + "\\")),
				DataSource = csv
			};

			//Run and view the report
			arvMain.LoadDocument(productList);

			Cursor = Cursors.Arrow;
		}
	}
}
