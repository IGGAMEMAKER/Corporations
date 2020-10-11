namespace Assets.Core
{
    public static partial class Economy
    {
        public static void IncreaseCompanyBalance(GameEntity c, long sum)
        {
            Companies.AddResources(c, sum);
        }

        public static void DecreaseInvestmentFunds(GameEntity investor, long sum)
        {
            Companies.SpendResources(investor, sum);
        }
    }
}
