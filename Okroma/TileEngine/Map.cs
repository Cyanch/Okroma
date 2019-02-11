using Cyanch.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;

namespace Okroma.TileEngine
{
    public class Map
    {
        public Point Dimensions { get; set; }
        public ContentManager Content { get; }

        MapLayer _backgroundTiles;
        MapLayer _tiles;

        Game _game;
        SpriteBatch _spriteBatch;
        protected Map(Game game)
        {
            this._game = game;
            this._spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.Content = new ContentManager(game.Content.ServiceProvider, game.Content.RootDirectory);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime)
        {
            foreach (var tile in _backgroundTiles)
            {
                tile.Draw(gameTime, _spriteBatch);
            }

            foreach (var tile in _tiles)
            {
                tile.Draw(gameTime, _spriteBatch);
            }
        }

        public void LoadContent()
        {

        }

        public void UnloadContent()
        {
            Content.Unload();
        }

        public static Map Load(Game game, string mapPath)
        {
            var map = new Map(game);

            return map;
        }
    }

    public class MapLayer : IEnumerable<Tile>
    {
        Tile[,] _tileArray;
        ICollisionService _collisionService;

        public MapLayer(Map map, Tile[,] tiles)
        {
            _tileArray = tiles;
            //_tileArray = new Tile[width, height];
            //for (int y = 0; y < height; y++)
            //{
            //    for (int x = 0; x < width; x++)
            //    {
            //        _tileArray[x, y] = new Tile(map, x, y);
            //    }
            //}
        }

        public IEnumerator<Tile> GetEnumerator()
        {
            return (IEnumerator<Tile>)_tileArray.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
        
        public void SetCollisionSerice(ICollisionService collisionService)
        {
            foreach (var tile in _tileArray)
            {
                _collisionService?.UnregisterCollider(tile);
                collisionService.RegisterCollider(tile);
            }

            _collisionService = collisionService;
        }
    }
}
