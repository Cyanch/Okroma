using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Cyanch.UI
{
    public enum MouseButton
    {
        None,
        LeftButton,
        RightButton,
        MiddleButton,
        XButton1,
        XButton2
    }

    public static class InputStateMouseExtensions
    {
        public static bool IsDown(this InputState state, MouseButton button)
        {
            return state.CurrentMouseState.IsPressed(button);
        }

        public static bool IsUp(this InputState state, MouseButton button)
        {
            return !state.CurrentMouseState.IsPressed(button);
        }

        public static bool IsPressed(this InputState state, MouseButton button)
        {
            return state.CurrentMouseState.IsPressed(button) && !state.PreviousMouseState.IsPressed(button);
        }

        public static bool IsReleased(this InputState state, MouseButton button)
        {
            return !state.CurrentMouseState.IsPressed(button) && state.PreviousMouseState.IsPressed(button);
        }

        public static Point GetMousePosition(this InputState state)
        {
            return state.CurrentMouseState.Position;
        }

        public static int GetDeltaScroll(this InputState state)
        {
            return state.CurrentMouseState.ScrollWheelValue - state.PreviousMouseState.ScrollWheelValue;
        }

        private static bool IsPressed(this MouseState state, MouseButton button)
        {
            switch (button)
            {
                case MouseButton.LeftButton:
                    return state.LeftButton == ButtonState.Pressed;
                case MouseButton.MiddleButton:
                    return state.MiddleButton == ButtonState.Pressed;
                case MouseButton.RightButton:
                    return state.RightButton == ButtonState.Pressed;
                case MouseButton.XButton1:
                    return state.XButton1 == ButtonState.Pressed;
                case MouseButton.XButton2:
                    return state.XButton2 == ButtonState.Pressed;

                default:
                    return false;
            }
        }
    }
}
