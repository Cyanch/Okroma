using Microsoft.Xna.Framework.Input;

namespace Cyanch.Input
{
    public static class MouseStateExtensions
    {
        public static bool IsPressed(this MouseState state, MouseButton button)
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
