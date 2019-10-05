using UnityEngine;

namespace Assets.Utils
{
    partial class EconomyUtils
    {
        private static long GetProductCompanyCost(GameContext context, int companyId)
        {
            var risks = NicheUtils.GetCompanyRisk(context, companyId);

            return GetProductCompanyBaseCost(context, companyId) * (100 - risks) / 100;
        }

        public static long GetProductCompanyBaseCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            long audienceCost = GetClientBaseCost(context, companyId);
            long profitCost = GetCompanyIncome(c, context) * GetCompanyCostNicheMultiplier();

            return audienceCost + profitCost;
        }

        public static long GetClientBaseCost(GameContext context, int companyId)
        {
            return 0;
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return MarketingUtils.GetClients(c) * 100;
        }

        public static long GetCompanyMarketingMaintenance(GameEntity e, GameContext gameContext)
        {
            if (TeamUtils.IsUpgradePicked(e, TeamUpgrade.DevelopmentCrossplatform))
                return TeamUtils.GetImprovementCost(gameContext, e, TeamUpgrade.DevelopmentCrossplatform);

            if (TeamUtils.IsUpgradePicked(e, TeamUpgrade.DevelopmentPolishedApp))
                return TeamUtils.GetImprovementCost(gameContext, e, TeamUpgrade.DevelopmentPolishedApp);

            return 0;
        }

        internal static long GetProductCompanyMaintenance(GameEntity e, GameContext gameContext)
        {
            return GetTeamMaintenance(e) + GetCompanyMarketingMaintenance(e, gameContext);
        }


        // TODO DUPLICATING GETTING MAINTENANCE
        public static bool IsCanAffordMarketing(GameEntity company, GameContext gameContext)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, company.product.Niche);
            var baseCost = NicheUtils.GetBaseMarketingMaintenance(niche);

            return CompanyUtils.IsEnoughResources(company, baseCost);
        }

        // TODO DUPLICATING GETTING MAINTENANCE
        public static long GetCompanyAdsMaintenance(GameEntity company, GameContext gameContext)
        {
            if (!company.hasProduct)
                return 0;

            var niche = NicheUtils.GetNicheEntity(gameContext, company.product.Niche);
            var baseCost = NicheUtils.GetBaseMarketingMaintenance(niche).money;

            if (IsCanAffordMarketing(company, gameContext))
                return baseCost;

            return 0;
        }
    }
}
