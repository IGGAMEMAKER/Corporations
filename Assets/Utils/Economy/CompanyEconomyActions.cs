using System;

namespace Assets.Utils
{
    public static partial class EconomyUtils
    {
        public static long GetCompanyBalance(GameEntity company)
        {
            return company.companyResource.Resources.money;
        }

        public static void IncreaseCompanyBalance(GameContext context, int companyId, long sum)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            CompanyUtils.AddResources(c, new Classes.TeamResource(sum));
        }

        public static void DecreaseInvestmentFunds(GameContext context, int investorId, long sum)
        {
            var investor = CompanyUtils.GetInvestorById(context, investorId);

            CompanyUtils.SpendResources(investor, new Classes.TeamResource(sum));
        }
    }
}
