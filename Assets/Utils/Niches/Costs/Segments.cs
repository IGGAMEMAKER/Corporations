using Assets.Core.Formatting;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Markets
    {
        public static float GetSegmentProductPrice(GameContext gameContext, NicheType nicheType, int segmentId)
        {
            var priceModifier = GetPositioningPriceModifier(gameContext, nicheType, segmentId);

            var baseIncome = GetBaseProductPrice(nicheType, gameContext);

            return baseIncome * priceModifier;
        }



        public static float GetBaseProductPrice(GameEntity e, GameContext context) => GetBaseProductPrice(e.product.Niche, context);
        public static float GetBaseProductPrice(NicheType nicheType, GameContext context)
        {
            return GetNicheCosts(context, nicheType).BaseIncome;
        }



        // positionings
        public static float GetPositioningPriceModifier(GameContext gameContext, NicheType nicheType, int segmentId)
        {
            var positioningData = GetProductPositioningInfo(gameContext, nicheType, segmentId);

            var priceModifier = positioningData.priceModifier;
            if (priceModifier == 0)
                priceModifier = 1;

            return priceModifier;
        }

        public static ProductPositioning GetProductPositioningInfo(GameContext GameContext, NicheType nicheType, int segmentId)
        {
            //Debug.Log("GetProductPositioningInfo " + EnumUtils.GetFormattedNicheName(nicheType) + " segment " + segmentId);
            var positionings = GetNichePositionings(nicheType, GameContext);

            return positionings[segmentId];
        }


        public static string GetCompanyPositioning(GameEntity company, GameContext gameContext)
        {
            var positioning = company.productPositioning.Positioning;
            var posTextual = GetNichePositionings(company.product.Niche, gameContext)[positioning].name;

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
