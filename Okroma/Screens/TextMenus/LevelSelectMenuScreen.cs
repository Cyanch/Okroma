using System;
using System.IO;

namespace Okroma.Screens.TextMenus
{
    public class LevelSelectMenuScreen : TextMenuScreen
    {
        public LevelSelectMenuScreen() : base("Level Select")
        {
        }

        protected override void Initialize()
        {
            this.TransitionOutTime = TimeSpan.FromSeconds(0.5f);
        }

        public override void LoadContent()
        {
            var content = CreateContentManager();
            var levelFileNames = Directory.GetFiles(Path.Combine(content.RootDirectory, "Levels"));
            foreach (var file in levelFileNames)
            {
                var levelEntry = new MenuEntry(Path.GetFileNameWithoutExtension(file), ScreenManager.Font);
                levelEntry.OnSelected += LevelEntry_OnSelected;
                AddEntries(levelEntry);
            }
        }

        private void LevelEntry_OnSelected(object sender, System.EventArgs e)
        {
            if (sender is MenuEntry entry)
            {
                LoadingScreen.Load(ScreenManager, new LevelScreen(Path.Combine("Levels", entry.Text)));
            }
        }
    }
}
