Imports System.IO
Imports System
Imports GrapeCity.ActiveReports.Extensibility.Rendering.IO

Friend NotInheritable Class FileStreamProvider
	Inherits StreamProvider
	Private ReadOnly _outputPath As String
	Private _primaryStream As StreamInfo

	Public Sub New(outputPath As String)
		_outputPath = outputPath
	End Sub

#Region "Overrides of StreamProvider"

	Public Overrides Function CreatePrimaryStream(mimeType As String, suggestedFileExtension As String) As StreamInfo
		_primaryStream = New FileStreamInfo(New Uri(_outputPath, UriKind.RelativeOrAbsolute), mimeType)
		Return _primaryStream
	End Function

	Public Overrides Function CreateSecondaryStream(relativeName As String, mimeType As String, suggestedFileExtension As String) As StreamInfo
		Throw New NotSupportedException()
	End Function

	Public Overrides Function GetPrimaryStream() As StreamInfo
		Return _primaryStream
	End Function

	Public Overrides Function GetSecondaryStreams() As StreamInfo()
		Throw New NotSupportedException()
	End Function

#End Region

	Private NotInheritable Class FileStreamInfo
		Inherits StreamInfo
		Public Sub New(uri As Uri, mimeType As String)
			MyBase.New(uri, mimeType)
		End Sub

		Public Overrides Function OpenStream() As Stream
			Return New FileStream(Uri.OriginalString, FileMode.Create, FileAccess.ReadWrite, FileShare.None)
		End Function
	End Class
End Class
