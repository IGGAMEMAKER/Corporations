using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static float GetSegmentProductPrice(GameContext gameContext, NicheType nicheType, int segmentId)
        {
            var niche = GetNicheEntity(gameContext, nicheType);

            var priceModifier = GetSegmentProductPriceModifier(gameContext, nicheType, segmentId);

            return niche.nicheCosts.BasePrice * priceModifier;
        }


        public static float GetSegmentProductPriceModifier(GameContext gameContext, NicheType nicheType, int segmentId)
        {
            var positioningData = GetProductPositioningInfo(gameContext, nicheType, segmentId);

            var priceModifier = positioningData.priceModifier;
            if (priceModifier == 0)
                priceModifier = 1;

            return priceModifier;
        }

        public static ProductPositioning GetProductPositioningInfo(GameContext GameContext, NicheType nicheType, int segmentId)
        {
            var positionings = GetNichePositionings(nicheType, GameContext);
            var niche = GetNicheEntity(GameContext, nicheType);

            return positionings[segmentId];
        }
    }
}
