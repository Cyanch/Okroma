namespace Okroma.Screens.TextMenus
{
    public class OptionsMenuScreen : TextMenuScreen
    {
        MenuEntry controlsEntry;
        MenuEntry exitLevel;

        bool inLevel = false;
        public OptionsMenuScreen(bool inLevel) : base("Options")
        {
            this.inLevel = inLevel;
        }

        protected override void Initialize()
        {
            controlsEntry = new MenuEntry("Controls", ScreenManager.Font);
            controlsEntry.OnSelected += ControlsEntry_OnSelected;
            AddEntries(controlsEntry);

            if (inLevel)
            {
                exitLevel = new MenuEntry("Exit Level", ScreenManager.Font);
                exitLevel.OnSelected += ExitLevel_OnSelected;
                AddEntries(exitLevel);
            }
        }

        private void ExitLevel_OnSelected(object sender, System.EventArgs e)
        {
            LoadingScreen.Load(ScreenManager, new BackgroundScreen("Background"), new MainMenuScreen());
        }

        private void ControlsEntry_OnSelected(object sender, System.EventArgs e)
        {
            ScreenManager.AddScreen(new ControlMenuScreen());
        }
    }
}
