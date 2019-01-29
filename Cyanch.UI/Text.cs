using System;

namespace Cyanch.UI
{
    /// <summary>
    /// UI Element that shows text.
    /// </summary>
    public class Text : TextElement
    {
        /// <summary>
        /// Allows setting the text. Requires Font to be set.
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="sizeToText">Whether to set the size to the text measure or not.</param>
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
