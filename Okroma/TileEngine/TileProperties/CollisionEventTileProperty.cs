using Okroma.Physics;
using static Okroma.Physics.Collider;

namespace Okroma.TileEngine.TileProperties
{
    class CollisionEventTileProperty : TileProperty
    {
        public static CollisionEventTileProperty Default { get; } = new CollisionEventTileProperty(_ => CollisionAction.Ignore);
        public CollisionFunction OnCollision { get; }

        public CollisionEventTileProperty(CollisionFunction onCollision)
        {
            this.OnCollision = onCollision;
        }
    }
}
