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
