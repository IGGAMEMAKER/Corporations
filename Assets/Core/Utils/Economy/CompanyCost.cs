namespace Assets.Core
{
    public static partial class Economy
    {
        public static long GetCompanyCost(GameContext context, int companyId) => GetCompanyCost(context, Companies.Get(context, companyId));
        public static long GetCompanyCost(GameContext context, GameEntity c)
        {
            return GetFullCompanyCost(context, c);
            //return GetCompanyBaseCost(context, c.company.Id);
        }

        public static long GetFullCompanyCost(GameContext context, GameEntity c)
        {
            long cost;
            if (Companies.IsProductCompany(c))
                cost = GetProductCompanyCost(context, c.company.Id);
            else
                cost = GetGroupOfCompaniesCost(context, c);

            long capital = BalanceOf(c);

            // +1 to avoid division by zero
            return cost + capital + 1;
        }


        public static long GetCompanySellingPrice(GameContext context, int companyId)
        {
            var target = Companies.Get(context, companyId);

            var desireToSell = Companies.GetDesireToSellCompany(target, context);

            return GetCompanyCost(context, companyId) * desireToSell;
        }


        public static long GetCompanyBaseCost(GameContext context, int companyId) => GetCompanyBaseCost(context, Companies.Get(context, companyId));
        public static long GetCompanyBaseCost(GameContext context, GameEntity company)
        {
            if (Companies.IsProductCompany(company))
                return GetProductCompanyBaseCost(context, company);

            return GetCompanyCost(context, company.company.Id);
        }

        public static long GetCompanyIncomeBasedCost(GameContext context, int companyId)
        {
            var c = Companies.Get(context, companyId);

            return GetCompanyIncome(context, c) * GetCompanyCostNicheMultiplier() * 30 / C.PERIOD;
        }

        public static long GetCompanyCostNicheMultiplier()
        {
            return 15;
        }
    }
}
