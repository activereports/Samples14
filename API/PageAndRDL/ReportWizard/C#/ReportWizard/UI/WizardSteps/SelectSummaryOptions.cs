using System.Collections.Generic;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Samples.ReportWizard.MetaData;

namespace GrapeCity.ActiveReports.Samples.ReportWizard.UI.WizardSteps
{
	public partial class SelectSummaryOptions : GrapeCity.ActiveReports.Samples.ReportWizard.UI.WizardSteps.BaseStep
	{
		public SelectSummaryOptions()
		{
			InitializeComponent();
		}

		protected override void OnDisplay( bool firstDisplay )
		{
			if (firstDisplay)
			{
				selectedGroups.DisplayMember = "Title";
				outputFields.DisplayMember = "Title";
			}
			
			masterReport.Text = ReportWizardState.SelectedMasterReport.Title;
			List<FieldMetaData> groups = new List<FieldMetaData>(ReportWizardState.GroupingFields);
			if (ReportWizardState.GroupingFields != null)
			{
				if (ReportWizardState.DetailGroupingField != null)
				{
					groups.Add(ReportWizardState.DetailGroupingField);
				}
				BindingSource groupingfieldsbs = new BindingSource();
				groupingfieldsbs.DataSource = groups;
				selectedGroups.DataSource = groupingfieldsbs;
				selectedGroups.DisplayMember = "Title";
			}
			if (ReportWizardState.DisplayFields != null)
			{
				BindingSource outputfieldsbs = new BindingSource();
				outputfieldsbs.DataSource = ReportWizardState.DisplayFields;
				outputFields.DataSource = outputfieldsbs;
				outputFields.DisplayMember = "Title";
			}
		}

		public override void UpdateState()
		{
			ReportWizardState.DisplayGrandTotals = grandTotal.Checked;
			ReportWizardState.DisplayGroupSubtotals = subtotals.Checked;
		}
	}
}
