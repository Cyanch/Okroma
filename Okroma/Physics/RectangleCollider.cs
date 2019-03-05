using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Okroma.Common;
using Okroma.Debug;

namespace Okroma.Physics
{
    class RectangleCollider : Collider, ICanIntersect<RectangleCollider>, IDebuggable
    {
        public Rectangle Bounds { get; set; }

        public RectangleCollider(CollisionFunction onCollision, Rectangle rectangle) : base(onCollision)
        {
            this.Bounds = rectangle;
        }

        public bool Intersects(RectangleCollider other)
        {
            return this.Bounds.Intersects(other.Bounds);
        }

        void IDebuggable.DrawDebug(GameTime gameTime, SpriteBatch spriteBatch, Color color, params DebugOption[] debugOptions)
        {
            if (debugOptions.Exists(DebugOption.DrawColliderBounds))
                C3.Primitives2D.DrawRectangle(spriteBatch, Bounds, color);
        }
    }
}
