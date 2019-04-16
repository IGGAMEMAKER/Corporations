namespace Assets.Utils
{
    public static partial class CompanyEconomyUtils
    {
        public static long GetCompanyIncome(GameEntity e, GameContext context)
        {
            if (CompanyUtils.IsProductCompany(e))
                return ProductEconomicsUtils.GetIncome(e);

            return GetGroupIncome(context, e);
        }

        public static long GetCompanyCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            if (CompanyUtils.IsCompanyGroupLike(c))
                return GetGroupOfCompaniesCost(context, c);

            return GetProductCompanyCost(context, companyId);
        }

        public static long GetCompanyCostEnthusiasm()
        {
            return 15;
        }

        public static long GetCompanyIncomeBasedCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return GetCompanyIncome(c, context) * GetCompanyCostEnthusiasm();
        }

        internal static long GetCompanyMaintenance(GameEntity c, GameContext gameContext)
        {
            return ProductEconomicsUtils.GetMaintenance(c);
        }

        internal static long GetBalanceChange(GameEntity c, GameContext context)
        {
            return ProductEconomicsUtils.GetBalance(c);
        }

        internal static bool IsROICounable(GameEntity c, GameContext context)
        {
            return GetCompanyMaintenance(c, context) > 0;
        }

        internal static long GetBalanceROI(GameEntity c, GameContext context)
        {
            long maintenance = GetCompanyMaintenance(c, context);
            long change = GetBalanceChange(c, context);

            return change * 100 / maintenance;
        }

        public static void IncreaseCompanyBalance(GameContext context, int companyId, long sum)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            long balance = c.companyResource.Resources.money + sum;

            c.ReplaceCompanyResource(c.companyResource.Resources.SetMoney(balance));
        }

        public static void RestructureFinances(GameContext context, int percent, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            var balance = c.companyResource.Resources.money;
            var investments = c.shareholder.Money;

            var total = balance + investments;

            investments = total * percent / 100;
            balance = total - investments;

            c.ReplaceCompanyResource(c.companyResource.Resources.SetMoney(balance));
            c.ReplaceShareholder(c.shareholder.Id, c.shareholder.Name, investments);
        }

        public static int GetCompanyRating(int companyId)
        {
            return UnityEngine.Random.Range(1, 6);
        }
    }
}
