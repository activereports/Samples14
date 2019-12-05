Imports System.Collections.Specialized
Imports GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map
Imports GrapeCity.ActiveReports.Extensibility.Rendering
Namespace GrapeCity.ActiveReports.Samples.CustomTileProviders
	''' <summary>
	''' Represents service which provides map tile images from Open Street Map (http://www.openstreetmap.org).
	''' </summary>
	Public NotInheritable Class OpenStreetMapTileProvider
		Implements IMapTileProvider
		Private Const UrlTemplate As String = "http://a.tile.openstreetmap.org/{0}/{1}/{2}.png"

		''' <summary>
		''' Provider settings:
		''' Timeout - Response timout
		''' </summary>
		Public ReadOnly Property Settings() As NameValueCollection Implements IMapTileProvider.Settings
			Get
				Return _settings
			End Get

		End Property
		Private _settings As NameValueCollection

		Public Sub New()
			_settings = New NameValueCollection()
			_settings.Set("UseSecureConnection.IsVisible", "False")
			_settings.Set("Style.IsVisible", "False")
		End Sub

		Public Sub GetTile(key As MapTileKey, success As Action(Of IMapTile), [error] As Action(Of Exception)) Implements IMapTileProvider.GetTile
			Dim url = String.Format(UrlTemplate, key.LevelOfDetail, key.Col, key.Row)
			Dim timeout = If(Not String.IsNullOrEmpty(Settings("Timeout")), Integer.Parse(Settings("Timeout")), -1)

			WebRequestHelper.DownloadDataAsync(url, timeout, Sub(stream) success(New MapTile(key, New ImageInfo(stream, Nothing))), [error])
			'WebRequestHelper.DownloadDataAsync(url, timeout, success(New MapTile(key, stream)), [error])
		End Sub

		Private Const _copyright As String = "@2015 OpenStreet Tile Provider Sample Copyright"

		Public ReadOnly Property Copyright() As String Implements IMapTileProvider.Copyright
			Get
				Return _copyright
			End Get
		End Property

	End Class
End Namespace
