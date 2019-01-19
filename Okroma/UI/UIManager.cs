using Microsoft.Xna.Framework;

namespace Okroma.UI
{
    public interface UIManagerService
    {

    }

    public class UIManager : DrawableGameComponent, UIManagerService
    {
        public UIManager(Game game) : base(game)
        {
        }
    }
}
