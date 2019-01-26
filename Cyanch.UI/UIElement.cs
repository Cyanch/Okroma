using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Cyanch.UI
{
    public abstract class UIElement
    {
        public SpriteFont Font
        {
            get
            {
                return _font ?? Parent.Font;
            }
            set
            {
                _font = value;
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
        public int RenderOrder
        {
            get
            {
                return _renderOrder;
            }
            set
            {
                _renderOrder = value;
                RenderOrderChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Vector2 LocalPosition { get; set; }
        public Vector2 Position
        {
            get
            {
                return LocalPosition + Parent?.Position ?? Vector2.Zero;
            }
            set
            {
                LocalPosition = value - Parent?.Position ?? Vector2.Zero;
            }
        }

        public InputState Input
        {
            get
            {
                return _input ?? Parent.Input;
            }
            set
            {
                _input = value;
            }
        }
        
        public float Width { get; set; }
        public float Height { get; set; }


        public UIElement Parent { get; private set; }
        public bool HasParent => Parent != null;

        private List<UIElement> _childElements = new List<UIElement>();
        private SpriteFont _font;
        private GraphicsDevice _graphicsDevice;
        private int _renderOrder;
        private InputState _input;
        private bool _isMouseHovering;
        private bool _isMouseDown;

        public virtual void HandleInput()
        {
            _input?.Update();
            HandleMouseEvents();

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

        public Rectangle GetBounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height);
        }

        private void HandleMouseEvents()
        {
            if (GetBounds().Contains(Input.GetMousePosition()) && !_isMouseHovering)
            {
                MouseEnter?.Invoke(this, Input);
                _isMouseHovering = true;
            }
            else if (!GetBounds().Contains(Input.GetMousePosition()) && _isMouseHovering)
            {
                MouseExit?.Invoke(this, Input);
                _isMouseHovering = false;
                _isMouseDown = false;
            }

            if (_isMouseHovering)
            {
                if (Input.IsDown(MouseButton.LeftButton) && !_isMouseDown)
                {
                    MouseDown?.Invoke(this, Input);
                }
                else if (Input.IsUp(MouseButton.LeftButton) && _isMouseDown)
                {
                    MouseUp?.Invoke(this, Input);
                }
            }
        }

        public event EventHandler<InputState> MouseEnter;
        public event EventHandler<InputState> MouseExit;
        public event EventHandler<InputState> MouseDown;
        public event EventHandler<InputState> MouseUp;
        public event EventHandler RenderOrderChanged;
    }
}
