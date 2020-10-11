using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static ProductPositioning GetPositioning(GameEntity product)
        {
            return GetNichePositionings(product)[product.productPositioning.Positioning];
        }

        public static List<ProductPositioning> GetNichePositionings(GameEntity product)
        {
            return product.nicheSegments.Positionings;
        }
    }
}
