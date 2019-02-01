using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Okroma.TileEngine
{
    public struct Tile : IEquatable<Tile>
    {
        public ushort Id { get; }
        public byte Variant { get; }

        public Texture2D Texture { get; }
        private Dictionary<byte, Point> _texturePositions;

        public void Draw(SpriteBatch spriteBatch, Vector2 drawPosition, byte variant, Color color)
        {
            spriteBatch.Draw(Texture, drawPosition, new Rectangle(_texturePositions[variant], GameScale.TileSizePoint), color);
        }

        public override bool Equals(object obj)
        {
            return obj is Tile && Equals((Tile)obj);
        }

        public bool Equals(Tile other)
        {
            return Id == other.Id &&
                   Variant == other.Variant;
        }

        public override int GetHashCode()
        {
            var hashCode = 31871270;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + Variant.GetHashCode();
            return hashCode;
        }
    }
}
