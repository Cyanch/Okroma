using Cyanch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Okroma.TileEngine.TileProperties;

namespace Okroma.TileEngine
{
    public interface ITileDataSource
    {
        TileData LoadTileData(string path);
    }

    public class TileData
    {
        public static TileData None { get; } = new TileData(null, Rectangle.Empty);

        public ISprite Sprite { get; }
        public Rectangle Bounds { get; }

        public TileProperties Properties { get; }

        public TileData(ISprite sprite, Rectangle bounds)
        {
            this.Sprite = sprite;
            this.Bounds = bounds;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 drawPosition, Color color)
        {
            if (Sprite != null)
            {
                spriteBatch.DrawSprite(Sprite, gameTime, drawPosition, 0, Vector2.One, color);
            }
        }

        public class TileProperties
        {
            public TileWallJumpProperty WallJump { get; set; }
        }
    }
}
