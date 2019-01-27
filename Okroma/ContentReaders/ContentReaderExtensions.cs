using Cyanch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Okroma.ContentReaders
{
    public static class ContentReaderExtensions
    {
        public static Sprite ReadSprite(this ContentReader input)
        {
            var texturePath = input.ReadString();
            Rectangle? sourceRectangle = input.ReadRectangle();
            var origin = input.ReadVector2();

            var texture = input.ContentManager.Load<Texture2D>(texturePath);
            if (sourceRectangle == Rectangle.Empty)
                sourceRectangle = null;

            return new Sprite(texture, sourceRectangle, origin);
        }

        public static Rectangle ReadRectangle(this ContentReader input)
        {
            return new Rectangle(input.ReadInt32(), input.ReadInt32(), input.ReadInt32(), input.ReadInt32());
        }
    }
}
