using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Okroma.Input
{
    public interface IClickService
    {
        void Register(IClickable clickable);
        void Unregister(IClickable clickable);
    }

    public interface IClickable
    {
        Rectangle ClickTriggerArea { get; }
        int ClickLayer { get; }

        /// <summary>
        /// This will be triggered while the mouse is hovering over this <see cref="IClickable"/>. You may check to see if the required button is being pressed.
        /// </summary>
        /// <param name="mouse">Access to mouse-related services.</param>
        void NotifyMouseAction(IInputManagerMouse mouse);
    }

    public class ClickManager : GameComponent, IClickService
    {
        private List<IClickable> registeredClickables = new List<IClickable>();

        public ClickManager(Game game) : base(game)
        {
        }

        public void Register(IClickable interactable)
        {
            registeredClickables.Add(interactable);
        }

        public void Unregister(IClickable clickable)
        {
            registeredClickables.Remove(clickable);
        }

        public override void Update(GameTime gameTime)
        {
            var input = Game.Services.GetService<IInputManagerService>();

            IClickable best = null;
            foreach (var clickable in registeredClickables)
            {
                if (clickable.ClickTriggerArea.Contains(input.GetMousePosition()) && (best == null || best.ClickLayer < clickable.ClickLayer))
                {
                    best = clickable;
                }
            }

            best?.NotifyMouseAction(input);
        }
    }
}
