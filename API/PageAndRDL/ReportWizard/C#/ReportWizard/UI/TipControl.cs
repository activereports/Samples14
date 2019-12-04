using System.ComponentModel;
using System.Windows.Forms;

namespace GrapeCity.ActiveReports.Samples.ReportWizard.UI 
{
	public partial class TipControl : UserControl
	{
		public TipControl()
		{
			InitializeComponent();
		}

		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text
		{
			get
			{
				return tipText.Text;
			}
			set
			{
				tipText.Text = value;
			}
		}
	}
}
