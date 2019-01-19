using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Okroma.World.Tiles.Objects;
using System;

namespace Okroma.World.Tiles
{
    public interface ITile
    {
        /// <summary>
        /// Id of <see cref="ITile"/>
        /// </summary>
        string Id { get; }

        ISprite Sprite { get; }
        ITileModifier TileModifier { get; }

        void Draw(GameTime gameTime, SpriteBatch spriteBatch, ITransform2D transform, Color color, SpriteEffects spriteEffects, float depth);

        ITileObject ToTileObject(TileLocation location);
    }

    public interface ITileModifier
    {
        bool CanPlace(ITile tile, TileLocation location);
        ITileObject Place(ITile tile, TileLocation location);
        void Remove(ITileObject tile);
    }

    public class TileModifier : ITileModifier
    {
        public virtual bool CanPlace(ITile tile, TileLocation location)
        {
            return location.Chunk.GetTile(location.AsLocalized.X, location.AsLocalized.Y, location.Z) == null;
        }

        public virtual ITileObject Place(ITile tile, TileLocation location)
        {
            var tileObject = tile.ToTileObject(location);
            location.Chunk.AddTile(tileObject);
            return tileObject;
        }

        public virtual void Remove(ITileObject tile)
        {
            tile.Location.Chunk.PlaceTile(tile.Location.AsLocalized.X, tile.Location.AsLocalized.Y, tile.Location.Z, null);
        }
    }

    public class Tile : ITile
    {
        public string Id { get; }
        public ISprite Sprite { get; }

        public ITileModifier TileModifier { get; }

        public Tile(string id, ISprite sprite) : this(id, sprite, null)
        {
        }

        public Tile(string id, ISprite sprite, ITileModifier modifier)
        {
            this.Id = id ?? throw new ArgumentNullException(nameof(id));
            this.Sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));
            this.TileModifier = modifier ?? new TileModifier();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, ITransform2D transform, Color color, SpriteEffects spriteEffects, float depth)
        {
            Sprite.DrawAt(gameTime, spriteBatch, transform, color, spriteEffects, depth);
        }

        public virtual ITileObject ToTileObject(TileLocation location)
        {
            return new TileObject(this, location);
        }

        public void Remove(ITileObject tile)
        {
            TileModifier.Remove(tile);
        }
    }

    public static class TileExtensions
    {
        /// <summary>
        /// Draws using the ITile.Draw(GameTime, SpriteBatch, ITransform2D, Color). Color is set to Color.White.
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="transform"></param>
        public static void Draw(this ITile tile, GameTime gameTime, SpriteBatch spriteBatch, ITransform2D transform, float depth)
        {
            tile.Draw(gameTime, spriteBatch, transform, Color.White, SpriteEffects.None, depth);
        }
    }
}
