using Microsoft.Xna.Framework.Content.Pipeline;

namespace Okroma.Content.Pipeline.TileList
{
    [ContentProcessor(DisplayName = "TileList - Okroma.Content.Pipeline")]
    public class TileListProcessor : ContentProcessor<TileListFile, TileListProcessorResult>
    {
        public override TileListProcessorResult Process(TileListFile file, ContentProcessorContext context)
        {
            return new TileListProcessorResult(file);
        }
    }
}
