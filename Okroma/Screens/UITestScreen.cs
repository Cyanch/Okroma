using Cyanch.UI;
using Microsoft.Xna.Framework;
using Okroma.Input;
using Okroma.Utils.C3;

namespace Okroma.Screens
{
    public class UITestScreen : GameScreen
    {
        Panel panel;
        Text text;

        Panel panel2;
        Text text2;
        public override void LoadContent()
        {
            base.LoadContent();
            panel = new Panel()
            {
                Width = 250,
                Height = 100,
                LocalPosition = new Vector2(32, 32),
                ClipToBounds = true,
                GraphicsDevice = ScreenManager.GraphicsDevice,
                Font = ScreenManager.Font,
                SpriteBatch = ScreenManager.SpriteBatch
            };
            text = panel.AddElement<Text>();
            text.SetText("A Long String of Text", true);

            panel2 = panel.AddElement<Panel>();
            panel2.Width = 50;
            panel2.Height = 50;
            panel2.ClipToBounds = true;
            panel2.LocalPosition = new Vector2(40, 70);

            text2 = panel2.AddElement<Text>();
            text2.SetText("Hello", true);
        }

        int alignments = 9;
        int textAlignIndex = 0;

        public override void HandleInput()
        {
            base.HandleInput();

            if (Game.Services.GetService<IInputManagerService>().WasPressed(Microsoft.Xna.Framework.Input.Keys.Q))
            {
                textAlignIndex = (textAlignIndex + 1) % alignments;
                text.Alignment = (Alignment)textAlignIndex;
            }

            panel.HandleInput();
        }

        protected override void Update(GameTime gameTime, IGameScreenInfo info)
        {
            base.Update(gameTime, info);
            panel.Update(gameTime);
            panel.UpdateLayout(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            var spriteBatch = ScreenManager.SpriteBatch;

            // Panel must be drawn outside of a Begin()-End() as it will Begin/End by its self.
            panel.Draw(gameTime);

            spriteBatch.Begin();
            Primitives2D.DrawRectangle(spriteBatch, text.GetBounds(), Color.Orange);
            Primitives2D.DrawRectangle(spriteBatch, panel.GetBounds(), Color.Red);
            Primitives2D.DrawRectangle(spriteBatch, panel2.GetBounds(), Color.Green);
            Primitives2D.DrawRectangle(spriteBatch, text2.GetBounds(), Color.Yellow);
            spriteBatch.End();
        }
    }
}
