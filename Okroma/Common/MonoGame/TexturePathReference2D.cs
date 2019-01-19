using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Okroma.Common.MonoGame
{
    public class TextureReference2D
    {
        public string TexturePath { get; }
        public Texture2D Texture { get; private set; }

        TextureReference2D(string texturePath)
        {
            this.TexturePath = texturePath ?? throw new ArgumentNullException(nameof(texturePath));
        }

        TextureReference2D(Texture2D texture)
        {
            TexturePath = texture.Name;
        }

        public Texture2D Load(ContentManager content)
        {
            if (Texture == null)
                Texture = content.Load<Texture2D>(TexturePath);
            return Texture;
        }

        public static implicit operator Texture2D(TextureReference2D textureReference)
        {
            return textureReference.Texture;
        }

        public static implicit operator TextureReference2D(Texture2D texture)
        {
            return new TextureReference2D(texture);
        }

        public static implicit operator TextureReference2D(string path)
        {
            return new TextureReference2D(path);
        }
    }

    public static partial class ContentManagerExtensions
    {
        public static Texture2D Load<TTexture2D>(this ContentManager content, TextureReference2D textureReference) where TTexture2D : Texture2D
        {
            return textureReference.Load(content);
        }
    }
}
