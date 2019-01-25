using Microsoft.Xna.Framework.Input;

namespace Cyanch.UI
{
    public class InputState
    {
        public KeyboardState PreviousKeyboardState { get; set; }
        public KeyboardState CurrentKeyboardState { get; set; }

        public MouseState PreviousMouseState { get; set; }
        public MouseState CurrentMouseState { get; set; }

        public void Update()
        {
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }
    }
}
