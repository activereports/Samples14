
namespace GrapeCity.ActiveReports.Samples.IListBinding
{
	/// <summary>
	/// Summary description for ViewerForm.
	/// </summary>
	public partial class ViewerForm : System.Windows.Forms.Form
	{
		public ViewerForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		public void LoadDocument(SectionReport rpt)
		{
			arvMain.LoadDocument(rpt);
		}
	}
}
