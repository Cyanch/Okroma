namespace Okroma.Screens
{
    public interface IGameScreenInfo
    {
        bool IsFocused { get; }
    }

    public struct GameScreenInfo : IGameScreenInfo
    {
        public bool IsFocused { get; }

        public GameScreenInfo(bool isFocused) : this()
        {
            IsFocused = isFocused;
        }
    }
}