Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Namespace UI
	Partial Public Class TipControl
		Inherits UserControl
		Public Sub New()
			InitializeComponent()
		End Sub
		<Browsable(True)> _
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
		Public Overloads Overrides Property Text() As String
			Get
				Return tipText.Text
			End Get
			Set(ByVal value As String)
				tipText.Text = Value
			End Set
		End Property
	End Class
End Namespace
