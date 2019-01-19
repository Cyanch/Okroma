using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace OkromaContentPipeline.ContentWriters
{
    [ContentTypeWriter]
    public class SpriteContentWriter : ContentTypeWriter<Sprite>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            throw new System.NotImplementedException();
        }

        protected override void Write(ContentWriter output, Sprite value)
        {
            output.Write(value.TexturePath);
            output.WriteObject(value.SourceRectangle, new XnaRectangleContentWriter());
            output.Write(value.Origin);
        }
    }
}
