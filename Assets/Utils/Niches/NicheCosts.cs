using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static NicheCostsComponent GetNicheCosts(GameEntity niche)
        {
            var state = GetMarketState(niche);
            var multiplier = GetMarketStateCostsModifier(state);

            var costs = niche.nicheCosts;

            var component = new NicheCostsComponent
            {
                BasePrice = costs.BasePrice,
                AdCost = costs.AdCost * multiplier,
                ClientBatch = costs.ClientBatch * multiplier,
                IdeaCost = costs.IdeaCost * multiplier,
                MarketingCost = costs.MarketingCost * multiplier,
                TechCost = costs.TechCost * multiplier
            };

            return component;
        }

        public static NicheCostsComponent GetNicheCosts(GameContext context, NicheType nicheType)
        {
            var niche = GetNicheEntity(context, nicheType);

            return GetNicheCosts(niche);
        }

        public static long GetStartCapital(NicheType nicheType, GameContext gameContext)
        {
            var niche = GetNicheEntity(gameContext, nicheType);

            return GetStartCapital(niche);
        }

        public static long GetStartCapital(GameEntity niche)
        {
            var marketDemand = ProductUtils.GetMarketDemand(null, niche);
            var iterationTimeInMonths = 12;
            var timeToMarket = marketDemand * iterationTimeInMonths;


            return timeToMarket * GetBaseProductMaintenance(niche);
        }


        public static int GetMarketStateCostsModifier(NicheLifecyclePhase phase)
        {
            switch (phase)
            {
                case NicheLifecyclePhase.Innovation: return 1;
                case NicheLifecyclePhase.Trending: return 5;
                case NicheLifecyclePhase.MassUse: return 9;
                case NicheLifecyclePhase.Decay: return 7;

                default: return 0;
            }
        }



        internal static long GetBaseProductMaintenance(GameEntity niche)
        {
            return GetNicheCosts(niche).AdCost;
        }
    }
}
