using System.Collections.Generic;

namespace Assets.Utils
{
    partial class CompanyEconomyUtils
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
            var c = CompanyUtils.GetCompanyById(context, companyId);

            var holdings = CompanyUtils.GetCompanyHoldings(context, companyId, true);

            return GetHoldingCost(context, holdings);
        }

        static long GetHoldingCost(GameContext context, List<CompanyHolding> holdings)
        {
            long sum = 0;

            foreach (var h in holdings)
                sum += h.control * GetCompanyCost(context, h.companyId) / 100;

            return sum;
        }

        private static long GetGroupMaintenance(GameContext gameContext, int companyId)
        {
            var holdings = CompanyUtils.GetCompanyHoldings(gameContext, companyId, true);

            return GetHoldingMaintenance(gameContext, holdings) + GetTeamMaintenance(gameContext, companyId);
        }

        private static long GetGroupIncome(GameContext context, GameEntity e)
        {
            var holdings = CompanyUtils.GetCompanyHoldings(context, e.company.Id, true);

            return GetHoldingIncome(context, holdings);
        }

        private static long GetGroupCost(GameContext context, int companyId)
        {
            var holdings = CompanyUtils.GetCompanyHoldings(context, companyId, true);

            return GetHoldingCost(context, holdings);
        }


        private static string GetGroupIncomeDescription(GameContext context, int companyId)
        {
            string description = "Group income:\n";

            var holdings = CompanyUtils.GetCompanyHoldings(context, companyId, false);

            foreach (var h in holdings)
            {
                var c = CompanyUtils.GetCompanyById(context, h.companyId);

                string name = c.company.Name;
                long income = GetCompanyIncome(c, context);
                string tiedIncome = ValueFormatter.Shorten(h.control * income / 100);

                description += $"\n  {name}: +${tiedIncome} ({h.control}%)";
            }

            return description;
        }

        private static string GetGroupMaintenanceDescription(GameContext context, int companyId)
        {
            string description = "Group maintenance:\n";

            var holdings = CompanyUtils.GetCompanyHoldings(context, companyId, false);

            foreach (var h in holdings)
            {
                var c = CompanyUtils.GetCompanyById(context, h.companyId);

                string name = c.company.Name;
                long income = GetCompanyMaintenance(c, context);
                string tiedIncome = ValueFormatter.Shorten(h.control * income / 100);

                description += $"\n  {name}: -${tiedIncome} ({h.control}%)";
            }

            return description;
        }

    }
}
