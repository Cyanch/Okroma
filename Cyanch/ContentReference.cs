using Microsoft.Xna.Framework.Content;
using System;

namespace Cyanch
{
    /// <summary>
    /// Allows <typeparamref name="TContent"/> to be passed as either a path to be loaded or as a preloaded <typeparamref name="TContent"/>
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    public class ContentReference<TContent>
    {
        public string ContentPath { get; }
        public TContent Content { get; private set; }

        ContentReference(string contentPath)
        {
            this.ContentPath = contentPath ?? throw new ArgumentNullException(nameof(contentPath));
        }

        ContentReference(TContent content)
        {
            this.Content = content;
        }

        public TContent Load(ContentManager content)
        {
            if (Content == null)
                Content = content.Load<TContent>(ContentPath);
            return Content;
        }

        public static implicit operator TContent(ContentReference<TContent> contentReference)
        {
            return contentReference.Content;
        }

        public static implicit operator ContentReference<TContent>(TContent content)
        {
            return new ContentReference<TContent>(content);
        }

        public static implicit operator ContentReference<TContent>(string path)
        {
            return new ContentReference<TContent>(path);
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
