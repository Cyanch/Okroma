using Microsoft.Xna.Framework.Input;
using Okroma.GameControls;

namespace Okroma.Screens.TextMenus
{
    public class ControlMenuScreen : TextMenuScreen
    {
        MenuEntry movementEntry;
        MenuEntry backEntry;

        int movementPresetIndex = 0;
        string[] movementPresets = new[] { "Arrows", "WASD" };
        public ControlMenuScreen() : base("Controls")
        {
        }

        protected override void Initialize()
        {
            if (GameControl.MoveLeft.Key == Keys.A)
                movementPresetIndex = 1;

            movementEntry = new MenuEntry(movementPresets[movementPresetIndex], ScreenManager.Font);
            movementEntry.OnSelected += MovementEntry_OnSelected;

            backEntry = new MenuEntry("Back", ScreenManager.Font);
            backEntry.OnSelected += BackEntry_OnSelected;

            AddEntries(movementEntry, backEntry);
        }

        private void BackEntry_OnSelected(object sender, System.EventArgs e)
        {
            OnEscape();
        }

        private void MovementEntry_OnSelected(object sender, System.EventArgs e)
        {
            movementPresetIndex = (movementPresetIndex + 1) % movementPresets.Length;

            if (movementPresetIndex == 0)
            {
                GameControl.MoveUp.ChangeKey(Keys.Up);
                GameControl.MoveLeft.ChangeKey(Keys.Left);
                GameControl.MoveRight.ChangeKey(Keys.Right);
            }
            else if (movementPresetIndex == 1)
            {
                GameControl.MoveUp.ChangeKey(Keys.W);
                GameControl.MoveLeft.ChangeKey(Keys.A);
                GameControl.MoveRight.ChangeKey(Keys.D);
            }
            movementEntry.SetText(movementPresets[movementPresetIndex]);
        }
    }
}
