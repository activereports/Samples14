Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing.Design
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows.Forms.Design

Public NotInheritable Class QueryEditor
	Inherits UITypeEditor

	Public Overloads Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As UITypeEditorEditStyle
		Return UITypeEditorEditStyle.DropDown
	End Function

	Public Overloads Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
		Dim edSvc As IWindowsFormsEditorService = CType(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
		Dim fileSelector As New CSVFileSelector()
		edSvc.DropDownControl(fileSelector)
		Dim csvFileName As String = fileSelector.CSVFileName
		If String.IsNullOrEmpty(csvFileName) Then
			Return String.Empty
		End If
		If Not File.Exists(csvFileName) Then
			Return String.Empty
		End If
		Return GetCSVQuery(csvFileName)
	End Function

	Private Shared Function GetCSVQuery(ByVal csvFileName As String) As String
		Dim sr As StreamReader = Nothing
		Try
			sr = New StreamReader(csvFileName)
			Dim ret As String = String.Empty
			Dim currentLine As String
			Dim line As Integer = 0

			currentLine = sr.ReadLine()
			While (currentLine) IsNot Nothing
				If (line).Equals(0) Then
					ret += ProcessColumnsDefinition(currentLine) + vbCr & vbLf
				Else
					ret += currentLine + vbCr & vbLf
				End If
				line = line + 1
				currentLine = sr.ReadLine()
			End While
			Return ret
		Catch generatedExceptionName As IOException
			Return String.Empty
		Finally
			If Not (sr) Is Nothing Then
				sr.Close()
			End If
		End Try
	End Function

	Private Shared Function ProcessColumnsDefinition(ByVal line As String) As String
		Const ColumnWithDataTypeRegex As String = "[""]?\w+[\""]?\(.+\)"
		Dim columns As String() = line.Split(New String() {","}, StringSplitOptions.None)
		Dim ret As String = Nothing
		For Each column As String In columns
			If Not String.IsNullOrEmpty(ret) Then
				ret += ","
			End If
			If Not Regex.Match(column, ColumnWithDataTypeRegex).Success Then
				ret += column + "(string)"
			Else
				ret += column
			End If
		Next
		Return ret
	End Function

End Class
