using Okroma.Cameras;
using Okroma.Physics;
using Okroma.World;

namespace Okroma.Screens
{
    public class LevelScreen : GameScreen
    {
        Player player;
        Camera camera;

        World2D world;
        ICollidableSource collidables;

        protected override void Initialize()
        {
        }
    }
}
