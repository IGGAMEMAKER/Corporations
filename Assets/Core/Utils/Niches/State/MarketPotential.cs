namespace Assets.Core
{
    public static partial class Markets
    {
        public static long GetMarketPotential(GameContext gameContext, NicheType nicheType)
        {
            return GetMarketPotential(Get(gameContext, nicheType));
        }

        public static long GetSegmentMarketShare(GameContext GameContext, NicheType nicheType, int segmentId)
        {
            var positioningData = GetProductPositioningInfo(GameContext, nicheType, segmentId);

            return positioningData.marketShare;
        }

        public static long GetMarketSegmentAudiencePotential(GameContext GameContext, NicheType nicheType, int segmentId)
        {
            var niche = Get(GameContext, nicheType);

            var segmentShare = GetSegmentMarketShare(GameContext, nicheType, segmentId);

            return GetMarketAudiencePotential(niche) * segmentShare / 100;
        }


        public static long GetRelativeAnnualMarketGrowth(GameEntity niche)
        {
            var phase = GetMarketState(niche);

            // 12 months
            return niche.nicheLifecycle.Growth[phase] * 12;
        }

        public static long GetAbsoluteAnnualMarketGrowth(GameContext gameContext, GameEntity niche)
        {
            //var relativeGrowth = GetRelativeAnnualMarketGrowth(niche);

            var flow = Marketing.GetClientFlow(gameContext, niche.niche.NicheType);

            long clients = flow; // * relativeGrowth;

            var price = GetNicheCosts(niche).BaseIncome * 1.5f;

            return (long)(clients * price);
        }

        public static long GetMarketAudiencePotential(GameEntity niche)
        {
            var audienceMax = niche.nicheBaseProfile.Profile.AudienceSize;
            int nicheId = (int)niche.niche.NicheType;

            return Companies.GetRandomValue((long)audienceMax, nicheId, 0);
        }

        public static long GetMarketPotential(GameEntity niche)
        {
            var clients = GetMarketAudiencePotential(niche);

            var price = GetNicheCosts(niche).BaseIncome * 1.5f;

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

            return Companies.GetRandomValue(potential, nicheId, nicheId + 3, min, max);
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

            return Companies.GetRandomValue(potential, nicheId, nicheId + 1, 0.1f, 0.8f);
        }
    }
}
