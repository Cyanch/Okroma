using C3;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Cyanch.Entities
{
    public class EntityManager
    {
        public SpriteBatch SpriteBatch { get; set; }
        private readonly QuadTree<Entity> _entityMap;

        public EntityManager(Rectangle mapSize)
        {
            _entityMap = new QuadTree<Entity>(mapSize);
        }

        public void Add(Entity entity)
        {
            _entityMap.Add(entity);
            entity.Initialize(this);
        }

        public void Remove(Entity entity)
        {
            _entityMap.Remove(entity);
        }

        public IList<Entity> GetAllEntities()
        {
            return _entityMap.GetAllObjects();
        }

        public IList<Entity> GetEntitiesIn(Rectangle rectangle)
        {
            return _entityMap.GetObjects(rectangle);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var entity in _entityMap)
            {
                if (entity != null)
                {
                    var updateResult = entity.Update(gameTime);

                    if (updateResult.HasMoved)
                    {
                        _entityMap.Move(entity);
                    }
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var entity in _entityMap)
            {
                entity?.Draw(gameTime);
            }
        }
    }
}
