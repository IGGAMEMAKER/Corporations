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

        public static void ChangePositioning(GameEntity product, int positioningId)
        {
            product.productPositioning.Positioning = positioningId;
        }

        public static bool IsFocusingMoreThanOneAudience(GameEntity product)
        {
            var audiences = Marketing.GetAudienceInfos();

            var positioning = product.productPositioning.Positioning;
            bool isFocusingMoreThanOneAudience = positioning > audiences.Count;

            return isFocusingMoreThanOneAudience;
        }
    }
}
