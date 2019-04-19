using System;

namespace Assets.Utils
{
    partial class CompanyEconomyUtils
    {
        private static long GetProductCompanyCost(GameContext context, int companyId)
        {
            int risks = NicheUtils.GetCompanyRisk(context, companyId);

            return GetProductCompanyBaseCost(context, companyId) * (100 - risks) / 100;
        }

        private static long GetProductCompanyBaseCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            long audienceCost = GetClientBaseCost(context, companyId);
            long profitCost = GetCompanyIncome(c, context) * GetCompanyCostNicheMultiplier();

            return audienceCost + profitCost;
        }

        public static long GetClientBaseCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return c.marketing.Clients * 100;
        }

        public static float GetBaseProductPrice(GameEntity e)
        {
            return e.finance.basePrice;
        }

        public static float GetProductPrice(GameEntity e)
        {
            if (e.finance.price <= 0) return 0;

            float price = 10 + (e.finance.price - 1);

            return GetBaseProductPrice(e) * price / 10;
        }

        private static long GetProductCompanyIncome(GameEntity e)
        {
            float income = e.marketing.Clients * GetProductPrice(e);

            return Convert.ToInt64(income);
        }

        private static string GetProductCompanyIncomeDescription(GameEntity gameEntity)
        {
            return $"Income of this company equals {GetProductCompanyIncome(gameEntity)}";
        }

        internal static string GetProductCompanyMaintenanceDescription(GameEntity company)
        {
            return $"Maintenance of this company equals {GetProductCompanyMaintenance(company)}";
        }

        private static long GetProductCompanyMaintenance(GameEntity e)
        {
            return GetTeamMaintenance(e);
        }
    }
}
