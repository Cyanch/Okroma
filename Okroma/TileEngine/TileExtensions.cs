using Okroma.TileEngine.TileProperties;
using System;

namespace Okroma.TileEngine
{
    static class TileExtensions
    {
        public static bool HasPropeerty<T>(this Tile tile) where T : TileProperty
        {
            return tile.Properties.HasProperty<T>();
        }

        public static bool HasProperty(this Tile tile, Type propertyType)
        {
            return tile.Properties.HasProperty(propertyType);
        }

        public static T GetProperty<T>(this Tile tile) where T : TileProperty
        {
            return tile.Properties.GetProperty<T>();
        }

        public static T GetPropertyOrDefault<T>(this Tile tile, T valIfNone) where T : TileProperty
        {
            return tile.Properties.GetPropertyOrDefault(valIfNone);
        }
    }
}
