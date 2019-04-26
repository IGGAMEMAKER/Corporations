using System;

namespace Assets.Utils
{
    public static partial class CompanyEconomyUtils
    {
        public static void IncreaseCompanyBalance(GameContext context, int companyId, long sum)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            c.ReplaceCompanyResource(c.companyResource.Resources.AddMoney(sum));
        }

        public static void DecreaseInvestmentFunds(GameContext context, int investorId, long sum)
        {
            var investor = CompanyUtils.GetInvestorById(context, investorId);

            var resources = investor.companyResource;
            resources.Resources.Spend(new Classes.TeamResource(sum));

            investor.ReplaceCompanyResource(resources.Resources);
        }
    }
}
