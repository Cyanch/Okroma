using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Okroma.TileEngine
{
    public class Map
    {
        public Point Dimensions { get; set; }

        MapLayer _backgroundTiles;
        MapLayer _tiles;

        Game _game;
        SpriteBatch _spriteBatch;
        public Map(Game game, MapLayer backgroundTiles, MapLayer tiles)
        {
            this._game = game;
            this._spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var tile in _backgroundTiles)
            {
                _spriteBatch.DrawTile(tile, Vector2.Zero, Color.White);
            }

            foreach (var tile in _tiles)
            {
                _spriteBatch.DrawTile(tile, Vector2.Zero, Color.White);
            }
        }
    }

    // id ref. points?
    // SpriteBatch.Texture.

    public class MapLayer : IEnumerable<Tile>
    {
        Tile[,,] _tileArray;

        public IEnumerator<Tile> GetEnumerator()
        {
            return (IEnumerator<Tile>)_tileArray.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
