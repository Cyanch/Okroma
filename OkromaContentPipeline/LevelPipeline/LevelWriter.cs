using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace OkromaContentPipeline.LevelPipeline
{
    [ContentTypeWriter]
    public class LevelWriter : ContentTypeWriter<LevelProcessorResult>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Okroma.ContentReaders.LevelContentReader, Okroma";
        }

        protected override void Write(ContentWriter output, LevelProcessorResult value)
        {
            output.Write(value.TilesetPaths.Length);
            foreach (var path in value.TilesetPaths)
            {
                output.Write(path);
            }
            output.Write(value.TileMapPaths.Length);
            foreach (var path in value.TileMapPaths)
            {
                output.Write(path);
            }
            output.Write(value.PlayerSpawnPosition);
            output.Write(value.PlayerLayer);
        }
    }
}
