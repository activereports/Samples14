
using System.Resources;

namespace GrapeCity.ActiveReports.Sample.Inheritance
{
	/// <summary>
	/// A description of the overview of the rptDesignChild.
	/// </summary>
	public partial class rptDesignChild : Inheritance.rptDesignBase
	{
		private ResourceManager _resource;
		public rptDesignChild()
		{
			_resource = new ResourceManager(typeof(rptDesignChild));
			//
			// Designer support is required to ActiveReport.
			//

			ReportStart += new System.EventHandler(this.rptDesignChild_ReportStart);
			InitializeComponent();
		}

		private void rptDesignChild_ReportStart(object sender, System.EventArgs e)
		{
			GrapeCity.ActiveReports.Data.OleDBDataSource oleDBDataSource1 = new GrapeCity.ActiveReports.Data.OleDBDataSource();
			oleDBDataSource1.ConnectionString = _resource.GetString("ConnectionString");
			oleDBDataSource1.SQL = "Select * from Customers Order By Val(CustomerID)";
			this.DataSource = oleDBDataSource1;
		}
	}
}
