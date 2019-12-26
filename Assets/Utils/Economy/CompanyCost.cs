namespace Assets.Core
{
    public static partial class Economy
    {
        public static long GetCompanyCost(GameContext context, int companyId) => GetCompanyCost(context, Companies.GetCompany(context, companyId));
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

            long capital = Companies.BalanceOf(c);

            // +1 to avoid division by zero
            return cost + capital + 1;
        }


        public static long GetCompanySellingPrice(GameContext context, int companyId)
        {
            var target = Companies.GetCompany(context, companyId);

            var desireToSell = Companies.GetDesireToSellCompany(target, context);

            return GetCompanyCost(context, companyId) * desireToSell;
        }

        public static long GetCompanyCostNicheMultiplier()
        {
            return 15;
        }

        public static long GetCompanyBaseCost(GameContext context, int companyId) => GetCompanyBaseCost(context, Companies.GetCompany(context, companyId));
        public static long GetCompanyBaseCost(GameContext context, GameEntity company)
        {
            if (Companies.IsProductCompany(company))
                return GetProductCompanyBaseCost(context, company);

            return GetCompanyCost(context, company.company.Id);
        }

        public static long GetCompanyIncomeBasedCost(GameContext context, int companyId)
        {
            var c = Companies.GetCompany(context, companyId);

            return GetCompanyIncome(c, context) * GetCompanyCostNicheMultiplier();
        }
    }
}
