using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Okroma.Sprites;

namespace Okroma.TileEngine
{
    class TileMap
    {
        public GameScale Width { get; }
        public GameScale Height { get; }

        public TileMapLayer[] Layers { get; }

        public Tile this[int x, int y, int layer]
        {
            get => Layers[layer].GetTile(x, y);
            set => Layers[layer].SetTile(x, y, value);
        }

        public TileMap(int mapWidthInTiles, int mapHeightInTiles, int mapLayers)
        {
            this.Width = GameScale.FromTiles(mapWidthInTiles);
            this.Height = GameScale.FromTiles(mapHeightInTiles);

            this.Layers = new TileMapLayer[mapLayers];

            for (int i = 0; i < this.Layers.Length; i++)
            {
                Layers[i] = new TileMapLayer(mapWidthInTiles, mapHeightInTiles, TileList.NullTile);
            }
        }

        public void DrawTiles(GameTime gameTime, SpriteBatch spriteBatch, Rectangle renderArea)
        {
            this.ForEach((x, y, layer, tile) =>
            {
                spriteBatch.DrawSprite(tile.Sprite, gameTime, new Vector2(x, y) * Tile.Vector2Size);
            }, renderArea);
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
            /// <param name="initializingTile">The 'default' <see cref="Tile"/></param>
            public TileMapLayer(int mapWidth, int mapHeight, Tile initializingTile)
            { 
                this._mapWidth = mapWidth;
                this._mapHeight = mapHeight;

                this._tiles = new Tile[mapWidth, mapHeight];

                Initialize(initializingTile);
            }

            private void Initialize(Tile tile)
            {
                for (int x = 0; x < _mapWidth; x++)
                {
                    for (int y = 0; y < _mapHeight; y++)
                    {
                        SetTile(x, y, tile);
                    }
                }
            }

            /// <summary>
            /// Sets <see cref="Tile"/> at given position.
            /// </summary>
            /// <param name="x">X position of <see cref="Tile"/></param>
            /// <param name="y">Y position of <see cref="Tile"/></param>
            /// <param name="tile">Tile to put at location</param>
            public void SetTile(int x, int y, Tile tile)
            {
                _tiles[x, y] = tile;
            }

            /// <summary>
            /// Gets <see cref="Tile"/> at given position.
            /// </summary>
            /// <param name="x">X position of <see cref="Tile"/></param>
            /// <param name="y">Y position of <see cref="Tile"/></param>
            /// <returns>The tile at the provided X and Y.</returns>
            public Tile GetTile(int x, int y)
            {
                return _tiles[x, y];
            }
        }
    }
}
