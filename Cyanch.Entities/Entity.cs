using C3;
using Microsoft.Xna.Framework;

namespace Cyanch.Entities
{
    public class Entity : IQuadStorable
    {
        protected EntityManager EntityManager { get; private set; }

        Rectangle IQuadStorable.Rect => Bounds;

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public Rectangle Bounds => new Rectangle(Position.ToPoint(), Size.ToPoint());
        
        public void Initialize(EntityManager entityManager)
        {
            this.EntityManager = entityManager;
        }

        public virtual EntityUpdateResult Update(GameTime gameTime)
        {
            return EntityUpdateResult.Empty;
        }

        public virtual void Draw(GameTime gameTime)
        {

        }

        public void Destroy()
        {
            EntityManager.Remove(this);
        }
    }

    public struct EntityUpdateResult
    {
        public bool HasMoved { get; set; }

        public static EntityUpdateResult Empty { get; } = new EntityUpdateResult();
    }
}
