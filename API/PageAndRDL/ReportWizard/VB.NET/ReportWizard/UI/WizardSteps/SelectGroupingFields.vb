Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports GrapeCity.ActiveReports.Samples.ReportWizard.MetaData
Namespace UI.WizardSteps
	Partial Public Class SelectGroupingFields
		Inherits BaseStep
		Private Class FieldMetaDataDragDropObject
			Public Shared ReadOnly Format As DataFormats.Format
			Shared Sub New()
				Format = DataFormats.GetFormat(GetType(FieldMetaDataDragDropObject).FullName)
			End Sub
			Public Sub New(ByVal data As FieldMetaData, ByVal source As Object)
				Me._data = data
				Me._source = source
			End Sub
			Private ReadOnly _data As FieldMetaData
			Public ReadOnly Property Data() As FieldMetaData
				Get
					Return _data
				End Get
			End Property
			Private ReadOnly _source As Object
			Public ReadOnly Property Source() As Object
				Get
					Return _source
				End Get
			End Property
		End Class
		Private Class FieldMetaDataTreeNode
			Inherits TreeNode
			Private ReadOnly _field As FieldMetaData
			Public Sub New(ByVal field As FieldMetaData)
				MyBase.New(field.Title)
				Me._field = field
			End Sub
			Public ReadOnly Property Field() As FieldMetaData
				Get
					Return _field
				End Get
			End Property
		End Class
		Private selectedReport As ReportMetaData
		Private ReadOnly selectedGroupFields As List(Of FieldMetaData)
		Private ReadOnly availableGroupFields As BindingList(Of FieldMetaData)
		Public Sub New()
			selectedGroupFields = New List(Of FieldMetaData)()
			availableGroupFields = New BindingList(Of FieldMetaData)()
			InitializeComponent()
		End Sub
		Protected Overloads Overrides Sub OnDisplay(ByVal firstDisplay As Boolean)
			If firstDisplay Then
				SetupDataBinding()
			End If
			If Not selectedReport Is ReportWizardState.SelectedMasterReport Then
				selectedReport = ReportWizardState.SelectedMasterReport
				isLastGroupDetail.Checked = False
				LoadAvailableFields()
			End If
		End Sub
		Private Sub SetupDataBinding()
			availableGroups.DataSource = availableGroupFields
			availableGroups.DisplayMember = "Title"
		End Sub
		Private Sub LoadAvailableFields()
			availableGroupFields.Clear()
			selectedGroups.Nodes.Clear()
			selectedGroupFields.Clear()
			For Each kvp As KeyValuePair(Of String, FieldMetaData) In selectedReport.Fields
				Dim field As FieldMetaData = kvp.Value
				If Not field.AllowTotaling Then
					availableGroupFields.Add(field)
				End If
			Next
		End Sub
		Private Sub selectedGroups_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles selectedGroups.DragDrop
			If e.Data.GetDataPresent(FieldMetaDataDragDropObject.Format.Name) Then
				Dim ddoField As FieldMetaDataDragDropObject = CType(e.Data.GetData(FieldMetaDataDragDropObject.Format.Name), FieldMetaDataDragDropObject)
				Dim localPoint As Point = selectedGroups.PointToClient(New Point(e.X, e.Y))
				'Find position of the drop
				Dim parentNode As FieldMetaDataTreeNode = CType(selectedGroups.GetNodeAt(localPoint), FieldMetaDataTreeNode)
				If Not (parentNode) Is Nothing Then
					Dim parentIndex As Integer = selectedGroupFields.IndexOf(parentNode.Field)
					If (ddoField.Source).Equals(selectedGroups) Then
						'need to remove existing node
						selectedGroupFields.Remove(ddoField.Data)
					End If
					selectedGroupFields.Insert(parentIndex, ddoField.Data)
				Else 'First node or not over a node
					If (ddoField.Source).Equals(selectedGroups) Then
						'need to remove existing node
						selectedGroupFields.Remove(ddoField.Data)
					End If
					selectedGroupFields.Add(ddoField.Data)
				End If
				RebuildSelectedGroupsTree()
				'Remove the old field from the list
				If (ddoField.Source).Equals(availableGroups) Then
					availableGroupFields.Remove(ddoField.Data)
				End If
			End If
		End Sub
		Private Sub RebuildSelectedGroupsTree()
			selectedGroups.BeginUpdate()
			selectedGroups.Nodes.Clear()
			Dim parentNode As FieldMetaDataTreeNode = Nothing
			For Each fieldMetaData As FieldMetaData In selectedGroupFields
				If (parentNode) Is Nothing Then
					parentNode = New FieldMetaDataTreeNode(fieldMetaData)
					selectedGroups.Nodes.Add(parentNode)
				Else
					Dim child As New FieldMetaDataTreeNode(fieldMetaData)
					parentNode.Nodes.Add(child)
					parentNode = child
				End If
			Next
			selectedGroups.EndUpdate()
			selectedGroups.ExpandAll()
		End Sub
		Private Sub availableGroups_GetDataObject(ByVal sender As Object, ByVal e As DragDropListBox.DataObjectEventArgs) Handles availableGroups.GetDataObject
			Dim index As Integer = availableGroups.IndexFromPoint(e.MouseLocation)
			If index < 0 Then
				e.DataObject = Nothing
				Return
			End If
			Dim data As FieldMetaData = CType(availableGroups.Items(index), FieldMetaData)
			e.DataObject = New FieldMetaDataDragDropObject(data, availableGroups)
		End Sub
		Private Sub availableGroups_GetDragEffects(ByVal sender As Object, ByVal e As DragDropListBox.DragDropEffectsEventArgs) Handles availableGroups.GetDragEffects
			e.DragDropEffects = DragDropEffects.Move
		End Sub
		Private Sub selectedGroups_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles selectedGroups.DragEnter
			Dim ddoField As FieldMetaDataDragDropObject = CType(e.Data.GetData(FieldMetaDataDragDropObject.Format.Name), FieldMetaDataDragDropObject)
			If Not (ddoField) Is Nothing AndAlso ((ddoField.Source).Equals(selectedGroups) OrElse (ddoField.Source).Equals(availableGroups)) Then
				e.Effect = DragDropEffects.Move
			Else
				e.Effect = DragDropEffects.None
			End If
		End Sub
		Private Sub selectedGroups_ItemDrag(ByVal sender As Object, ByVal e As ItemDragEventArgs) Handles selectedGroups.ItemDrag
			Dim node As FieldMetaDataTreeNode = TryCast(e.Item, FieldMetaDataTreeNode)
			If Not (node) Is Nothing Then
				selectedGroups.DoDragDrop(New FieldMetaDataDragDropObject(node.Field, selectedGroups), DragDropEffects.Move)
				Invalidate()
			End If
		End Sub
		Private Sub addGroup_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addGroup.Click
			Dim field As FieldMetaData = TryCast(availableGroups.SelectedItem, FieldMetaData)
			If Not (field) Is Nothing Then
				selectedGroupFields.Add(field)
				availableGroupFields.Remove(field)
				RebuildSelectedGroupsTree()
			End If
		End Sub
		Private Sub removeGroup_Click(ByVal sender As Object, ByVal e As EventArgs) Handles removeGroup.Click
			Dim selectedNode As FieldMetaDataTreeNode = TryCast(selectedGroups.SelectedNode, FieldMetaDataTreeNode)
			If Not (selectedNode) Is Nothing Then
				Dim field As FieldMetaData = selectedNode.Field
				selectedGroupFields.Remove(field)
				availableGroupFields.Add(field)
				RebuildSelectedGroupsTree()
				selectedGroups.Focus()
			End If
			removeGroup.Enabled = selectedGroups.SelectedNode IsNot Nothing
		End Sub
		Private Sub selectedGroups_DragOver(ByVal sender As Object, ByVal e As DragEventArgs) Handles selectedGroups.DragOver
			Dim ddoField As FieldMetaDataDragDropObject = CType(e.Data.GetData(FieldMetaDataDragDropObject.Format.Name), FieldMetaDataDragDropObject)
			If Not (ddoField) Is Nothing AndAlso (e.Effect).Equals(DragDropEffects.Move) Then
				Dim currentLocation As Point = Control.MousePosition
				currentLocation = selectedGroups.PointToClient(currentLocation)
				Dim node As TreeNode = selectedGroups.GetNodeAt(currentLocation)
				selectedGroups.Refresh()
				If Not (node) Is Nothing Then
					Dim nodeBounds As Rectangle = node.Bounds
					Dim start As New Point(nodeBounds.Left, nodeBounds.Top + 1)
					Dim [end] As New Point(nodeBounds.Right, nodeBounds.Top + 1)
					Using pen As New Pen(Color.Black, 2.0F)
						Using g As Graphics = selectedGroups.CreateGraphics()
							g.DrawLine(pen, start, [end])
						End Using
					End Using
				ElseIf selectedGroups.Nodes.Count > 0 Then
					Dim lastNode As TreeNode = selectedGroups.Nodes(selectedGroups.Nodes.Count - 1)
					While lastNode.Nodes.Count > 0
						lastNode = lastNode.Nodes(selectedGroups.Nodes.Count - 1)
					End While
					Dim nodeBounds As Rectangle = lastNode.Bounds
					Dim start As New Point(nodeBounds.Left, nodeBounds.Bottom)
					Dim [end] As New Point(nodeBounds.Right, nodeBounds.Bottom)
					Using pen As New Pen(Color.Black, 2.0F)
						Using g As Graphics = selectedGroups.CreateGraphics()
							g.DrawLine(pen, start, [end])
						End Using
					End Using
				End If
			End If
		End Sub
		Public Overloads Overrides Sub UpdateState()
			ReportWizardState.GroupingFields.Clear()
			If selectedGroupFields.Count > 0 Then
				Dim lastIndex As Integer = selectedGroupFields.Count
				If isLastGroupDetail.Checked Then
					lastIndex = lastIndex - 1
				End If
				For i As Integer = 0 To lastIndex - 1
					ReportWizardState.GroupingFields.Add(selectedGroupFields(i))
				Next
			End If
			If isLastGroupDetail.Checked AndAlso selectedGroupFields.Count > 0 Then
				ReportWizardState.DetailGroupingField = selectedGroupFields(selectedGroupFields.Count - 1)
			End If
		End Sub
		Private Sub selectedGroups_DragLeave(ByVal sender As Object, ByVal e As EventArgs) Handles selectedGroups.DragLeave
			selectedGroups.Refresh()
		End Sub
		Private Sub selectedGroups_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles selectedGroups.AfterSelect
			removeGroup.Enabled = selectedGroups.SelectedNode IsNot Nothing
		End Sub
	End Class
End Namespace
