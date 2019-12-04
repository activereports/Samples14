Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports GrapeCity.ActiveReports.Samples.ReportWizard.MetaData
Namespace UI.WizardSteps
	Partial Public Class SelectSummaryOptions
		Inherits BaseStep
		Public Sub New()
			InitializeComponent()
		End Sub
		Protected Overloads Overrides Sub OnDisplay(ByVal firstDisplay As Boolean)
			If firstDisplay Then
				selectedGroups.DisplayMember = "Title"
				outputFields.DisplayMember = "Title"

			End If
			masterReport.Text = ReportWizardState.SelectedMasterReport.Title
			Dim groups As New List(Of FieldMetaData)(ReportWizardState.GroupingFields)
			If ReportWizardState.GroupingFields IsNot Nothing Then
				If Not (ReportWizardState.DetailGroupingField) Is Nothing Then
					groups.Add(ReportWizardState.DetailGroupingField)
				End If
				Dim groupingFieldsbs As New BindingSource()
				groupingFieldsbs.DataSource = groups
				selectedGroups.DataSource = groupingFieldsbs
			End If
			If ReportWizardState.DisplayFields IsNot Nothing Then
				Dim displayFieldsbs As New BindingSource()
				displayFieldsbs.DataSource = ReportWizardState.DisplayFields
				outputFields.DataSource = displayFieldsbs
			End If
		End Sub
		Public Overloads Overrides Sub UpdateState()
			ReportWizardState.DisplayGrandTotals = grandTotal.Checked
			ReportWizardState.DisplayGroupSubtotals = subtotals.Checked
		End Sub
	End Class
End Namespace
