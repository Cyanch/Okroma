using Microsoft.Xna.Framework;
using System;

namespace Cyanch.UI
{
    /// <summary>
    /// Panel that stacks its elements.
    /// </summary>
    public class StackPanel : Panel
    {
        private Orientation _orientation;
        public Orientation Orientation { get => _orientation; set => _orientation = value; }

        public override void UpdateLayout(GameTime gameTime)
        {
            base.UpdateLayout(gameTime);

            float total = 0;
            foreach (var element in GetChildren())
            {
                if (Orientation == Orientation.Vertical)
                {
                    var height = element.Height;
                    element.LocalPosition = new Vector2(element.LocalPosition.X, total);
                    total += height;
                }
                else
                {
                    var width = element.Width;
                    element.LocalPosition = new Vector2(total, element.LocalPosition.Y);
                    total += width;
                }
            }
        }

        public event EventHandler OrientationChanged;

        protected virtual void OnOrientationChanged(object sender, EventArgs e)
        {
            OrientationChanged?.Invoke(sender, e);
        }
    }
}
