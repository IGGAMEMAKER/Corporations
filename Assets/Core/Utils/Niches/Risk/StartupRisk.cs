namespace Assets.Core
{
    public static partial class Markets
    {
        public static Bonus<long> GetCompanyRiskBonus(GameContext gameContext, int companyId)
        {
            int marketDemand = GetMarketDemandRisk(gameContext, companyId);
            int monetisation = GetMonetisationRisk(gameContext, companyId);
            int competitors = GetCompetitionRisk(gameContext, companyId);

            return new Bonus<long>("Total risk")
                .SetDimension("%")
                .Append("Market state", marketDemand)
                .AppendAndHideIfZero("Is not profitable", monetisation);
        }

        public static int GetCompetitionRisk(GameContext gameContext, int companyId)
        {
            var company = Companies.Get(gameContext, companyId);

            return GetCompetitorsAmount(company, gameContext) * 5;
        }

        public static long GetCompanyRisk(GameContext gameContext, int companyId)
        {
            return (long)GetCompanyRiskBonus(gameContext, companyId).Sum();
        }

        public static string GetCompanyRiskDescription(GameContext gameContext, int companyId)
        {
            return GetCompanyRiskBonus(gameContext, companyId).ToString(true);
        }

        public static int GetMonetisationRisk(GameContext gameContext, int companyId)
        {
            int num = Balance.RISKS_MONETISATION_MAX;

            if (Economy.IsProfitable(gameContext, companyId))
                num -= Balance.RISKS_MONETISATION_IS_PROFITABLE;

            return num;
        }


        public static int GetMarketDemandRisk(GameContext gameContext, int companyId)
        {
            var c = Companies.Get(gameContext, companyId);

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
