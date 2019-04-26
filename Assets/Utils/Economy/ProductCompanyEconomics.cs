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

            return MarketingUtils.GetClients(c) * 100;
        }

        public static float GetBaseProductPrice(GameEntity e, GameContext context)
        {
            return e.finance.basePrice;
        }

        public static float GetProductPrice(GameEntity e, GameContext context)
        {
            if (e.finance.price <= 0) return 0;

            float price = 10 + (e.finance.price - 1);

            return GetBaseProductPrice(e, context) * price / 10;
        }

        private static long GetProductCompanyIncome(GameEntity e, GameContext context)
        {
            float income = 0;

            foreach (var pair in e.marketing.Segments)
                income += GetIncomeBySegment(context, e.company.Id, pair.Key);

            return Convert.ToInt64(income);
        }

        internal static float GetSegmentPrice(GameContext gameContext, UserType userType)
        {
            return 5f;
        }

        internal static long GetIncomeBySegment(GameContext gameContext, int companyId, UserType userType)
        {
            //return 0;

            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            long clients = c.marketing.Segments[userType];

            float price = GetSegmentPrice(gameContext, userType) * GetProductPrice(c, gameContext);

            return clients * Convert.ToInt64(price);
        }


        private static string GetProductCompanyIncomeDescription(GameEntity gameEntity, GameContext gameContext)
        {
            return $"Income of this company equals {GetProductCompanyIncome(gameEntity, gameContext)}";
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
