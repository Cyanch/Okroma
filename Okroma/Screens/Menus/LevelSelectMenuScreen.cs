using System;
using System.IO;

namespace Okroma.Screens.Menus
{
    public class LevelSelectMenuScreen : MenuScreen
    {
        protected override void Initialize()
        {
            this.TransitionOutTime = TimeSpan.FromSeconds(0.5f);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            this.ShowBackButton = true;

            var levelFileNames = Directory.GetFiles(Path.Combine(Content.RootDirectory, "Levels"));

            AddTitle("Level Select");

            foreach (var file in levelFileNames)
            {
                var levelEntry = NewEntry(Path.GetFileNameWithoutExtension(file));
                levelEntry.MouseDown += LevelEntry_MouseDown;
            }

            ApplyChanges();
        }

        private void LevelEntry_MouseDown(object sender, Cyanch.Input.MouseStateEventArgs e)
        {
            if (sender is MenuEntry text)
            {
                LoadingScreen.Load(ScreenManager, new GameplayScreen(Path.Combine("Levels", text.Text)));
            }
        }
    }
}
