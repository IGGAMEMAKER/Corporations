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

        internal static long GetProductCompanyMaintenance(GameEntity e, GameContext gameContext)
        {
            if (TeamUtils.IsUpgradePicked(e, TeamUpgrade.DevelopmentCrossplatform))
                return TeamUtils.GetImprovementCost(gameContext, e, TeamUpgrade.DevelopmentCrossplatform);

            if (TeamUtils.IsUpgradePicked(e, TeamUpgrade.DevelopmentPolishedApp))
                return TeamUtils.GetImprovementCost(gameContext, e, TeamUpgrade.DevelopmentPolishedApp);

            if (TeamUtils.IsUpgradePicked(e, TeamUpgrade.DevelopmentPrototype))
                return TeamUtils.GetImprovementCost(gameContext, e, TeamUpgrade.DevelopmentPrototype);

            return 0;
        }
    }
}
