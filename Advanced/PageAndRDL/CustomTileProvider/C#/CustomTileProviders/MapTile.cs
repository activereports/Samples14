using System.IO;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components.Map;

namespace GrapeCity.ActiveReports.Samples.CustomTileProviders
{
	/// <summary>
	/// Represents single map tile.
	/// </summary>
	internal sealed class MapTile : IMapTile
	{
		/// <summary>
		/// Initializes new instance of <see cref="MapTile"/>
		/// </summary>
		public MapTile(MapTileKey id, ImageInfo imageStream)
		{
			Id = id;
			Image = imageStream;
		}

		/// <summary>
		/// Gets the tile identifier
		/// </summary>
		public MapTileKey Id { get; private set; }

		/// <summary>
		/// Gets the tile image stream.
		/// </summary>
		public ImageInfo Image { get; private set; }
	}
}
