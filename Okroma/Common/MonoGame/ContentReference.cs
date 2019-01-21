using Microsoft.Xna.Framework.Content;
using System;

namespace Okroma.Common.MonoGame
{
    public class ContentReference<T>
    {
        public string ContentPath { get; }
        public T Content { get; private set; }

        ContentReference(string texturePath)
        {
            this.ContentPath = texturePath ?? throw new ArgumentNullException(nameof(texturePath));
        }

        ContentReference(T content)
        {
            this.Content = content;
        }

        public T Load(ContentManager content)
        {
            if (Content == null)
                Content = content.Load<T>(ContentPath);
            return Content;
        }

        public static implicit operator T(ContentReference<T> contentReference)
        {
            return contentReference.Content;
        }

        public static implicit operator ContentReference<T>(T content)
        {
            return new ContentReference<T>(content);
        }

        public static implicit operator ContentReference<T>(string path)
        {
            return new ContentReference<T>(path);
        }
    }

    public static partial class ContentManagerExtensions
    {
        public static T Load<T>(this ContentManager content, ContentReference<T> contentReference)
        {
            return contentReference.Load(content);
        }
    }
}
