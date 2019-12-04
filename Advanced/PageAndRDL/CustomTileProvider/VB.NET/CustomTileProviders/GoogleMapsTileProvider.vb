Imports System.Collections.Specialized
Imports System.Globalization
Imports GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map
Imports GrapeCity.ActiveReports.Extensibility.Rendering
Namespace GrapeCity.ActiveReports.Samples.CustomTileProviders
	''' <summary>
	''' Represents service which provides map tile images from Google Maps (http://maps.google.com).
	''' </summary>
	Public NotInheritable Class GoogleMapsTileProvider
		Implements IMapTileProvider
		Private Const UrlTemplate As String = "{3}://maps.googleapis.com/maps/api/staticmap?center={0:g5},{1:g5}&zoom={2}&size=256x256&sensor=false"

		Public Sub New()
			_settings = New NameValueCollection()
			_settings.Set("Styles", "Roadmap;Satellite;Hybrid;Terrain")
		End Sub
		Public Sub GetTile(key As MapTileKey, success As Action(Of IMapTile), [error] As Action(Of Exception)) Implements IMapTileProvider.GetTile
			Dim tilePosition = key.ToWorldPos()
			Dim parameters = GetParameters()

			Dim url = String.Format(CultureInfo.InvariantCulture.NumberFormat, UrlTemplate, tilePosition.Y, tilePosition.X, key.LevelOfDetail, If(parameters.UseSecureConnection, "https", "http"))

			If parameters.MapType.HasValue Then
				url += "&maptype=" + parameters.MapType.ToString().ToLower()
			End If

			If Not String.IsNullOrEmpty(parameters.Key) Then
				url += Convert.ToString("&key=") & parameters.Key
			End If

			If Not String.IsNullOrEmpty(parameters.Language) Then
				url += Convert.ToString("&language=") & parameters.Language
			End If

			WebRequestHelper.DownloadDataAsync(url, parameters.Timeout, Sub(stream) success(New MapTile(key, New ImageInfo(stream, Nothing))), [error])
		End Sub

		Public ReadOnly Property Settings As NameValueCollection Implements IMapTileProvider.Settings
			Get
				Return _settings
			End Get
		End Property
		Private _settings As NameValueCollection

#Region "Parameters"

		Dim params As Parameters
		Private Function GetParameters() As Parameters
			params = New Parameters() With { _
				   .Key = Settings("ApiKey"), _
				   .Timeout = If(Not String.IsNullOrEmpty(Settings("Timeout")), Integer.Parse(Settings("Timeout")), -1), _
				   .Language = Settings("Language"), _
				   .UseSecureConnection = Not String.IsNullOrEmpty(Settings("UseSecureConnection")) AndAlso Convert.ToBoolean(Settings("UseSecureConnection")) _
			   }

			Select Case Settings("Style")
				Case "Road"
					params.MapType = MapTypes.Roadmap
					Exit Select
				Case "Aerial"
					params.MapType = MapTypes.Satellite
					Exit Select
				Case "Hybrid"
					params.MapType = MapTypes.Hybrid
					Exit Select
			End Select

			Return params
		End Function

		Private Class Parameters
			Public Key As String
			Public MapType As Nullable(Of MapTypes)
			Public Language As String
			Public UseSecureConnection As Boolean
			Public Timeout As Integer
		End Class

		Private Enum MapTypes
			Roadmap
			Satellite
			Hybrid
			Terrain
		End Enum

#End Region
		Private Const _copyright As String = "@2015 Google Tile Provider Sample Copyright"

		Public ReadOnly Property Copyright() As String Implements IMapTileProvider.Copyright
			Get
				Return _copyright
			End Get
		End Property
	End Class

End Namespace
