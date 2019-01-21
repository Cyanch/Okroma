﻿using Microsoft.Xna.Framework;
using Okroma.Common;
using Okroma.World;
using Okroma.World.Tiles;
using System;
using System.Collections.Generic;

namespace Okroma
{
    public class Level
    {
        ICollection<Tileset> tilesets;
        IList<Color[]> colorMaps;

        public Point LevelSize { get; }

        public Vector2 PlayerSpawnLocation { get; }
        public byte PlayerLayer { get; }

        public Level(ICollection<Tileset> tilesets, Point mapSize, IList<Color[]> colorMaps, Vector2 playerSpawnLocation, byte playerLayer)
        {
            this.tilesets = tilesets ?? throw new ArgumentNullException(nameof(tilesets));
            this.LevelSize = mapSize;
            this.colorMaps = colorMaps ?? throw new ArgumentNullException(nameof(colorMaps));
            PlayerSpawnLocation = playerSpawnLocation;
            PlayerLayer = playerLayer;
        }

        public World2D Create(Range<float> depthRange, int chunkSize)
        {
            byte layerCount = (byte)colorMaps.Count;
            var world = new World2D(GameScale.TileSize, layerCount, depthRange, chunkSize, (int)Math.Ceiling(LevelSize.X / (float)chunkSize), (int)Math.Ceiling(LevelSize.Y / (float)chunkSize));

            for (var layer = 0; layer < layerCount; layer++)
            {
                for (var y = 0; y < LevelSize.Y; y++)
                {
                    for (var x = 0; x < LevelSize.Y; x++)
                    {
                        var tile = GetTileForColor(colorMaps[layer][x + y * LevelSize.X]);
                        if (tile != null)
                        {
                            world.PlaceTile(x, y, layer, tile);
                        }
                    }
                }
            }
            return world;
        }

        private ITile GetTileForColor(Color color)
        {
            foreach (var set in tilesets)
            {
                if (set.ContainsKey(color))
                {
                    return set[color];
                }
            }
            return null;
        }
    }
}
