using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json;
using System.IO;

namespace OkromaContentPipeline.TilePipelline
{
    [ContentImporter(
        ".json",
        DefaultProcessor = nameof(TileProcessor),
        DisplayName = "Tile Importer -- " + nameof(OkromaContentPipeline))]
    public class TileImporter : ContentImporter<TileFile>
    {
        public override TileFile Import(string filename, ContentImporterContext context)
        {
            using (var reader = new StreamReader(filename))
            {
                return JsonConvert.DeserializeObject<TileFile>(reader.ReadToEnd());
            }
        }
    }
}
