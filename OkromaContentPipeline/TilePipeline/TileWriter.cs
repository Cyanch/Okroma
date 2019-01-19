using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace OkromaContentPipeline.TilePipeline
{
    [ContentTypeWriter]
    public class TileWriter : ContentTypeWriter<ProcessorResult<TileFile>>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Okroma.ContentReaders.TileContentReader, Okroma";
        }

        protected override void Write(ContentWriter output, ProcessorResult<TileFile> value)
        {
            var result = value.Result;
            output.Write(result.Type);
            output.Write(result.Sprite);
            output.Write(result.IsWallJumpable);
        }
    }
}
