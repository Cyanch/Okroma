using Cyanch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Okroma.World.Tiles.Objects
{
    public interface ITileObject : IGameObject2D, IUpdateableGameObject2D, IDrawableGameObject2D, ITile
    {
        TileLocation Location { get; }

        void Remove();
        ITile GetBaseTile();
    }

    public class TileObject : GameObject2D, ITileObject
    {
        public ISprite Sprite { get; protected set; }
        public ITransform2D Transform { get; set; }
        public float RenderDepth { get; set; }

        public TileLocation Location { get; } 

        protected ITile BaseTile { get; }
        ITileModifier ITile.TileModifier => GetBaseTile().TileModifier;

        public TileObject(ITile tile, TileLocation location) : base(tile.Id)
        {
            this.BaseTile = tile;
            this.Sprite = tile.Sprite;
            this.Location = location;
            this.Transform = LocationToTransform(location);
        }

        protected ITransform2D LocationToTransform(TileLocation location)
        {
            Vector2 pos = new Vector2(
                location.X * location.World.Info.TileSize + location.World.Info.HalfTileSize,
                location.Y * location.World.Info.TileSize + location.World.Info.HalfTileSize
                );
            if (this.Transform is Transform2D transform)
            {
                return new Transform2D(pos, transform.Scale);
            }
            else
            {
                return new Transform2D(pos);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            BaseTile.Draw(gameTime, spriteBatch, Transform, RenderDepth);
        }

        void ITile.Draw(GameTime gameTime, SpriteBatch spriteBatch, ITransform2D transform, Color color, SpriteEffects spriteEffects, float depth)
        {
            BaseTile.Draw(gameTime, spriteBatch, transform, color, spriteEffects, depth);
        }

        public ITile GetBaseTile()
        {
            return BaseTile;
        }

        public void Remove()
        {
            GetBaseTile().TileModifier.Remove(this);
        }

        public ITileObject ToTileObject(TileLocation location)
        {
            return BaseTile.ToTileObject(location);
        }
    }

    /// <summary>
    /// Tile which is an extension of another Tile Object.
    /// </summary>
    /// <typeparam name="TTileObject"></typeparam>
    public class TileExtensionObject<TTileObject> : TileObject, ITileObject where TTileObject : ITileObject
    {
        protected TTileObject MainTile;
        public TileExtensionObject(TTileObject tile, TileLocation location) : base(tile, location)
        {
            this.MainTile = tile;
        }

        void ITileObject.Remove()
        {
            MainTile.Remove();
        }
    }
}
