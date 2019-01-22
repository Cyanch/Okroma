using Microsoft.Xna.Framework.Input;

namespace Okroma.GameControls
{
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
}
