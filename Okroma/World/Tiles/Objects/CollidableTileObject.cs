using Microsoft.Xna.Framework;
using Okroma.Physics;

namespace Okroma.World.Tiles.Objects
{
    public class CollidableTileObject : TileObject, ICollidableGameObject2D
    {
        public CollidableTileObject(ITile tile, TileLocation location, int collisionMask) : base(tile, location)
        {
            this.CollisionMask = collisionMask;
            this.Bounds = new Rectangle(location.AsPixel.X, location.AsPixel.Y, location.World.Info.TileSize, location.World.Info.TileSize);
        }

        public virtual Rectangle Bounds { get; }
        public CollisionProperties CollisionProperties { get; } = new CollisionProperties();
        public int CollisionMask { get; }


        public virtual bool IsPassable(ICollidableGameObject2D collider)
        {
            return CollisionMask != collider.CollisionMask;
        }

        public virtual void NotifyCollision(ICollidableGameObject2D collider, CollisionData data)
        {
        }
    }
}
