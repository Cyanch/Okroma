using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Okroma.ContentExtensions
{
    static class ContentReaderExtensions
    {
        public static Rectangle ReadRectangle(this ContentReader reader)
        {
            return new Rectangle(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
        }

        /// <summary>
        /// Empty is treated as null rectangle.
        /// </summary>
        /// <returns></returns>
        public static Rectangle? ReadNullableRectangle(this ContentReader reader)
        {
            Rectangle? rect = ReadRectangle(reader);
            return rect == Rectangle.Empty ? null : rect;
        }
    }
}
