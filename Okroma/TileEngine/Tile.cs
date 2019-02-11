using Cyanch;
using Cyanch.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Okroma.Physics;
using Okroma.TileEngine.TileProperties;
using System;
using System.IO;

namespace Okroma.TileEngine
{
    public class TileData
    {
        public ISprite Sprite { get; }
        public Rectangle Bounds { get; }

        public TileProperties Properties { get; }

        public TileData(ISprite sprite, Rectangle bounds)
        {
            this.Sprite = sprite;
            this.Bounds = bounds;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 drawPosition, Color color)
        {
            spriteBatch.DrawSprite(Sprite, gameTime, drawPosition, 0, Vector2.One, color);
        }

        public class TileProperties
        {
            public TileWallJumpProperty WallJump { get; set; }
        }
    }

    public enum GameTile
    {
        None = 0,
    }

    public struct Tile : ICollider, IEquatable<Tile>
    {
        public Map Map { get; }

        public int Id { get; private set; }

        //Bounding Box.
        public Rectangle Rect { get; private set; }

        public TileData Data { get; private set; }

        public int MapX { get; }
        public int MapY { get; }

        private readonly Vector2 _drawPosition;

        public event EventHandler Moved;

        public Tile(Map map, int mapX, int mapY)
        {
            this.Map = map;

            this.Id = (int)GameTile.None;
            this.Rect = Rectangle.Empty;

            this.Data = null;

            this.MapX = mapX;
            this.MapY = mapY;

            _drawPosition = new Vector2(GameScale.FromTile(mapX).Pixels, GameScale.FromTile(mapY).Pixels);

            Moved = default;
        }

        public void SetId(int id)
        {
            this.Id = id;
            this.Data = Map.Content.Load<TileData>(Path.Combine("Tiles", id.ToString()));

            var bounds = Data.Bounds;
            bounds.Offset(_drawPosition);
            this.Rect = bounds;
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

        public bool CanCollide(ICollider collider)
        {
            return Collision.CanCollide(this, collider);
        }
    }
}
