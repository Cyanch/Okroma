using Microsoft.Xna.Framework;

namespace Okroma.TileEngine.TileProperties
{
    class CustomCollisionBoundsTileProperty : TileProperty
    {
        public static CustomCollisionBoundsTileProperty Default { get; } = new CustomCollisionBoundsTileProperty(new Rectangle(0, 0, Tile.Size, Tile.Size));

        public Rectangle Bounds { get; }

        public CustomCollisionBoundsTileProperty(Rectangle bounds)
        {
            Bounds = bounds;
        }

        public Rectangle CreateBounds(int mapX, int mapY)
        {
            var bounds = Bounds;
            bounds.Offset(mapX * Tile.Size, mapY * Tile.Size);
            return bounds;
        }
    }
}
