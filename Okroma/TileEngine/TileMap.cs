using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Okroma.TileEngine
{
    class TileMap
    {
        public int Width { get; }
        public int Height { get; }

        public TileMapLayer[] Layers { get; }

        public Tile this[int x, int y, int layer]
        {
            get => Layers[layer].GetTile(x, y);
            set => Layers[layer].SetTile(x, y, value);
        }

        public TileMap(int mapWidth, int mapHeight, params Tile[] initializingTiles)
        {
            this.Layers = new TileMapLayer[initializingTiles.Length];

            for (int i = 0; i < this.Layers.Length; i++)
            {
                Layers[i] = new TileMapLayer(mapWidth, mapHeight, initializingTiles[i]);
            }
        }

        public void DrawLayers(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var layer in Layers)
            {
                layer.DrawTiles(gameTime, spriteBatch);
            }
        }

        public class TileMapLayer
        {
            readonly Tile[,] _tiles;

            private readonly int _mapWidth;
            private readonly int _mapHeight;

            /// <summary>
            /// <see cref="TileMapLayer"/> constructor.
            /// </summary>
            /// <param name="mapWidth">Width of map</param>
            /// <param name="mapHeight">Height of map</param>
            /// <param name="initializingTile">Initial tile</param>
            public TileMapLayer(int mapWidth, int mapHeight, Tile initializingTile)
            { 
                this._mapWidth = mapWidth;
                this._mapHeight = mapHeight;

                this._tiles = new Tile[mapWidth, mapHeight];
                for (int x = 0; x < mapWidth; x++)
                {
                    for (int y = 0; y < mapHeight; y++)
                    {
                        this._tiles[x, y] = initializingTile;
                    }
                }
            }

            public void SetTile(int x, int y, Tile tile)
            {
                _tiles[x, y] = tile;
            }

            public Tile GetTile(int x, int y)
            {
                return _tiles[x, y];
            }

            public void DrawTiles(GameTime gameTime, SpriteBatch spriteBatch)
            {
                for (int x = 0; x < _mapWidth; x++)
                {
                    for (int y = 0; y < _mapHeight; y++)
                    {
                        _tiles[x, y].Sprite.Draw(gameTime, spriteBatch, new Vector2(x, y) * Tile.Size);
                    }
                }
            }
        }
    }
}
