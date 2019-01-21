using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace OkromaContentPipeline.TilesetPipeline
{
    [ContentImporter(".json", DefaultProcessor = nameof(TilesetProcessor), DisplayName = "Tileset Importer -- " + nameof(OkromaContentPipeline))]
    public class TilesetImporter : ContentImporter<Dictionary<string, string>>
    {
        public override Dictionary<string, string> Import(string filename, ContentImporterContext context)
        {
            using (var reader = new StreamReader(filename))
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(reader.ReadToEnd());
            }
        }
    }
}
