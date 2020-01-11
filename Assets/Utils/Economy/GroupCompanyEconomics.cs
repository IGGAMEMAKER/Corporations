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
            long sum = 0;

            foreach (var h in holdings)
                sum += h.control * GetCompanyIncome(h.companyId, context) / 100;

            return sum;
        }

        static long GetHoldingMaintenance(GameContext context, List<CompanyHolding> holdings)
        {
            //return holdings.Sum(h => h.control * GetCompanyMaintenance(context, h.companyId) / 100);
            long sum = 0;

            foreach (var h in holdings)
                sum += h.control * GetCompanyMaintenance(context, h.companyId) / 100;

            return sum;
        }

        public static long GetHoldingCost(GameContext context, int companyId) => GetHoldingCost(context, Companies.GetCompanyHoldings(context, companyId, true));
        public static long GetHoldingCost(GameContext context, List<CompanyHolding> holdings)
        {
            long sum = 0;

            foreach (var h in holdings)
                sum += h.control * GetCompanyCost(context, h.companyId) / 100;

            return sum;
        }



        private static long GetGroupIncome(GameContext context, GameEntity e)
        {
            var holdings = Companies.GetCompanyHoldings(context, e.company.Id, true);

            return GetHoldingIncome(context, holdings);
        }
    }
}
