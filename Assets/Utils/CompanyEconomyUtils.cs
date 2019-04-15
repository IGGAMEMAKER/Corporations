namespace Assets.Utils
{
    public static class CompanyEconomyUtils
    {
        public static long GetIncome(GameEntity e, GameContext context)
        {
            if (e.company.CompanyType == CompanyType.ProductCompany)
                return ProductEconomicsUtils.GetIncome(e);

            return 1000000;
        }

        public static long GetCompanyCost(int companyId)
        {
            return 1000000000;
        }

        internal static long GetMaintenance(GameEntity c, GameContext gameContext)
        {
            return ProductEconomicsUtils.GetMaintenance(c);
        }

        internal static long GetBalanceChange(GameEntity c, GameContext context)
        {
            return ProductEconomicsUtils.GetBalance(c);
        }

        internal static bool IsROICounable(GameEntity c, GameContext context)
        {
            return GetMaintenance(c, context) > 0;
        }

        internal static long GetBalanceROI(GameEntity c, GameContext context)
        {
            long maintenance = GetMaintenance(c, context);
            long change = GetBalanceChange(c, context);

            return change * 100 / maintenance;
        }

        public static GameEntity[] GetDaughterCompanies(GameEntity e, GameContext context)
        {
            return new GameEntity[] { e };
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
    }
}
