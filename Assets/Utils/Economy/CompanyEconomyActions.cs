namespace Assets.Core
{
    public static partial class Economy
    {
        public static void IncreaseCompanyBalance(GameContext context, int companyId, long sum)
        {
            var c = Companies.Get(context, companyId);

            Companies.AddResources(c, sum);
        }

        public static void DecreaseInvestmentFunds(GameContext context, int investorId, long sum)
        {
            var investor = Companies.GetInvestorById(context, investorId);

            Companies.SpendResources(investor, sum);
        }
    }
}
