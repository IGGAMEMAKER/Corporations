using System;

namespace Assets.Utils
{
    partial class CompanyEconomyUtils
    {
        private static long GetProductCompanyCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            long audienceCost = GetClientBaseCost(context, companyId);
            long profitCost = GetCompanyIncome(c, context) * 15;

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

        public static long GetProductCompanyIncome(GameEntity e)
        {
            float income = e.marketing.Clients * GetProductPrice(e);

            return Convert.ToInt64(income);
        }

        internal static long GetBalance(GameEntity e)
        {
            return GetProductCompanyIncome(e) - GetProductCompanyMaintenance(e);
        }

        internal static string GetIncomeDescription(GameEntity company)
        {
            return $"Income of this company equals {GetProductCompanyIncome(company)}";
        }

        internal static string GetMaintenanceDescription(GameEntity company)
        {
            return $"Maintenance of this company equals {GetProductCompanyMaintenance(company)}";
        }

        public static long GetProductCompanyMaintenance(GameEntity e)
        {
            if (e.hasTeam)
                return GetTeamMaintenance(e);
            else
                return 1;
        }

        public static long GetTeamMaintenance(GameEntity e)
        {
            return (e.team.Managers + e.team.Marketers + e.team.Programmers) * 2000;
        }
    }
}
