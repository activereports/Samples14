using System;
using System.Collections.Specialized;
using System.Globalization;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map;

namespace GrapeCity.ActiveReports.Samples.CustomTileProviders
{
	/// <summary>
	/// Represents service which provides map tile images from Google Maps (http://maps.google.com).
	/// </summary>
	public sealed class GoogleMapsTileProvider : IMapTileProvider
	{
		private const string UrlTemplate = 
			"{3}://maps.googleapis.com/maps/api/staticmap?center={0:g5},{1:g5}&zoom={2}&size=256x256&sensor=false";

		/// <summary>
		/// Provider settings:
		/// ApiKey - The key to access API
		/// Timeout - Response timout
		/// Language
		/// </summary>
		public NameValueCollection Settings { get; private set; }

		public GoogleMapsTileProvider()
		{
			Settings = new NameValueCollection();
			Settings.Set("Styles", "Roadmap;Satellite;Hybrid;Terrain");
		}

		public void GetTile(MapTileKey key, Action<IMapTile> success, Action<Exception> error)
		{
			var tilePosition = key.ToWorldPos();
			var parameters = GetParameters();
			
			var url = string.Format(CultureInfo.InvariantCulture.NumberFormat, UrlTemplate,
				tilePosition.Y, 
				tilePosition.X,
				key.LevelOfDetail,
				parameters.UseSecureConnection ? "https" : "http");

			if (parameters.MapType.HasValue)
				url += "&maptype=" + parameters.MapType.ToString().ToLower();

			if (!string.IsNullOrEmpty(parameters.Key))
				url += "&key=" + parameters.Key;

			if (!string.IsNullOrEmpty(parameters.Language))
				url += "&language=" + parameters.Language;

			WebRequestHelper.DownloadDataAsync(url, parameters.Timeout, stream => success(new MapTile(key, new ImageInfo(stream, null))), error);
		}
		
		#region Parameters

		private Parameters GetParameters()
		{
			var parameters = new Parameters
				{
					Key = Settings["ApiKey"],
					Timeout = !string.IsNullOrEmpty(Settings["Timeout"]) ? int.Parse(Settings["Timeout"]) : -1,
					Language = Settings["Language"],
					UseSecureConnection = !string.IsNullOrEmpty(Settings["UseSecureConnection"]) 
						&& Convert.ToBoolean(Settings["UseSecureConnection"])
				};

			switch (Settings["Style"])
			{
				case "Road": parameters.MapType = MapTypes.Roadmap;
					break;
				case "Aerial": parameters.MapType = MapTypes.Satellite;
					break;
				case "Hybrid": parameters.MapType = MapTypes.Hybrid;
					break;
			}

			return parameters;
		}

		class Parameters
		{
			public string Key;
			public MapTypes? MapType;
			public string Language;
			public bool UseSecureConnection;
			public int Timeout;
		}

		enum MapTypes
		{
			Roadmap,
			Satellite,
			Hybrid,
			Terrain
		}

		#endregion


		private const string _copyright = "Google Maps ©2015 Google, Please see http://www.google.com/intl/en_us/help/legalnotices_maps.html for more details.";

		public string Copyright
		{
			get { return _copyright; }
		}
	}
}
