using Microsoft.Xna.Framework;
using Okroma.Input;

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
}