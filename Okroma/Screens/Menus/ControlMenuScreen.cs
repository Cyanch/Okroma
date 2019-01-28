using Microsoft.Xna.Framework.Input;
using Okroma.GameControls;

namespace Okroma.Screens.Menus
{
    public class ControlMenuScreen : MenuScreen
    {
        MenuEntry movementEntry;

        int movementPresetIndex = 0;
        string[] movementPresets = new[] { "Arrows", "WASD" };

        protected override void Initialize()
        {
            if (GameControl.MoveLeft.Key == Keys.A)
                movementPresetIndex = 1;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            this.ShowBackButton = true;

            AddTitle("Controls");

            movementEntry = NewEntry(movementPresets[movementPresetIndex]);
            movementEntry.MouseDown += MovementEntry_MouseDown;

            ApplyChanges();
        }

        private void MovementEntry_MouseDown(object sender, Cyanch.Input.MouseStateEventArgs e)
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
            ModifyEntryText(movementEntry, movementPresets[movementPresetIndex]);
            ApplyChanges();
        }
    }
}
