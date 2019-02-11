using System;
using System.Collections.Generic;

namespace OkromaContentPipeline.TilePipelline
{
    public class TileProcessorResult
    {
        public string Type { get; }
        public string TexturePath { get; }
        public Dictionary<int, byte> Properties { get; }

        public TileProcessorResult(string type, string texturePath, Dictionary<int, byte> properties)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            TexturePath = texturePath ?? throw new ArgumentNullException(nameof(texturePath));
            Properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }
    }
}
