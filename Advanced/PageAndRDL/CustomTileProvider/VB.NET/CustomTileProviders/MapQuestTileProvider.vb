Imports System.Collections.Specialized
Imports System.Globalization
Imports GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map
Imports GrapeCity.ActiveReports.Extensibility.Rendering
Namespace GrapeCity.ActiveReports.Samples.CustomTileProviders
	''' <summary>
	''' Represents service which provides map tile images from Map Quest (http://www.mapquest.com/).
	''' </summary>
	Public NotInheritable Class MapQuestTileProvider
		Implements IMapTileProvider
		Private Const UrlTemplate As String = "http://open.mapquestapi.com/staticmap/v4/getmap?key={0}&center={1},{2}&zoom={3}&size=256,256&type={4}&imagetype=png"

		''' <summary>
		''' Provider settings:
		''' ApiKey - The key to access API
		''' Timeout - Response timout
		''' Language
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
			_settings.Set("Styles", "Map;Sat;Hybrid")
		End Sub

		Public Sub GetTile(key As MapTileKey, success As Action(Of IMapTile), [error] As Action(Of Exception)) Implements IMapTileProvider.GetTile
			Dim p = key.ToWorldPos()

			Dim parameters = GetParameters()

			Dim url = String.Format(CultureInfo.InvariantCulture.NumberFormat, UrlTemplate, parameters.Key, p.Y, p.X, key.LevelOfDetail, _
				parameters.MapType.ToString().ToLower())

			If Not String.IsNullOrEmpty(parameters.Language) Then
				url += Convert.ToString("&language=") & parameters.Language
			End If

			WebRequestHelper.DownloadDataAsync(url, parameters.Timeout, Sub(stream) success(New MapTile(key, New ImageInfo(stream, Nothing))), [error])
		End Sub

#Region "Parameters"

		Dim params As Parameters
		Private Function GetParameters() As Parameters
			params = New Parameters() With { _
				.Key = If(Settings("ApiKey"), "Fmjtd%7Cluur21ua2l%2C2x%3Do5-90t5h6"), _
				.Language = Settings("Language"), _
				.Timeout = If(Not String.IsNullOrEmpty(Settings("Timeout")), Integer.Parse(Settings("Timeout")), -1) _
			}

			Select Case Settings("Style")
				Case "Road"
					params.MapType = MapTypes.Map
					Exit Select
				Case "Aerial"
					params.MapType = MapTypes.Sat
					Exit Select
				Case "Hybrid"
					params.MapType = MapTypes.Hyb
					Exit Select
			End Select

			Return params
		End Function

		Private Class Parameters
			Public Key As String
			Public MapType As MapTypes
			Public Language As String
			Public Timeout As Integer
		End Class

		Private Enum MapTypes
			Map
			Sat
			Hyb
		End Enum

#End Region
		Private Const _copyright As String = "@2015 MapQuest Tile Provider Sample Copyright"

		Public ReadOnly Property Copyright() As String Implements IMapTileProvider.Copyright
			Get
				Return _copyright
			End Get
		End Property
	End Class
End Namespace
