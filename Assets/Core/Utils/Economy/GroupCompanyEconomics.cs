using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    partial class Economy
    {
        private static long GetGroupOfCompaniesCost(GameContext context, GameEntity e)
        {
            var holdings = Companies.GetCompanyHoldings(context, e.company.Id, true);

            return GetHoldingCost(context, holdings);
        }


        public static long GetHoldingCost(GameContext context, GameEntity company) => GetHoldingCost(context, Companies.GetCompanyHoldings(context, company.company.Id, true));
        public static long GetHoldingCost(GameContext context, List<CompanyHolding> holdings)
        {
            return holdings.Sum(h => h.control * GetCompanyCost(context, h.companyId) / 100);
        }



        private static long GetGroupIncome(GameContext context, GameEntity e)
        {
            return Companies.GetCompanyHoldings(context, e.company.Id, true)
                .Sum(h => h.control * GetCompanyIncome(context, h.companyId) / 100);
        }
    }
}
