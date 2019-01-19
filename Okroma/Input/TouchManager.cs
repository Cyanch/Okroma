using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Linq;

namespace Okroma.Input
{
    public interface ITappable : IClickable
    {

    }

    public class TouchPadInteractInfo : InteractInfo
    {
        public TouchPadInteractInfo(int type) : base(type)
        {
        }
    }

    public class TouchPadTapInteractInfo : TouchPadInteractInfo
    {
        public Point Position { get; }

        public TouchPadTapInteractInfo(int type, Point position) : base(type)
        {
            this.Position = position;
        }
    }

    public static class TouchPadInteractInfoExtensions
    {
        public static TouchInteractType GetInteractType(this TouchPadInteractInfo info)
        {
            return (TouchInteractType)Enum.GetValues(typeof(TouchInteractType)).GetValue(info.Type);
        }
    }

    public enum TouchInteractType
    {
        Unknown = 0,
        Tap = 1
    }

    //un-tested, just exists for the unlikely possibility of a touch-support version of game.
    public class TouchManager : ScreenInteractManager
    {
        public TouchManager(Game game) : base(game)
        {
            TouchPanel.EnabledGestures = TouchPanel.EnabledGestures | GestureType.Tap;
        }

        public override void Update(GameTime gameTime)
        {
            var info = (TouchPadInteractInfo)GetInteractInfo();
            if (info.GetInteractType() == TouchInteractType.Unknown)
                return;
            if (info.GetInteractType() == TouchInteractType.Tap)
            {
                var tapInfo = (TouchPadTapInteractInfo)info;
                IClickable best = null;
                foreach (var tappable in RegisteredInterables.Select(x => (ITappable)x))
                {
                    if (tappable.TriggerArea.Contains(tapInfo.Position) && (best == null || best.Layer < tappable.Layer))
                    {
                        best = tappable;
                    }
                }

                best?.NotifyInteract(tapInfo);
            }
        }

        public override InteractInfo GetInteractInfo()
        {
            var touch = TouchPanel.GetState();
            if (TouchPanel.ReadGesture().GestureType == GestureType.Tap)
            {
                return new TouchPadTapInteractInfo((int)TouchInteractType.Tap,touch[0].Position.ToPoint());
            }
            return new TouchPadInteractInfo((int)TouchInteractType.Unknown);
        }
    }
}
