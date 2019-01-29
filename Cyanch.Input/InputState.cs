using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Cyanch.Input
{
    public interface IInputState
    {
        KeyboardState CurrentKeyboardState { get; }
        MouseState CurrentMouseState { get; }
        KeyboardState PreviousKeyboardState { get; }
        MouseState PreviousMouseState { get; }

        int GetDeltaScroll();
        Point GetMousePosition();
        bool IsDown(Keys key);
        bool IsDown(MouseButton button);
        bool IsPressed(Keys key);
        bool IsPressed(MouseButton button);
        bool IsReleased(Keys key);
        bool IsReleased(MouseButton button);
        bool IsUp(Keys key);
        bool IsUp(MouseButton button);
    }

    public interface IInputService : IInputState
    {
    }

    public class InputState : GameComponent, IInputState, IInputService
    {
        public InputState(Game game) : base(game)
        {
        }

        public KeyboardState PreviousKeyboardState { get; private set; }
        public KeyboardState CurrentKeyboardState { get; private set; }

        public MouseState PreviousMouseState { get; private set; }
        public MouseState CurrentMouseState { get; private set; }

        public override void Update(GameTime gameTime)
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
