using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Okroma.GameControls;
using Okroma.Input;
using System;
using System.Collections.Generic;
using System.IO;

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
            public static Menu Main { get; private set; } = new Menu(null,
                ("Play Game", screen => screen.AddMenu(Level)),
                ("Controls", screen => screen.AddMenu(Controls)),
                ("Exit", screen => screen.Game.Exit()));
            public static Menu Level { get; set; }
            public static Menu Controls { get; private set; } = new Menu("Controls",
                ("Movement", screen => screen.AddMenu(ControlMovement)),
                ("Back", screen => screen.MenuPop()));
            public static Menu ControlMovement { get; private set; } = new Menu("Movement Control Selection",
                ("Arrows", screen => { screen.MenuPop(); GameControl.MoveLeft.ChangeKey(Keys.Left); GameControl.MoveRight.ChangeKey(Keys.Right); GameControl.MoveUp.ChangeKey(Keys.Up); }),
                ("WASD", screen => { screen.MenuPop(); GameControl.MoveLeft.ChangeKey(Keys.A); GameControl.MoveRight.ChangeKey(Keys.D); GameControl.MoveUp.ChangeKey(Keys.W); }));

            public string Title { get; }
            public MenuOption[] Options { get; }

            public Menu(string title, params MenuOption[] options) : this()
            {
                Title = title;
                Options = options ?? throw new ArgumentNullException(nameof(options));
            }

            public int GetSelectedIndex()
            {
                if (this == ControlMovement)
                {
                    if (GameControl.MoveLeft.Key == Keys.A)
                        return 1;
                }
                return 0;
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

            public struct MenuOption
            {
                public string Name { get; }
                public Action<MenuScreen> Action { get; }

                public MenuOption(string name, Action<MenuScreen> action) : this()
                {
                    Name = name ?? throw new ArgumentNullException(nameof(name));
                    Action = action ?? throw new ArgumentNullException(nameof(action));
                }

                public static implicit operator MenuOption(ValueTuple<string, Action<MenuScreen>> valueTuple)
                {
                    return new MenuOption(valueTuple.Item1, valueTuple.Item2);
                }
            }
        }

        Dictionary<string, Vector2> textMeasures = new Dictionary<string, Vector2>();

        ContentManager content;
        public override void LoadContent(ContentManager content)
        {
            this.content = content;
            font = content.Load<SpriteFont>(Path.Combine("Fonts", "Cyfont-I"));
            fontHeight = font.MeasureString("|").Y;

            var levelFileNames = Directory.GetFiles(Path.Combine(content.RootDirectory, "Levels"));
            Menu.MenuOption[] levelOptions = new Menu.MenuOption[levelFileNames.Length];

            for (int i = 0; i < levelFileNames.Length; i++)
            {
                var levelName = Path.GetFileNameWithoutExtension(levelFileNames[i]);
                levelOptions[i] = new Menu.MenuOption(levelName, screen => { screen.Exit(); screen.Game.Services.GetService<IScreenManagerService>().AddScreen(new LevelScreen(Path.Combine("Levels", levelName)), content, true); });
            }

            Menu.Level = new Menu(
                "Level Select",
                levelOptions
                );

            AddMenu(Menu.Main);
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
                currentMenu.Options[selectedIndex].Action(this);
            }
            else if (input.WasPressed(Keys.Escape))
            {
                if (menu.Count > 1)
                {
                    MenuPop();
                }
            }
        }

        void AddMenu(Menu menu)
        {
            this.menu.Push(menu);
            selectedIndex = menu.GetSelectedIndex();
        }

        void MenuPop()
        {
            menu.Pop();
            selectedIndex = 0;
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
                string text = menu.Options[i].Name;
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
