namespace Okroma.Screens
{
    public interface IGameScreenInfo
    {
        bool IsFocused { get; }
        bool IsCovered { get; }
    }

    public struct GameScreenInfo : IGameScreenInfo
    {
        public bool IsFocused { get; }
        public bool IsCovered { get; }

        public GameScreenInfo(bool isFocused, bool isCovered) : this()
        {
            IsFocused = isFocused;
        }
    }
}