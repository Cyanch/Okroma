using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Okroma.Screens
{
    public class MenuScreen : GameScreen
    {
        SpriteFont font;
        Menu menu;
        int selectedIndex = 0;
        float fontHeight;

        struct Menu
        {
            static Menu()
            {
                Main = new Menu(
                    new string[]
                    {
                        "Play Game",
                        "Exit"
                    });
            }

            public static Menu Main { get; private set; }
            public static Menu Level { get; set; }

            public string[] Options { get; }

            public Menu(string[] options) : this()
            {
                Options = options ?? throw new ArgumentNullException(nameof(options));
            }
        }

        Dictionary<string, Vector2> textMeasures = new Dictionary<string, Vector2>();

        ContentManager content;
        public override void LoadContent(ContentManager content)
        {
            this.content = content;
            font = content.Load<SpriteFont>(Path.Combine("Fonts", "Cyfont-I"));
            Menu.Level = new Menu(Directory.GetFiles(Path.Combine(content.RootDirectory, "Levels")).Select(s => Path.GetFileNameWithoutExtension(s)).ToArray());
            menu = Menu.Main;

            foreach (var str in Menu.Main.Options)
            {
                if (!textMeasures.ContainsKey(str))
                    textMeasures.Add(str, font.MeasureString(str));
                if (fontHeight == 0)
                    fontHeight = font.MeasureString(str).Y;
            }
            foreach (var str in Menu.Level.Options)
            {
                if (!textMeasures.ContainsKey(str))
                    textMeasures.Add(str, font.MeasureString(str));
            }
            textMeasures.Add("Level Select", font.MeasureString("Level Select"));
        }

        KeyboardState kb;
        KeyboardState oldKb;
        public override void Update(GameTime gameTime, IGameScreenInfo info)
        {
            oldKb = kb;
            kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Down) && oldKb.IsKeyUp(Keys.Down))
            {
                selectedIndex = (selectedIndex + 1) % menu.Options.Length;
            }
            else if (kb.IsKeyDown(Keys.Up) && oldKb.IsKeyUp(Keys.Up))
            {
                selectedIndex = (selectedIndex - 1);
                if (selectedIndex == -1)
                {
                    selectedIndex = menu.Options.Length - 1;
                }
            }

            if (kb.IsKeyDown(Keys.Enter) && oldKb.IsKeyUp(Keys.Enter))
            {
                if (menu.Options == Menu.Main.Options)
                {
                    if (selectedIndex == 0) // Play Game
                    {
                        menu = Menu.Level;
                        selectedIndex = 0;
                    }
                    else if (selectedIndex == 1) // Exit
                    {
                        Game.Exit();
                    }
                }
                else if (menu.Options == Menu.Level.Options)
                {
                    Exit();
                    Game.Services.GetService<IScreenManagerService>().AddScreen(new LevelScreen(Path.Combine("Levels", menu.Options[selectedIndex])), content, true);
                }
            }
            else if (kb.IsKeyDown(Keys.Back) && oldKb.IsKeyUp(Keys.Back))
            {
                if (menu.Options == Menu.Level.Options)
                {
                    menu = Menu.Main;
                    selectedIndex = 0;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            var startY = (fontHeight * menu.Options.Length / 2);
            int centerX = (Game.GraphicsDevice.Viewport.TitleSafeArea.Width / 2);
            int centerY = (Game.GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            if (menu.Options == Menu.Level.Options)
                spriteBatch.DrawString(font, "Level Select", new Vector2(centerX - (textMeasures["Level Select"].X / 2), centerY - startY - fontHeight), Color.LightGreen);

            for (int i = 0; i < menu.Options.Length; i++)
            {
                string text = menu.Options[i];
                Vector2 position = new Vector2(
                    centerX - (textMeasures[text].X / 2),
                    centerY - startY + (fontHeight * (i + (menu.Options == Menu.Level.Options ? 1 : 0)))
                    );
                spriteBatch.DrawString(font, text, position, selectedIndex == i ? Color.White : Color.Gray);
            }
            spriteBatch.End();
        }
    }
}
