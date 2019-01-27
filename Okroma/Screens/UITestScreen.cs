using C3;
using Cyanch.Input;
using Cyanch.UI;
using Microsoft.Xna.Framework;

namespace Okroma.Screens
{
    public class UITestScreen : GameScreen
    {
        UniformGrid grid;
        public override void LoadContent()
        {
            var content = CreateContentManager();

            grid = new UniformGrid()
            {
                ClipToBounds = true,
                Input = Game.Services.GetService<IInputService>(),
                GraphicsDevice = ScreenManager.GraphicsDevice,
                Font = ScreenManager.Font,
                SpriteBatch = ScreenManager.SpriteBatch
            };

            grid.Initialize(32, 32, 4, 4);
        }

        public override void HandleInput()
        {
            grid.HandleInput();
        }

        protected override void Update(GameTime gameTime, IGameScreenInfo info)
        {
            grid.Update(gameTime);
            grid.UpdateLayout(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            var spriteBatch = ScreenManager.SpriteBatch;

            // IPanel-s must be drawn outside of a Begin()-End() as it will Begin/End by its self.
            grid.Draw(gameTime);
        }
    }
}
