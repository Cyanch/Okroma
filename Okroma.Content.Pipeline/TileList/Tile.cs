using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Newtonsoft.Json;

namespace Okroma.Content.Pipeline.TileList
{
    public class Tile : IContentWriteable
    {
        [JsonProperty("sprites", Required = Required.Always)]
        public TexturedSprite[] Sprites { get; set; }

        [JsonProperty("inherits_from")]
        public string[] Variants { get; set; } = new string[0];

        [JsonProperty("properties")]
        public TileProperties Properties { get; set; }

        public void Write(ContentWriter writer)
        {
            writer.Write(Sprites);
            writer.Write(Properties);
        }
    }
}
