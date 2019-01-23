using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace Okroma.Screens
{
    public abstract class GameScreen
    {
        public bool IsPopup { get; protected set; }

        protected Game Game { get; private set; }
        protected ScreenManager ScreenManager { get; private set; }

        public TimeSpan TransitionInTime { get; protected set; }
        public TimeSpan TransitionOutTime { get; protected set; }
        public float TransitionPosition { get; protected set; }
        public float TransitionAlpha => 1f - TransitionPosition;
        public bool IsTransitioning { get; protected set; }
        public bool IsExiting { get; private set; }

        public float DarkenEffectWhenCovered { get; protected set; }
        private IGameScreenInfo info;

        public void Initialize(ScreenManager screenManager)
        {
            this.Game = screenManager.Game;
            this.ScreenManager = screenManager;

            this.TransitionPosition = 1f;
            this.IsTransitioning = true;
            this.IsExiting = false;

            Initialize();
        }

        protected virtual void Initialize()
        {
        }

        public virtual void LoadContent()
        {

        }

        public virtual void UnloadContent()
        {

        }

        public virtual void HandleInput()
        {

        }

        public void UpdateScreen(GameTime gameTime, IGameScreenInfo info)
        {
            this.info = info;
            if (IsExiting)
            {
                if (!UpdateTransition(gameTime, TransitionOutTime, 1))
                {
                    IsTransitioning = false;
                    ScreenManager.RemoveScreen(this);
                }
            }
            else if (IsTransitioning)
            {
                if (!UpdateTransition(gameTime, TransitionInTime, -1))
                {
                    IsTransitioning = false;
                }
            }
            Update(gameTime, info);
        }

        protected virtual void Update(GameTime gameTime, IGameScreenInfo info)
        {
        }

        bool UpdateTransition(GameTime gameTime, TimeSpan time, int direction)
        {
            float transitionDelta = time == TimeSpan.Zero ? 1 : (float)(gameTime.ElapsedGameTime.TotalMilliseconds / time.TotalMilliseconds);

            TransitionPosition += transitionDelta * direction;

            if ((direction == -1 && TransitionPosition <= 0) || (direction == 1 && TransitionPosition >= 1))
            {
                TransitionPosition = MathHelper.Clamp(TransitionPosition, 0, 1);
                return false;
            }

            return true;
        }

        public void DrawScreen(GameTime gameTime)
        {
            Draw(gameTime);
            if (!info.IsFocused && DarkenEffectWhenCovered != 0)
                ScreenManager.FadeBackBufferToBlack(DarkenEffectWhenCovered);
            ScreenManager.FadeBackBufferToBlack(TransitionPosition);
        }

        protected virtual void Draw(GameTime gameTime)
        {
        }

        /// <summary>
        /// Creates a content manager based off the one main provided one by the Game.
        /// </summary>
        /// <returns></returns>
        protected ContentManager CreateContentManager()
        {
            return new ContentManager(Game.Services, Game.Content.RootDirectory);
        }

        public void ExitScreen()
        {
            IsExiting = true;
            if (TransitionOutTime == TimeSpan.Zero)
            {
                ScreenManager.RemoveScreen(this);
            }
            else
            {
                IsTransitioning = true;
            }
        }
    }
}
