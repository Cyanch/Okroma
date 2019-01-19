using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Okroma.Common;
using Okroma.Physics;
using Okroma.World.Tiles;
using Okroma.World.Tiles.Objects;
using System;
using System.Collections.Generic;

namespace Okroma.World
{
    public class World2D : ICollidableSource
    {
        /// <summary>
        /// Information about <see cref="World2D"/>
        /// </summary>
        public WorldInfo Info { get; }

        public struct WorldInfo
        {
            public WorldInfo(int layerCount, int tileSize, int chunkSizeAsTiles, int horizontalChunkSpan, int verticalChunkSpan) : this()
            {
                ChunkSize = chunkSizeAsTiles * tileSize;
                Width = ChunkSize * horizontalChunkSpan;
                Height = ChunkSize * verticalChunkSpan;
                TileWidth = Width / tileSize;
                TileHeight = Height / tileSize;
                LayerCount = layerCount;
                TileSize = tileSize;
                HalfTileSize = tileSize / 2;
                ChunkTileSpan = chunkSizeAsTiles;
                HorizontalChunkSpan = horizontalChunkSpan;
                VerticalChunkSpan = verticalChunkSpan;
            }



            /// <summary>
            /// Width of <see cref="World2D"/> (px).
            /// </summary>
            public int Width { get; }
            /// <summary>
            /// Height of <see cref="World2D"/> (px).
            /// </summary>
            public int Height { get; }

            /// <summary>
            /// Width of <see cref="World2D"/> (<see cref="ITileObject"/>).
            /// </summary>
            public int TileWidth { get; }
            /// <summary>
            /// Height of <see cref="World2D"/> (<see cref="ITileObject"/>).
            /// </summary>
            public int TileHeight { get; }

            /// <summary>
            /// Layer (Z-axis) Count of <see cref="World2D"/>.
            /// </summary>
            public int LayerCount { get; }

            /// <summary>
            /// The Width/Height of an <see cref="ITileObject"/>.
            /// </summary>
            public int TileSize { get; }   
            
            /// <summary>
            /// Half the Width/Height of an <see cref="ITileObject"/>.
            /// </summary>
            public int HalfTileSize { get; }

            /// <summary>
            /// The Width/Height of a <see cref="Chunk2D"/> (in pixels).
            /// </summary>
            public int ChunkSize { get; }
            /// <summary>
            /// Number of <see cref="ITileObject"/> horizontally or vertically in <see cref="Chunk2D"/>.
            /// </summary>
            public int ChunkTileSpan { get; }

            /// <summary>
            /// Number of <see cref="Chunk2D"/> objects horizontally in <see cref="World2D"/>
            /// </summary>
            public int HorizontalChunkSpan { get; }
            /// <summary>
            /// Number of <see cref="Chunk2D"/> objects vertically in <see cref="World2D"/>
            /// </summary>
            public int VerticalChunkSpan { get; }
        }

        /// <summary>
        /// Depth tiles can be drawn to.
        /// </summary>
        public Range<float> DepthRange { get; }

        Chunk2D[,] chunks;

        public World2D(int tileSize, byte layerCount, Range<float> depthRange, int chunkSpan, int chunksHorizontally, int chunksVertically)
        {
            if (layerCount < 0)
                throw new ArgumentException("Cannot be zero.", nameof(layerCount));

            this.DepthRange = depthRange;

            this.Info = new WorldInfo(
                layerCount: layerCount,
                tileSize: tileSize,
                chunkSizeAsTiles: chunkSpan,
                horizontalChunkSpan: chunksHorizontally,
                verticalChunkSpan: chunksVertically);

            chunks = new Chunk2D[chunksHorizontally, chunksVertically];

            for (int chunkY = 0; chunkY < chunksVertically; chunkY++)
            {
                for (int chunkX = 0; chunkX < chunksHorizontally; chunkX++)
                {
                    CreateChunk(chunkX, chunkY);
                }
            }
        }

        protected void CreateChunk(int x, int y)
        {
            var chunk = new Chunk2D(new ChunkLocation(this, x, y), new ITileObject[Info.ChunkTileSpan, Info.ChunkTileSpan, Info.LayerCount]);
            chunks[x, y] = chunk;
        }

        Dictionary<int, float> depthForLayer = new Dictionary<int, float>();
        public float LayerToDepth(int layer)
        {
            if (depthForLayer.TryGetValue(layer, out float depth))
            {
                return depth;
            }
            else
            {
                if (Info.LayerCount == 1)
                    return 1;
                float range = DepthRange.Maximum - DepthRange.Minimum;
                float zdepth = (range * layer / (Info.LayerCount - 1)) + DepthRange.Minimum;
                depthForLayer.Add(layer, zdepth);
                return zdepth;
            }
        }

        /// <summary>
        /// Returns chunk by world coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Chunk2D GetChunkAt(int x, int y)
        {
            return GetChunk((int)Math.Floor((float)x / Info.ChunkSize), (int)Math.Floor((float)y / Info.ChunkSize));
        }

        /// <summary>
        /// Returns chunk by chunk-based coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Chunk2D GetChunk(int x, int y)
        {
            return chunks[x, y];
        }

        public Point ChunkPointFromTileLocation(int x, int y)
        {
            return new Point((int)Math.Floor((float)x / Info.ChunkTileSpan), (int)Math.Floor((float)y / Info.ChunkTileSpan));
        }

        public bool PlaceTile(int x, int y, int layer, ITile tile)
        {
            return PlaceTile(x, y, layer, tile, out var temp);
        }

        public bool PlaceTile(int x, int y, int layer, ITile tile, out ITileObject tileObject)
        {
            var chunkPoint = ChunkPointFromTileLocation(x, y);
            var chunk = GetChunk(chunkPoint.X, chunkPoint.Y);
            return chunk.PlaceTile(x - chunk.Location.AsTile.X, y - chunk.Location.AsTile.Y, layer, tile, out tileObject);
        }

        public void AddTile(ITileObject tile)
        {
            tile.Location.Chunk.AddTile(tile);
        }

        public ITileObject GetTile(int x, int y, int layer)
        {
            var chunkPoint = ChunkPointFromTileLocation(x, y);
            var chunk = GetChunk(chunkPoint.X, chunkPoint.Y);
            return chunk.GetTile(x - chunkPoint.X * Info.ChunkTileSpan, y - chunkPoint.Y * Info.ChunkTileSpan, layer);
        }

        public ITileObject[] GetTiles(int x, int y)
        {
            var tiles = new ITileObject[Info.LayerCount];
            for (int layer = 0; layer < Info.LayerCount; layer++)
            {
                tiles[layer] = GetTile(x, y, layer);
            }
            return tiles;
        }

        public bool IsValidTile(int x, int y)
        {
            return (0 < x && x < Info.TileWidth) && (0 < y && y < Info.TileHeight);
        }

        public IEnumerable<ITileObject> GetTilesInArea(Rectangle area)
        {
            int tilesAcrossHorizontally = (int)Math.Ceiling(area.Width / (float)Info.TileSize);
            int tilesAcrossVertically = (int)Math.Ceiling(area.Height / (float)Info.TileSize);

            int xOffsetFromTile = area.X % Info.TileSize;
            if (xOffsetFromTile != 0 && xOffsetFromTile > (tilesAcrossHorizontally * Info.TileSize) - area.Width)
                tilesAcrossHorizontally++;
            int yOffsetFromTile = area.Y % Info.TileSize;
            if (yOffsetFromTile != 0 && yOffsetFromTile > (tilesAcrossVertically * Info.TileSize) - area.Height)
                tilesAcrossVertically++;

            int initialX = (int)Math.Floor(area.X / (float)Info.TileSize);
            int initialY = (int)Math.Floor(area.Y / (float)Info.TileSize);
            for (int x = initialX; x < tilesAcrossHorizontally + initialX; x++)
            {
                for (int y = initialY; y < tilesAcrossVertically + initialY; y++)
                {
                    for (int layer = 0; layer < Info.LayerCount; layer++)
                    {
                        yield return GetTile(x, y, layer);
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var chunk in GetAllChunks())
            {
                chunk?.UpdateTiles(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle? drawOnly = null)
        {
            foreach (var chunk in GetAllChunks())
            {
                if (drawOnly == null || chunk.WorldArea.Intersects(drawOnly.Value))
                {
                    chunk?.DrawTiles(gameTime, spriteBatch);
                }
            }
        }

        public IEnumerable<Chunk2D> GetAllChunks()
        {
            foreach (var chunk in chunks)
                yield return chunk;
        }

        public IEnumerable<ICollidableGameObject2D> GetCollidables(Rectangle rectangle)
        {
            if (rectangle.Y + rectangle.Height > Info.Height)
                {
                    rectangle.Height = Info.Height - rectangle.Y;
                }
                if (rectangle.Y < 0)
                {
                    rectangle.Height += rectangle.Y;
                    rectangle.Y = 0;
                }

                if (rectangle.X + rectangle.Width > Info.Width)
                {
                    rectangle.Width = Info.Width - rectangle.X;
                }
                if (rectangle.X < 0)
                {
                    rectangle.Width += rectangle.X;
                    rectangle.X = 0;
                }

                foreach (var tile in GetTilesInArea(rectangle))
                    if (tile is ICollidableGameObject2D collidable && rectangle.Intersects(collidable.Bounds))
                        yield return collidable;
        }
    }
}
