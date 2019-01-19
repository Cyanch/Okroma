using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Okroma
{
    public interface ISprite
    {
        int TextureWidth { get; }
        int TextureHeight { get; }
        void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, float rotation, Vector2 scale, Color color, SpriteEffects flip = default(SpriteEffects), float depth = 0);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle destinationRectangle, float rotation, Color color, SpriteEffects flip = default(SpriteEffects), float depth = 0);
    }

    public class Sprite : ISprite
    {
        public Texture2D Texture { get; }

        public Rectangle? SourceRectangle { get; }
        public Vector2 Origin { get; }

        public int TextureWidth => Texture.Width;
        public int TextureHeight => Texture.Height;

        public Sprite(Texture2D texture) : this(texture, null) { }
        public Sprite(Texture2D texture, Rectangle? sourceRectangle) : this(texture, sourceRectangle, default(Vector2)) { }
        public Sprite(Texture2D texture, Rectangle? sourceRectangle, Vector2 origin)
        {
            this.Texture = texture;
            this.SourceRectangle = sourceRectangle;
            this.Origin = origin;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, float rotation, Vector2 scale, Color color, SpriteEffects flip = default(SpriteEffects), float depth = 0)
        {
            spriteBatch.Draw(Texture, position, SourceRectangle, color, rotation, Origin, scale, flip, depth);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle destinationRectangle, float rotation, Color color, SpriteEffects flip = default(SpriteEffects), float depth = 0)
        {
            spriteBatch.Draw(Texture, destinationRectangle, SourceRectangle, color, rotation, Origin, flip, depth);
        }
    }

    public static class SpriteExtensions
    {
        public static void DrawAt(this ISprite sprite, GameTime gameTime, SpriteBatch spriteBatch, ITransform2D transform, Color color, SpriteEffects flip = default(SpriteEffects), float depth = 0)
        {
            transform.DrawSprite(gameTime, spriteBatch, sprite, color, flip, depth);
        }
    }
}
