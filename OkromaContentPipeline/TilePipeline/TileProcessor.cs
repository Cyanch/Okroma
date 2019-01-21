using Microsoft.Xna.Framework.Content.Pipeline;

namespace OkromaContentPipeline.TilePipeline
{
    [ContentProcessor(DisplayName = "Tile -- " + nameof(OkromaContentPipeline))]
    public class TileProcessor : ContentProcessor<TileFile, ProcessorResult<TileFile>>
    {
        public override ProcessorResult<TileFile> Process(TileFile input, ContentProcessorContext context)
        {
            return new ProcessorResult<TileFile>(input);
        }
    }
}
