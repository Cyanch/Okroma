using Cyanch.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Okroma.TileEngine
{
    public interface ITileMap : ICollisionProvider
    {
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        Tile GetTile(int x, int y, int layer);
        void SetTile(int x, int y, int layer, int id);
    }

    /// <summary>
    /// Map of tiles.
    /// </summary>
    public class TileMap : ITileMap, ITileDataSource
    {
        private Tile[,,] _tileArray;
        private Rectangle _mapArea;

        private ContentManager _content;

        public TileMap(int width, int height, int layers)
        {
            _tileArray = new Tile[width, height, layers];

            // Initialize Tile Array Contents
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int l = 0; l < layers; l++)
                    {
                        _tileArray[x, y, l] = new Tile(this, x, y);
                    }
                }
            }

            _mapArea = new Rectangle(0, 0, GameScale.FromTile(_tileArray.GetLength(0)).Pixels, GameScale.FromTile(_tileArray.GetLength(1)).Pixels);
        }

        public void LoadContent(ContentManager content)
        {
            _content = new ContentManager(content.ServiceProvider, content.RootDirectory);
        }

        public void UnloadContent()
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var tile in _tileArray)
            {
                tile.Draw(gameTime, spriteBatch);
            }
        }

        public Tile GetTile(int x, int y, int layer)
        {
            return _tileArray[x, y, layer];
        }

        public void SetTile(int x, int y, int layer, int tileId)
        {
            _tileArray[x, y, layer].Set(this, tileId);
        }

        public virtual IReadOnlyList<ICollider> GetColliders(Rectangle rect)
        {
            var colliders = new List<ICollider>();
            Rectangle inTileMapRect = Rectangle.Intersect(rect, _mapArea);
            if (inTileMapRect != Rectangle.Empty)
            {
                int xInitial = (int)Math.Floor((float)inTileMapRect.X / GameScale.TileSize);
                int yInitial = (int)Math.Floor((float)inTileMapRect.Y / GameScale.TileSize);
                int xEnd = (int)Math.Ceiling((float)inTileMapRect.Width / GameScale.TileSize) - xInitial;
                int yEnd = (int)Math.Ceiling((float)inTileMapRect.Height / GameScale.TileSize) - yInitial;

                int layerCount = _tileArray.GetLength(2);
                for (int x = xInitial; x < xEnd; x++)
                {
                    for (int y = yInitial; y < yEnd; y++)
                    {
                        for (int layer = 0; layer < layerCount; layer++)
                        {
                            colliders.Add(_tileArray[x, y, layer]);
                        }
                    }
                }
            }
            return colliders;
        }

        public virtual IReadOnlyList<ICollider> GetAllColliders()
        {
            var colliders = new List<ICollider>();
            foreach (var tile in _tileArray)
            {
                colliders.Add(tile);
            }
            return colliders;
        }

        TileData ITileDataSource.LoadTileData(string path)
        {
            return _content.Load<TileData>(path);
        }
    }

    //public class Map : IEquatable<Map>
    //{
    //    public int Id { get; }

    //    public Vector2 PlayerSpawnLocation { get; }
    //    public List<MapLayer> Layers { get; }
    //    private  MapLayer[] _nonModifiedLayers;

    //    public Point Dimensions { get; }
    //    public ContentManager Content { get; }

    //    public MapState  CurrentState { get; private set; }
    //    public override bool Equals(object obj)
    //    {
    //        return Equals(obj as Map);
    //    }

    //    public bool Equals(Map other)
    //    {
    //        return other != null &&
    //               Id == other.Id;
    //    }

    //    public override int GetHashCode()
    //    {
    //        return Id;
    //    }

    //    public Map(int id, Vector2 playerSpawnLocation, List<MapLayer> layers, List<Entity> entities)
    //    {
    //        Id = id;
    //        PlayerSpawnLocation = playerSpawnLocation;
    //        Layers = layers ?? throw new ArgumentNullException(nameof(layers));
    //    }

    //    public void UnloadContent()
    //    {
    //        Content.Unload();
    //    }

    //    public void UpdateFromSave(SaveGame saveGame)
    //    {
    //        _nonModifiedLayers = Layers.ToArray();
    //        MapState state = saveGame.GetMapState(this.Id);
    //        if (state != null)
    //            SetState(state);
    //    }

    //    private void SetState(MapState state)
    //    {
    //        foreach (var tileState in state.TileMap)
    //        {
    //            (int layer, Point position) = tileState.Key;
    //            int value = tileState.Value;

    //            Layers[layer].SetTile(position.X, position.Y, value);
    //        }
    //        CurrentState = state;
    //    }

    //    public void SetTile(int layer, int x, int y, int id)
    //    {
    //        Layers[layer].SetTile(x, y, id);
    //    }
    //}

    //public class MapState
    //{
    //    private Dictionary<(int, Point), int> _tileMap = new Dictionary<(int, Point), int>();
    //    public IReadOnlyDictionary<(int, Point), int> TileMap => _tileMap;

    //    public static void WriteToBinary(MapState mapState, BinaryWriter writer)
    //    {
    //        writer.Write(mapState.TileMap.Count);
    //        foreach (var tile in mapState.TileMap)
    //        {
    //            (int layer, Point position) = tile.Key;
    //            int tileId = tile.Value;

    //            writer.Write(layer);
    //            writer.Write(position);
    //            writer.Write(tileId);
    //        }
    //    }

    //    public static MapState ReadFromBinary(BinaryReader reader)
    //    {
    //        var mapState = new MapState();

    //        int tileCount = reader.ReadInt32();
    //        for (int i = 0; i < tileCount; i++)
    //        {
    //            int layer = reader.ReadInt32();
    //            Point pos = reader.ReadPoint();
    //            int tileId = reader.ReadInt32();

    //            mapState._tileMap.Add((layer, pos), tileId);
    //        }

    //        return mapState;
    //    }
    //}

    //public class MapLayer : IEnumerable<Tile>
    //{
    //    public const int Background = 0;
    //    public const int Foreground = 1;

    //    Tile[,] _tileArray;
    //    ICollisionService _collisionService;

    //    public MapLayer(Map map, Tile[,] tiles)
    //    {
    //        _tileArray = tiles;
    //        //_tileArray = new Tile[width, height];
    //        //for (int y = 0; y < height; y++)
    //        //{
    //        //    for (int x = 0; x < width; x++)
    //        //    {
    //        //        _tileArray[x, y] = new Tile(map, x, y);
    //        //    }
    //        //}
    //    }

    //    public IEnumerator<Tile> GetEnumerator()
    //    {
    //        return (IEnumerator<Tile>)_tileArray.GetEnumerator();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public void SetCollisionService(ICollisionService collisionService)
    //    {
    //        foreach (var tile in _tileArray)
    //        {
    //            _collisionService?.UnregisterCollisionProvider(tile);
    //            collisionService.RegisterCollisionProvider(tile);
    //        }

    //        _collisionService = collisionService;
    //    }

    //    public Tile GetTile(int x, int y)
    //    {
    //        return _tileArray[x, y];
    //    }

    //    public void SetTile(int x, int y, int id)
    //    {
    //        _tileArray[x, y].SetId(id);
    //    }
    //}
}
