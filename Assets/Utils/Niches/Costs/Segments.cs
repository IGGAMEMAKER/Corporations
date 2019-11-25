using System.Collections.Generic;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static float GetSegmentProductPrice(GameContext gameContext, NicheType nicheType, int segmentId)
        {
            var priceModifier = GetSegmentProductPriceModifier(gameContext, nicheType, segmentId);

            var costs = GetNicheCosts(gameContext, nicheType);

            return costs.BaseIncome * priceModifier;
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

            return positionings[segmentId];
        }


        public static string GetCompanyPositioning(GameEntity company, GameContext gameContext)
        {
            var positioning = company.productPositioning.Positioning;
            var posTextual = NicheUtils.GetNichePositionings(company.product.Niche, gameContext)[positioning].name;

            return posTextual;
        }

        internal static Dictionary<int, ProductPositioning> GetNichePositionings(NicheType niche, GameContext gameContext)
        {
            var e = GetNiche(gameContext, niche);

            var p = e.nicheSegments.Positionings;

            return p;
        }
    }
}
