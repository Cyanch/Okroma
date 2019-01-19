using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace OkromaContentPipeline.ContentWriters
{
    public static class ContentWriterExtensions
    {
        public static void Write(this ContentWriter output, Rectangle rectangle)
        {
            output.Write(rectangle.X);
            output.Write(rectangle.Y);
            output.Write(rectangle.Width);
            output.Write(rectangle.Height);
        }
    }
}
