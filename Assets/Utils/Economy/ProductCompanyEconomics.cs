namespace Assets.Utils
{
    partial class CompanyEconomyUtils
    {
        private static long GetProductCompanyCost(GameContext context, int companyId)
        {
            var risks = NicheUtils.GetCompanyRisk(context, companyId);

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

        private static string GetProductCompanyIncomeDescription(GameEntity gameEntity, GameContext gameContext)
        {
            var income = GetProductCompanyIncome(gameEntity, gameContext);

            return $"Income of this company equals {Format.Money(income)}";
        }

        internal static string GetProductCompanyMaintenanceDescription(GameEntity company)
        {
            var maintenance = GetProductCompanyMaintenance(company);

            return $"Maintenance of this company equals {Format.Money(maintenance)}";
        }

        private static long GetProductCompanyMaintenance(GameEntity e)
        {
            return GetTeamMaintenance(e);
        }
    }
}
