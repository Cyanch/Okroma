using System;

namespace Okroma.Physics
{
    class CouldNotFindValidIntersectsMethod : Exception
    {
        public CouldNotFindValidIntersectsMethod(string colliderType1, string colliderType2) : base(string.Format("Could not find valid {0}.Intersects<{1}> or {1}.Intersects<{0}>", colliderType1, colliderType2))
        {

        }
    }
}
