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

        static long GetHoldingIncome(GameContext context, List<CompanyHolding> holdings)
        {
            return holdings.Sum(h => h.control * GetCompanyIncome(h.companyId, context) / 100);
        }

        static long GetHoldingMaintenance(GameContext context, List<CompanyHolding> holdings)
        {
            return holdings.Sum(h => h.control * GetCompanyMaintenance(context, h.companyId) / 100);
        }

        public static long GetHoldingCost(GameContext context, int companyId) => GetHoldingCost(context, Companies.GetCompanyHoldings(context, companyId, true));
        public static long GetHoldingCost(GameContext context, List<CompanyHolding> holdings)
        {
            return holdings.Sum(h => h.control * GetCompanyCost(context, h.companyId) / 100);
        }



        private static long GetGroupIncome(GameContext context, GameEntity e)
        {
            var holdings = Companies.GetCompanyHoldings(context, e.company.Id, true);

            return GetHoldingIncome(context, holdings);
        }
    }
}
