using System;

namespace Okroma.TileEngine.TileProperties
{
    class RequiredPropertyNotAvailableException : Exception
    {
        public RequiredPropertyNotAvailableException(Type missingPropertyType) : base(missingPropertyType + " is not available even though it is a required property for this property.")
        {
        }
    }
}
