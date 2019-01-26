using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Okroma.Utils.C3;
using System;
using System.Diagnostics;

namespace Cyanch.UI
{
    public interface ITextElement : IUIElement
    {
        string Text { get; set; }
        Color TextColor { get; set; }
    }

    public abstract class TextElement : UIElement, ITextElement
    {
        public string Text
        {
            get
            {
                return _text ?? string.Empty;
            }
            set
            {
                _text = value ?? string.Empty;
                CalculateTextMeasure();
            }
        }
        public Color TextColor { get; set; } = Color.White;
        public Vector2 TextMeasure { get; private set; }

        private string _text;
        private Vector2 _textOrigin;

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.DrawString(Font, Text, GetAlignedPosition(), TextColor, 0f, _textOrigin, 1f, SpriteEffects.None, 0f);
        }

        public void SetText(string text, bool sizeToText)
        {
            this.Text = text ?? throw new ArgumentNullException(nameof(text));
            if (sizeToText)
            {
                Width = TextMeasure.X;
                Height = TextMeasure.Y;
            }
        }

        private void CalculateTextMeasure()
        {
            var oldMeasure = TextMeasure;
            TextMeasure = Font.MeasureString(Text);
            if (oldMeasure != TextMeasure)
            {
                OnTextMeasureChanged(this, EventArgs.Empty);
            }
            UpdateTextOrigin();
        }

        private void UpdateTextOrigin()
        {
            switch (Alignment)
            {
                default:
                    //Inc. Alignment.TopLeft
                    _textOrigin = new Vector2(0, 0);
                    break;

                case Alignment.TopCenter:
                    _textOrigin = new Vector2(TextMeasure.X / 2, 0);
                    break;
                case Alignment.TopRight:
                    _textOrigin = new Vector2(TextMeasure.X, 0);
                    break;

                case Alignment.MiddleLeft:
                    _textOrigin = new Vector2(0, TextMeasure.Y / 2);
                    break;
                case Alignment.MiddleCenter:
                    _textOrigin = new Vector2(TextMeasure.X / 2, TextMeasure.Y / 2);
                    break;
                case Alignment.MiddleRight:
                    _textOrigin = new Vector2(TextMeasure.X, TextMeasure.Y / 2);
                    break;

                case Alignment.BottomLeft:
                    _textOrigin = new Vector2(0, TextMeasure.Y);
                    break;
                case Alignment.BottomCenter:
                    _textOrigin = new Vector2(TextMeasure.X / 2, TextMeasure.Y);
                    break;
                case Alignment.BottomRight:
                    _textOrigin = new Vector2(TextMeasure.X, TextMeasure.Y);
                    break;
            }
        }

        public event EventHandler TextMeasureChanged;

        protected override void OnAlignmentChanged(object sender, EventArgs e)
        {
            UpdateTextOrigin();
            base.OnAlignmentChanged(sender, e);
        }

        protected override void OnFontChanged(object sender, EventArgs e)
        {
            CalculateTextMeasure();
            base.OnFontChanged(sender, e);
        }

        protected virtual void OnTextMeasureChanged(object sender, EventArgs e)
        {
            TextMeasureChanged?.Invoke(sender, e);
        }
    }
}
