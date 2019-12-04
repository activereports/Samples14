using System;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace GrapeCity.ActiveReports.Samples.DataSetDataSource
{
	public partial class ViewerForm : Form
	{
		public ViewerForm()
		{
			InitializeComponent();
		}

		// Fill DataSet with Data
		DataTable LoadData()
		{
			var invoiceTable = new DataTable("Invoice");

			invoiceTable.Columns.Add("CustomerID", typeof(string));
			invoiceTable.Columns.Add("CompanyName", typeof(string));
			invoiceTable.Columns.Add("Address", typeof(string));
			invoiceTable.Columns.Add("PostalCode", typeof(int));
			invoiceTable.Columns.Add("ProductID", typeof(int));
			invoiceTable.Columns.Add("ProductName", typeof(string));
			invoiceTable.Columns.Add("QuantityPerUnit", typeof(int));
			invoiceTable.Columns.Add("Price", typeof(int));

			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 89, "Biscuit", 1, 10);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 12, "Coffee", 5, 20);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 567, "Meat", 8, 43);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 687, "Sushi", 12, 23);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 987, "Eggs", 23, 13);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 981, "Flakes", 1, 10);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 982, "Card", 2, 10);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 112, "Pins", 10, 1);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 129, "FootBall", 2, 17);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 123, "Matchstick", 22, 12);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 127, "Lighter", 1, 10);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 221, "Wine", 1, 130);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 132, "Apples", 10, 130);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 133, "Energy Drink", 2, 130);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 332, "Mapple Syrup", 8, 11);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 126, "Box Set", 2, 90);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 11, "DVD", 1, 13);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 112, "CD", 2, 12);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 34, "MP3 Player", 1, 1300);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 134, "Needle", 2, 89);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 456, "SoftDrink", 3, 23);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 83, "Jam", 1, 34);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 100, "Olives", 1, 78);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 189, "Oil", 3, 68);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 102, "Grapes", 3, 56);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 103, "Chair", 1, 900);
			invoiceTable.Rows.Add("ALFKI", "Amigo International", "Seoul", 110063, 107, "Bottle", 1, 67);

			return invoiceTable;
		}

		// Loads and shows the report.
		private void ViewerForm_Load(object sender, EventArgs e)
		{
			var rptPath = new FileInfo(@"..\..\Invoice2.rdlx");
			var definition = new PageReport(rptPath);
			definition.Document.LocateDataSource += new LocateDataSourceEventHandler(OnLocateDataSource);

			arvMain.LoadDocument(definition.Document);
		}

		// To connect to unbound data sources at run time,  the DataSet provider can be used with the LocateDataSource event. The reporting engine raises the LocateDataSource event when it needs input on the data to use.
		void OnLocateDataSource(object sender, LocateDataSourceEventArgs args)
		{
			args.Data = LoadData();
		}
	}
}
