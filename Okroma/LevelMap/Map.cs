using Cyanch.Entities;
using Cyanch.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Okroma.TileEngine;
using System.Collections.Generic;

namespace Okroma.LevelMap
{
    public class Map : ITileMap, IEntityMap
    {
        public int Id { get; }

        ITileMap _tileMap;
        IEntityMap _entityMap;

        public Map(int width, int height, int layers)
        {
            _tileMap = new TileMap(width / GameScale.TileSize, height / GameScale.TileSize, layers);
            _entityMap = new EntityManager(new Rectangle(0, 0, width, height));
        }

        public void Update(GameTime gameTime)
        {
            _entityMap.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _tileMap.Draw(gameTime, spriteBatch);
            _entityMap.Draw(gameTime, spriteBatch);
        }

        public IReadOnlyList<ICollider> GetAllColliders()
        {
            return _tileMap.GetAllColliders();
        }

        public IReadOnlyList<ICollider> GetColliders(Rectangle rect)
        {
            return _tileMap.GetColliders(rect);
        }

        public Tile GetTile(int x, int y, int layer)
        {
            return _tileMap.GetTile(x, y, layer);
        }

        public void SetTile(int x, int y, int layer, int tileId)
        {
            _tileMap.SetTile(x, y, layer, tileId);
        }

        public void AddEntity(Entity entity)
        {
            _entityMap.AddEntity(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            _entityMap.RemoveEntity(entity);
        }

        public IReadOnlyList<Entity> GetEntities(Rectangle rectangle)
        {
            return _entityMap.GetEntities(rectangle);
        }

        public IReadOnlyList<Entity> GetAllEntities()
        {
            return _entityMap.GetAllEntities();
        }
    }
}
