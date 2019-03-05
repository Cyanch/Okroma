using System;

namespace Okroma.Common
{
    static class ArrayExtensions
    {
        public static bool Exists<T>(this T[] debugOptions, T exist)
        {
            return Array.Exists(debugOptions, o => o.Equals(exist));
        }
    }
}
