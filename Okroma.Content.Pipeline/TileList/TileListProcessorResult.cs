using System;

namespace Okroma.Content.Pipeline.TileList
{
    public class TileListProcessorResult
    {
        public TileListFile Data { get; }

        public TileListProcessorResult(TileListFile data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }
    }
}
