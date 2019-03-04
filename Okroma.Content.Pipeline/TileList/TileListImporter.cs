using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json;
using System.IO;

namespace Okroma.Content.Pipeline.TileList
{
    [ContentImporter(".json", DisplayName = "TileList Importer -- Okroma.Content.Pipeline", DefaultProcessor = nameof(TileListProcessor))]
    public class TileListImporter : ContentImporter<TileListFile>
    {
        public override TileListFile Import(string filename, ContentImporterContext context)
        {
            using (var reader = new StreamReader(filename))
                return JsonConvert.DeserializeObject<TileListFile>(reader.ReadToEnd());
        }
    }
}
