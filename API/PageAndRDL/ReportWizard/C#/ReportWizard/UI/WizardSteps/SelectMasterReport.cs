using System;
using GrapeCity.ActiveReports.Samples.ReportWizard.MetaData;

namespace GrapeCity.ActiveReports.Samples.ReportWizard.UI.WizardSteps
{
	public partial class SelectMasterReport : BaseStep
	{
		public SelectMasterReport()
		{
			InitializeComponent();
		}

		protected override void OnDisplay( bool firstDisplay )
		{
			if( firstDisplay )
			{
				reportList.DataSource = Program.AvailableReportTemplates;
				reportList.DisplayMember = "Title";
				reportList.SelectedIndex = 0;
			}
		}

		public override void UpdateState()
		{
			ReportWizardState.SelectedMasterReport = (ReportMetaData) reportList.SelectedItem;
		}

		private void reportList_SelectedIndexChanged(object sender, EventArgs e)
		{
			ReportWizardState.DetailGroupingField = null;
		}
	}
}
