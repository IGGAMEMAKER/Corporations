namespace Assets.Utils
{
    partial class Economy
    {
        private static long GetProductCompanyCost(GameContext context, int companyId)
        {
            var risks = Markets.GetCompanyRisk(context, companyId);

            return GetProductCompanyBaseCost(context, companyId) * (100 - risks) / 100;
        }

        public static long GetProductCompanyBaseCost(GameContext context, int companyId)
        {
            var c = Companies.GetCompany(context, companyId);

            long audienceCost = GetClientBaseCost(context, companyId);
            long profitCost = GetCompanyIncome(c, context) * GetCompanyCostNicheMultiplier();

            return audienceCost + profitCost;
        }

        public static long GetClientBaseCost(GameContext context, int companyId)
        {
            return 0;
            var c = Companies.GetCompany(context, companyId);

            return MarketingUtils.GetClients(c) * 100;
        }
    }
}
