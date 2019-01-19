using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace OkromaContentPipeline.JsonConverters
{
    public class XnaRectangleJsonConverter : JsonConverter<Rectangle>
    {
        public override bool CanRead => true;
        public override Rectangle ReadJson(JsonReader reader, Type objectType, Rectangle existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            //Expected: [X(int) Y(int) Width(int) Height(int)]
            string[] values = ((string)reader.Value).Split(' ');
            return new Rectangle(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3]));
        }
        
        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, Rectangle value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
