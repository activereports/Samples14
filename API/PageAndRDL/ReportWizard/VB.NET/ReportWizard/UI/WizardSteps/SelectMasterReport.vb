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
	Partial Public Class SelectMasterReport
		Inherits BaseStep
		Public Sub New()
			InitializeComponent()
		End Sub
		Protected Overloads Overrides Sub OnDisplay(ByVal firstDisplay As Boolean)
			If firstDisplay Then
				reportList.DataSource = ReportsForm.AvailableReportTemplates
				reportList.DisplayMember = "Title"

				reportList.SelectedIndex = 0
			End If
		End Sub
		Public Overloads Overrides Sub UpdateState()
			ReportWizardState.SelectedMasterReport = CType(reportList.SelectedItem, ReportMetaData)
		End Sub
		Private Sub reportList_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles reportList.SelectedIndexChanged
			ReportWizardState.DetailGroupingField = Nothing
		End Sub
	End Class
End Namespace
