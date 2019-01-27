using Cyanch.Input;
using System;

namespace Okroma.Screens.TextMenus
{
    public class MainMenuScreen : TextMenuScreen
    {
        MenuEntry playEntry, optionsEntry, exitEntry;

        public MainMenuScreen() : base(null)
        {
            this.TransitionInTime = TimeSpan.FromSeconds(0.5f);
        }

        protected override void Initialize()
        {
            var font = ScreenManager.Font;
            playEntry = new MenuEntry("Play", font);
            playEntry.OnSelected += PlayEntry_OnSelected;
            optionsEntry = new MenuEntry("Options", font);
            optionsEntry.OnSelected += OptionsEntry_OnSelected;
            exitEntry = new MenuEntry("Exit", font);
            exitEntry.OnSelected += ExitEntry_OnSelected;

            AddEntries(playEntry, optionsEntry, exitEntry);
        }

        private void OptionsEntry_OnSelected(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(false));
        }

        public override void HandleInput()
        {
            base.HandleInput();
            if (Game.Services.GetService<IInputService>().IsDown(Microsoft.Xna.Framework.Input.Keys.U))
            {
                ScreenManager.AddScreen(new UITestScreen());
            }
        }

        public override void OnEscape()
        {
            //overriden so the the screen isn't closed when the escape button is pressed.
        }

        private void ExitEntry_OnSelected(object sender, EventArgs e)
        {
            Game.Exit();
        }

        private void PlayEntry_OnSelected(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new LevelSelectMenuScreen());
        }
    }
}
