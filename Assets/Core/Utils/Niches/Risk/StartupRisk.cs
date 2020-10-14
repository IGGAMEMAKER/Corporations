namespace Assets.Core
{
    public static partial class Markets
    {
        static Bonus<long> GetCompanyRiskBonus(GameContext gameContext, GameEntity company)
        {
            int marketDemand = GetMarketDemandRisk(gameContext, company);
            int monetisation = GetMonetisationRisk(gameContext, company);
            int competitors = GetCompetitionRisk(gameContext, company);

            return new Bonus<long>("Total risk")
                .SetDimension("%")
                .Append("Market state", marketDemand)
                .AppendAndHideIfZero("Is not profitable", monetisation);
        }

        public static int GetCompetitionRisk(GameContext gameContext, GameEntity company)
        {
            return GetCompetitorsAmount(company, gameContext) * 5;
        }

        public static long GetCompanyRisk(GameContext gameContext, GameEntity company)
        {
            return (long)GetCompanyRiskBonus(gameContext, company).Sum();
        }

        public static string GetCompanyRiskDescription(GameContext gameContext, GameEntity company)
        {
            return GetCompanyRiskBonus(gameContext, company).ToString(true);
        }

        public static int GetMonetisationRisk(GameContext gameContext, GameEntity company)
        {
            int num = C.RISKS_MONETISATION_MAX;

            if (Economy.IsProfitable(gameContext, company))
                num -= C.RISKS_MONETISATION_IS_PROFITABLE;

            return num;
        }


        public static int GetMarketDemandRisk(GameContext gameContext, GameEntity c)
        {
            return GetMarketDemandRisk(gameContext, c.product.Niche);
        }

        public static Risk ShowRiskStatus(long risk)
        {
            if (risk < 10)
                return Risk.Guaranteed;

            if (risk < 50)
                return Risk.Risky;

            return Risk.TooRisky;
        }
    }
}
