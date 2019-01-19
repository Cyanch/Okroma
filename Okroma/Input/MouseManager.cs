using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace Okroma.Input
{
    public interface IClickable : IScreenInteractable
    {
        Rectangle TriggerArea { get; }
        int Layer { get; }
    }

    public class MouseClickInteractInfo : InteractInfo
    {
        public Point Position { get; }

        public MouseClickInteractInfo(int type, Point position) : base(type)
        {
            this.Position = position;
        }
    }

    public static class MouseClickInteractInfoExtensions
    {
        public static MouseInteractType GetInteractType(this MouseClickInteractInfo info)
        {
            return (MouseInteractType)Enum.GetValues(typeof(MouseInteractType)).GetValue(info.Type);
        }
    }

    public enum MouseInteractType
    {
        Unknown = 0,
        Left = 1,
        Right = 2
    }

    public class MouseManager : ScreenInteractManager
    {
        public MouseManager(Game game) : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            var info = (MouseClickInteractInfo)GetInteractInfo();
            if (info.GetInteractType() == MouseInteractType.Unknown)
                return;

            IClickable best = null;
            foreach (var clickable in RegisteredInterables.Select(x => (IClickable)x))
            {
                if (clickable.TriggerArea.Contains(info.Position) && (best == null || best.Layer < clickable.Layer))
                {
                    best = clickable;
                }
            }

            best?.NotifyInteract(info);
        }

        MouseState oldMouseState;
        public override InteractInfo GetInteractInfo()
        {
            var mouse = Mouse.GetState();
            var position = mouse.Position;
            int type = (int)MouseInteractType.Unknown;

            if (mouse.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed)
            {
                type = (int)MouseInteractType.Left;
            }
            else if (mouse.RightButton == ButtonState.Released && oldMouseState.RightButton == ButtonState.Pressed)
            {
                type = (int)MouseInteractType.Right;
            }

            oldMouseState = mouse;
            return new MouseClickInteractInfo(type, position);
        }
    }
}
