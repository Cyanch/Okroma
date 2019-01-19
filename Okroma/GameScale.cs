namespace Okroma
{
    /// <summary>
    /// Game Scale class.
    /// </summary>
    public struct GameScale
    {
        public const int TileSize = 64;

        public int Pixels { get; }
        public int Tiles => Pixels / TileSize;
        public float TotalTiles => Pixels / TileSize;

        public GameScale(int pixels)
        {
            this.Pixels = pixels;
        }

        public static GameScale FromPixel(int pixels)
        {
            return new GameScale(pixels);
        }

        public static GameScale FromTile(float tiles)
        {
            return new GameScale((int)(TileSize * tiles));
        }
    }
}
