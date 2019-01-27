using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyanch.UI
{
    public class Image : UIElement
    {
        private float _alpha = 1f;

        /// <summary>
        /// The Texture being drawn.
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Clipped area of Texture that is actually rendered.
        /// </summary>
        public Rectangle? SourceRectangle { get; set; }

        public void SetTexture(Texture2D texture, Rectangle? sourceRectangle, bool autoSize)
        {
            Texture = texture;
            SourceRectangle = sourceRectangle;
            if (autoSize)
                SizeToTexture();
        }

        public void SizeToTexture()
        {
            Width = SourceRectangle?.Width ?? Texture.Width;
            Height = SourceRectangle?.Height ?? Texture.Height;
        }

        /// <summary>
        /// Transparency, 0-1 ranging from fully transparent to opaque.
        /// </summary>
        public float Alpha
        {
            get => _alpha; set
            {
                _alpha = MathHelper.Clamp(value, 0, 1);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (Texture != null)
            {
                SpriteBatch.Draw(Texture, GetBounds(), SourceRectangle, Color.White * Alpha);
            }
        }
    }
}
