using Cyanch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Okroma
{
    public interface IHasTransform2D
    {
        ITransform2D Transform { get; set; }
    }

    public interface ITransform2D
    {
        Vector2 Position { get; set; }
        float Rotation { get; set; }

        void Offset(float x, float y);
        void Offset(Vector2 move);

        void DrawSprite(GameTime gameTime, SpriteBatch spriteBatch, ISprite sprite, Color color, SpriteEffects flip = default(SpriteEffects), float depth = 0);
    }

    public struct Transform2D : ITransform2D, IEquatable<Transform2D>
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public static Transform2D None { get; } = new Transform2D(0, 0);

        public Transform2D(float x, float y) : this(new Vector2(x, y)) { }
        public Transform2D(Vector2 position) : this(position, Vector2.One)
        {
            this.Position = position;
        }
        public Transform2D(float x, float y, float scale) : this(new Vector2(x, y), new Vector2(scale)) { }
        public Transform2D(float x, float y, float scaleX, float scaleY) : this(new Vector2(x, y), new Vector2(scaleX, scaleY)) { }
        public Transform2D(Vector2 position, Vector2 scale) : this()
        {
            this.Position = position;
            this.Scale = scale;
        }

        public void Offset(float x, float y) => Offset(new Vector2(x, y));
        public void Offset(Vector2 move)
        {
            Position += move;
        }

        public void DrawSprite(GameTime gameTime, SpriteBatch spriteBatch, ISprite sprite, Color color, SpriteEffects flip = default(SpriteEffects), float depth = 0)
        {
            sprite.Draw(gameTime, spriteBatch, Position, Rotation, Scale, color, flip, depth);
        }

        public bool Equals(Transform2D other)
        {
            return (Position, Rotation, Scale) == (other.Position, other.Rotation, other.Scale);
        }

        public override string ToString()
        {
            return "{Position" + Position.ToString() + " Rotation{" + Rotation.ToString() + "} Scale" + Scale.ToString() + "}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Transform2D transform)
                return transform.Equals(this);

            return false;
        }

        public override int GetHashCode()
        {
            return (Position, Rotation, Scale).GetHashCode();
        }
    }

    public static class Transform2DExtensions
    {
        public static void Rotate(this ITransform2D transform, float radians)
        {
            transform.Rotation = (transform.Rotation + radians) % MathHelper.TwoPi;
        }

        public static void RotateByDegrees(this ITransform2D transform, float degrees)
        {
            transform.Rotation = (transform.Rotation + MathHelper.ToRadians(degrees)) % MathHelper.TwoPi;
        }
    }
}
