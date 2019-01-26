using Cyanch.UI;
using Microsoft.Xna.Framework;
using Okroma.Input;
using Okroma.Utils.C3;
using System;

namespace Okroma.Screens
{
    public class UITestScreen : GameScreen
    {
        Text text;
        public override void LoadContent()
        {
            base.LoadContent();
            text = new Text();
            text.SpriteBatch = ScreenManager.SpriteBatch;
            text.Input = new InputState();
            text.Font = ScreenManager.Font;
            text.Text = "Greg";
            text.Width = 200;
            text.Height = 100;
            text.Alignment = Alignment.TopLeft;
        }

        int alignments = 9;
        int alignIndex = 0;

        public override void HandleInput()
        {
            base.HandleInput();

            if (Game.Services.GetService<IInputManagerService>().WasPressed(Microsoft.Xna.Framework.Input.Keys.Q))
            {
                alignIndex = (alignIndex + 1) % alignments;
                text.Alignment = (Alignment)alignIndex;
            }

            text.HandleInput();
        }

        protected override void Update(GameTime gameTime, IGameScreenInfo info)
        {
            base.Update(gameTime, info);
            text.Update(gameTime);
            text.UpdateLayout(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            var spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            text.Draw(gameTime);
            Primitives2D.DrawRectangle(spriteBatch, text.GetBounds(), Color.Orange);
            spriteBatch.End();
        }
    }
}
