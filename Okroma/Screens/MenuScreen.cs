using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace Okroma.Screens
{
    public class MenuScreen : GameScreen
    {
        SpriteFont font;
        string[] options = new string[]
        {
            "Play Game",
            "Exit"
        };
        int selectedIndex = 0;

        Dictionary<string, Vector2> textMeasures = new Dictionary<string, Vector2>();

        ContentManager content;
        public override void LoadContent(ContentManager content)
        {
            this.content = content;
            font = content.Load<SpriteFont>(Path.Combine("Fonts", "Cyfont-I"));
            foreach (var str in options)
            {
                textMeasures.Add(str, font.MeasureString(str));
            }
        }

        KeyboardState kb;
        KeyboardState oldKb;
        public override void Update(GameTime gameTime, IGameScreenInfo info)
        {
            oldKb = kb;
            kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Down) && oldKb.IsKeyUp(Keys.Down))
            {
                selectedIndex = (selectedIndex + 1) % options.Length;
            }
            else if (kb.IsKeyDown(Keys.Up) && oldKb.IsKeyDown(Keys.Up))
            {
                selectedIndex = (selectedIndex - 1) % options.Length;
            }

            if (kb.IsKeyDown(Keys.Enter))
            {
                Exit();
                if (selectedIndex == 0) // Play Game
                {
                    Game.Services.GetService<IScreenManagerService>().AddScreen(new LevelScreen(Path.Combine("Levels", "Test")), content, true);
                }
                else if (selectedIndex == 1)
                {
                    Game.Exit();
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            for (int i = 0; i < options.Length; i++)
            {
                string text = options[i];
                Vector2 position = new Vector2(
                    (Game.GraphicsDevice.Viewport.TitleSafeArea.Width / 2) - (textMeasures[text].X / 2),
                    (Game.GraphicsDevice.Viewport.TitleSafeArea.Height / 2) - (textMeasures[text].Y * options.Length / 2) + (textMeasures[text].Y * i)
                    );
                spriteBatch.DrawString(font, text, position, selectedIndex == i ? Color.White : Color.Gray);
            }
            spriteBatch.End();
        }
    }
}
