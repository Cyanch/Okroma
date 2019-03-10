using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Okroma.Input
{
    class InputManager
    {
        public KeyboardState PreviousKeyboardState { get; private set; }
        public KeyboardState CurrentKeyboardState { get; private set; }
        
        public MouseState PreviousMouseState { get; private set; }
        public MouseState CurrentMouseState { get; private set; }

        public void Update()
        {
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
            
            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }
       
        //Keyboard
        /// <summary>
        /// Is key down.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsDown(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Is key up.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsUp(Keys key)
        {
            return CurrentKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Has key been just pressed.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsPressed(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Has key been just released.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsReleased(Keys key)
        {
            return CurrentKeyboardState.IsKeyUp(key) && PreviousKeyboardState.IsKeyDown(key);
        }

        // Mouse
        public bool IsDown(MouseButton button)
        {
            return CurrentMouseState.IsPressed(button);
        }

        public bool IsUp(MouseButton button)
        {
            return !CurrentMouseState.IsPressed(button);
        }

        public bool IsPressed(MouseButton button)
        {
            return CurrentMouseState.IsPressed(button) && !PreviousMouseState.IsPressed(button);
        }

        public bool IsReleased(MouseButton button)
        {
            return !CurrentMouseState.IsPressed(button) && PreviousMouseState.IsPressed(button);
        }

        public Point GetMousePosition()
        {
            return CurrentMouseState.Position;
        }

        public int GetDeltaScroll()
        {
            return CurrentMouseState.ScrollWheelValue - PreviousMouseState.ScrollWheelValue;
        }
    }
}
