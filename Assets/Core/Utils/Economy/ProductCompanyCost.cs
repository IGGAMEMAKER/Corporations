namespace Assets.Core
{
    partial class Economy
    {
        private static long GetProductCompanyCost(GameContext context, int companyId)
        {
            var risks = Markets.GetCompanyRisk(context, companyId);

            return GetProductCompanyBaseCost(context, companyId) * (100 - risks) / 100;
        }

        public static long GetProductCompanyBaseCost(GameContext context, int companyId) => GetProductCompanyBaseCost(context, Companies.Get(context, companyId));
        public static long GetProductCompanyBaseCost(GameContext context, GameEntity company)
        {
            long audienceCost = GetClientBaseCost(context, company.company.Id);
            long profitCost = GetCompanyIncomeBasedCost(context, company.company.Id);

            return audienceCost + profitCost;
        }

        public static long GetClientBaseCost(GameContext context, int companyId)
        {
            return 0;
            //var c = Companies.Get(context, companyId);

            //return Marketing.GetClients(c) * 100;
        }
    }
}
