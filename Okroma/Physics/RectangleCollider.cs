using System;
using Microsoft.Xna.Framework;

namespace Okroma.Physics
{
    class RectangleCollider : Collider, ICanIntersect<RectangleCollider>
    {
        public Rectangle Bounds { get; set; }

        public RectangleCollider(Func<Collider, CollisionAction> onCollision, Rectangle rectangle) : base(onCollision)
        {
            this.Bounds = rectangle;
        }

        public bool Intersects(RectangleCollider other)
        {
            return this.Bounds.Intersects(other.Bounds);
        }
    }
}
