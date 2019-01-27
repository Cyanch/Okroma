using System;

namespace Cyanch.UI
{
    public class Text : TextElement
    {
        public void SetText(string text, bool sizeToText)
        {
            this.Text = text ?? throw new ArgumentNullException(nameof(text));
            if (sizeToText)
            {
                Width = TextMeasure.X;
                Height = TextMeasure.Y;
            }
        }
    }
}
