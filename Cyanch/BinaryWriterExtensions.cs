using Microsoft.Xna.Framework;
using System.IO;

namespace Cyanch
{
    /// <summary>
    /// Adds support for MonoGame/Xna Types.
    /// </summary>
    public static class BinaryWriterExtensions
    {
        public static void Write(this BinaryWriter writer, Point point)
        {
            writer.Write(point.X);
            writer.Write(point.Y);
        }
    }
}
