namespace Assets.Utils
{
    public static partial class Economy
    {
        public static long GetCompanyBalance(GameEntity company) => company.companyResource.Resources.money;

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



        internal static long GetProfit(GameEntity c, GameContext context)
        {
            return GetCompanyIncome(c, context) - GetCompanyMaintenance(c, context);
        }

        internal static long GetProfit(GameContext context, int companyId)
        {
            var c = Companies.GetCompany(context, companyId);

            return GetProfit(c, context);
        }

        public static bool IsCanMaintain(GameEntity company, GameContext gameContext, long money)
        {
            return GetProfit(company, gameContext) >= money;
        }


        internal static bool IsProfitable(GameContext gameContext, int companyId)
        {
            return GetProfit(gameContext, companyId) > 0;
        }




        public static bool IsCompanyNeedsMoreMoneyOnMarket(GameContext gameContext, GameEntity product)
        {
            return !IsProfitable(gameContext, product.company.Id);
        }
    }
}
