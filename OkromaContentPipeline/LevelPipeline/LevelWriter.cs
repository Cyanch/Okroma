using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace OkromaContentPipeline.LevelPipeline
{
    [ContentTypeWriter]
    public class LevelWriter : ContentTypeWriter<ProcessorResult<LevelFile>>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Okroma.ContentReaders.LevelContentReader, Okroma";
        }

        protected override void Write(ContentWriter output, ProcessorResult<LevelFile> value)
        {
            var result = value.Result;
            output.Write(result.TilesetPaths.Length);
            foreach (var path in result.TilesetPaths)
            {
                output.Write(path);
            }
            output.Write(result.TileMapPaths.Length);
            foreach (var path in result.TileMapPaths)
            {
                output.Write(path);
            }
        }
    }
}
