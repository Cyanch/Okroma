using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Okroma.TileEngine
{
    public struct Tile : IEquatable<Tile>
    {
        public int Id { get; }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawPosition, Color color)
        {
            throw new System.NotImplementedException();
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
    }

    public static class TileExtensions
    {
        public static void DrawTile(this SpriteBatch spriteBatch, Tile tile, Vector2 drawPosition, Color color)
        {
            tile.Draw(spriteBatch, drawPosition, color);
        }
    }
}
