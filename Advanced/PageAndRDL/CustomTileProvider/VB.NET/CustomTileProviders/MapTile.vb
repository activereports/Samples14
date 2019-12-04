Imports System.IO
Imports GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map
Imports GrapeCity.ActiveReports.Extensibility.Rendering

Namespace GrapeCity.ActiveReports.Samples.CustomTileProviders
	''' <summary>
	''' Represents single map tile.
	''' </summary>
	Friend NotInheritable Class MapTile
		Implements IMapTile
		''' <summary>
		''' Initializes new instance of <see cref="MapTile"/>
		''' </summary>
		Public Sub New(id As MapTileKey, imageStream As ImageInfo)
			_id = id
			_image = imageStream
		End Sub

		''' <summary>
		''' Gets the tile identifier
		''' </summary>
		''' 
		Public ReadOnly Property Id() As MapTileKey Implements IMapTile.Id
			Get
				Return _id
			End Get
		End Property
		Private _id As MapTileKey

		''' <summary>
		''' Gets the tile image stream.
		''' </summary>
		Public ReadOnly Property Image() As ImageInfo Implements IMapTile.Image
			Get
				Return _image
			End Get

		End Property
		Private _image As ImageInfo
	End Class
End Namespace
