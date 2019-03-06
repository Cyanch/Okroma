using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System.Collections.Generic;
using System.Linq;

namespace Okroma.Content.Pipeline
{
    public interface IContentWriteable
    {
        void Write(ContentWriter writer);
    }
    
    public static class ContentWriteableExtensions
    {
        public static void Write<TContentWriteable>(this ContentWriter writer, TContentWriteable content) where TContentWriteable : IContentWriteable
        {
            content.Write(writer);
        }
        
        public static void Write<T>(this ContentWriter writer, ICollection<T> collection) where T : IContentWriteable
        {
            writer.Write(collection.Count);
            foreach (var item in collection)
            {
                writer.Write(item);
            }
        }

        public static void Write<T>(this ContentWriter writer, IEnumerable<T> collection) where T : IContentWriteable
        {
            writer.Write(collection.Count());
            foreach (var item in collection)
            {
                writer.Write(item);
            }
        }

        public static void Write<T>(this ContentWriter writer, T[] array) where T : IContentWriteable
        {
            writer.Write(array.Length);
            foreach (var item in array)
            {
                writer.Write(item);
            }
        }
    }
}
