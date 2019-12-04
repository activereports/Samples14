Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Namespace UI
	Public Class DragDropListBox
		Inherits ListBox
		Public Class DataObjectEventArgs
			Inherits EventArgs
			Private _dataObject As Object
			Private ReadOnly _mouseLocation As Point
			Public Sub New(ByVal pt As Point)
				_mouseLocation = pt
			End Sub
			Public ReadOnly Property MouseLocation() As Point
				Get
					Return _mouseLocation
				End Get
			End Property
			Public Property DataObject() As Object
				Get
					Return _dataObject
				End Get
				Set(ByVal value As Object)
					_dataObject = value
				End Set
			End Property
		End Class
		Public Class DragDropEffectsEventArgs
			Inherits EventArgs
			Private effects As DragDropEffects = System.Windows.Forms.DragDropEffects.None
			Public Property DragDropEffects() As DragDropEffects
				Get
					Return effects
				End Get
				Set(ByVal value As DragDropEffects)
					effects = Value
				End Set
			End Property
		End Class
		Public Class DragDropEventArgs
			Inherits EventArgs
			Private ReadOnly _allowedEffects As DragDropEffects
			Private ReadOnly _effects As DragDropEffects
			Private ReadOnly _mouseLocation As Point
			Private ReadOnly _data As Object
			Public Sub New(ByVal data As Object, ByVal mouseLocation As Point, ByVal allowedEffects As DragDropEffects, ByVal effects As DragDropEffects)
				Me._data = data
				Me._mouseLocation = mouseLocation
				Me._allowedEffects = allowedEffects
				Me._effects = effects
			End Sub
			Public ReadOnly Property Data() As Object
				Get
					Return _data
				End Get
			End Property
			Public ReadOnly Property MouseLocation() As Point
				Get
					Return _mouseLocation
				End Get
			End Property
			Public ReadOnly Property Effects() As DragDropEffects
				Get
					Return _effects
				End Get
			End Property
			Public ReadOnly Property AllowedEffects() As DragDropEffects
				Get
					Return _allowedEffects
				End Get
			End Property
		End Class
		Public Class AllowDropEventArgs
			Inherits EventArgs
			Public AllowDrop As Boolean
		End Class
		Private Shared ReadOnly NotDragging As New Point(-1, -1)
		Private startDragLocation As Point

		<Browsable(True)> _
		<Category("Drag Drop")>
		<Description("Raised when the control needs to know what data should be dragged from the control.")>
		Public Event GetDataObject As EventHandler(Of DataObjectEventArgs)

		Protected Overridable Sub OnGetDataObject(ByVal e As DataObjectEventArgs)
			'Dim handler As EventHandler(Of DataObjectEventArgs) = AddressOf GetDataObject
		   RaiseEvent GetDataObject(Me, e)
		End Sub

		<Browsable(True)> _
		<Category("Drag Drop")> _
		<Description("Raised when the control needs to know what effects are allowed.")> _
		Public Event GetDragEffects As EventHandler(Of DragDropEffectsEventArgs)

		Protected Overridable Sub OnGetDragEffects(ByVal e As DragDropEffectsEventArgs)
			RaiseEvent GetDragEffects(Me, e)
		End Sub

		<Browsable(True)> _
		<Category("Drag Drop")>
		<Description("Raised when the drag and drop operation has completed.")>
		Public Event DragCompleted As EventHandler(Of DragDropEventArgs)

		Protected Overridable Sub OnDragCompleted(ByVal e As DragDropEventArgs)
			RaiseEvent DragCompleted(Me, e)
		End Sub

		<Browsable(True)> _
		<Category("Drag Drop")> _
		<Description("Raised when a drag operation is over the control to determine whether the drop is allowed")> _
		Public Event AllowDropOperation As EventHandler(Of AllowDropEventArgs)


		Protected Overridable Sub OnAllowDropOperation(ByVal e As AllowDropEventArgs)
			RaiseEvent AllowDropOperation(Me, e)
		End Sub
		Protected Overloads Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
			If IsLeftMouseButton(e.Button) Then
				startDragLocation = e.Location
			End If
			MyBase.OnMouseDown(e)
		End Sub
		Protected Overloads Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
			If IsLeftMouseButton(e.Button) Then
				startDragLocation = NotDragging
			End If
			MyBase.OnMouseUp(e)
		End Sub
		Protected Overloads Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
			MyBase.OnMouseMove(e)
			If IsLeftMouseButton(e.Button) AndAlso Not (startDragLocation).Equals(NotDragging) Then
				'' Last check to see if we've moved far enough to begin the drag/drop
				Dim sz As New Size(Math.Abs(startDragLocation.X - e.Location.X), Math.Abs(startDragLocation.Y - e.Location.Y))
				If sz.Height > SystemInformation.DragSize.Height OrElse sz.Width > SystemInformation.DragSize.Width Then
					BeginDragDrop(startDragLocation)
				End If
			End If
		End Sub
		Private Sub BeginDragDrop(ByVal startingPoint As Point)
			Dim e As New DataObjectEventArgs(startingPoint)
			OnGetDataObject(e)
			Dim data As Object = e.DataObject
			If (data) Is Nothing Then
				Return
			End If
			Dim de As New DragDropEffectsEventArgs()
			OnGetDragEffects(de)
			Dim allowedEffects As DragDropEffects = de.DragDropEffects
			Dim effects As DragDropEffects = DoDragDrop(data, allowedEffects)
			Dim finishedArgs As New DragDropEffectsEventArgs()
			finishedArgs.DragDropEffects = effects
			Dim ddea As New DragDropEventArgs(data, startingPoint, allowedEffects, effects)
			OnDragCompleted(ddea)
		End Sub
		Private Shared Function IsLeftMouseButton(ByVal buttons As MouseButtons) As Boolean
			Return 0 <> (buttons And MouseButtons.Left)
		End Function

		Private Sub InitializeComponent()
			Me.SuspendLayout()
			Me.ResumeLayout(False)

		End Sub
	End Class
End Namespace
