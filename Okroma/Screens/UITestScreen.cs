using C3;
using Cyanch.Input;
using Cyanch.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Okroma.Screens
{
    public class UITestScreen : GameScreen
    {
        Panel panel;
        Text text;

        Panel panel2;
        Image image;
        public override void LoadContent()
        {
            var content = CreateContentManager();

            panel = new Panel()
            {
                Width = 250,
                Height = 100,
                LocalPosition = new Vector2(32, 32),
                ClipToBounds = true,
                Input = Game.Services.GetService<IInputService>(),
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

            image = panel2.AddElement<Image>();
            image.SetTexture(content.Load<Texture2D>(Path.Combine("Textures", "Tiles", "GrayTile")),
                new Rectangle(0, 0, 64, 64),
                true);
        }

        int alignments = 9;
        int textAlignIndex = 0;

        public override void HandleInput()
        {
            if (Game.Services.GetService<IInputService>().IsPressed(Microsoft.Xna.Framework.Input.Keys.Q))
            {
                textAlignIndex = (textAlignIndex + 1) % alignments;
                text.Alignment = (Alignment)textAlignIndex;
            }

            panel.HandleInput();
        }

        protected override void Update(GameTime gameTime, IGameScreenInfo info)
        {
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
            Primitives2D.DrawRectangle(spriteBatch, image.GetBounds(), Color.Yellow);
            Primitives2D.DrawRectangle(spriteBatch, panel.GetBounds(), Color.Red);
            Primitives2D.DrawRectangle(spriteBatch, panel2.GetBounds(), Color.Red);
            spriteBatch.End();
        }
    }
}
