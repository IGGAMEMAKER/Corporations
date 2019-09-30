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

        public static long GetClientSupportCost(GameEntity e, GameContext gameContext)
        {
            var clients = MarketingUtils.GetClients(e);

            var hasBaseSupport = TeamUtils.IsUpgradePicked(e, TeamUpgrade.ClientSupport);
            var hasAdvancedSupport = TeamUtils.IsUpgradePicked(e, TeamUpgrade.ClientSupportImproved);

            var income = GetProductCompanyIncome(e, gameContext);

            if (hasAdvancedSupport)
                return income / 4;

            if (hasBaseSupport)
                return income / 10;

            return 0;
        }

        public static long GetCompanyMarketingMaintenance(GameEntity e, GameContext gameContext)
        {
            var hasBaseMarketing = TeamUtils.IsUpgradePicked(e, TeamUpgrade.MarketingBase);
            var hasAggressiveMarketing = TeamUtils.IsUpgradePicked(e, TeamUpgrade.MarketingAggressive);

            var baseMarketing = hasBaseMarketing ? TeamUtils.GetImprovementCost(gameContext, e, TeamUpgrade.MarketingBase) : 0; // NicheUtils.GetBaseMarketingMaintenance(niche).money
            var aggressiveMarketing = hasAggressiveMarketing ? TeamUtils.GetImprovementCost(gameContext, e, TeamUpgrade.MarketingAggressive) : 0; // NicheUtils.GetAggressiveMarketingMaintenance(niche).money 

            return baseMarketing + aggressiveMarketing;
        }

        internal static long GetProductCompanyMaintenance(GameEntity e, GameContext gameContext)
        {
            return GetTeamMaintenance(e) + GetCompanyMarketingMaintenance(e, gameContext) + GetClientSupportCost(e, gameContext);
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
