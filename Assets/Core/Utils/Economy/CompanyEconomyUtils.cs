using System.Linq;

namespace Assets.Core
{
    public static partial class Economy
    {
        public static long BalanceOf(GameEntity company) => company.companyResource.Resources.money;

        public static long GetCompanyIncome(GameContext context, GameEntity e)
        {
            if (Companies.IsProductCompany(e))
                return GetProductCompanyIncome(e);

            return GetGroupIncome(context, e);
        }

        // TODO move to raise investments
        public static bool IsCanTakeFastCash(GameContext gameContext, GameEntity company) => !IsHasCashOverflow(gameContext, company);

        public static bool IsHasCashOverflow(GameContext gameContext, GameEntity company)
        {
            var valuation = CostOf(company, gameContext);

            var balance = Economy.BalanceOf(company);
            var maxCash = valuation * 7 / 100;

            return balance > maxCash;
        }

        public static long GetProfit(GameContext context, GameEntity c)
        {
            return GetCompanyIncome(context, c) - GetCompanyMaintenance(context, c);
        }

        private static long GetGroupIncome(GameContext context, GameEntity e)
        {
            return Investments.GetHoldings(context, e, true)
                .Sum(h => h.control * GetCompanyIncome(context, Companies.Get(context, h.companyId)) / 100);
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


        // TODO move to raise investments
        public static long GetFastCashAmount(GameContext gameContext, GameEntity company)
        {
            int fraction = C.FAST_CASH_COMPANY_SHARE;

            return CostOf(company, gameContext) * fraction / 100;
        }
        
        // TODO move to raise investments
        public static void RaiseFastCash(GameContext gameContext, GameEntity company, bool CeoNeedsToCommitToo = false)
        {
            if (IsHasCashOverflow(gameContext, company))
                return;

            var offer = GetFastCashAmount(gameContext, company);
            var proposal = new InvestmentProposal
            {
                Investment = new Investment(offer, 0, InvestorBonus.None, InvestorGoal.GrowCompanyCost),

                WasAccepted = false
            };

            for (var i = 0; i < company.shareholders.Shareholders.Count; i++)
            {
                var investorId = company.shareholders.Shareholders.Keys.ToArray()[i];
                var inv = Companies.GetInvestorById(gameContext, investorId);

                bool isCeo = inv.shareholder.InvestorType == InvestorType.Founder;

                if (isCeo && !CeoNeedsToCommitToo)
                    continue;

                proposal.ShareholderId = investorId;

                Companies.AddInvestmentProposal(company, proposal);
                Companies.AcceptInvestmentProposal(gameContext, company, investorId);
            }
        }
    }
}
