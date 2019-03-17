using System;

namespace Okroma.TileEngine.TileProperties
{
    [AttributeUsage(AttributeTargets.Class)]
    class RequirePropertyAttribute : Attribute
    {
        public Type PropertyType { get; }

        public RequirePropertyAttribute(Type propertyType)
        {
            this.PropertyType = propertyType;
        }
    }
}
