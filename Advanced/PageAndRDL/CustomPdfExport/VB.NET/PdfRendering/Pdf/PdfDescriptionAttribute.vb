Imports System.ComponentModel

Public Class PdfDescriptionAttribute
	Inherits DescriptionAttribute

	Public Sub New(description As String)
		DescriptionValue = My.Resources.ResourceManager.GetString(description)
	End Sub

End Class
