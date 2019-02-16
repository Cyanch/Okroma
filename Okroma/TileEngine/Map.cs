using Cyanch;
using Cyanch.Entities;
using Cyanch.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Okroma.TileEngine
{
    public class Map : IEquatable<Map>
    {
        public int Id { get; }

        public Vector2 PlayerSpawnLocation { get; }
        public List<MapLayer> Layers { get; }
        private  MapLayer[] _nonModifiedLayers;

        public Point Dimensions { get; }
        public ContentManager Content { get; }
        
        public MapState CurrentState { get; private set; }
        public override bool Equals(object obj)
        {
            return Equals(obj as Map);
        }

        public bool Equals(Map other)
        {
            return other != null &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public Map(int id, Vector2 playerSpawnLocation, List<MapLayer> layers, List<Entity> entities)
        {
            Id = id;
            PlayerSpawnLocation = playerSpawnLocation;
            Layers = layers ?? throw new ArgumentNullException(nameof(layers));
        }

        public void UnloadContent()
        {
            Content.Unload();
        }

        public void UpdateFromSave(SaveGame saveGame)
        {
            _nonModifiedLayers = Layers.ToArray();
            MapState state = saveGame.GetMapState(this.Id);
            if (state != null)
                SetState(state);
        }

        private void SetState(MapState state)
        {
            foreach (var tileState in state.TileMap)
            {
                (int layer, Point position) = tileState.Key;
                int value = tileState.Value;

                Layers[layer].SetTile(position.X, position.Y, value);
            }
            CurrentState = state;
        }

        public void SetTile(int layer, int x, int y, int id)
        {
            Layers[layer].SetTile(x, y, id);
        }
    }

    public class MapState
    {
        private Dictionary<(int, Point), int> _tileMap = new Dictionary<(int, Point), int>();
        public IReadOnlyDictionary<(int, Point), int> TileMap => _tileMap;

        public void WriteToBinary(BinaryWriter writer)
        {
            writer.Write(TileMap.Count);
            foreach (var tile in TileMap)
            {
                (int layer, Point position) = tile.Key;
                int tileId = tile.Value;

                writer.Write(layer);
                writer.Write(position);
                writer.Write(tileId);
            }
        }

        public static MapState ReadFromBinary(BinaryReader reader)
        {
            var mapState = new MapState();

            int tileCount = reader.ReadInt32();
            for (int i = 0; i < tileCount; i++)
            {
                int layer = reader.ReadInt32();
                Point pos = reader.ReadPoint();
                int tileId = reader.ReadInt32();

                mapState._tileMap.Add((layer, pos), tileId);
            }

            return mapState;
        }
    }

    public class MapLayer : IEnumerable<Tile>
    {
        public const int Background = 0;
        public const int Foreground = 1;

        Tile[,] _tileArray;
        ICollisionService _collisionService;

        public MapLayer(Map map, Tile[,] tiles)
        {
            _tileArray = tiles;
            //_tileArray = new Tile[width, height];
            //for (int y = 0; y < height; y++)
            //{
            //    for (int x = 0; x < width; x++)
            //    {
            //        _tileArray[x, y] = new Tile(map, x, y);
            //    }
            //}
        }

        public IEnumerator<Tile> GetEnumerator()
        {
            return (IEnumerator<Tile>)_tileArray.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
        
        public void SetCollisionService(ICollisionService collisionService)
        {
            foreach (var tile in _tileArray)
            {
                _collisionService?.UnregisterCollider(tile);
                collisionService.RegisterCollider(tile);
            }

            _collisionService = collisionService;
        }

        public Tile GetTile(int x, int y)
        {
            return _tileArray[x, y];
        }

        public void SetTile(int x, int y, int id)
        {
            _tileArray[x, y].SetId(id);
        }
    }
}
