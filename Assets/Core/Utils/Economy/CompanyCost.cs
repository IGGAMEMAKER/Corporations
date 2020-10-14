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
                cost = GetProductCost(c);
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


        public static long GetCompanyIncomeBasedCost(long potentialIncome)
        {
            return potentialIncome * GetCompanyCostNicheMultiplier() * 30 / C.PERIOD;
        }

        public static long GetCompanyCostNicheMultiplier()
        {
            return 15;
        }


        // Group cost
        public static long GetGroupCost(GameEntity e, GameContext context)
        {
            var holdings = Companies.GetHoldings(context, e, true);

            return GetHoldingCost(context, holdings);
        }

        public static long GetHoldingCost(GameContext context, List<CompanyHolding> holdings)
        {
            return holdings.Sum(h => h.control * CostOf(Companies.Get(context, h.companyId), context) / 100);
        }

        private static long GetGroupIncome(GameContext context, GameEntity e)
        {
            return Companies.GetHoldings(context, e, true)
                .Sum(h => h.control * GetCompanyIncome(context, Companies.Get(context, h.companyId)) / 100);
        }
        //
    }
}
