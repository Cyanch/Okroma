using Microsoft.Xna.Framework;
using System.IO;

namespace Cyanch
{
    /// <summary>
    /// Adds support for MonoGame/Xna Types.
    /// </summary>
    public static class BinaryReaderExtensions
    {
        public static Point ReadPoint(this BinaryReader reader)
        {
            return new Point(reader.ReadInt32(), reader.ReadInt32());
        }
    }
}
