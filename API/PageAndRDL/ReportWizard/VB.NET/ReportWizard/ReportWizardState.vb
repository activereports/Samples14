Imports System.Collections.Generic
Imports GrapeCity.ActiveReports.Samples.ReportWizard.MetaData

Public Class ReportWizardState
	Public Sub New()
		AvailableMasterReports = New List(Of ReportMetaData)()
		GroupingFields = New List(Of FieldMetaData)()
		DisplayFields = New List(Of FieldMetaData)()
	End Sub

	Public ReadOnly AvailableMasterReports As List(Of ReportMetaData)
	Public SelectedMasterReport As ReportMetaData
	Public ReadOnly GroupingFields As List(Of FieldMetaData)
	Public DetailGroupingField As FieldMetaData
	Public ReadOnly DisplayFields As List(Of FieldMetaData)
	Public DisplayGroupSubtotals As Boolean
	Public DisplayGrandTotals As Boolean

End Class
