using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Cyanch.UI
{
    /// <summary>
    /// An interface for UI Elements that contain other elements.
    /// </summary>
    public interface IPanel : IUIElement
    {
        bool ClipToBounds { get; set; }
        T AddElement<T>() where T : UIElement, new();
        void RemoveElement(UIElement element);
    }
    
    /// <summary>
    /// UIElement that implements IPanel.
    /// </summary>
    public class Panel : UIElement, IPanel
    {
        public bool ClipToBounds { get; set; }

        List<UIElement> _elements = new List<UIElement>();

        private static readonly RasterizerState scissorRasterizer = new RasterizerState() { ScissorTestEnable = true };

        public override void Draw(GameTime gameTime)
        {
            if (!(Parent is IPanel))
            {
                SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, scissorRasterizer);
            }

            if (ClipToBounds)
            {
                GraphicsDevice.ScissorRectangle = Parent is IPanel elementContainer && elementContainer.ClipToBounds
                    ? Rectangle.Intersect(elementContainer.GetBounds(), GetBounds())
                    : GetBounds();
            }

            base.Draw(gameTime);

            //Restore previous ScissorRectangle.
            if (Parent is IPanel parentContainer && parentContainer.ClipToBounds && ClipToBounds)
                GraphicsDevice.ScissorRectangle = Parent.GetBounds();

            if (!(Parent is IPanel))
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
