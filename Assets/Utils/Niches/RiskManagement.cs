using System;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        static BonusContainer GetCompanyRiskBonusContainer(GameContext gameContext, int companyId)
        {
            int monetisation = GetMonetisationRisk(gameContext, companyId);
            int marketDemand = GetMarketDemandRisk(gameContext, companyId);
            int competitiveness = GetCompetititiveRiskOnNiche(gameContext, companyId);

            return new BonusContainer("Total risk")
                .SetDimension("%")
                .Append("Niche demand risk", marketDemand)
                .Append("Competitors risk", competitiveness)
                .Append("Is profitable?", monetisation);
        }

        internal static long GetCompanyRisk(GameContext gameContext, int companyId)
        {
            return GetCompanyRiskBonusContainer(gameContext, companyId).Sum();
        }

        public static string GetCompanyRiskDescription(GameContext gameContext, int companyId)
        {
            return GetCompanyRiskBonusContainer(gameContext, companyId).ToString(true);
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
            return 45;
        }

        public static int GetCompetititiveRiskOnNiche(GameContext gameContext, int companyId)
        {
            return 33;
        }
    }
}
