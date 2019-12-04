Imports System.Text
Imports System.Windows.Forms

Imports GrapeCity.ActiveReports.Samples.Rtf.Native

Namespace Control
	Public Class RichTextBoxFixed
		Inherits RichTextBox

		Public Overrides Property Text As String
			Get
				Dim textLength As Integer = GetTextLength(GTL_USECRLF)
				Dim getText = New GETTEXTEX()
				getText.cb = textLength + 2
				getText.flags = GT_USECRLF
				getText.codepage = 1200
				Dim sb = New StringBuilder(CType(getText.cb / 2, Integer))
				UnsafeNativeMethods.SendMessage(Handle, EM_GETTEXTEX, getText, sb)
				Return sb.ToString()
			End Get

			Set(ByVal value As String)
				MyBase.Text = value
			End Set
		End Property

		Public Overrides ReadOnly Property TextLength As Integer
			Get
				Return GetTextLength(GTL_USECRLF) / 2
			End Get
		End Property

		Public Overrides Property SelectionLength As Integer
			Get
				Dim charRange = New CHARRANGE()
				UnsafeNativeMethods.SendMessage(Handle, EM_EXGETSEL, 0, charRange)
				Return charRange.cpMax - charRange.cpMin
			End Get

			Set(ByVal value As Integer)
				MyBase.SelectionLength = value
			End Set
		End Property

		Private Function GetTextLength(ByVal gtl As Integer) As Integer
			Dim getLength = New GETTEXTLENGTHEX()
			getLength.flags = gtl
			getLength.codepage = 1200
			Return UnsafeNativeMethods.SendMessage(Handle, EM_GETTEXTLENGTHEX, getLength, 0)
		End Function
	End Class
End NameSpace