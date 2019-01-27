using Cyanch.Input;
using Cyanch.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.IO;
using System.Linq;

namespace Okroma.Screens.Menus
{
    public class MenuEntry : Text
    {
        public Vector2 ScaleHover { get; set; } = Vector2.One;
        public TimeSpan TransitionTime { get; set; }
        public SoundEffect HoverSoundEffect { get; set; }
        private Vector2 _targetScale = Vector2.One;
        TimeSpan elapsed;
        public override void Update(GameTime gameTime)
        {
            if (elapsed.TotalSeconds < TransitionTime.TotalSeconds)
            {
                elapsed += gameTime.ElapsedGameTime;
                Scale = Vector2.Lerp(Scale, _targetScale, MathHelper.Clamp((float)elapsed.TotalSeconds / (float)TransitionTime.TotalSeconds, 0, 1));
            }
            base.Update(gameTime);
        }

        protected override void OnMouseEnter(object sender, MouseStateEventArgs e)
        {
            _targetScale = ScaleHover;

            HoverSoundEffect?.Play(0.5f, 0, 0);

            elapsed = elapsed.TotalMilliseconds > 0 ? TransitionTime.Subtract(elapsed) : TransitionTime;

            base.OnMouseEnter(sender, e);
        }

        protected override void OnMouseExit(object sender, MouseStateEventArgs e)
        {
            _targetScale = Vector2.One;

            elapsed = elapsed.TotalMilliseconds > 0 ? TransitionTime.Subtract(elapsed) : TransitionTime;

            base.OnMouseExit(sender, e);
        }
    }

    public class MenuScreen : GameScreen
    {
        private StackPanel _panel;
        private MenuEntry _backText;

        const float scalingWhenHoveredOver = 1.2f;
        const float scaleTransitionTime = 0.7f;
        const int menuEntryHeight = 100;
        readonly string hoverSfxPath = Path.Combine("SoundEffects", "MenuSelect");
        SoundEffect hoverSfx;
        readonly Color titleColor = Color.LightGreen;

        public bool ShowBackButton { get; set; } = false;

        protected ContentManager Content { get; private set; }
        public override void LoadContent()
        {
            Content = CreateContentManager();

            hoverSfx = Content.Load<SoundEffect>(hoverSfxPath);

            _panel = new StackPanel()
            {
                Input = Game.Services.GetService<IInputService>(),
                SpriteBatch = ScreenManager.SpriteBatch,
                GraphicsDevice = ScreenManager.GraphicsDevice,
                Font = ScreenManager.Font,
                ClipToBounds = true,
                Orientation = Orientation.Vertical
            };

            _backText = new MenuEntry()
            {
                Input = Game.Services.GetService<IInputService>(),
                SpriteBatch = ScreenManager.SpriteBatch,
                GraphicsDevice = ScreenManager.GraphicsDevice,
                Font = ScreenManager.Font,
                ScaleHover = new Vector2(1.2f),
                TransitionTime = TimeSpan.FromSeconds(0.7f),
                HoverSoundEffect = hoverSfx
            };
            _backText.SetText(" < ", true);
            _backText.MouseDown += _backText_MouseDown;
        }

        private void _backText_MouseDown(object sender, MouseStateEventArgs e)
        {
            ScreenManager.RemoveScreen(this);
        }

        public override void UnloadContent()
        {
            Content.Unload();
        }

        public override void HandleInput()
        {
            if (ShowBackButton)
            {
                _backText.HandleInput();
            }
            _panel.HandleInput();
        }

        protected override void Update(GameTime gameTime, IGameScreenInfo info)
        {
            if (!info.IsCovered)
            {
                if (ShowBackButton)
                {
                    _backText.Update(gameTime);
                    _backText.UpdateLayout(gameTime);
                }

                _panel.Update(gameTime);
                _panel.UpdateLayout(gameTime);

                var viewport = ScreenManager.GraphicsDevice.Viewport;
                _panel.Position = new Vector2(viewport.Bounds.Center.X - (_panel.Width / 2), viewport.Bounds.Center.Y - (_panel.Height / 2));
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            if (!ScreenInfo.IsCovered)
            {
                if (ShowBackButton)
                {
                    var spriteBatch = ScreenManager.SpriteBatch;
                    spriteBatch.Begin();
                    _backText.Draw(gameTime);
                    spriteBatch.End();
                }

                _panel.Draw(gameTime);
            }
        }

        public T NewElement<T>() where T : UIElement, new()
        {
            return _panel.AddElement<T>();
        }

        public Text AddTitle(string text)
        {
            var title = NewElement<Text>();
            title.TextColor = titleColor;
            title.SetText(text, true);
            return title;
        }

        public MenuEntry NewEntry(string text)
        {
            var menuEntry = NewElement<MenuEntry>();

            menuEntry.Text = text;
            menuEntry.Width = menuEntry.TextMeasure.X;
            menuEntry.Height = menuEntryHeight;
            menuEntry.ScaleHover = new Vector2(scalingWhenHoveredOver);
            menuEntry.TransitionTime = TimeSpan.FromSeconds(scaleTransitionTime);
            menuEntry.Alignment = Alignment.MiddleCenter;
            menuEntry.HoverSoundEffect = hoverSfx;

            return menuEntry;
        }

        public void ApplyChanges()
        {
            _panel.Width = _panel.GetChildren().Max(element => element.Width * (element is MenuEntry menuText ? menuText.ScaleHover.X : 1));

            var height = 0f;
            foreach (var element in _panel.GetChildren())
            {
                element.LocalPosition = new Vector2(_panel.Width / 2 - element.Width / 2, element.LocalPosition.Y);
                height += element.Height;
            }
            _panel.Height = height;
        }
    }
}
