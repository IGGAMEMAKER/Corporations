namespace Assets.Utils
{
    public static partial class Markets
    {
        static Bonus<long> GetCompanyRiskBonus(GameContext gameContext, int companyId)
        {
            int marketDemand = GetMarketDemandRisk(gameContext, companyId);
            int monetisation = GetMonetisationRisk(gameContext, companyId);
            int competitors = GetCompetitionRisk(gameContext, companyId);

            return new Bonus<long>("Total risk")
                .SetDimension("%")
                .Append("Niche demand risk", marketDemand)
                //.Append("Competition risk", competitors)
                .AppendAndHideIfZero("Is not profitable", monetisation);
        }

        internal static int GetCompetitionRisk(GameContext gameContext, int companyId)
        {
            var company = Companies.GetCompany(gameContext, companyId);

            return GetCompetitorsAmount(company, gameContext) * 5;
        }

        internal static long GetCompanyRisk(GameContext gameContext, int companyId)
        {
            return (long)GetCompanyRiskBonus(gameContext, companyId).Sum();
        }

        public static string GetCompanyRiskDescription(GameContext gameContext, int companyId)
        {
            return GetCompanyRiskBonus(gameContext, companyId).ToString(true);
        }

        public static int GetMonetisationRisk(GameContext gameContext, int companyId)
        {
            int num = Constants.RISKS_MONETISATION_MAX;

            if (EconomyUtils.IsProfitable(gameContext, companyId))
                num -= Constants.RISKS_MONETISATION_IS_PROFITABLE;

            return num;
        }


        public static int GetMarketDemandRisk(GameContext gameContext, int companyId)
        {
            var c = Companies.GetCompany(gameContext, companyId);

            return GetMarketDemandRisk(gameContext, c.product.Niche);
        }

        public static int GetMarketDemandRisk(GameContext gameContext, NicheType nicheType)
        {
            var phase = GetMarketState(gameContext, nicheType);

            switch (phase)
            {
                case NicheState.Idle:
                    return Constants.RISKS_DEMAND_MAX;

                case NicheState.Innovation:
                    return Constants.RISKS_DEMAND_MAX / 2;

                case NicheState.Trending:
                    return Constants.RISKS_DEMAND_MAX / 5;

                case NicheState.MassGrowth:
                    return Constants.RISKS_DEMAND_MAX / 10;

                case NicheState.Decay:
                    return Constants.RISKS_DEMAND_MAX / 2;

                case NicheState.Death:
                default:
                    return 100;
            }
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
