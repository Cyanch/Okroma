using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;

namespace Okroma.Content.Pipeline.JsonConverters
{
    public class Vector2JsonConverter : JsonConverter<Vector2>
    {
        public override bool CanRead => true;
        public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string[] values = ((string)reader.Value).Split(' ');
            return new Vector2(int.Parse(values[0]), int.Parse(values[1]));
        }

        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}