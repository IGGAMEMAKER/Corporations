using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static long GetMarketPotential(GameContext gameContext, NicheType nicheType)
        {
            return GetMarketPotential(GetNicheEntity(gameContext, nicheType));
        }

        public static long GetMarketSegmentPotential(GameContext GameContext, NicheType nicheType, int segmentId)
        {
            var price = (long)(GetSegmentProductPrice(GameContext, nicheType, segmentId) * 100);

            return price * GetMarketSegmentAudiencePotential(GameContext, nicheType, segmentId) / 100;
        }

        public static long GetMarketSegmentAudiencePotential(GameContext GameContext, NicheType nicheType, int segmentId)
        {
            var niche = NicheUtils.GetNicheEntity(GameContext, nicheType);

            var positioningData = GetProductPositioningInfo(GameContext, nicheType, segmentId);

            return positioningData.marketShare * NicheUtils.GetMarketAudiencePotential(niche) / 100;
        }

        public static long GetMarketAudiencePotential(GameEntity niche)
        {
            var lifecycle = niche.nicheLifecycle;

            var clientBatch = niche.nicheCosts.ClientBatch;

            long clients = 0;

            foreach (var g in lifecycle.Growth)
            {
                var phasePeriod = GetMinimumPhaseDurationInPeriods(g.Key) * GetNichePeriodDuration(niche) * 30;

                var brandModifier = 1.5f;
                var financeReach = MarketingUtils.GetMarketingFinancingAudienceReachModifier(MarketingFinancing.High);

                clients += (long)(clientBatch * g.Value * phasePeriod * brandModifier * financeReach);
            }

            return clients;
        }

        public static long GetMarketPotential(GameEntity niche)
        {
            var clients = GetMarketAudiencePotential(niche);

            var price = niche.nicheCosts.BasePrice * 1.5f;

            return (long)(clients * CompanyEconomyUtils.GetCompanyCostNicheMultiplier() * price);
        }
    }
}
