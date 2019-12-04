using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Samples.ReportWizard.UI.WizardSteps;

namespace GrapeCity.ActiveReports.Samples.ReportWizard.UI
{
	public partial class WizardDialog : Form
	{
		private readonly ReportWizardState currentState;
		private BaseStep currentStep;
		private int currentStepIndex;
		private readonly List<BaseStep> steps;

		public WizardDialog( ReportWizardState state )
		{
			currentState = state;
			steps = new List<BaseStep>();
			InitializeComponent();
		}

		public IList<BaseStep> Steps
		{
			get { return steps; }
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			SetStep(0);
		}

		private void SetStep(int index)
		{
			UpdateWizardState();
			if (currentStep != null)
			{
				stepPanel.Controls.Remove(currentStep);
			}
			currentStepIndex = index;
			currentStep = steps[index];
			currentStep.ReportWizardState = currentState;
			stepPanel.Controls.Add(currentStep);
			currentStep.OnDisplay();
			stepTitle.Text = currentStep.Title;
			stepDescription.Text = currentStep.Description;
			UpdateNavigationButtons();
		}

		private void UpdateWizardState()
		{
			if (currentStep != null)
				currentStep.UpdateState();
		}

		private void UpdateNavigationButtons()
		{
			back.Enabled = (currentStepIndex != 0);
			string nextText = Properties.Resources.NextText;

			if (currentStepIndex == (steps.Count - 1))
			{
				 nextText = Properties.Resources.FinishText;
			}
			next.Text = nextText;
		}

		private void back_Click(object sender, EventArgs e)
		{
			ShowPreviousStep();
		}

		private void ShowPreviousStep()
		{
			if (currentStepIndex > 0)
			{
				SetStep(currentStepIndex - 1);
			}
		}

		private void next_Click(object sender, EventArgs e)
		{
			if (currentStepIndex < steps.Count)
			{
				if (currentStepIndex == steps.Count - 1)
				{
					FinishWizard();
				}
				else
				{
					ShowNextStep();
				}
			}
		}

		private void ShowNextStep()
		{
			SetStep(currentStepIndex + 1);
		}

		private void FinishWizard()
		{
			UpdateWizardState();
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
