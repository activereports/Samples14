using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace GrapeCity.ActiveReports.Samples.LINQ
{
	public partial class ViewerForm : Form
	{
		private System.Resources.ResourceManager _manager;
		public ViewerForm()
		{
			_manager = new System.Resources.ResourceManager(typeof(ViewerForm));
			InitializeComponent();
		}
	  
		// Define a structure for LINQtoObject.
		private void ViewerForm_Load(object sender, EventArgs e)
		{
			// To generate a report.
			var rpt = new SectionReport();
			rpt.LoadLayout(XmlReader.Create("..\\..\\rptLINQtoObject.rpx"));
			// To run the report.
			arvMain.LoadDocument(rpt);
		}
	}
}
