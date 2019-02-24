using C3;
using Cyanch.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Cyanch.Entities
{
    public abstract class Entity : ICollider, IQuadStorable
    {
        public int Id { get; }

        private Rectangle _bounds;
        public Rectangle Bounds
        {
            get
            {
                return _bounds;
            }
            private set
            {
                if (_bounds != value)
                {
                    _bounds = value;
                    EntityMoved?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        Rectangle IQuadStorable.Rect => Bounds;

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        public void Destroy()
        {
            EntityDestroyed?.Invoke(this, EventArgs.Empty);
        }

        public virtual void IsPassable(ICollider collider)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Collide(ICollider collider)
        {
            throw new System.NotImplementedException();
        }

        public event EventHandler EntityMoved;
        public event EventHandler EntityDestroyed;
    }
}
