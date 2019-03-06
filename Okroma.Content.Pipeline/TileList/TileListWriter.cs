using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace Okroma.Content.Pipeline.TileList
{
    [ContentTypeWriter]
    public class TileListWriter : ContentTypeWriter<TileListProcessorResult>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Okroma.TileEngine.TileListReader, Okroma";
        }

        protected override void Write(ContentWriter writer, TileListProcessorResult value)
        {
            writer.Write(value.Tiles);
        }
    }
}
