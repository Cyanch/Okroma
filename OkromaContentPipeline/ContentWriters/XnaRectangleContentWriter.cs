using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace OkromaContentPipeline.ContentWriters
{
    public class XnaRectangleContentWriter : ContentTypeWriter<Rectangle>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            throw new System.NotImplementedException();
        }

        protected override void Write(ContentWriter output, Rectangle value)
        {
            output.Write(value.X);
            output.Write(value.Y);
            output.Write(value.Width);
            output.Write(value.Height);
        }
    }
}
