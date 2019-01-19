using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Okroma.Input
{
    public interface IScreenInteractService
    {
        void Register(IScreenInteractable clickable);
        void Unregister(IScreenInteractable clickable);
    }

    public interface IScreenInteractable
    {
        void NotifyInteract(InteractInfo position);
    }
    
    public class InteractInfo
    {
        public int Type { get; }

        public InteractInfo(int type)
        {
            Type = type;
        }
    }

    public abstract class ScreenInteractManager : GameComponent, IScreenInteractService
    {
        protected List<IScreenInteractable> RegisteredInterables = new List<IScreenInteractable>();

        public ScreenInteractManager(Game game) : base(game)
        {
        }

        public void Register(IScreenInteractable interactable)
        {
            RegisteredInterables.Add(interactable);
        }

        public void Unregister(IScreenInteractable clickable)
        {
            RegisteredInterables.Remove(clickable);
        }

        public abstract InteractInfo GetInteractInfo();
    }
}
