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
                SpriteBatch.Draw(Texture, GetBounds(), Color.White * Alpha);
            }
        }
    }
}
