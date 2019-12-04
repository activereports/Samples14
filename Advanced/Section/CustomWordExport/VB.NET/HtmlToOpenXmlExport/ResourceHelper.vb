Imports System.Reflection
Imports System.IO
Imports System.Resources

Public Class ResourceHelper
	Public Shared Function GetString(resourceName As String) As String
		Return GetString(GetType(ResourceHelper).GetTypeInfo(), resourceName)
	End Function

	Public Shared Function GetString(typeInfo As TypeInfo, resourceName As String) As String
		Using stream = GetStream(typeInfo, resourceName)
			Using reader As New StreamReader(stream)
				Return reader.ReadToEnd()
			End Using
		End Using
	End Function

	Public Shared Function GetStream(resourceName As String) As Stream
		Return GetStream(GetType(ResourceHelper).GetTypeInfo(), resourceName)
	End Function

	Public Shared Function GetStream(typeInfo As TypeInfo, resourceName As String) As Stream
		Dim assembly = typeInfo.Assembly
		Dim stream = assembly.GetManifestResourceStream(typeInfo.Namespace + "." + resourceName)
		If stream Is Nothing Then
			Throw New MissingManifestResourceException("Requested resource " + resourceName + " was not found in the assembly " + assembly.ToString + ".")
		End If
		Return stream
	End Function
End Class
