Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows.Forms
Imports GrapeCity.ActiveReports.Samples.ReportWizard.MetaData
Namespace UI.WizardSteps
	Partial Public Class SelectOutputFields
		Inherits BaseStep
		Private Class SourcedFieldMetaData
			Public Shared ReadOnly Format As DataFormats.Format
			Shared Sub New()
				Format = DataFormats.GetFormat(GetType(SourcedFieldMetaData).FullName)
			End Sub
			Public Field As FieldMetaData
			Public Source As Object
		End Class
		Private ReadOnly availableFields As BindingList(Of FieldMetaData)
		Private ReadOnly selectedFields As BindingList(Of FieldMetaData)
		Private report As ReportMetaData
		Public Sub New()
			report = Nothing
			availableFields = New BindingList(Of FieldMetaData)()
			selectedFields = New BindingList(Of FieldMetaData)()
			InitializeComponent()
		End Sub
		Protected Overloads Overrides Sub OnDisplay(ByVal firstDisplay As Boolean)
			If Not report Is ReportWizardState.SelectedMasterReport Then
				report = ReportWizardState.SelectedMasterReport
				LoadFields()
			End If
		End Sub
		Public Overloads Overrides Sub UpdateState()
			ReportWizardState.DisplayFields.Clear()
			ReportWizardState.DisplayFields.AddRange(selectedFields)
		End Sub
		Private Sub LoadFields()
			availableFields.Clear()
			selectedFields.Clear()
			For Each keyValuePair As KeyValuePair(Of String, FieldMetaData) In report.Fields
				availableFields.Add(keyValuePair.Value)
			Next
			If (availableOutputFields.DataSource) Is Nothing Then

				availableOutputFields.DisplayMember = "Title"

				'' availableOutputFields 
				availableOutputFields.DataSource = availableFields
			End If
			If (selectedOutputFields.DataSource) Is Nothing Then
				selectedOutputFields.DisplayMember = "Title"

				selectedOutputFields.DataSource = selectedFields
			End If
		End Sub
		Private Sub availableOutputFields_GetDataObject(ByVal sender As Object, ByVal e As DragDropListBox.DataObjectEventArgs) Handles availableOutputFields.GetDataObject
			Dim index As Integer = availableOutputFields.IndexFromPoint(e.MouseLocation)
			If index < 0 Then
				e.DataObject = Nothing
				Return
			End If
			Dim data As New SourcedFieldMetaData()
			data.Field = availableFields(index)
			data.Source = availableOutputFields
			e.DataObject = data
		End Sub
		Private Sub availableOutputFields_GetDragEffects(ByVal sender As Object, ByVal e As DragDropListBox.DragDropEffectsEventArgs) Handles availableOutputFields.GetDragEffects
			e.DragDropEffects = DragDropEffects.Move
		End Sub
		Private Sub selectedOutputFields_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles selectedOutputFields.DragDrop
			Dim ddoField As SourcedFieldMetaData = CType(e.Data.GetData(SourcedFieldMetaData.Format.Name), SourcedFieldMetaData)
			If Not (ddoField) Is Nothing Then
				AddFieldToSelected(ddoField.Field)
			End If
		End Sub
		Private Sub AddFieldToSelected(ByVal field As FieldMetaData)
			selectedFields.Add(field)
			availableFields.Remove(field)
		End Sub
		Private Sub RemoveFieldFromSelected(ByVal field As FieldMetaData)
			availableFields.Add(field)
			selectedFields.Remove(field)
		End Sub
		Private Sub selectedOutputFields_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles selectedOutputFields.DragEnter
			Dim ddoField As SourcedFieldMetaData = CType(e.Data.GetData(SourcedFieldMetaData.Format.Name), SourcedFieldMetaData)
			If Not (ddoField) Is Nothing Then
				e.Effect = DragDropEffects.Move
			End If
		End Sub
		Private Sub addOutputField_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addOutputField.Click
			Dim field As FieldMetaData = TryCast(availableOutputFields.SelectedItem, FieldMetaData)
			If Not (field) Is Nothing Then
				AddFieldToSelected(field)
			End If
		End Sub
		Private Sub removeOutputField_Click(ByVal sender As Object, ByVal e As EventArgs) Handles removeOutputField.Click
			Dim field As FieldMetaData = TryCast(selectedOutputFields.SelectedItem, FieldMetaData)
			If Not (field) Is Nothing Then
				RemoveFieldFromSelected(field)
			End If
			removeOutputField.Enabled = (selectedOutputFields.SelectedIndices.Count > 0)
		End Sub
		Private Sub OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles selectedOutputFields.SelectedIndexChanged, availableOutputFields.SelectedIndexChanged
			addOutputField.Enabled = (availableOutputFields.SelectedIndices.Count > 0)
			removeOutputField.Enabled = (selectedOutputFields.SelectedIndices.Count > 0)
		End Sub
	End Class
End Namespace
