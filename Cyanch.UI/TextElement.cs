using System;
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
        public Vector2 TextSize { get; private set; }

        public override void Draw(GameTime gameTime)
        {
            //TODO: Align properly to actual text size.
            //TODO: Use Origin. Related to UIElement#223.
            SpriteBatch.DrawString(Font, Text, GetAlignedDrawPosition(), TextColor);
        }
    }
}
