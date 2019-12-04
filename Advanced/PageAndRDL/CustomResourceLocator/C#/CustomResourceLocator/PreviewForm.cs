using System;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Document;

namespace GrapeCity.ActiveReports.Samples.CustomResourceLocator
{
	public partial class PreviewForm : Form
	{
		private readonly PageDocument _reportRuntime;

		public PreviewForm()
		{
			InitializeComponent();
		}

		public PreviewForm(PageDocument runtime) : this()
		{
			_reportRuntime = runtime;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (_reportRuntime != null)
			{
				reportPreview.ReportViewer.LoadDocument(_reportRuntime);
			}
		}
	}
}
