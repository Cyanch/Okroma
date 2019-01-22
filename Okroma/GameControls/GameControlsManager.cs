using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Okroma.Input;

namespace Okroma.GameControls
{
    public interface IHandleGameControlInput
    {
        void HandleInput(IGameControlsService controls);
    }

    public interface IGameControlsService
    {
        bool Get(Control control);
    }

    public class GameControlsManager : GameComponent, IGameControlsService
    {
        public GameControlsManager(Game game) : base(game)
        {
        }

        public bool Get(Control control)
        {
            var input = Game.Services.GetService<IInputManagerService>();
            switch (control)
            {
                case Control.MoveLeft:
                    return IsAnyHeld(input, Keys.Left, Keys.A);
                case Control.MoveRight:
                    return IsAnyHeld(input, Keys.Right, Keys.D);
                case Control.MoveUp:
                    return IsAnyHeld(input, Keys.Up, Keys.W);
                case Control.Jump:
                    return input.IsHeld(Keys.Space);
                case Control.JumpOnce:
                    return input.WasPressed(Keys.Space);

                default:
                    return false;
            }
        }

        private bool IsAnyHeld(IInputManagerService input, params Keys[] keys)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (input.IsHeld(keys[i]))
                    return true;
            }
            return false;
        }
    }

    public enum Control
    {
        MoveLeft,
        MoveRight,
        MoveUp,
        /// <summary>
        /// When Jump key is held.
        /// </summary>
        Jump,
        /// <summary>
        /// When Jump key is pressed.
        /// </summary>
        JumpOnce
    }
}