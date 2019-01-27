using Cyanch.Common;
using Cyanch.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Okroma.Screens.TextMenus
{
    public class MenuEntry
    {
        public string Text { get; private set; }
        public SpriteFont Font { get; set; }

        public MenuEntry(string text, SpriteFont font)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Font = font ?? throw new ArgumentNullException(nameof(font));
        }

        //Cache
        private Invalidatable<Vector2> sizeCache;
        public Vector2 Size
        {
            get
            {
                if (sizeCache == Vector2.Zero || !sizeCache.IsValid)
                {
                    sizeCache = Font.MeasureString(Text);
                }
                return sizeCache;
            }
        }

        public event EventHandler OnSelected;
        internal void NotifySelect()
        {
            OnSelected?.Invoke(this, EventArgs.Empty);
        }

        public void SetText(string text)
        {
            this.Text = text;
            sizeCache.Invalidate();
        }
    }

    public abstract class TextMenuScreen : GameScreen
    {
        private Vector2 titleSize;
        public string Title { get; set; }

        protected Color TitleColor = Color.LightGreen;
        protected Color EntryColor = Color.Gray;
        protected Color SelectedColor = Color.LightBlue;

        List<MenuEntry> menuEntries = new List<MenuEntry>();

        int selectedIndex = 0;

        public TextMenuScreen(string title)
        {
            Title = title;
        }

        public override void HandleInput()
        {
            var input = Game.Services.GetService<IInputService>();
            if (input.IsPressed(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuEntries.Count - 1;
            }
            if (input.IsPressed(Keys.Down))
            {
                selectedIndex = (selectedIndex + 1) % menuEntries.Count;
            }

            if (input.IsPressed(Keys.Enter))
            {
                OnSelected(selectedIndex);
            }
            else if (input.IsPressed(Keys.Escape))
            {
                OnEscape();
            }
        }

        IGameScreenInfo info;
        protected override void Update(GameTime gameTime, IGameScreenInfo info)
        {
            this.info = info;
        }

        protected override void Draw(GameTime gameTime)
        {
            if (!info.IsFocused)
                return;
            var spriteBatch = ScreenManager.SpriteBatch;
            var font = ScreenManager.Font;
            spriteBatch.Begin();
            if (Title != null)
            {
                spriteBatch.DrawString(font, Title, CalculateTitlePosition(), TitleColor);
            }

            var entryPositions = CalculateEntryPositions();
            for (int i = 0; i < menuEntries.Count; i++)
            {
                var entry = menuEntries[i];
                var pos = entryPositions[i];
                spriteBatch.DrawString(font, entry.Text, pos,
                    selectedIndex == i ? SelectedColor : EntryColor);
            }
            spriteBatch.End();
        }

        private Vector2 CalculateTitlePosition()
        {
            var screenBounds = ScreenManager.GraphicsDevice.Viewport.Bounds;
            var font = ScreenManager.Font;
            int spacing = font.LineSpacing;
            if (titleSize == Vector2.Zero)
                titleSize = font.MeasureString(Title);

            return new Vector2(
                screenBounds.Center.X - (titleSize.X / 2), 
                screenBounds.Center.Y - (menuEntries.Count * spacing / 2) - spacing);
        }

        private IReadOnlyList<Vector2> CalculateEntryPositions()
        {
            var screenBounds = ScreenManager.GraphicsDevice.Viewport.Bounds;
            var font = ScreenManager.Font;
            int spacing = font.LineSpacing;
            int beginY = screenBounds.Center.Y - (menuEntries.Count * spacing) / 2;
            if (Title != null)
            {
                beginY += spacing;
            }
            var positions = new List<Vector2>();
            for (int i = 0; i < menuEntries.Count; i++)
            {
                var entry = menuEntries[i];
                positions.Add(new Vector2(
                    screenBounds.Center.X - (entry.Size.X / 2),
                    beginY + (i * spacing)));
            }
            return positions;
        }

        public virtual void OnSelected(int index)
        {
            menuEntries[index].NotifySelect();
        }

        public virtual void OnEscape()
        {
            ExitScreen();
        }

        protected void AddEntries(params MenuEntry[] entry)
        {
            menuEntries.AddRange(entry);
        }
    }
}
