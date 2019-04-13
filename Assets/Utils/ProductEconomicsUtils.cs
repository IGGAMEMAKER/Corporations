using System;

namespace Assets.Utils
{
    public static class ProductEconomicsUtils
    {
        public static float GetBasePrice(GameEntity e)
        {
            return e.finance.basePrice;
        }

        public static float GetProductPrice(GameEntity e)
        {
            if (e.finance.price <= 0) return 0;

            float price = 10 + (e.finance.price - 1);

            return GetBasePrice(e) * price / 10;
        }

        public static long GetIncome(GameEntity e)
        {
            if (e.company.CompanyType == CompanyType.ProductCompany)
            {
                float income = e.marketing.Clients * GetProductPrice(e);

                return Convert.ToInt64(income);
            }

            return 1000000;
        }

        internal static long GetBalance(GameEntity e)
        {
            return GetIncome(e) - GetMaintenance(e);
        }

        internal static string GetIncomeDescription(GameEntity company)
        {
            return $"Income of this company equals {GetIncome(company)}";
        }

        internal static string GetMaintenanceDescription(GameEntity company)
        {
            return $"Maintenance of this company equals {GetMaintenance(company)}";
        }

        public static long GetMaintenance(GameEntity e)
        {
            if (e.hasTeam)
                return GetTeamMaintenance(e);
            else
                return 0;
        }

        public static long GetTeamMaintenance(GameEntity e)
        {
            return (e.team.Managers + e.team.Marketers + e.team.Programmers) * 2000;
        }
    }
}
