Imports System.Drawing
Imports System.Windows.Forms
Imports GrapeCity.ActiveReports.Samples.Rtf.Control

Imports GrapeCity.ActiveReports.Samples.Rtf.Native

Public Class RtfEditor
	Inherits RichTextBoxFixed

	Private _designer As RtfDesigner
	Private _isDeactivating As Boolean
	Public Property IsActive As Boolean

	Public Sub New(ByVal designer As RtfDesigner)
		MyBase.New()
		_designer = designer
	End Sub

	Public Sub Activate()
		If Not _designer.Controls.Contains(Me) Then
			_designer.Controls.Add(Me)
			AddHandler LostFocus, AddressOf EditorLostFocus
		End If

		SyncWithSource()
		Focus()
		IsActive = True
	End Sub

	Public Sub Deactivate(ByVal save As Boolean)
		If _isDeactivating Then Return
		_isDeactivating = True

		If save Then
			_designer.SetRtf(Rtf)
		Else
			_designer.SetRtf(_designer.GetRtf())

			While CanUndo
				Undo()
			End While
		End If

		_designer.Controls.Clear()
		_designer.RePaint()
		_designer.Select()

		IsActive = False
		_isDeactivating = False
	End Sub

	Public Overrides Function PreProcessMessage(ByRef msg As Message) As Boolean
		Dim e = CreateKeyEventArgs(msg)
		If msg.Msg = &H100 OrElse msg.Msg = &H104 Then OnKeyDown(e)
		Return e.Handled OrElse MyBase.PreProcessMessage(msg)
	End Function

	Protected Overrides Sub OnKeyDown(ByVal e As KeyEventArgs)
		MyBase.OnKeyDown(e)
		Dim command = GetEditorCommand(e.KeyData)

		If command IsNot Nothing Then
			command()
			e.Handled = True
		End If
	End Sub

	Private Function CreateKeyEventArgs(ByVal msg As Message) As KeyEventArgs
		Dim keyData = msg.WParam.ToInt32()
		Dim keyCode = keyData And CInt(Keys.KeyCode)
		Dim keyModifier = 0

		If (GetKeyState(Keys.ShiftKey)) < 0 Then
			keyModifier = keyModifier Or CInt(Keys.Shift)
		End If

		If GetKeyState(Keys.ControlKey) < 0 Then
			keyModifier = keyModifier Or CInt(Keys.Control)
		End If

		If GetKeyState(Keys.Menu) < 0 Then
			keyModifier = keyModifier Or CInt(Keys.Alt)
		End If

		Return New KeyEventArgs(CType((keyCode Or keyModifier), Keys))
	End Function

	Private Sub EditorLostFocus(ByVal sender As Object, ByVal e As EventArgs)
		RemoveHandler LostFocus, AddressOf EditorLostFocus
		Deactivate(True)
	End Sub

	Private Sub SyncWithSource()
		Rtf = _designer.GetRtf()
		Size = _designer.Size
		Location = Point.Empty
		Margin = Padding.Empty
		BorderStyle = BorderStyle.None
	End Sub

	Private Function GetEditorCommand(ByVal keys As Keys) As Action
		Select Case keys
			Case Keys.Control Or Keys.A
				Return AddressOf SelectAll

			Case Keys.Control Or Keys.C
				Return AddressOf Copy

			Case Keys.Control Or Keys.X
				Return AddressOf Cut

			Case Keys.Control Or Keys.V
				Return AddressOf Paste

			Case Keys.Control Or Keys.B
				Return Sub()
						   SelectionFont = New Font(SelectionFont, SelectionFont.Style Xor FontStyle.Bold)
					   End Sub

			Case Keys.Control Or Keys.I
				Return Sub()
						   SelectionFont = New Font(SelectionFont, SelectionFont.Style Xor FontStyle.Italic)
					   End Sub

			Case Keys.Control Or Keys.U
				Return Sub()
						   SelectionFont = New Font(SelectionFont, SelectionFont.Style Xor FontStyle.Underline)
					   End Sub

			Case Keys.Control Or Keys.T
				Return Sub()
						   SelectionFont = New Font(SelectionFont, SelectionFont.Style Xor FontStyle.Strikeout)
					   End Sub

			Case Keys.Control Or Keys.Z
				Return Sub()
						   If CanUndo Then Undo()
					   End Sub

			Case Keys.Control Or Keys.Shift Or Keys.Z
				Return Sub()
						   If CanRedo Then Redo()
					   End Sub

			Case Keys.Delete
				Return Sub()
						   SelectionLength = If(SelectionLength > 0, SelectionLength, 1)
						   SelectedText = String.Empty
					   End Sub

			Case Keys.Escape
				Return Sub()
						   Deactivate(save:=False)
					   End Sub

			Case Keys.Alt Or Keys.Enter
				Return Sub()
						   Deactivate(save:=True)
					   End Sub

			Case Else
				Return Nothing
		End Select
	End Function
End Class
