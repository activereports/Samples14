using System.Collections.Generic;
using GrapeCity.ActiveReports.Samples.ReportWizard.MetaData;

namespace GrapeCity.ActiveReports.Samples.ReportWizard 
{
	public class ReportWizardState
	{
		public ReportWizardState()
		{
			AvailableMasterReports = new List<ReportMetaData>();
			GroupingFields = new List<FieldMetaData>();
			DisplayFields = new List<FieldMetaData>();
		}

		public readonly List<ReportMetaData> AvailableMasterReports;
		
		public ReportMetaData SelectedMasterReport;
		public readonly List<FieldMetaData> GroupingFields;
		public FieldMetaData DetailGroupingField;
		public readonly List<FieldMetaData> DisplayFields;
		public bool DisplayGroupSubtotals;
		public bool DisplayGrandTotals;
	}
}
