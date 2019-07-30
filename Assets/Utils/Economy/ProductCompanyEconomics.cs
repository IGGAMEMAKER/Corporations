using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyEconomyUtils
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
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return MarketingUtils.GetClients(c) * 100;
        }

        private static string GetProductCompanyIncomeDescription(GameEntity gameEntity, GameContext gameContext)
        {
            var income = GetProductCompanyIncome(gameEntity, gameContext);

            return $"Income of this company equals {Format.Money(income)}";
        }

        internal static string GetProductCompanyMaintenanceDescription(GameEntity company, GameContext gameContext)
        {
            var maintenance = GetProductCompanyMaintenance(company, gameContext);

            return $"Maintenance of {company.company.Name} equals {Format.Money(maintenance)}";
        }

        private static long GetProductCompanyMaintenance(GameEntity e, GameContext gameContext)
        {
            return GetTeamMaintenance(e);
        }



        public static long GetAverageMarketingMaintenance(GameEntity e, GameContext gameContext)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, e.product.Niche);
            var baseCost = NicheUtils.GetBaseMarketingMaintenance(niche).salesPoints;

            var financing = MarketingUtils.GetMarketingFinancingPriceModifier(e);

            return baseCost * financing;
        }
        public static long GetAdsMaintenance(GameEntity company, GameContext gameContext)
        {
            if (!company.hasProduct)
                return 0;

            var niche = NicheUtils.GetNicheEntity(gameContext, company.product.Niche);
            var baseCost = NicheUtils.GetBaseMarketingMaintenance(niche).money;

            var financing = MarketingUtils.GetMarketingFinancingPriceModifier(company);

            Debug.Log("Financing " + financing);

            return baseCost * financing;
        }
    }
}
