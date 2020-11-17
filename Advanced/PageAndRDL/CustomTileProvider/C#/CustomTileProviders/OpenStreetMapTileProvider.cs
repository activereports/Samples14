using System;
using System.Collections.Specialized;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map;

namespace GrapeCity.ActiveReports.Samples.CustomTileProviders
{
	/// <summary>
	/// Represents service which provides map tile images from Open Street Map (http://www.openstreetmap.org).
	/// </summary>
	public sealed class OpenStreetMapTileProvider : IMapTileProvider
	{
		private const string UrlTemplate = "http://a.tile.openstreetmap.org/{0}/{1}/{2}.png";

		/// <summary>
		/// Provider settings:
		/// Timeout - Response timout
		/// </summary>
		public NameValueCollection Settings { get; private set; }

		public OpenStreetMapTileProvider()
		{
			Settings = new NameValueCollection();
			Settings.Set("UseSecureConnection.IsVisible", "False");
			Settings.Set("Style.IsVisible", "False");
		}

		public void GetTile(MapTileKey key, Action<IMapTile> success, Action<Exception> error)
		{
			var url = string.Format(UrlTemplate, key.LevelOfDetail, key.Col, key.Row);
			var timeout = !string.IsNullOrEmpty(Settings["Timeout"]) ? int.Parse(Settings["Timeout"]) : -1;
			string userAgent = $"ActiveReports Core {typeof(IMapTile).Assembly.GetName().Version} contact ActiveReports.Sales@grapecity.com";

			WebRequestHelper.DownloadDataAsync(url, timeout, stream => success(new MapTile(key, new ImageInfo(stream, null))), error, userAgent);
		}

		private const string _copyright = "© 2015 OpenStreetMap contributors. Please see http://www.openstreetmap.org/copyright for more details.";

		public string Copyright
		{
			get { return _copyright; }
		}
	}
}
