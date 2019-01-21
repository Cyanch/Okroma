using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace OkromaContentPipeline.TilePipeline
{
    [ContentTypeWriter]
    public class TileWriter : ContentTypeWriter<TileProcessorResult>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Okroma.ContentReaders.TileContentReader, Okroma";
        }

        protected override void Write(ContentWriter output, TileProcessorResult value)
        {
            output.Write(value.Type);
            output.Write(value.Sprite);
            output.Write(value.IsWallJumpable);
        }
    }
}
