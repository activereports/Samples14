Imports GrapeCity.ActiveReports.Extensibility
Imports System.Globalization
Imports System.IO
Imports System.Runtime.InteropServices

Public Class MyPicturesLocator
	Inherits ResourceLocator

	Const UriSchemeMyImages As String = "MyPictures:"

	'<summary>
	'Look for the resources in My Pictures folder. 
	'</summary>
	'<param name="resourceInfo">The information about the resource to be obtained. </param>
	'<returns>The resource, null if it was not found. </returns>
	Public Overrides Function GetResource(ByVal resourceInfo As ResourceInfo) As Resource
		Dim resource As Resource
		Dim name As String = resourceInfo.Name

		If (String.IsNullOrEmpty(name)) Then

			Throw New ArgumentException(My.Resources.ResourceNameIsNull, "name")

		End If

		Dim uri As New Uri(name)
		If (uri.GetLeftPart(UriPartial.Scheme).StartsWith(UriSchemeMyImages, True, CultureInfo.InvariantCulture)) Then

			Dim stream As Stream = GetPictureFromSpecialFolder(uri)
			If (stream Is Nothing) Then
				stream = New MemoryStream()
				My.Resources.NoImage.Save(stream, My.Resources.NoImage.RawFormat)
			End If
			resource = New Resource(stream, uri)

		Else
			Throw New InvalidOperationException(My.Resources.ResourceSchemeIsNotSupported)
		End If
		Return resource
	End Function

	Function GetPictureFromSpecialFolder(ByVal path As Uri) As Stream
		Dim startPathPos As Integer = UriSchemeMyImages.Length

		If (startPathPos >= path.ToString().Length) Then
			Return Nothing
		End If

		Dim pictureName As String = path.ToString().Substring(startPathPos)
		Dim myPicturesPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)

		If (Not myPicturesPath.EndsWith("\\")) Then
			myPicturesPath += "\\"
		End If

		Dim picturePath As String = System.IO.Path.Combine(myPicturesPath, pictureName)

		If (Not File.Exists(picturePath)) Then
			Return Nothing
		End If

		Dim stream As New MemoryStream()

		Try
			Dim picture As Image = Image.FromFile(picturePath)
			picture.Save(stream, picture.RawFormat)
			stream.Position = 0
		Catch ex As OutOfMemoryException ' The file is not valid image, or GDI+ doesn't support such images.
			Return Nothing
		Catch ex As ExternalException
			Return Nothing
		End Try

		Return stream
	End Function

End Class
