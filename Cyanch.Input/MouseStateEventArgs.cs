using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Cyanch.Input
{
    public class MouseStateEventArgs : EventArgs
    {
        public MouseState Previous { get; }
        public MouseState Current { get; }

        private IInputState input;
        public MouseStateEventArgs(IInputState input)
        {
            this.input = input;
        }

        public Point GetMousePosition()
        {
            return input.GetMousePosition();
        }
    }
}
