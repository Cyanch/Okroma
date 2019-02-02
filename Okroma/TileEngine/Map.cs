using Microsoft.Xna.Framework;

namespace Okroma.TileEngine
{
    public class Map
    {
        public Point Dimensions { get; set; }

        MapLayer _backgroundTiles;
        MapLayer _tiles;

        public Map(MapLayer backgroundTiles, MapLayer tiles)
        {

        }
    }

    public class MapLayer
    {
        Tile[,,] _tileArray;

    }
}
