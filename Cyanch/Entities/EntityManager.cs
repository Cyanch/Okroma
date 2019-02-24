using C3;
using Cyanch.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Cyanch.Entities
{
    public interface IEntityMap : ICollisionProvider
    {
        void AddEntity(Entity entity);
        void RemoveEntity(Entity entity);

        void Update(GameTime gameTime);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        IReadOnlyList<Entity> GetEntities(Rectangle rectangle);
        IReadOnlyList<Entity> GetAllEntities();
    }

    public class EntityManager : IEntityMap
    {
        private readonly QuadTree<Entity> _entityMap;

        public EntityManager(Rectangle mapSize)
        {
            _entityMap = new QuadTree<Entity>(mapSize);
        }

        public void AddEntity(Entity entity)
        {
            _entityMap.Add(entity);
            entity.EntityMoved += Entity_Moved;
        }

        public void RemoveEntity(Entity entity)
        {
            _entityMap.Remove(entity);
            entity.EntityMoved -= Entity_Moved;
        }

        private void Entity_Moved(object sender, System.EventArgs e)
        {
            if (sender is Entity entity)
            {
                _entityMap.Move(sender as Entity);
            }
        }

        public IReadOnlyList<Entity> GetAllEntities()
        {
            return _entityMap.GetAllObjects();
        }

        public IReadOnlyList<Entity> GetEntities(Rectangle rectangle)
        {
            return _entityMap.GetObjects(rectangle);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var entity in _entityMap)
            {
                entity.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var entity in _entityMap)
            {
                entity.Draw(gameTime, spriteBatch);
            }
        }

        public IReadOnlyList<ICollider> GetColliders(Rectangle rect)
        {
            return GetEntities(rect) as IReadOnlyList<ICollider>;
        }

        public IReadOnlyList<ICollider> GetAllColliders()
        {
            return GetAllEntities() as IReadOnlyList<ICollider>;
        }
    }
}
