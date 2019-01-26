using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Cyanch.UI
{
    public class MouseStateEventArgs : EventArgs
    {
        public MouseState Previous { get; }
        public MouseState Current { get; }

        private InputState input;
        public MouseStateEventArgs(InputState input)
        {
            this.input = input;
        }

        public Point GetMousePosition()
        {
            return input.GetMousePosition();
        }
    }
}
