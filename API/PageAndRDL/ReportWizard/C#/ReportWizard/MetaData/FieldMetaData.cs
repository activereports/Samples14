
namespace GrapeCity.ActiveReports.Samples.ReportWizard.MetaData
{
	public class FieldMetaData
	{
		private string _title;

		public FieldMetaData()
		{
			Name = "";
			Title = "";
			IsNumeric = false;
			AllowTotaling = false;
			FormatString = "";
			PreferredWidth = "1cm";
			SummaryFunction = "";
		}

		public string Name;

		// property for data binding purposes
		public string Title
		{
			get { return _title; }
			set { _title = value; }
		}

		public bool IsNumeric;
		public bool AllowTotaling;
		public string FormatString;
		public string PreferredWidth;
		public string SummaryFunction;
	}
}
