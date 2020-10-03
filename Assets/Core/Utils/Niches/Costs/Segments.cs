using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class Markets
    {
        public static float GetBaseProductPrice(GameEntity e, GameContext context) => GetBaseProductPrice(e.product.Niche, context);
        public static float GetBaseProductPrice(NicheType nicheType, GameContext context)
        {
            return GetNicheCosts(context, nicheType).BaseIncome;
        }



        // positionings

        public static void AddProductPositioning(GameContext gameContext, NicheType nicheType, ProductPositioning positioning)
        {
            var market = Get(gameContext, nicheType);

            market.nicheSegments.Positionings.Add(positioning);
        }

        public static ProductPositioning GetProductPositioningInfo(GameContext GameContext, NicheType nicheType, int segmentId)
        {
            //Debug.Log("GetProductPositioningInfo " + EnumUtils.GetFormattedNicheName(nicheType) + " segment " + segmentId);
            var positionings = GetNichePositionings(nicheType, GameContext);

            return positionings[segmentId];
        }


        public static string GetCompanyPositioningName(GameEntity company, GameContext gameContext)
        {
            var positioning = company.productPositioning.Positioning;

            return GetNichePositionings(company.product.Niche, gameContext)[positioning].name;
        }

        public static List<ProductPositioning> GetNichePositionings(NicheType niche, GameContext gameContext)
        {
            var e = Get(gameContext, niche);

            return e.nicheSegments.Positionings;
        }

        public static long GetPositioningValue(ProductPositioning p)
        {
            var infos = Marketing.GetAudienceInfos();
            var worth = 0L;

            int segmentId = 0;
            foreach (var l in p.Loyalties)
            {
                if (l > 0)
                {
                    var segment = infos[segmentId];
                    var incomePerUser = 1.5f; // Economy.GetIncomePerUser(gameContext, playerFlagship, segmentId)

                    var segmentWorth = (long)(segment.Size * incomePerUser);

                    worth += segmentWorth * (100 + l * 10) / 100;
                }

                segmentId++;
            }

            return worth;
        }
    }
}
