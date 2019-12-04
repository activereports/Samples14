Imports System.ComponentModel

Public NotInheritable Class PdfDisplayNameAttribute
	Inherits DisplayNameAttribute

	Public Sub New(displayName As String)
		DisplayNameValue = My.Resources.ResourceManager.GetString(displayName)
	End Sub


End Class
