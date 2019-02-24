using Cyanch.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace Okroma.TileEngine
{
    public struct Tile : ICollider, IEquatable<Tile>
    {
        public TileMap Map { get; }

        public int Id { get; private set; }

        //Bounding Box.
        public Rectangle Bounds { get; private set; }

        public TileData Data { get; private set; }

        public int MapX { get; }
        public int MapY { get; }

        private readonly Vector2 _drawPosition;

        public Tile(TileMap map, int mapX, int mapY)
        {
            this.Map = map;

            this.Id = 0;
            this.Bounds = Rectangle.Empty;

            this.Data = null;

            this.MapX = mapX;
            this.MapY = mapY;

            _drawPosition = new Vector2(GameScale.FromTile(mapX).Pixels, GameScale.FromTile(mapY).Pixels);
        }

        public void Set(ITileDataSource tileDataSource, int id)
        {
            if (id == this.Id)
                return;

            this.Id = id;

            this.Data = id <= 0 ? TileData.None : tileDataSource.LoadTileData(Path.Combine("Tiles", id.ToString()));

            var bounds = Data.Bounds;
            bounds.Offset(_drawPosition);

            this.Bounds = bounds;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Data.Draw(gameTime, spriteBatch, _drawPosition, Color.White);
        }

        public override bool Equals(object obj)
        {
            return obj is Tile && Equals((Tile)obj);
        }

        public bool Equals(Tile other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
        
        public void IsPassable(ICollider collider)
        {
            //get from tiledata.
            throw new NotImplementedException();
        }

        public void Collide(ICollider collider)
        {
            // get reaction from tiledata.
            throw new NotImplementedException();
        }
    }
}
