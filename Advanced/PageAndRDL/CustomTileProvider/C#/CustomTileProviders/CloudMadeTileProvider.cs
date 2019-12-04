using System;
using System.Collections.Specialized;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map;

namespace GrapeCity.ActiveReports.Samples.CustomTileProviders
{
	/// <summary>
	/// Represents service which provides map tile images from http://cloudmade.com/. 
	/// </summary>
	public sealed class CloudMadeTileProvider : IMapTileProvider
	{
		private const string UrlTemplate = "http://a.tile.cloudmade.com/{0}/{1}/256/{2}/{3}/{4}.png";
		
		/// <summary>
		/// Provider settings:
		/// ApiKey - The key to access API
		/// ColorStyle - The style number from (http://maps.cloudmade.com/editor)
		/// Timeout - Response timout
		/// </summary>
		public NameValueCollection Settings { get; private set; }
		
		public CloudMadeTileProvider()
		{
			Settings = new NameValueCollection();		   
			Settings.Set("UseSecureConnection.IsVisible", "False");
			Settings.Set("Style.IsVisible", "False");
		}

		public void GetTile(MapTileKey key, Action<IMapTile> success, Action<Exception> error)
		{
			var parameters = GetParameters();
			
			var url = string.Format(UrlTemplate,
				parameters.Key,
				parameters.ColorStyle,
				key.LevelOfDetail,
				key.Col,
				key.Row);

			WebRequestHelper.DownloadDataAsync(url, parameters.Timeout, stream => success(new MapTile(key, new ImageInfo(stream, null))), error);
		}

		#region Parameters

		private Parameters GetParameters()
		{
			var parameters = new Parameters
			{
				Key = Settings["ApiKey"] ?? "8ee2a50541944fb9bcedded5165f09d9",
				ColorStyle = !string.IsNullOrEmpty(Settings["ColorStyle"]) ? int.Parse(Settings["ColorStyle"]) : 1,
				Timeout = !string.IsNullOrEmpty(Settings["Timeout"]) ? int.Parse(Settings["Timeout"]) : -1
			};

			return parameters;
		}

		class Parameters
		{
			public string Key;
			public int ColorStyle;
			public int Timeout;
		}

		#endregion

		private const string _copyright = "© 2015 CloudMade-Map data ODbL 2015 OpenStreetMap.org. Please see http://cloudmade.com/website-terms-conditions for more details.";

		public string Copyright
		{
			get { return _copyright; }
		}
	}
}
