namespace Assets.Core
{
    public static partial class Economy
    {
        public static long BalanceOf(GameEntity company) => company.companyResource.Resources.money;

        public static long GetCompanyIncome(int companyId, GameContext context)
        {
            var e = Companies.GetCompany(context, companyId);

            return GetCompanyIncome(e, context);
        }

        public static long GetCompanyIncome(GameEntity e, GameContext context)
        {
            if (Companies.IsProductCompany(e))
                return GetProductCompanyIncome(e, context);

            return GetGroupIncome(context, e);
        }



        internal static long GetProfit(GameContext context, int companyId) => GetProfit(context, Companies.GetCompany(context, companyId));
        internal static long GetProfit(GameContext context, GameEntity c)
        {
            return GetCompanyIncome(c, context) - GetCompanyMaintenance(context, c);
        }


        public static bool IsCanMaintain(GameEntity company, GameContext gameContext, long money)
        {
            return GetProfit(gameContext, company) >= money;
        }


        internal static bool IsProfitable(GameContext gameContext, int companyId) => IsProfitable(gameContext, Companies.GetCompany(gameContext, companyId));
        internal static bool IsProfitable(GameContext gameContext, GameEntity company)
        {
            return GetProfit(gameContext, company) > 0;
        }




        public static bool IsCompanyNeedsMoreMoneyOnMarket(GameContext gameContext, GameEntity product)
        {
            return !IsProfitable(gameContext, product.company.Id);
        }
    }
}
