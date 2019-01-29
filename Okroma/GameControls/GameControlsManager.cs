using Cyanch.Input;
using Microsoft.Xna.Framework;

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
            var input = Game.Services.GetService<IInputService>();

            var controlProperty = ControlProperty.None;
            if (input.IsPressed(control))
            {
                controlProperty |= ControlProperty.JustPressed;
            }

            if (input.IsDown(control))
            {
                controlProperty |= ControlProperty.Held;
            }
            return controlProperty;
        }
    }
}