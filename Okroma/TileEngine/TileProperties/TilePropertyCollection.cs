using System;
using System.Collections.Generic;

namespace Okroma.TileEngine.TileProperties
{
    interface IReadOnlyTilePropertyCollection
    {
        T GetProperty<T>() where T : TileProperty;
        T GetPropertyOrDefault<T>(T valueIfNotExist) where T : TileProperty;
        bool HasProperty(Type type);
        bool HasProperty<T>() where T : TileProperty;
        bool TryGetProperty<T>(out T property) where T : TileProperty;
    }

    interface ITilePropertyCollection : IReadOnlyTilePropertyCollection
    {
        void AddProperty<T>() where T : TileProperty, new();
        void AddProperty<T>(T property) where T : TileProperty;
    }

    class TilePropertyCollection : ITilePropertyCollection
    {
        public static ITilePropertyCollection Empty { get; } = new NullTilePropertyCollection();
        private Dictionary<Type, TileProperty> _properties;

        public TilePropertyCollection()
        {
            _properties = new Dictionary<Type, TileProperty>();
        }

        public bool HasProperty<T>() where T : TileProperty
        {
            return HasProperty(typeof(T));
        }

        public bool HasProperty(Type type)
        {
            return _properties.ContainsKey(type);
        }

        public T GetProperty<T>() where T : TileProperty
        {
            return (T)_properties[typeof(T)];
        }

        public bool TryGetProperty<T>(out T property) where T : TileProperty
        {
            bool success = _properties.TryGetValue(typeof(T), out var prop);
            property = (T)prop;
            return success;
        }

        public T GetPropertyOrDefault<T>(T valueIfNotExist) where T : TileProperty
        {
            bool success = _properties.TryGetValue(typeof(T), out var prop);
            return (prop as T) ?? valueIfNotExist;
        }

        /// <summary>
        /// Adds property if not sealed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void AddProperty<T>(T property) where T : TileProperty
        {
            Type propType = property.GetType();
            _properties.Add(propType, property);
        }

        /// <summary>
        /// Adds property if not sealed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        public void AddProperty<T>() where T : TileProperty, new()
        {
            AddProperty(new T());
        }

        private sealed class NullTilePropertyCollection : ITilePropertyCollection
        {
            public void AddProperty<T>() where T : TileProperty, new()
            {
            }

            public void AddProperty<T>(T property) where T : TileProperty
            {
            }

            public T GetProperty<T>() where T : TileProperty => throw new NotImplementedException();

            public T GetPropertyOrDefault<T>(T valueIfNotExist) where T : TileProperty => valueIfNotExist;

            public bool HasProperty(Type type) => false;

            public bool HasProperty<T>() where T : TileProperty => false;

            public bool TryGetProperty<T>(out T property) where T : TileProperty
            {
                property = null;
                return false;
            }
        }
    }
}
