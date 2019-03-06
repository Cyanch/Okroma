using Newtonsoft.Json;
using System.Collections.Generic;

namespace Okroma.Content.Pipeline.TileList
{
    public class TileListFile
    {
        [JsonProperty("presets")]
        public Dictionary<string, TileProperties> Presets { get; set; }

        [JsonProperty("tiles", Required = Required.Always)]
        public Tile[] Tiles { get; set; }
    }
}
