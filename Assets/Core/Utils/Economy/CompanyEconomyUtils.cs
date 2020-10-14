using System.Linq;

namespace Assets.Core
{
    public static partial class Economy
    {
        public static long BalanceOf(GameEntity company) => company.companyResource.Resources.money;

        public static long GetIncome(GameContext context, GameEntity e)
        {
            if (e.hasProduct)
                return GetProductIncome(e);
            
            return GetGroupIncome(context, e);
        }

        private static long GetGroupIncome(GameContext context, GameEntity e)
        {
            return Investments.GetHoldings(context, e, true)
                .Sum(h => h.control * GetIncome(context, h.company) / 100);
        }

        private static long GetGroupMaintenance(GameContext context, GameEntity company)
        {
            return Investments.GetHoldings(context, company, true)
                .Sum(h => h.control * GetMaintenance(context, h.company) / 100);
        }

        public static long GetMaintenance(GameContext context, GameEntity c)
        {
            if (c.hasProduct)
                return GetProductMaintenance(c, context);
            else
                return GetGroupMaintenance(context, c);
        }

        public static long GetProfit(GameContext context, GameEntity c)
        {
            return GetProfit(context, c, true).Sum();
        }
        public static Bonus<long> GetProfit(GameContext context, GameEntity c, bool isBonus)
        {
            var bonus = new Bonus<long>("Profit");

            if (c.hasProduct)
            {
                // ** income **
                // * base income
                // * investments

                // ** expenses **
                // * teams
                // * managers
                // * marketing
                // * servers

                // income
                bonus.Append("Product income", GetProductIncome(c));

                // expenses
                var maintenance = GetProductCompanyMaintenance(c, context, true);
                foreach (var m in maintenance.bonusDescriptions)
                {
                    if (m.HideIfZero)
                        bonus.AppendAndHideIfZero(m.Name, -m.Value);
                    else
                        bonus.Append(m.Name, -m.Value);
                }
            }

            // investments
            ApplyInvestmentsToProfitBonus(c, context, bonus);

            // group
            if (Companies.IsGroup(c))
            {
                var holdings = Investments.GetHoldings(context, c, true);

                bool isOnlyHolding = holdings.Count == 1;

                foreach (var h in holdings)
                {
                    var b = GetProfit(context, h.company, true);

                    if (isOnlyHolding)
                    {
                        // render full description
                        foreach (var d in b.bonusDescriptions)
                        {
                            if (d.HideIfZero)
                            {
                                bonus.AppendAndHideIfZero(d.Name, d.Value);
                            }
                            else
                            {
                                bonus.Append(d.Name, d.Value);
                            }
                        }
                    }
                    else
                    {
                        // general info is enough
                        bonus.Append(h.company.company.Name, b.Sum());
                    }
                }

                //bonus.Append("Group Income", GetGroupIncome(context, c));
                //bonus.Append("Maintenance", -GetGroupMaintenance(context, c));
            }

            return bonus;
        }

        public static void ApplyInvestmentsToProfitBonus(GameEntity c, GameContext context, Bonus<long> bonus)
        {
            //var investmentTaker = c.isFlagship ? Companies.GetManagingCompanyOf(c, context) : c;
            var investmentTaker = c;

            if (investmentTaker.shareholders.Shareholders.Count > 1)
            {
                var investments = investmentTaker.shareholders.Shareholders.Values
                    .Select(v => v.Investments.Where(z => z.RemainingPeriods > 0).Select(z => z.Portion).Sum())
                    .Sum();

                bonus.AppendAndHideIfZero("Investments", investments);
            }
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
