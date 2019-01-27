using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Cyanch.UI
{
    public interface ITextElement : IUIElement, IScalable
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
        public Vector2 Scale
        {
            get => _scale; set
            {
                _scale = value;
                OnScaleChanged(this, EventArgs.Empty);
            }
        }

        private string _text;
        private Vector2 _origin;
        private Vector2 _scale = Vector2.One;

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.DrawString(Font, Text, GetAlignedPosition(), TextColor, 0f, _origin, Scale, SpriteEffects.None, 0f);
        }

        private void CalculateTextMeasure()
        {
            // may cause problems, but is workaround for allowing the setting text before font.
            if (Font == null)
                return;

            var oldMeasure = TextMeasure;
            TextMeasure = Font.MeasureString(Text);
            if (oldMeasure != TextMeasure)
            {
                OnTextMeasureChanged(this, EventArgs.Empty);
            }
            UpdateOrigin();
        }

        private void UpdateOrigin()
        {
            switch (Alignment)
            {
                default:
                    //Inc. Alignment.TopLeft
                    _origin = new Vector2(0, 0);
                    break;

                case Alignment.TopCenter:
                    _origin = new Vector2(TextMeasure.X / 2, 0);
                    break;
                case Alignment.TopRight:
                    _origin = new Vector2(TextMeasure.X, 0);
                    break;

                case Alignment.MiddleLeft:
                    _origin = new Vector2(0, TextMeasure.Y / 2);
                    break;
                case Alignment.MiddleCenter:
                    _origin = new Vector2(TextMeasure.X / 2, TextMeasure.Y / 2);
                    break;
                case Alignment.MiddleRight:
                    _origin = new Vector2(TextMeasure.X, TextMeasure.Y / 2);
                    break;

                case Alignment.BottomLeft:
                    _origin = new Vector2(0, TextMeasure.Y);
                    break;
                case Alignment.BottomCenter:
                    _origin = new Vector2(TextMeasure.X / 2, TextMeasure.Y);
                    break;
                case Alignment.BottomRight:
                    _origin = new Vector2(TextMeasure.X, TextMeasure.Y);
                    break;
            }
        }

        public event EventHandler TextMeasureChanged;
        public event EventHandler ScaleChanged;

        protected override void OnAlignmentChanged(object sender, EventArgs e)
        {
            UpdateOrigin();
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

        protected virtual void OnScaleChanged(object sender, EventArgs e)
        {
            ScaleChanged?.Invoke(sender, e);
        }
    }
}
