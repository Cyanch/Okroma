using Cyanch.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Cyanch.UI
{
    public interface IUIElement
    {
        SpriteFont Font { get; set; }
        GraphicsDevice GraphicsDevice { get; set; }
        SpriteBatch SpriteBatch { get; set; }
        int RenderOrder { get; set; }
        Vector2 LocalPosition { get; set; }
        Vector2 Position { get; set; }
        IInputState Input { get; set; }
        float Width { get; set; }
        float Height { get; set; }
        Alignment Alignment { get; set; }
        UIElement Parent { get; }
        bool HasParent { get; }

        event EventHandler<MouseStateEventArgs> MouseEnter;
        event EventHandler<MouseStateEventArgs> MouseExit;
        event EventHandler<MouseStateEventArgs> MouseDown;
        event EventHandler<MouseStateEventArgs> MouseUp;
        event EventHandler AlignmentChanged;
        event EventHandler FontChanged;
        event EventHandler RenderOrderChanged;
        event EventHandler SizeChanged;

        void AddChild(UIElement child);
        void Draw(GameTime gameTime);
        Rectangle GetBounds();
        IReadOnlyCollection<UIElement> GetChildren();
        void HandleInput();
        void RemoveChild(UIElement child);
        void Update(GameTime gameTime);
        void UpdateLayout(GameTime gameTime);
    }

    public abstract class UIElement : IUIElement
    {
        public SpriteFont Font
        {
            get
            {
                return _font ?? Parent.Font;
            }
            set
            {
                if (_font != value)
                {
                    _font = value;
                    OnFontChanged(this, EventArgs.Empty);
                }
            }
        }
        public GraphicsDevice GraphicsDevice
        {
            get
            {
                return _graphicsDevice ?? Parent.GraphicsDevice;
            }
            set
            {
                _graphicsDevice = value;
            }
        }
        public SpriteBatch SpriteBatch
        {
            get
            {
                return _spriteBatch ?? Parent.SpriteBatch;
            }
            set
            {
                _spriteBatch = value;
            }
        }
        public int RenderOrder
        {
            get
            {
                return _renderOrder;
            }
            set
            {
                _renderOrder = value;
                OnRenderOrderChanged(this, EventArgs.Empty);
            }
        }

        public Vector2 LocalPosition { get; set; }
        public Vector2 Position
        {
            get
            {
                return LocalPosition + (Parent?.Position ?? Vector2.Zero);
            }
            set
            {
                LocalPosition = value - (Parent?.Position ?? Vector2.Zero);
            }
        }

        public IInputState Input
        {
            get
            {
                return _input ?? Parent?.Input;
            }
            set
            {
                _input = value;
            }
        }

        public float Width
        {
            get => _width;
            set
            {
                _width = value;
                OnSizeChanged(this, EventArgs.Empty);
            }
        }
        public float Height
        {
            get => _height;
            set
            {
                _height = value;
                OnSizeChanged(this, EventArgs.Empty);
            }
        }

        public Alignment Alignment
        {
            get
            {
                return _alignment;
            }
            set
            {
                _alignment = value;
                OnAlignmentChanged(this, EventArgs.Empty);
            }
        }

        public UIElement Parent { get; private set; }
        public bool HasParent => Parent != null;

        private List<UIElement> _childElements = new List<UIElement>();
        private SpriteFont _font;
        private GraphicsDevice _graphicsDevice;
        private int _renderOrder;
        private IInputState _input;
        private bool _isMouseHovering;
        private bool _isMouseDown;
        private SpriteBatch _spriteBatch;
        private Alignment _alignment;
        private float _width;
        private float _height;

        public virtual void HandleInput()
        {
            HandleMouseEvents(GetEffectiveBounds());

            foreach (var child in GetChildren())
            {
                child.HandleInput();
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var child in GetChildren())
            {
                child.Update(gameTime);
            }
        }

        public virtual void UpdateLayout(GameTime gameTime)
        {
            foreach (var child in GetChildren())
            {
                child.UpdateLayout(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            foreach (var child in GetChildren())
            {
                child.Draw(gameTime);
            }
        }

        public void AddChild(UIElement child)
        {
            child.Parent = this;
            child.RenderOrderChanged += OnChildRenderOrderChanged;
            _childElements.Add(child);
        }

        public void RemoveChild(UIElement child)
        {
            child.Parent = null;
            child.RenderOrderChanged -= OnChildRenderOrderChanged;
            _childElements.Remove(child);
        }

        private void OnChildRenderOrderChanged(object sender, EventArgs e)
        {
            SortForDrawing();
        }

        private void SortForDrawing()
        {
            _childElements.Sort((x, y) => x.RenderOrder.CompareTo(y.RenderOrder));
        }

        public IReadOnlyCollection<UIElement> GetChildren()
        {
            return _childElements.AsReadOnly();
        }

        public virtual Rectangle GetEffectiveBounds()
        {
            if (HasParent)
                return Rectangle.Intersect(Parent.GetEffectiveBounds(), GetBounds());

            return GetBounds();
        }

        public virtual Rectangle GetBounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height);
        }

        private void HandleMouseEvents(Rectangle bounds)
        {
            if (bounds.Contains(Input.GetMousePosition()) && !_isMouseHovering)
            {
                OnMouseEnter(this, new MouseStateEventArgs(Input));
                _isMouseHovering = true;
            }
            else if (!bounds.Contains(Input.GetMousePosition()) && _isMouseHovering)
            {
                OnMouseExit(this, new MouseStateEventArgs(Input));
                _isMouseHovering = false;
                _isMouseDown = false;
            }

            if (_isMouseHovering)
            {
                if (Input.IsPressed(MouseButton.LeftButton) && !_isMouseDown)
                {
                    OnMouseDown(this, new MouseStateEventArgs(Input));
                    _isMouseDown = true;
                }
                else if (Input.IsReleased(MouseButton.LeftButton) && _isMouseDown)
                {
                    OnMouseUp(this, new MouseStateEventArgs(Input));
                    _isMouseDown = false;
                }
            }
        }

        /// <summary>
        /// Gets the draw position.
        /// </summary>
        /// <returns>The draw position.</returns>
        protected Vector2 GetAlignedPosition()
        {
            var bounds = GetBounds();
            switch (Alignment)
            {
                default:
                    return new Vector2(bounds.Left, bounds.Top);
                case Alignment.TopCenter:
                    return new Vector2(bounds.Center.X, bounds.Top);
                case Alignment.TopRight:
                    return new Vector2(bounds.Right, bounds.Top);

                case Alignment.MiddleLeft:
                    return new Vector2(bounds.Left, bounds.Center.Y);
                case Alignment.MiddleCenter:
                    return new Vector2(bounds.Center.X, bounds.Center.Y);
                case Alignment.MiddleRight:
                    return new Vector2(bounds.Right, bounds.Center.Y);

                case Alignment.BottomLeft:
                    return new Vector2(bounds.Left, bounds.Bottom);
                case Alignment.BottomCenter:
                    return new Vector2(bounds.Center.X, bounds.Bottom);
                case Alignment.BottomRight:
                    return new Vector2(bounds.Right, bounds.Bottom);
            }
        }

        public event EventHandler<MouseStateEventArgs> MouseEnter;
        public event EventHandler<MouseStateEventArgs> MouseExit;
        public event EventHandler<MouseStateEventArgs> MouseDown;
        public event EventHandler<MouseStateEventArgs> MouseUp;
        public event EventHandler AlignmentChanged;
        public event EventHandler FontChanged;
        public event EventHandler RenderOrderChanged;
        public event EventHandler SizeChanged;

        protected virtual void OnMouseEnter(object sender, MouseStateEventArgs e)
        {
            MouseEnter?.Invoke(sender, e);
        }

        protected virtual void OnMouseExit(object sender, MouseStateEventArgs e)
        {
            MouseExit?.Invoke(sender, e);
        }

        protected virtual void OnMouseDown(object sender, MouseStateEventArgs e)
        {
            MouseDown?.Invoke(sender, e);
        }

        protected virtual void OnMouseUp(object sender, MouseStateEventArgs e)
        {
            MouseUp?.Invoke(sender, e);
        }

        protected virtual void OnAlignmentChanged(object sender, EventArgs e)
        {
            AlignmentChanged?.Invoke(sender, e);
        }

        protected virtual void OnFontChanged(object sender, EventArgs e)
        {
            //As child elements may reference the same font, notify child elements of this event.
            foreach (var child in GetChildren())
            {
                if (child._font == null)
                {
                    child.OnFontChanged(this, e);
                }
            }

            FontChanged?.Invoke(sender, e);
        }

        protected virtual void OnRenderOrderChanged(object sender, EventArgs e)
        {
            RenderOrderChanged?.Invoke(sender, e);
        }

        protected virtual void OnSizeChanged(object sender, EventArgs e)
        {
            SizeChanged?.Invoke(sender, e);
        }
    }
}
