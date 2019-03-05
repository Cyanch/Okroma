using Okroma.TileEngine;

namespace Okroma
{
    struct GameScale
    {
        public float TotalTiles { get; set; }

        public int Pixels { get; set; }
        public int Tiles { get; set; }

        public GameScale(int pixels)
        {
            this.Pixels = pixels;
            
            this.TotalTiles = pixels / Tile.Size;
            this.Tiles = (int)TotalTiles;
        }

        public static GameScale FromTiles(float tiles)
        {
            return new GameScale((int)(tiles * Tile.Size));
        }
    }
}
