namespace Okroma.TileEngine.TileProperties
{
    abstract class TileProperty
    {
        public void Initialize(Tile tile)
        {
            var requiredProperties = (RequirePropertyAttribute[])GetType().GetCustomAttributes(typeof(RequirePropertyAttribute), true);
            foreach (var reqProp in requiredProperties)
            {
                if (!tile.HasProperty(reqProp.PropertyType))
                {
                    throw new RequiredPropertyNotAvailableException(reqProp.PropertyType);
                }
            }
        }
    }
}
