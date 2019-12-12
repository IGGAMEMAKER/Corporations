using System.Collections.Generic;

namespace Assets.Utils
{
    partial class Economy
    {
        private static long GetGroupOfCompaniesCost(GameContext context, GameEntity e)
        {
            return GetGroupCost(context, e.company.Id);
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
            long sum = 0;

            foreach (var h in holdings)
                sum += h.control * GetCompanyMaintenance(context, h.companyId) / 100;

            return sum;
        }

        public static long GetHoldingCost(GameContext context, int companyId)
        {
            var c = Companies.GetCompany(context, companyId);

            var holdings = Companies.GetCompanyHoldings(context, companyId, true);

            return GetHoldingCost(context, holdings);
        }

        public static long GetHoldingCost(GameContext context, List<CompanyHolding> holdings)
        {
            long sum = 0;

            foreach (var h in holdings)
                sum += h.control * GetCompanyCost(context, h.companyId) / 100;

            return sum;
        }

        private static long GetGroupMaintenance(GameContext gameContext, int companyId)
        {
            var holdings = Companies.GetCompanyHoldings(gameContext, companyId, true);

            return GetHoldingMaintenance(gameContext, holdings);
        }

        private static long GetGroupIncome(GameContext context, GameEntity e)
        {
            var holdings = Companies.GetCompanyHoldings(context, e.company.Id, true);

            return GetHoldingIncome(context, holdings);
        }

        private static long GetGroupCost(GameContext context, int companyId)
        {
            var holdings = Companies.GetCompanyHoldings(context, companyId, true);

            return GetHoldingCost(context, holdings);
        }
    }
}
