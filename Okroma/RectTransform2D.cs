using Cyanch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Okroma
{
    public struct RectTransform2D : ITransform2D, IEquatable<RectTransform2D>
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Vector2 Position
        {
            get
            {
                return new Vector2(X, Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)X, (int)Y, Width, Height);
            }
            set
            {
                X = value.X;
                Y = value.Y;
                Width = value.Width;
                Height = value.Height;
            }
        }

        public float Rotation { get; set; }

        public static RectTransform2D Empty { get; } = new RectTransform2D(0, 0, 0, 0);

        public RectTransform2D(int x, int y, int width, int height) : this()
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public RectTransform2D(Rectangle rectangle) : this(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)
        {
        }

        public void Offset(float x, float y) => Offset(new Vector2(x, y));
        public void Offset(Vector2 pos)
        {
            X += pos.X;
            Y += pos.Y;
        }

        public void DrawSprite(GameTime gameTime, SpriteBatch spriteBatch, ISprite sprite, Color color, SpriteEffects flip = SpriteEffects.None, float depth = 0)
        {
            sprite.Draw(gameTime, spriteBatch, Rectangle, Rotation, color, flip, depth);
        }

        public bool Equals(RectTransform2D other)
        {
            return (X, Y, Width, Height, Rotation) == (other.X, other.Y, other.Width, other.Height, other.Rotation);
        }

        public override bool Equals(object obj)
        {
            if (obj is RectTransform2D)
                return Equals((RectTransform2D)obj);
            return false;
        }

        public override string ToString()
        {
            return string.Join(", ", Rectangle, Rotation);
        }

        public override int GetHashCode()
        {
            return (X, Y, Width, Height, Rotation).GetHashCode();
        }
    }
}
