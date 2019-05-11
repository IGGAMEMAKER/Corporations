using System;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        internal static int GetCompanyRisk(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return
                GetMonetisationRisk(gameContext, c.company.Id) +
                GetMarketDemandRisk(gameContext, companyId) +
                GetCompetititiveRiskOnNiche(gameContext, companyId);
        }

        public static bool IsMarketingSelfPaying(GameContext gameContext, int companyId)
        {
            return true;
        }

        public static int GetMonetisationRisk(GameContext gameContext, int companyId)
        {
            int num = Constants.RISKS_MONETISATION_MAX;

            if (IsMarketingSelfPaying(gameContext, companyId))
                num -= Constants.RISKS_MONETISATION_ADS_REPAYABLE;

            if (CompanyEconomyUtils.IsCompanyProfitable(gameContext, companyId))
                num -= Constants.RISKS_MONETISATION_IS_PROFITABLE;

            return num;
        }


        public static int GetMarketDemandRisk(GameContext gameContext, NicheType nicheType)
        {
            // amount of users/niche fame
            return 45;
        }

        public static int GetMarketDemandRisk(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return GetMarketDemandRisk(gameContext, c.product.Niche);
        }

        public static int GetCompetititiveRiskOnNiche(GameContext gameContext, int companyId)
        {
            return 33;
        }

        public static int GetNewPlayerRiskOnNiche(GameContext gameContext, NicheType nicheType)
        {
            // based on competitors level and amount
            return 33;
        }

        public static int GetStartupRiskOnNiche(GameContext gameContext, NicheType nicheType)
        {
            return
                Constants.RISKS_MONETISATION_MAX +
                GetMarketDemandRisk(gameContext, nicheType) +
                GetNewPlayerRiskOnNiche(gameContext, nicheType);
        }

        public static Risk ShowRiskStatus(int risk)
        {
            if (risk < 10)
                return Risk.Guaranteed;

            if (risk < 50)
                return Risk.Risky;

            return Risk.TooRisky;
        }

        public static string GetStartupRiskOnNicheDescription(GameContext gameContext, NicheType nicheType)
        {
            int risk = GetStartupRiskOnNiche(gameContext, nicheType);
            string text = ShowRiskStatus(risk).ToString();

            int demand = GetMarketDemandRisk(gameContext, nicheType);
            int competitors = GetNewPlayerRiskOnNiche(gameContext, nicheType);

            return $"Current risk is {risk}%! ({text})" +
            $"\nUnknown demand: +{demand}%" +
            $"\nStrong competitors: +{competitors}%";
        }

        internal static int GetCompanyMarketPositionBonus(GameEntity company)
        {
            int marketLevel = 10;

            int productLevel = company.product.ProductLevel;

            int techLeadershipBonus = company.isTechnologyLeader ? 15 : 0;

            return productLevel - marketLevel + techLeadershipBonus;
        }
    }
}
