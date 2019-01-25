using Microsoft.Xna.Framework.Input;

namespace Cyanch.UI
{
    public static class InputStateKeyboardExtensions
    {
        /// <summary>
        /// Is key down.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsDown(this InputState input, Keys key)
        {
            return input.CurrentKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Is key up.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsUp(this InputState input, Keys key)
        {
            return input.CurrentKeyboardState.IsKeyUp(key);
        }
        
        /// <summary>
        /// Has key been just pressed.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsPressed(this InputState input, Keys key)
        {
            return input.CurrentKeyboardState.IsKeyDown(key) && input.PreviousKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Has key been just released.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsReleased(this InputState input, Keys key)
        {
            return input.CurrentKeyboardState.IsKeyUp(key) && input.PreviousKeyboardState.IsKeyDown(key);
        }
    }
}
