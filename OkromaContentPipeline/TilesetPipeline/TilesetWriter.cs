using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace OkromaContentPipeline.TilesetPipeline
{
    [ContentTypeWriter]
    public class TilesetWriter : ContentTypeWriter<TilesetData>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Okroma.ContentReaders.TilesetContentReader, Okroma";
        }

        protected override void Write(ContentWriter output, TilesetData value)
        {
            output.Write(value.ColorTilePairCount);
            foreach (var pair in value.GetColorTilePairs())
            {
                output.Write(pair.Key);
                output.Write(pair.Value);
            }
        }
    }
}
