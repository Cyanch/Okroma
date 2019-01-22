using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Okroma.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Okroma.Screens
{
    public class MenuScreen : GameScreen
    {
        SpriteFont font;
        Stack<Menu> menu = new Stack<Menu>();
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

            public string Title { get; }
            public string[] Options { get; }

            public Menu(string[] options) : this(null, options)
            {
            }

            public Menu(string title, params string[] options) : this()
            {
                Title = title;
                Options = options ?? throw new ArgumentNullException(nameof(options));
            }

            public static bool operator ==(Menu menu1, Menu menu2)
            {
                return menu1.Equals(menu2);
            }

            public static bool operator !=(Menu menu1, Menu menu2)
            {
                return !(menu1 == menu2);
            }

            public override bool Equals(object obj)
            {
                if (obj is Menu other)
                {
                    return Title == other.Title && Options == other.Options;
                }
                return false;
            }

            public override int GetHashCode()
            {
                return (Title, Options).GetHashCode();
            }
        }

        Dictionary<string, Vector2> textMeasures = new Dictionary<string, Vector2>();

        ContentManager content;
        public override void LoadContent(ContentManager content)
        {
            this.content = content;
            font = content.Load<SpriteFont>(Path.Combine("Fonts", "Cyfont-I"));
            fontHeight = font.MeasureString("|").Y;

            Menu.Level = new Menu("Level Select", Directory.GetFiles(Path.Combine(content.RootDirectory, "Levels")).Select(s => Path.GetFileNameWithoutExtension(s)).ToArray());

            menu.Push(Menu.Main);
        }

        private Vector2 MeasureString(string str)
        {
            if (!textMeasures.TryGetValue(str, out var size))
            {
                textMeasures.Add(str, font.MeasureString(str));
            }
            return size;
        }

        public override void Update(GameTime gameTime, IGameScreenInfo info)
        {
            var input = Game.Services.GetService<IInputManagerService>();
            var currentMenu = menu.Peek();

            if (input.WasPressed(Keys.Down))
            {
                selectedIndex = (selectedIndex + 1) % currentMenu.Options.Length;
            }
            else if (input.WasPressed(Keys.Up))
            {
                selectedIndex = (selectedIndex - 1);
                if (selectedIndex == -1)
                {
                    selectedIndex = currentMenu.Options.Length - 1;
                }
            }

            if (input.WasPressed(Keys.Enter))
            {
                if (currentMenu == Menu.Main)
                {
                    if (selectedIndex == 0) // Play Game
                    {
                        menu.Push(Menu.Level);
                        selectedIndex = 0;
                    }
                    else if (selectedIndex == 1) // Exit
                    {
                        Game.Exit();
                    }
                }
                else if (currentMenu == Menu.Level)
                {
                    Exit();
                    Game.Services.GetService<IScreenManagerService>().AddScreen(new LevelScreen(Path.Combine("Levels", currentMenu.Options[selectedIndex])), content, true);
                }
            }
            else if (input.WasPressed(Keys.Back))
            {
                if (menu.Count > 1)
                {
                    menu.Pop();
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            var menu = this.menu.Peek();

            var startY = (fontHeight * menu.Options.Length / 2);
            int centerX = (Game.GraphicsDevice.Viewport.TitleSafeArea.Width / 2);
            int centerY = (Game.GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            if (menu.Title != null)
                spriteBatch.DrawString(font, menu.Title, new Vector2(centerX - (MeasureString(menu.Title).X / 2), centerY - startY - fontHeight), Color.LightGreen);

            for (int i = 0; i < menu.Options.Length; i++)
            {
                string text = menu.Options[i];
                Vector2 position = new Vector2(
                    centerX - (MeasureString(text).X / 2),
                    centerY - startY + (fontHeight * (i + (menu.Title != null ? 1 : 0)))
                    );
                spriteBatch.DrawString(font, text, position, selectedIndex == i ? Color.White : Color.Gray);
            }
            spriteBatch.End();
        }
    }
}
