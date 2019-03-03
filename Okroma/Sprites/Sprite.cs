using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Okroma.Sprites
{
    struct SpriteProperties
    {
        public static readonly SpriteProperties Default = new SpriteProperties() { Color = Color.White, Scale = Vector2.One };

        public Color Color { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public SpriteEffects SpriteEffects { get; set; }
        public float LayerDepth { get; set; }
    }

    abstract class Sprite
    {
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position) => Draw(gameTime, spriteBatch, position, SpriteProperties.Default);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, SpriteProperties properties);
    }
}
