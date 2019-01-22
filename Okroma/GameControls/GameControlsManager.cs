using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Okroma.Input;
using System;

namespace Okroma.GameControls
{
    public interface IHandleGameControlInput
    {
        void HandleInput(IGameControlsService controls);
    }

    public interface IGameControlsService
    {
        ControlProperty Get(GameControl control);
    }

    public class GameControlsManager : GameComponent, IGameControlsService
    {
        public GameControlsManager(Game game) : base(game)
        {
        }

        public ControlProperty Get(GameControl control)
        {
            var input = Game.Services.GetService<IInputManagerService>();

            var controlProperty = ControlProperty.None;
            if (input.WasPressed(control))
            {
                controlProperty |= ControlProperty.JustPressed;
            }

            if (input.IsHeld(control))
            {
                controlProperty |= ControlProperty.Held;
            }
            return controlProperty;
        }
    }

    public class GameControl
    {
        public static GameControl MoveLeft { get; } = new GameControl(Keys.Left);
        public static GameControl MoveRight { get; } = new GameControl(Keys.Right);
        public static GameControl MoveUp { get; } = new GameControl(Keys.Up);

        public static GameControl Jump { get; } = new GameControl(Keys.Space);
        
        public Keys Key { get; private set; }

        public GameControl(Keys key) 
        {
            Key = key;
        }

        public void ChangeKey(Keys key)
        {
            Key = key;
        }

        public static implicit operator Keys(GameControl control)
        {
            return control.Key;
        }
    }
    
    [Flags]
    public enum ControlProperty
    {
        None = 0,

        JustPressed = 1 << 0,
        Held = 1 << 1,

        Pressed = JustPressed | Held
    }
}