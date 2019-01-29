using Microsoft.Xna.Framework;

namespace Okroma.Screens.Menus
{
    public class MainMenuScreen : MenuScreen
    {
        public override void LoadContent()
        {
            base.LoadContent();

            var playText = NewEntry("Play");
            var optionsText = NewEntry("Options");
            var exitText = NewEntry("Exit");

            playText.MouseDown += PlayText_MouseDown;
            optionsText.MouseDown += OptionsText_MouseDown;
            exitText.MouseDown += ExitText_MouseDown;

            ApplyChanges();
        }

        private void PlayText_MouseDown(object sender, Cyanch.Input.MouseStateEventArgs e)
        {
            ScreenManager.AddScreen(new LevelSelectMenuScreen());
        }

        private void OptionsText_MouseDown(object sender, Cyanch.Input.MouseStateEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(false));
        }

        private void ExitText_MouseDown(object sender, Cyanch.Input.MouseStateEventArgs e)
        {
            Game.Exit();
        }
    }
}
