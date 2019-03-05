using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Okroma.Sprites
{
    static class SpriteExtensions
    {
        public static void Draw(this Sprite sprite, GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
        {
            sprite.Draw(gameTime, spriteBatch, position, SpriteProperties.Default);
        }

        public static void DrawSprite(this SpriteBatch spriteBatch, Sprite sprite, GameTime gameTime, Vector2 position)
        {
            sprite.Draw(gameTime, spriteBatch, position, SpriteProperties.Default);
        }

        public static void DrawSprite(this SpriteBatch spriteBatch, Sprite sprite, GameTime gameTime, Vector2 position, SpriteProperties properties)
        {
            sprite.Draw(gameTime, spriteBatch, position, properties);
        }
    }
}
