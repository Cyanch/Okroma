using Microsoft.Xna.Framework;

namespace Okroma.Physics
{
    public interface IHasCollisionMask
    {
        int CollisionMask { get; }
    }

    public interface ICollidableGameObject2D : IGameObject2D, IHasCollisionMask
    {
        Rectangle Bounds { get; }
        CollisionProperties CollisionProperties { get; }
        bool IsPassable(ICollidableGameObject2D collider);
        void NotifyCollision(ICollidableGameObject2D collider, CollisionData data);
    }

    public static class ColliderObject2DExtensions
    {
        public static bool Intersects(this ICollidableGameObject2D collider1, ICollidableGameObject2D collider2)
        {
            return collider1.Intersects(collider2.Bounds);
        }

        public static bool Intersects(this ICollidableGameObject2D collider1, Rectangle area)
        {
            return collider1.Bounds.Intersects(area);
        }

        public static Rectangle GetIntersection(this ICollidableGameObject2D collider1, ICollidableGameObject2D collider2)
        {
            return collider1.GetIntersection(collider2.Bounds);
        }

        public static Rectangle GetIntersection(this ICollidableGameObject2D collider1, Rectangle area)
        {
            return Rectangle.Intersect(collider1.Bounds, area);
        }
    }
}
