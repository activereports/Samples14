using System.Windows.Forms;

namespace GrapeCity.ActiveReports.Samples.TestDesignerPro
{
	public partial class HelperForm : Form
	{
		public HelperForm()
		{
			InitializeComponent();
		}

		private void rtfHelp_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.LinkText);
		}

		private void HelperForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			Hide();
		}

		private void rtfHelp_TextChanged(object sender, System.EventArgs e)
		{

		}
	}
}
