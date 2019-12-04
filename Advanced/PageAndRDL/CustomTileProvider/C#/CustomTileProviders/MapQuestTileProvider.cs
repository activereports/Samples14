using System;
using System.Collections.Specialized;
using System.Globalization;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map;

namespace GrapeCity.ActiveReports.Samples.CustomTileProviders
{
	/// <summary>
	/// Represents service which provides map tile images from Map Quest (http://www.mapquest.com/).
	/// </summary>
	public sealed class MapQuestTileProvider : IMapTileProvider
	{
		private const string UrlTemplate = 
			"http://open.mapquestapi.com/staticmap/v4/getmap?key={0}&center={1},{2}&zoom={3}&size=256,256&type={4}&imagetype=png";

		/// <summary>
		/// Provider settings:
		/// ApiKey - The key to access API
		/// Timeout - Response timout
		/// Language
		/// </summary>
		public NameValueCollection Settings { get; private set; }

		public MapQuestTileProvider()
		{
			Settings = new NameValueCollection();
			Settings.Set("UseSecureConnection.IsVisible", "False");
			Settings.Set("Styles", "Map;Sat;Hybrid");
		}

		public void GetTile(MapTileKey key, Action<IMapTile> success, Action<Exception> error)
		{
			var p = key.ToWorldPos();

			var parameters = GetParameters();

			var url = string.Format(CultureInfo.InvariantCulture.NumberFormat, UrlTemplate,
				parameters.Key,
				p.Y,
				p.X,
				key.LevelOfDetail,
				parameters.MapType.ToString().ToLower());

			if (!string.IsNullOrEmpty(parameters.Language))
				url += "&language=" + parameters.Language;

			WebRequestHelper.DownloadDataAsync(url, parameters.Timeout, stream => success(new MapTile(key, new ImageInfo(stream, null))), error);
		}

		#region Parameters

		private Parameters GetParameters()
		{
			var parameters = new Parameters
			{
				Key = Settings["ApiKey"] ?? "Fmjtd%7Cluur21ua2l%2C2x%3Do5-90t5h6",
				Language = Settings["Language"],
				Timeout = !string.IsNullOrEmpty(Settings["Timeout"]) ? int.Parse(Settings["Timeout"]) : -1
			};

			switch (Settings["Style"])
			{
				case "Road": parameters.MapType = MapTypes.Map;
					break;
				case "Aerial": parameters.MapType = MapTypes.Sat;
					break;
				case "Hybrid": parameters.MapType = MapTypes.Hyb;
					break;
			}

			return parameters;
		}

		class Parameters
		{
			public string Key;
			public MapTypes MapType;
			public string Language;
			public int Timeout;
		}

		enum MapTypes
		{
			Map,
			Sat,
			Hyb
		}

		#endregion

		private const string _copyright = "Copyright © 2015 MapQuest, Inc. (\"MapQuest\"). Please see http://info.mapquest.com/terms-of-use/ for more details.";

		public string Copyright
		{
			get { return _copyright; }
		}
	}
}
