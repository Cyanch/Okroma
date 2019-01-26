using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cyanch.UI
{
    public interface ITextElement : IUIElement
    {
        string Text { get; set; }
        Color TextColor { get; set; }
    }

    public abstract class TextElement : UIElement, ITextElement
    {
        public string Text { get; set; }
        public Color TextColor { get; set; }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //TODO: Align properly to actual text size.
            SpriteBatch.DrawString(Font, Text, GetAlignedDrawPosition(), TextColor);
        }
    }
}
