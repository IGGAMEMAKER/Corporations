using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Economy
    {
        public static long CostOf(GameEntity c, GameContext context)
        {
            long capital = BalanceOf(c);
            long cost = NonCapitalCostOf(c, context);

            return cost + capital;
        }
        public static long NonCapitalCostOf(GameEntity c, GameContext context)
        {
            // +1 to avoid division by zero
            if (c.hasProduct)
                return GetProductCost(c) + 1;

            return GetGroupCost(c, context) + 1;
        }

        // Group cost
        public static long GetGroupCost(GameEntity e, GameContext context)
        {
            return GetHoldingsCost(context, e);
        }

        public static long GetHoldingsCost(GameContext context, GameEntity entity) => GetHoldingsCost(context, Investments.GetHoldings(context, entity, true));
        public static long GetHoldingsCost(GameContext context, List<CompanyHolding> holdings)
        {
            return holdings.Sum(h => h.control * CostOf(h.company, context)) / 100;
        }

        public static long GetCompanySellingPrice(GameContext context, GameEntity target)
        {
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
    }
}
