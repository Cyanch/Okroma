using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Newtonsoft.Json;
using Okroma.Content.Pipeline.JsonConverters;

namespace Okroma.Content.Pipeline.TileList
{
    public struct TileProperties : IContentWriteable
    {
        [JsonProperty("bounds")]
        [JsonConverter(typeof(RectangleJsonConverter))]
        public Rectangle CollisionBounds { get; set; }
        
        public TileProperties Override(TileProperties properties)
        {
            var newProperties = this;

            if (properties.CollisionBounds != Rectangle.Empty)
                newProperties.CollisionBounds = properties.CollisionBounds;

            return newProperties;
        }

        public void Write(ContentWriter writer)
        {
            writer.Write(CollisionBounds);
        }
    }
}
