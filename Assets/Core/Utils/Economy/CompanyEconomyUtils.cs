using System.Linq;

namespace Assets.Core
{
    public static partial class Economy
    {
        public static long BalanceOf(GameEntity company) => company.companyResource.Resources.money;

        public static long GetCompanyIncome(GameContext context, GameEntity e)
        {
            if (Companies.IsProductCompany(e))
                return GetIncomeFromProduct(e);

            return GetGroupIncome(context, e);
        }

        private static long GetGroupIncome(GameContext context, GameEntity e)
        {
            return Investments.GetHoldings(context, e, true)
                .Sum(h => h.control * GetCompanyIncome(context, Companies.Get(context, h.companyId)) / 100);
        }

        public static long GetCompanyMaintenance(GameContext gameContext, int companyId) => GetCompanyMaintenance(gameContext, Companies.Get(gameContext, companyId));
        public static long GetCompanyMaintenance(GameContext gameContext, GameEntity c)
        {
            if (Companies.IsProductCompany(c))
                return GetProductCompanyMaintenance(c, gameContext);
            else
                return GetGroupMaintenance(gameContext, c);
        }

        private static long GetGroupMaintenance(GameContext gameContext, GameEntity company)
        {
            var holdings = Investments.GetHoldings(gameContext, company, true);

            return holdings
                .Sum(h => h.control * GetCompanyMaintenance(gameContext, h.companyId) / 100);
        }

        public static Bonus<long> GetProfit(GameContext context, GameEntity c, bool isBonus)
        {
            var bonus = new Bonus<long>("Profit");

            if (c.hasProduct)
            {
                // income
                bonus.Append("Product", GetIncomeFromProduct(c));

                // expenses
                var maintenance = GetProductCompanyMaintenance(c, context, true);
                foreach (var m in maintenance.bonusDescriptions)
                {
                    if (m.HideIfZero)
                        bonus.AppendAndHideIfZero(m.Name, -m.Value);
                    else
                        bonus.Append(m.Name, -m.Value);
                }

                // investments
                var parent = Companies.GetManagingCompanyOf(c, context);

                if (parent.shareholders.Shareholders.Count > 1)
                {
                    var investments = parent.shareholders.Shareholders.Values
                        .Select(v => v.Investments.Where(z => z.RemainingPeriods > 0).Select(z => z.Portion).Sum())
                        .Sum();

                    bonus.AppendAndHideIfZero("Investments", investments);
                }

                return bonus;
            }

            // group
            bonus.Append("Group Income", GetGroupIncome(context, c));
            bonus.Append("Maintenance", -GetGroupMaintenance(context, c));

            return bonus;
        }
        public static long GetProfit(GameContext context, GameEntity c)
        {
            // PRODUCTS

            // ** income **
            // * base income
            // * investments

            // ** expenses **
            // * teams
            // * managers
            // * marketing
            // * servers

            return GetProfit(context, c, true).Sum();
        }




        public static bool IsWillBecomeBankruptOnNextPeriod(GameContext gameContext, GameEntity c)
        {
            var profit = GetProfit(gameContext, c);
            var balance = BalanceOf(c);

            return balance + profit < 0;
        }


        public static bool IsCanMaintainForAWhile(GameEntity company, GameContext gameContext, long money, int periods)
        {
            var balance = BalanceOf(company);
            var profit = GetProfit(gameContext, company);

            return balance + (profit - money) * periods >= 0;
        }
        public static bool IsCanMaintain(GameEntity company, GameContext gameContext, long money)
        {
            return GetProfit(gameContext, company) >= money;
        }


        public static bool IsProfitable(GameContext gameContext, GameEntity company)
        {
            return GetProfit(gameContext, company) > 0;
        }




        public static bool IsCompanyNeedsMoreMoneyOnMarket(GameContext gameContext, GameEntity product)
        {
            return !IsProfitable(gameContext, product);
        }
    }
}
