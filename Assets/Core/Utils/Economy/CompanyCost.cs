using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Economy
    {
        public static long CostOf(GameEntity c, GameContext context)
        {
            long cost;
            if (Companies.IsProductCompany(c))
                cost = GetProductCost(context, c);
            else
                cost = GetGroupCost(c, context);

            long capital = BalanceOf(c);

            // +1 to avoid division by zero
            return cost + capital + 1;
        }

        public static long GetCompanySellingPrice(GameContext context, int companyId)
        {
            var target = Companies.Get(context, companyId);

            var desireToSell = Companies.GetDesireToSellCompany(target, context);

            return CostOf(target, context) * desireToSell;
        }


        public static long GetCompanyBaseCost(GameEntity company, GameContext context)
        {
            if (Companies.IsProductCompany(company))
                return GetProductCompanyBaseCost(context, company);

            return CostOf(company, context);
        }


        public static long GetCompanyIncomeBasedCost(long potentialIncome)
        {
            return potentialIncome * GetCompanyCostNicheMultiplier() * 30 / C.PERIOD;
        }

        public static long GetCompanyIncomeBasedCost(GameEntity company, GameContext context)
        {
            return GetCompanyIncome(context, company) * GetCompanyCostNicheMultiplier() * 30 / C.PERIOD;
        }

        public static long GetCompanyCostNicheMultiplier()
        {
            return 15;
        }


        // Group cost
        private static long GetGroupCost(GameEntity e, GameContext context)
        {
            var holdings = Companies.GetCompanyHoldings(context, e.company.Id, true);

            return GetHoldingCost(context, holdings);
        }

        public static long GetHoldingCost(GameContext context, GameEntity company) => GetHoldingCost(context, Companies.GetCompanyHoldings(context, company.company.Id, true));
        public static long GetHoldingCost(GameContext context, List<CompanyHolding> holdings)
        {
            return holdings.Sum(h => h.control * CostOf(Companies.Get(context, h.companyId), context) / 100);
        }

        private static long GetGroupIncome(GameContext context, GameEntity e)
        {
            return Companies.GetCompanyHoldings(context, e.company.Id, true)
                .Sum(h => h.control * GetCompanyIncome(context, Companies.Get(context, h.companyId)) / 100);
        }
        //
    }
}
