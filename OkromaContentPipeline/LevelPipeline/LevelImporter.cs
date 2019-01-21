using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json;
using System.IO;

namespace OkromaContentPipeline.LevelPipeline
{
    [ContentImporter(".json", DefaultProcessor = nameof(LevelProcessor), DisplayName = "Level Importer -- " + nameof(OkromaContentPipeline))]
    public class LevelImporter : ContentImporter<LevelFile>
    {
        public override LevelFile Import(string filename, ContentImporterContext context)
        {
            using (var reader = new StreamReader(filename))
            {
                return JsonConvert.DeserializeObject<LevelFile>(reader.ReadToEnd());
            }
        }
    }
}
