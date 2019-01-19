namespace Okroma.Common
{
    /// <summary>
    /// Used to more easily cache data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Invalidatable<T>
    {
        public bool IsValid { get; private set; }
        public T Value { get; private set; }

        public Invalidatable(T value) : this()
        {
            this.IsValid = true;
            this.Value = value;
        }

        public void Invalidate()
        {
            IsValid = false;
        }

        public static implicit operator T(Invalidatable<T> value)
        {
            return value.Value;
        }

        public static implicit operator Invalidatable<T>(T value)
        {
            return new Invalidatable<T>(value);
        }
    }
}
