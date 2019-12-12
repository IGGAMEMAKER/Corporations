namespace Assets.Utils
{
    public static partial class Products
    {
        public static int GetBaseIterationTime(GameEntity niche) => GetBaseIterationTime(niche.nicheBaseProfile.Profile.NicheSpeed);
        public static int GetBaseIterationTime(NicheSpeed nicheChangeSpeed)
        {
            return 60;

            var modifier = 4;
            switch (nicheChangeSpeed)
            {
                case NicheSpeed.Quarter:   return 90 / modifier;

                case NicheSpeed.HalfYear:  return 180 / modifier;
                case NicheSpeed.Year:      return 360 / modifier;

                case NicheSpeed.ThreeYears: return 360 * 3 / modifier;

                default: return 0;
            }
        }

        public static int GetProductUpgradeIterationTime(GameContext gameContext, GameEntity company)
        {
            var niche = Markets.GetNiche(gameContext, company);
            var baseIterationTime = GetBaseIterationTime(niche);

            var innovationPenalty = IsWillInnovate(company, gameContext) ? 50 : 0;
            var financing = company.financing.Financing;

            var companyLimitPenalty = Companies.GetCompanyLimitPenalty(company, gameContext);

            var modifiers = 100 + innovationPenalty
                - financing[Financing.Team] * Constants.FINANCING_ITERATION_SPEED_PER_LEVEL
                //- mindsetModifier * Constants.CULTURE_ITERATION_SPEED_PER_LEVEL
                + companyLimitPenalty;


            var time = (int) (baseIterationTime * modifiers / 100f);

            return time;
        }

        public static int GetTimeToMarketFromScratch(GameEntity niche)
        {
            var demand = GetMarketDemand(niche);
            var iterationTime = GetBaseIterationTime(niche);
            var timeToMarket = demand * iterationTime / 30;

            return timeToMarket;
        }
    }
}
