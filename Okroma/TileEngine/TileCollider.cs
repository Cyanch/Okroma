using Okroma.Physics;
using Okroma.TileEngine.TileProperties;

namespace Okroma.TileEngine
{
    class TileCollider : RectangleCollider
    {
        public TileCollider(int mapX, int mapY, Tile tile) : base(tile.GetPropertyOrDefault(CollisionEventTileProperty.Default).OnCollision, tile.GetPropertyOrDefault(CustomCollisionBoundsTileProperty.Default).CreateBounds(mapX, mapY))
        {
        }
    }
}
