namespace Assets.Core
{
    public static partial class Economy
    {
        public static long BalanceOf(GameEntity company) => company.companyResource.Resources.money;

        public static long GetCompanyIncome(GameContext context, int companyId) => GetCompanyIncome(context, Companies.Get(context, companyId));
        public static long GetCompanyIncome(GameContext context, GameEntity e)
        {
            if (Companies.IsProductCompany(e))
                return GetProductCompanyIncome(e, context);

            return GetGroupIncome(context, e);
        }



        public static long GetProfit(GameContext context, int companyId) => GetProfit(context, Companies.Get(context, companyId));
        public static long GetProfit(GameContext context, GameEntity c)
        {
            return GetCompanyIncome(context, c) - GetCompanyMaintenance(context, c);
        }


        public static bool IsCanMaintain(GameEntity company, GameContext gameContext, long money)
        {
            return GetProfit(gameContext, company) >= money;
        }


        public static bool IsProfitable(GameContext gameContext, int companyId) => IsProfitable(gameContext, Companies.Get(gameContext, companyId));
        public static bool IsProfitable(GameContext gameContext, GameEntity company)
        {
            return GetProfit(gameContext, company) > 0;
        }




        public static bool IsCompanyNeedsMoreMoneyOnMarket(GameContext gameContext, GameEntity product)
        {
            return !IsProfitable(gameContext, product.company.Id);
        }
    }
}
