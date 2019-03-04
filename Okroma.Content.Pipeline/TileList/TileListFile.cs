using Newtonsoft.Json;

namespace Okroma.Content.Pipeline.TileList
{
    public class TileListFile
    {
        [JsonProperty("tiles", Required = Required.Always)]
        public Tile[] Tiles { get; set; }
    }
}
