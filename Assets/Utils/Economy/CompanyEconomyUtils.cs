namespace Assets.Utils
{
    public static partial class EconomyUtils
    {
        public static long GetCompanyIncome(GameEntity e, GameContext context)
        {
            if (CompanyUtils.IsProductCompany(e))
                return GetProductCompanyIncome(e, context);

            return GetGroupIncome(context, e);
        }

        public static long GetCompanyIncome(int companyId, GameContext context)
        {
            var e = CompanyUtils.GetCompanyById(context, companyId);

            return GetCompanyIncome(e, context);
        }

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
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return GetCompanyCost(context, c);
        }

        public static long GetCompanySellingPrice(GameContext context, int companyId)
        {
            var target = CompanyUtils.GetCompanyById(context, companyId);

            var desireToSell = CompanyUtils.GetDesireToSellCompany(target, context);

            return GetCompanyCost(context, companyId) * desireToSell;
        }

        public static long GetCompanyCostNicheMultiplier()
        {
            return 15;
        }

        public static long GetCompanyBaseCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            if (CompanyUtils.IsProductCompany(c))
                return GetProductCompanyBaseCost(context, companyId);

            return GetCompanyCost(context, companyId);
        }

        public static long GetCompanyIncomeBasedCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return GetCompanyIncome(c, context) * GetCompanyCostNicheMultiplier();
        }



        internal static long GetBalanceChange(GameEntity c, GameContext context)
        {
            return GetCompanyIncome(c, context) - GetCompanyMaintenance(c, context);
        }

        internal static long GetBalanceChange(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return GetBalanceChange(c, context);
        }

        internal static bool IsProfitable(GameContext gameContext, int companyId)
        {
            return GetBalanceChange(gameContext, companyId) > 0;
        }

        internal static long GetBalanceROI(GameEntity c, GameContext context)
        {
            long maintenance = GetCompanyMaintenance(c, context);
            long change = GetBalanceChange(c, context);

            return change * 100 / maintenance;
        }

        public static bool IsCompanyNeedsMoreMoneyOnMarket(GameContext gameContext, GameEntity product)
        {
            var isMaxDevelopment = TeamUtils.IsUpgradePicked(product, TeamUpgrade.Multiplatform);
            var isProfitable = IsProfitable(gameContext, product.company.Id);

            return isMaxDevelopment && isProfitable;
        }
    }
}
