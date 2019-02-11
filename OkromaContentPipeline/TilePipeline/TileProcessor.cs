using Microsoft.Xna.Framework.Content.Pipeline;
using Okroma.TileEngine.TileProperties;
using System;
using System.Collections.Generic;

namespace OkromaContentPipeline.TilePipelline
{
    [ContentProcessor(DisplayName = "Tile -- " + nameof(OkromaContentPipeline))]
    public class TileProcessor : ContentProcessor<TileFile, TileProcessorResult>
    {
        public override TileProcessorResult Process(TileFile input, ContentProcessorContext context)
        {
            var properties = new Dictionary<int, byte>();

            foreach (var property in input.Properties)
            {
                Type propValueType;
                switch(property.Key)
                {
                    case TileProperty.WallJump:
                        propValueType = typeof(TileWallJumpProperty);
                        break;

                    default:
                        propValueType = default;
                        break;
                }

                properties.Add((int)property.Key, (byte)Enum.Parse(propValueType, property.Value, true));
            }

            return new TileProcessorResult(input.Type, input.TexturePath, properties);
        }
    }
}
