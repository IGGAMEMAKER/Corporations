namespace Assets.Utils
{
    public static partial class EconomyUtils
    {
        public static long GetCompanyCost(GameContext context, GameEntity c)
        {
            long cost;
            if (CompanyUtils.IsProductCompany(c))
                cost = GetProductCompanyCost(context, c.company.Id);
            else
                cost = GetGroupOfCompaniesCost(context, c);

            long capital = c.companyResource.Resources.money;

            // +1 to avoid division by zero
            return cost + capital + 1;
        }

        public static long GetCompanyCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompany(context, companyId);

            return GetCompanyCost(context, c);
        }

        public static long GetCompanySellingPrice(GameContext context, int companyId)
        {
            var target = CompanyUtils.GetCompany(context, companyId);

            var desireToSell = CompanyUtils.GetDesireToSellCompany(target, context);

            return GetCompanyCost(context, companyId) * desireToSell;
        }

        public static long GetCompanyCostNicheMultiplier()
        {
            return 15;
        }

        public static long GetCompanyBaseCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompany(context, companyId);

            if (CompanyUtils.IsProductCompany(c))
                return GetProductCompanyBaseCost(context, companyId);

            return GetCompanyCost(context, companyId);
        }

        public static long GetCompanyIncomeBasedCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompany(context, companyId);

            return GetCompanyIncome(c, context) * GetCompanyCostNicheMultiplier();
        }
    }
}
