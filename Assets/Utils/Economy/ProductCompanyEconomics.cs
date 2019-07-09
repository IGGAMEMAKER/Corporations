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

        public static long GetMarketingMaintenance(GameEntity e, GameContext gameContext)
        {
            if (!e.hasProduct)
                return 0;

            var targetingCost = MarketingUtils.GetTargetingCost(gameContext, e.company.Id);
            var brandingCost = MarketingUtils.GetBrandingCost(gameContext, e);

            return targetingCost.money * 30 + brandingCost.money / 3;
        }
    }
}
