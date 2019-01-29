namespace Okroma.Screens.Menus
{
    public class OptionsMenuScreen : MenuScreen
    {
        bool inLevel = false;
        public OptionsMenuScreen(bool inLevel)
        {
            this.inLevel = inLevel;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            this.ShowBackButton = true;
            AddTitle("Options");

            var controlsEntry = NewEntry("Controls");
            controlsEntry.MouseDown += ControlsEntry_MouseDown;

            if (inLevel)
            {
                var exitToMainMenuEntry = NewEntry("Exit to Main Menu");
                exitToMainMenuEntry.MouseDown += ExitToMainMenuEntry_MouseDown;
            }

            ApplyChanges();
        }

        private void ControlsEntry_MouseDown(object sender, System.EventArgs e)
        {
            ScreenManager.AddScreen(new ControlMenuScreen());
        }

        private void ExitToMainMenuEntry_MouseDown(object sender, System.EventArgs e)
        {
            LoadingScreen.Load(ScreenManager, new BackgroundScreen("Background"), new MainMenuScreen());
        }
    }
}
