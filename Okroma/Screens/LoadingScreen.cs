using Microsoft.Xna.Framework;

namespace Okroma.Screens
{
    public class LoadingScreen : GameScreen
    {
        bool originalScreensAreGone;
        GameScreen[] screensToLoad;
        //List<AsyncLoadingGameScreen> screensToAsyncLoad;
        public LoadingScreen(GameScreen[] screensToLoad)
        {
            this.screensToLoad = screensToLoad;
        }

        public static void Load(IScreenManagerService screenManager, params GameScreen[] screensToLoad)
        {
            foreach (var screen in screenManager.GetScreens())
                screen.ExitScreen();

            var loadingScreen = new LoadingScreen(screensToLoad);
            screenManager.AddScreen(loadingScreen);
        }

        protected override void Update(GameTime gameTime, IGameScreenInfo info)
        {
            if (originalScreensAreGone)
            {
                ScreenManager.RemoveScreen(this);

                foreach (var screen in screensToLoad)
                {
                    if (screen != null)
                    {
                        ScreenManager.AddScreen(screen);
                    }
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            if (ScreenManager.GetScreens().Count == 1)
            {
                originalScreensAreGone = true;
            }
            base.Draw(gameTime);
        }
    }
}
