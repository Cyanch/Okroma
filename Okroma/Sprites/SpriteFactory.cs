using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Okroma.Sprites
{
    class SpriteFactory
    {
        public static Sprite Create(Texture2D texture)
        {
            return Create(texture, null, Vector2.Zero);
        }

        public static Sprite Create(Texture2D texture, Rectangle? sourceRectangle)
        {
            return Create(texture, sourceRectangle, Vector2.Zero);
        }

        public static Sprite Create(Texture2D texture, Vector2 origin)
        {
            return Create(texture, null, origin);
        }

        public static Sprite Create(Texture2D texture, Rectangle? sourceRectangle, Vector2 origin)
        {
            if (texture == null)
                return Sprite.Null;
            return new TexturedSprite(texture, sourceRectangle, origin);
        }
    }
}
