using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Okroma.Input
{
    public interface IHandleInput
    {
        void HandleInput(IGameControlsService controls);
    }

    public interface IGameControlsService
    {
        bool Get(Control control);
    }

    public class GameControlsManager : GameComponent, IGameControlsService
    {
        KeyboardState keyboard;
        KeyboardState keyboardOld;

        public GameControlsManager(Game game) : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            keyboardOld = keyboard;
            keyboard = Keyboard.GetState();
        }

        public bool Get(Control control)
        {
            switch (control)
            {
                case Control.MoveLeft:
                    return keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.A);
                case Control.MoveRight:
                    return keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.D);
                case Control.ShouldWallJumpUpward:
                    return keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.W);
                case Control.Jump:
                    return keyboard.IsKeyDown(Keys.Space);
                case Control.WallJump:
                    return keyboardOld.IsKeyUp(Keys.Space) && keyboard.IsKeyDown(Keys.Space);

                default:
                    return false;
            }
        }
    }

    public enum Control
    {
        MoveLeft,
        MoveRight,
        ShouldWallJumpUpward,
        /// <summary>
        /// When Jump key is held.
        /// </summary>
        Jump,
        /// <summary>
        /// When Jump key is pressed.
        /// </summary>
        WallJump
    }
}
