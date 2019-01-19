using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using OkromaContentPipeline.ContentWriters;

namespace OkromaContentPipeline.TilePipeline
{
    [ContentTypeWriter]
    public class TileWriter : ContentTypeWriter<ProcessorResult<TileFile>>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Okroma.ContentReaders.TileContentReader";
        }

        protected override void Write(ContentWriter output, ProcessorResult<TileFile> value)
        {
            var result = value.Result;
            output.Write(result.Type);
            output.WriteObject(result.Sprite, new SpriteContentWriter());
            output.Write(result.IsWallJumpable);
        }
    }
}
