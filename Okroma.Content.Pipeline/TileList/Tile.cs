using Newtonsoft.Json;

namespace Okroma.Content.Pipeline.TileList
{
    public class Tile
    {
        [JsonProperty("sprites", Required = Required.Always)]
        public TexturedSprite[] Sprites { get; set; }
    }
}
