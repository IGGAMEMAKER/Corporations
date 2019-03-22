namespace Assets.Utils
{
    public static class ProductEconomicsUtils
    {
        public static float GetBasePrice(GameEntity e)
        {
            return e.finance.basePrice;
        }

        public static float GetProductPrice(GameEntity e)
        {
            if (e.finance.price <= 0) return 0;

            float price = 10 + (e.finance.price - 1);

            return GetBasePrice(e) * price / 10;
        }

        public static long GetIncome(GameEntity e)
        {
            float income = e.marketing.Clients * GetProductPrice(e);

            return System.Convert.ToInt64(income);
        }

        internal static long GetBalance(GameEntity e)
        {
            return GetIncome(e) - GetTeamMaintenance(e);
        }

        public static long GetTeamMaintenance(GameEntity e)
        {
            return (e.team.Managers + e.team.Marketers + e.team.Programmers) * 2000;
        }
    }
}
