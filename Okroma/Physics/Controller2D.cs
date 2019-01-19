using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Okroma.Physics
{
    public abstract class Controller2D : GameObject2D, ICollidableGameObject2D, IHasTransform2D
    {
        protected int Width { get; }
        protected int Height { get; }
        protected ICollidableSource CollidableSource { get; }
        public CollisionProperties CollisionProperties { get; } = new CollisionProperties();
        public Controller2D(string id, ITransform2D transform, ICollidableSource collidableSource, int width, int height) : base(id)
        {
            this.CollidableSource = collidableSource;
            this.Transform = transform;
            this.Width = width;
            this.Height = height;
        }

        public Rectangle Bounds { get; protected set; }
        public abstract int CollisionMask { get; }

        public ITransform2D Transform { get; set; }

        private CollisionInfo collision;
        protected CollisionInfo Collision => collision;
        
        public virtual bool IsPassable(ICollidableGameObject2D collider)
        {
            return CollisionMask != collider.CollisionMask;
        }

        public virtual void NotifyCollision(ICollidableGameObject2D collider, CollisionData data)
        {
        }

        protected void Move(Vector2 velocity)
        {
            collision.Reset();
            if (velocity.X != 0)
            {
                HorizontalCollisions(ref velocity);
            }
            if (velocity.Y != 0)
            {
                VerticalCollisions(ref velocity);
            }

            Transform.Position += velocity;
            Transform.Position = new Vector2((float)Math.Round(Transform.Position.X), (float)Math.Round(Transform.Position.Y));
            Bounds = new Rectangle(Transform.Position.ToPoint(), new Point(Width, Height));
        }

        private void HorizontalCollisions(ref Vector2 velocity)
        {
            var projection = GetHorizontalMovementProjection(velocity);
            IEnumerable<ICollidableGameObject2D> collidablesInProjection = CollidableSource.GetCollidables(projection).Where(coll => coll != this);
            if (!collidablesInProjection.Any())
                return;

            ICollidableGameObject2D hit;
            if (velocity.X < 0)
            {
                hit = 
                    collidablesInProjection
                    .OrderByDescending(coll => coll.GetIntersection(projection).Right)
                    .First(coll =>
                    {
                        coll.NotifyCollision(this, null);
                        return !coll.IsPassable(this);
                    });
                var hitX = hit.Bounds.Right;
                velocity.X = hitX - Bounds.Left;
                collision.Left = true;
            }
            else
            {
                hit = 
                    collidablesInProjection
                    .OrderBy(coll => coll.GetIntersection(projection).Left)
                    .First(coll =>
                    {
                        coll.NotifyCollision(this, null);
                        return !coll.IsPassable(this);
                    });
                var hitX = hit.Bounds.Left;
                velocity.X = hitX - Bounds.Right;
                collision.Right = true;
            }

            if (hit != null)
            {
                if (hit.CollisionProperties.IsWallJumpable)
                {
                    collision.IsWallJumpable = true;
                }
            }
        }

        private void VerticalCollisions(ref Vector2 velocity)
        {
            var projection = GetVerticalMovementProjection(velocity);
            IEnumerable<ICollidableGameObject2D> collidablesInProjection = CollidableSource.GetCollidables(projection).Where(coll => coll != this);
            if (!collidablesInProjection.Any())
                return;
            if (velocity.Y < 0)
            {
                var hitY = collidablesInProjection.Select(coll => coll.GetIntersection(projection)).Max(b => b.Bottom);
                velocity.Y = hitY - Bounds.Top;
                collision.Above = true;
            }
            else
            {
                var hitY = collidablesInProjection.Select(coll => coll.GetIntersection(projection)).Min(b => b.Top);
                velocity.Y = hitY - Bounds.Bottom;
                collision.Below = true;
            }
        }

        Rectangle GetHorizontalMovementProjection(Vector2 velocity)
        {
            int x = (velocity.X < 0) ? Bounds.Left + (int)Math.Round(velocity.X) : Bounds.Right;
            return new Rectangle(x, Bounds.Top, (int)Math.Abs(Math.Round(velocity.X)), Bounds.Height);
        }

        Rectangle GetVerticalMovementProjection(Vector2 velocity)
        {
            int y = (velocity.Y < 0) ? Bounds.Top + (int)Math.Round(velocity.Y) : Bounds.Bottom;
            return new Rectangle(Bounds.Left, y, Bounds.Width, (int)Math.Abs(Math.Round(velocity.Y)));
        }

        protected float SmoothMovement(float current, float target, float smoothing, float lowerLimit = 0.05f)
        {
            var difference = target - current;
            if (target == 0 && Math.Abs(current) < lowerLimit)
                return 0;
            return current + (difference * smoothing);
        }

        protected struct CollisionInfo
        {
            // Collision Directions.
            public bool Above { get; set; }
            public bool Below { get; set; }
            public bool Left { get; set; }
            public bool Right { get; set; }

            //Wall Jump
            public bool IsWallJumpable { get; set; }

            public void Reset()
            {
                Above = Below = Left = Right = false;

                IsWallJumpable = false;
            }
        }
    }
}
