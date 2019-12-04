Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports GrapeCity.ActiveReports.Samples.ReportWizard.UI.WizardSteps
Namespace UI
	Partial Public Class WizardDialog
		Inherits Form
		Private ReadOnly currentState As ReportWizardState
		Private currentStep As BaseStep
		Private currentStepIndex As Integer
		Private ReadOnly _steps As List(Of BaseStep)
		Public Sub New(ByVal state As ReportWizardState)
			currentState = state
			_steps = New List(Of BaseStep)()
			InitializeComponent()
		End Sub
		Public ReadOnly Property Steps() As IList(Of BaseStep)
			Get
				Return _steps
			End Get
		End Property
		Protected Overloads Overrides Sub OnLoad(ByVal e As EventArgs)
			MyBase.OnLoad(e)
			SetStep(0)
		End Sub
		Private Sub SetStep(ByVal index As Integer)
			UpdateWizardState()
			If Not (currentStep) Is Nothing Then
				stepPanel.Controls.Remove(currentStep)
			End If
			currentStepIndex = index
			currentStep = steps(index)
			currentStep.ReportWizardState = currentState
			stepPanel.Controls.Add(currentStep)
			currentStep.OnDisplay()
			stepTitle.Text = currentStep.Title
			stepDescription.Text = currentStep.Description
			UpdateNavigationButtons()
		End Sub
		Private Sub UpdateWizardState()
			If Not (currentStep) Is Nothing Then
				currentStep.UpdateState()
			End If
		End Sub
		Private Sub UpdateNavigationButtons()
			back.Enabled = (currentStepIndex <> 0)

			Dim nextText As String = Resources.NextText
			If (currentStepIndex).Equals((Steps.Count - 1)) Then
				nextText = Resources.FinishText
			End If
			btnNext.Text = nextText
		End Sub
		Private Sub back_Click(ByVal sender As Object, ByVal e As EventArgs) Handles back.Click
			ShowPreviousStep()
		End Sub
		Private Sub ShowPreviousStep()
			If currentStepIndex > 0 Then
				SetStep(currentStepIndex - 1)
			End If
		End Sub
		Private Sub next_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNext.Click
			If currentStepIndex < Steps.Count Then
				If (currentStepIndex).Equals(Steps.Count - 1) Then
					FinishWizard()
				Else
					ShowNextStep()
				End If
			End If
		End Sub
		Private Sub ShowNextStep()
			SetStep(currentStepIndex + 1)
		End Sub
		Private Sub FinishWizard()
			UpdateWizardState()
			DialogResult = DialogResult.OK
			Close()
		End Sub
	End Class
End Namespace
