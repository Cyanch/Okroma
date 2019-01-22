using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Okroma.Input
{
    public interface IInputManagerKeyboard
    {
        bool IsHeld(Keys key);
        bool WasPressed(Keys key);
        bool WasReleased(Keys key);
        Keys[] GetKeysPressed();
    }

    public interface IInputManagerMouse
    {
        bool IsHeld(MouseButton key);
        bool WasPressed(MouseButton key);
        bool WasReleased(MouseButton key);
        Point GetMousePosition();
        bool HasMouseMoved();
        int GetDeltaScroll();
    }

    public interface IInputManagerService : IInputManagerKeyboard, IInputManagerMouse
    {
        
    }

    public class InputManager : GameComponent, IInputManagerService
    {
        public KeyboardState CurrentKeyboardState { get; set; }
        public KeyboardState PreviousKeyboardState { get; set; }
        public MouseState CurrentMouseState { get; set; }
        public MouseState PreviousMouseState { get; set; }

        public InputManager(Game game) : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }

        public bool IsHeld(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }

        public bool IsHeld(MouseButton button)
        {
            return CurrentMouseState.IsPressed(button);
        }

        public bool WasPressed(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key);
        }

        public bool WasPressed(MouseButton button)
        {
            return CurrentMouseState.IsPressed(button) && !PreviousMouseState.IsPressed(button);
        }

        public bool WasReleased(Keys key)
        {
            return CurrentKeyboardState.IsKeyUp(key) && PreviousKeyboardState.IsKeyDown(key);
        }

        public bool WasReleased(MouseButton button)
        {
            return !CurrentMouseState.IsPressed(button) && PreviousMouseState.IsPressed(button);
        }

        public Keys[] GetKeysPressed()
        {
            return CurrentKeyboardState.GetPressedKeys();
        }

        public Point GetMousePosition()
        {
            return CurrentMouseState.Position;
        }

        public bool HasMouseMoved()
        {
            return CurrentMouseState.Position != PreviousMouseState.Position;
        }

        public int GetDeltaScroll()
        {
            return CurrentMouseState.ScrollWheelValue + PreviousMouseState.ScrollWheelValue;
        }
    }
}
