using Microsoft.Xna.Framework;
using Okroma.Physics;
using System;
using System.Collections.Generic;

namespace Okroma.TileEngine
{
    static class TileMapExtensions
    {
        public static Colliders GetColliders(this TileMap map, Rectangle area)
        {
            var colliders = new List<Collider>();
            map.ForEach((mapX, mapY, layer, tile) =>
            {
                colliders.Add(new TileCollider(mapX, mapY, tile.Properties));
            }, area);
            return new Colliders(colliders);
        }

        public delegate void MappedTileAction(int mapX, int mapY, int layer, Tile tile);

        public static void ForEach(this TileMap map, MappedTileAction onTile, Rectangle screenArea)
        {
            int x1 = MathHelper.Clamp(screenArea.X / Tile.Size, 0, map.Width.Tiles - 1);
            int y1 = MathHelper.Clamp(screenArea.Y / Tile.Size, 0, map.Height.Tiles - 1);
            int x2 = MathHelper.Clamp((int)Math.Ceiling((screenArea.Width + screenArea.X - Tile.Size) / (float)Tile.Size), 0, map.Width.Tiles - 1);
            int y2 = MathHelper.Clamp((int)Math.Ceiling((screenArea.Height + screenArea.Y - Tile.Size) / (float)Tile.Size), 0, map.Height.Tiles - 1);

            for (int layer = 0; layer < map.Layers.Length; layer++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    for (int x = x1; x <= x2; x++)
                    {
                        onTile(x, y, layer, map[x, y, layer]);
                    }
                }
            }
        }
    }
}
