using Cyanch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Okroma
{
    public static class SpriteExtensions
    {
        public static void DrawAt(this ISprite sprite, GameTime gameTime, SpriteBatch spriteBatch, ITransform2D transform, Color color, SpriteEffects flip = default(SpriteEffects), float depth = 0)
        {
            transform.DrawSprite(gameTime, spriteBatch, sprite, color, flip, depth);
        }
    }
}
