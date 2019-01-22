using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Okroma.Screens
{
    public interface IScreenManagerService
    {
        GameScreen FocusedScreen { get; }
        void AddScreen(GameScreen screen, ContentManager content);
        void AddScreen(GameScreen screen, ContentManager content, bool waitUntilNextUpdate);
    }

    public class ScreenManager : DrawableGameComponent, IScreenManagerService
    {
        private List<ValueTuple<GameScreen, ContentManager>> screensToAdd = new List<ValueTuple<GameScreen, ContentManager>>();
        private Stack<GameScreen> screenStack = new Stack<GameScreen>();
        private SpriteBatch spriteBatch;

        public ScreenManager(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        public GameScreen FocusedScreen => screenStack.Peek();

        public void AddScreen(GameScreen screen, ContentManager content)
        {
            screen.Initialize(Game);
            screenStack.Push(screen);
            screen.LoadContent(content);
        }

        public void AddScreen(GameScreen screen, ContentManager content, bool waitUntilNextUpdate)
        {
            if (waitUntilNextUpdate)
            {
                screensToAdd.Add((screen, content));
            }
            else
            {
                AddScreen(screen, content);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (screenStack.Count != 0)
            {
                GameScreen focusedScreen = FocusedScreen;
                if (FocusedScreen.Exited)
                {
                    var exitedScreen = screenStack.Pop();
                    exitedScreen.NotifyRemovedFromScreenManager();
                }
            }

            if (screensToAdd.Count > 0)
            {
                foreach (var screen in screensToAdd)
                {
                    AddScreen(screen.Item1, screen.Item2);
                }
                screensToAdd.Clear();
            }

            foreach (var screen in screenStack.ToArray())
            {
                screen?.Update(gameTime, GetGameScreenInfo(screen));
            }
        }

        protected virtual IGameScreenInfo GetGameScreenInfo(GameScreen screen)
        {
            return new GameScreenInfo(screen == FocusedScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var screen in screenStack.ToArray())
            {
                screen.Draw(gameTime, spriteBatch);
            }
        }
    }
}
