using Okroma.Physics;

namespace Okroma.TileEngine
{
    class TileCollider : RectangleCollider
    {
        public TileCollider(int mapX, int mapY, TileProperties properties) : base(properties.OnCollision, properties.CreateBounds(mapX, mapY))
        {
        }
    }
}
