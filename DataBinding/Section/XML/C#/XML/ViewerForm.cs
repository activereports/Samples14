
namespace GrapeCity.ActiveReports.Samples.XML
{
	/// <summary>
	/// Viewer window for the XML based ActiveReport
	/// </summary>
	public partial class ViewerForm : System.Windows.Forms.Form
	{
		/// <summary>
		///Required designer variable.
		/// </summary>
		public ViewerForm()
		{
			//Required for Windows Form Designer support 
			InitializeComponent();
		}

		/// <summary>
		/// Set the document to the viewer on the form.
		/// </summary>
		public void LoadReport(SectionReport rpt)
		{
			arvMain.LoadDocument(rpt);
		}
	}
}
