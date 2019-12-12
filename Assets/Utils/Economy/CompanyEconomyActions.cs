namespace Assets.Utils
{
    public static partial class Economy
    {
        public static void IncreaseCompanyBalance(GameContext context, int companyId, long sum)
        {
            var c = Companies.GetCompany(context, companyId);

            Companies.AddResources(c, new Classes.TeamResource(sum));
        }

        public static void DecreaseInvestmentFunds(GameContext context, int investorId, long sum)
        {
            var investor = Companies.GetInvestorById(context, investorId);

            Companies.SpendResources(investor, new Classes.TeamResource(sum));
        }
    }
}
