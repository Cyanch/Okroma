using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Cyanch.UI
{
    public interface IElementContainer : IUIElement
    {
        bool ClipToBounds { get; set; }
        T AddElement<T>() where T : UIElement, new();
        void RemoveElement(UIElement element);
    }

    public class Panel : UIElement, IElementContainer
    {
        public bool ClipToBounds { get; set; }

        List<UIElement> _elements = new List<UIElement>();

        private static readonly RasterizerState scissorRasterizer = new RasterizerState() { ScissorTestEnable = true };

        public override void Draw(GameTime gameTime)
        {
            if (!(Parent is IElementContainer))
            {
                SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, scissorRasterizer);
            }

            if (ClipToBounds)
            {
                GraphicsDevice.ScissorRectangle = Parent is IElementContainer elementContainer && elementContainer.ClipToBounds
                    ? Rectangle.Intersect(elementContainer.GetBounds(), GetBounds())
                    : GetBounds();
            }

            base.Draw(gameTime);

            //Restore previous ScissorRectangle.
            if (Parent is IElementContainer parentContainer && parentContainer.ClipToBounds && ClipToBounds)
                GraphicsDevice.ScissorRectangle = Parent.GetBounds();

            if (!(Parent is IElementContainer))
            {
                SpriteBatch.End();
            }
        }

        public virtual T AddElement<T>() where T : UIElement, new()
        {
            var element = new T();
            _elements.Add(element);
            AddChild(element);
            return element;
        }

        public virtual void RemoveElement(UIElement element)
        {
            _elements.Remove(element);
            RemoveChild(element);
        }
    }
}
