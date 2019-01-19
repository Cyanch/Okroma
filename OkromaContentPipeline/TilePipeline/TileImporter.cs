using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkromaContentPipeline.TilePipeline
{
    [ContentImporter(
        ".json", 
        DefaultProcessor = nameof(TileProcessor), 
        DisplayName = "Tile Importer -- " + nameof(OkromaContentPipeline))]
    public class TileImporter : ContentImporter<Tile>
    {
    }
}
