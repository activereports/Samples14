using System.ComponentModel;
using System.Windows.Forms;

namespace GrapeCity.ActiveReports.Samples.ReportWizard.UI.WizardSteps
{
	public partial class BaseStep : UserControl
	{
		private bool _firstDisplay;
		private string _stepTitle;
		private string _stepDescription;
		private ReportWizardState _wizState;

		public BaseStep()
		{
			_firstDisplay = true;
			_stepTitle = "";
			_stepDescription = "";
			InitializeComponent();
		}

		[Browsable(false)]
		public ReportWizardState ReportWizardState
		{
			get { return _wizState; }
			set { _wizState = value; }
		}

		[Browsable(true)]
		[Description("The title to display in the wizard")]
		[Category("Appearance")]
		[DefaultValue("")]
		public string Title
		{
			get { return _stepTitle; }
			set { _stepTitle = value; }
		}

		[Browsable(true)]
		[Description("The text to display describing what to do on this step")]
		[Category("Appearance")]
		[DefaultValue("")]
		public string Description
		{
			get { return _stepDescription; }
			set { _stepDescription = value; }
		}

		public void OnDisplay()
		{
			OnDisplay(_firstDisplay);
			_firstDisplay = false;
		}

		protected virtual void OnDisplay(bool firstDisplay)
		{
		}

		/// <summary>
		///	Called before the wizard moves to the previous or next step
		/// </summary>
		public virtual void UpdateState()
		{
		}
	}
}
