using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Okroma.Sprites
{
    class TexturedSprite : Sprite
    {
        public Texture2D Texture { get; }
        public Rectangle? SourceRectangle { get; }
        public Vector2 Origin { get; }

        public TexturedSprite(Texture2D texture, Rectangle? sourceRectangle, Vector2 origin)
        {
            this.Texture = texture ?? throw new ArgumentNullException(nameof(texture));
            this.SourceRectangle = sourceRectangle;
            this.Origin = origin;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, SpriteProperties properties)
        {
            spriteBatch.Draw(this.Texture, position, this.SourceRectangle, properties.Color, properties.Rotation, this.Origin, properties.Scale, properties.SpriteEffects, properties.LayerDepth);
        }
    }
}
