namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        static BonusContainer GetCompanyRiskBonus(GameContext gameContext, int companyId)
        {
            int marketDemand = GetMarketDemandRisk(gameContext, companyId);
            int competitiveness = GetCompetititiveRiskOnNiche(gameContext, companyId);
            int monetisation = GetMonetisationRisk(gameContext, companyId);

            return new BonusContainer("Total risk")
                .SetDimension("%")
                .Append("Niche demand risk", marketDemand)
                .Append("Competition", competitiveness)
                .AppendAndHideIfZero("Is not profitable", monetisation);
        }

        internal static long GetCompanyRisk(GameContext gameContext, int companyId)
        {
            return GetCompanyRiskBonus(gameContext, companyId).Sum();
        }

        public static string GetCompanyRiskDescription(GameContext gameContext, int companyId)
        {
            return GetCompanyRiskBonus(gameContext, companyId).ToString(true);
        }

        public static int GetMonetisationRisk(GameContext gameContext, int companyId)
        {
            int num = Constants.RISKS_MONETISATION_MAX;

            if (CompanyEconomyUtils.IsCompanyProfitable(gameContext, companyId))
                num -= Constants.RISKS_MONETISATION_IS_PROFITABLE;

            return num;
        }


        public static int GetMarketDemandRisk(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return GetMarketDemandRisk(gameContext, c.product.Niche);
        }

        public static int GetMarketDemandRisk(GameContext gameContext, NicheType nicheType)
        {
            // amount of users/niche fame
            return Constants.RISKS_DEMAND_MAX;
        }

        public static int GetCompetititiveRiskOnNiche(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return GetCompetititiveRiskOnNiche(gameContext, c.product.Niche);
        }

        public static int GetCompetititiveRiskOnNiche(GameContext gameContext, NicheType nicheType, int productLevel = 0)
        {
            var risk = (GetMarketDemand(gameContext, nicheType) - productLevel + 2) * 5;

            return risk < Constants.RISKS_NICHE_COMPETITIVENESS_MAX ? risk : Constants.RISKS_NICHE_COMPETITIVENESS_MAX;
        }
    }
}
