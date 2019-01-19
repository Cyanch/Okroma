using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Okroma.World.Tiles;
using Okroma.World.Tiles.Objects;
using System.Collections.Generic;

namespace Okroma.World
{
    public class Chunk2D
    {
        public World2D World => Location.World;
        public ChunkLocation Location { get; }
        public Rectangle WorldArea { get; }

        ITileObject[,,] tiles;

        public Chunk2D(ChunkLocation location, ITileObject[,,] tiles)
        {
            this.Location = location;
            this.tiles = tiles;

            this.WorldArea = new Rectangle(
                Location.AsPixel.X,
                Location.AsPixel.Y,
                World.Info.ChunkSize,
                World.Info.ChunkSize
                );
        }

        public bool PlaceTile(int x, int y, int z, ITile tile)
        {
            return PlaceTile(x, y, z, tile, out var temp);
        }

        /// <summary>
        /// Instantiates the given tile at the given local coordinate.
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        public bool PlaceTile(int x, int y, int z, ITile tile, out ITileObject tileObject)
        {
            if (0 > z && z > World.Info.LayerCount)
                throw new System.ArgumentOutOfRangeException(nameof(z));

            if (IsValidChunkCoordinate(World, x, y))
            {
                if (tile == null)
                {
                    tiles[x, y, z] = tileObject = null;
                }
                else
                {
                    TileLocation location = new TileLocation(x + this.Location.AsTile.X, y + this.Location.AsTile.Y, z, this, x, y);
                    if (!tile.TileModifier.CanPlace(tile, location))
                    {
                        tileObject = null;
                        return false;
                    }

                    tileObject = tile?.TileModifier.Place(tile, location);
                }
            }
            else
            {
                return World.PlaceTile(x + Location.AsTile.X, y + Location.AsTile.Y, z, tile, out tileObject);
            }
            return true;
        }

        /// <summary>
        /// Sets a prepared tile.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="tile"></param>
        public void AddTile(ITileObject tile)
        {
            if (tile.Location.Chunk == this)
            {
                tiles[tile.Location.AsLocalized.X, tile.Location.AsLocalized.Y, tile.Location.Z] = tile;
            }
            else
            {
                World.AddTile(tile);
            }
        }

        public ITileObject GetTile(int x, int y, int z)
        {
            if (0 > z && z > World.Info.LayerCount)
                throw new System.ArgumentOutOfRangeException(nameof(z));

            if (IsValidChunkCoordinate(World, x, y))
            {
                return tiles[x, y, z];
            }
            else
            {
                return World.GetTile(x + Location.AsTile.X, y + Location.AsTile.Y, z);
            }
        }

        public void UpdateTiles(GameTime gameTime)
        {
            foreach (var tile in tiles)
            {
                tile?.Update(gameTime);
            }
        }

        public void DrawTiles(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var tile in tiles)
            {
                tile?.Draw(gameTime, spriteBatch);
            }
        }

        public static bool IsValidChunkCoordinate(World2D world, int x, int y)
        {
            return 0 <= x && x < world.Info.ChunkTileSpan &&
                0 <= y && y < world.Info.ChunkTileSpan;
        }

        public IEnumerable<ITileObject> GetAllTiles()
        {
            foreach (var tile in tiles)
                yield return tile;
        }
    }
}
