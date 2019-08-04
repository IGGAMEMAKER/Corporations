namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static long GetMarketPotential(GameContext gameContext, NicheType nicheType)
        {
            return GetMarketPotential(GetNicheEntity(gameContext, nicheType));
        }

        public static long GetSegmentMarketShare(GameContext GameContext, NicheType nicheType, int segmentId)
        {
            var positioningData = GetProductPositioningInfo(GameContext, nicheType, segmentId);

            return positioningData.marketShare;
        }

        public static long GetMarketSegmentPotential(GameContext GameContext, NicheType nicheType, int segmentId)
        {
            var niche = GetNicheEntity(GameContext, nicheType);

            var segmentShare = GetSegmentMarketShare(GameContext, nicheType, segmentId);

            return GetMarketPotential(niche) * segmentShare / 100;
        }

        public static long GetMarketSegmentAudiencePotential(GameContext GameContext, NicheType nicheType, int segmentId)
        {
            var niche = GetNicheEntity(GameContext, nicheType);

            var segmentShare = GetSegmentMarketShare(GameContext, nicheType, segmentId);

            return segmentShare * GetMarketAudiencePotential(niche) / 100;
        }

        public static long GetRelativeAnnualMarketGrowth(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            // 12 months
            return niche.nicheLifecycle.Growth[phase] * 12;
        }

        public static long GetAbsoluteAnnualMarketGrowth(GameEntity niche)
        {
            var relativeGrowth = GetRelativeAnnualMarketGrowth(niche);

            var clientBatch = MarketingUtils.GetCurrentClientFlow(gameContext, niche.niche.NicheType);


            long clients = 0;

            clients += clientBatch * relativeGrowth;
        }

        public static long GetMarketAudiencePotential(GameEntity niche)
        {
            var lifecycle = niche.nicheLifecycle;

            var clientBatch = GetNicheCosts(niche).ClientBatch;

            long clients = 0;

            foreach (var g in lifecycle.Growth)
            {
                var phasePeriod = GetMinimumPhaseDurationInPeriods(g.Key) * GetNichePeriodDurationInMonths(niche);

                clients += clientBatch * g.Value * phasePeriod;
            }

            //Debug.Log($"Audience potential for {niche.niche.NicheType}: {Format.Minify(clients)}");

            return clients;
        }

        public static long GetMarketPotential(GameEntity niche)
        {
            var clients = GetMarketAudiencePotential(niche);

            var price = GetNicheCosts(niche).BasePrice * 1.5f;

            // * CompanyEconomyUtils.GetCompanyCostNicheMultiplier()
            return (long)(clients * price);
        }

        // TODO
        // max can be less than min
        public static long GetMarketPotentialMax(GameEntity niche)
        {
            var potential = GetMarketPotential(niche);

            // TODO MAKE THIS FUNCTION STATIC!
            var nicheId = (int)niche.niche.NicheType;

            var min = 1f;
            var dispersion = 0.7f;
            var max = min + dispersion;

            return CompanyUtils.GetRandomValue(potential, nicheId, nicheId + 3, min, max);
        }
        // TODO
        // max can be less than min
        public static long GetMarketPotentialMin(GameEntity niche)
        {
            var potential = GetMarketPotential(niche);

            // TODO MAKE THIS FUNCTION STATIC!
            var nicheId = (int)niche.niche.NicheType;

            var min = 0.1f;
            var dispersion = 0.7f;
            var max = min + dispersion;

            return CompanyUtils.GetRandomValue(potential, nicheId, nicheId + 1, 0.1f, 0.8f);
        }
    }
}
