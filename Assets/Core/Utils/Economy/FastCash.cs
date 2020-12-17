namespace Assets.Core
{
    public static partial class Economy
    {
        //// TODO move to raise investments
        //public static bool IsCanTakeFastCash(GameContext gameContext, GameEntity company) => !IsHasCashOverflow(gameContext, company);

        public static bool IsHasCashOverflow(GameContext gameContext, GameEntity company)
        {
            var valuation = CostOf(company, gameContext);

            var balance = Economy.BalanceOf(company);
            var maxCash = valuation * 7 / 100;

            return balance > maxCash;
        }

        //// TODO move to raise investments
        //public static long GetFastCashAmount(GameContext gameContext, GameEntity company)
        //{
        //    int fraction = C.FAST_CASH_COMPANY_SHARE;

        //    return CostOf(company, gameContext) * fraction / 100;
        //}

        //// TODO move to raise investments
        //public static void RaiseFastCash(GameContext gameContext, GameEntity company, bool CeoNeedsToCommitToo = false)
        //{
        //    if (IsHasCashOverflow(gameContext, company))
        //        return;

        //    var offer = GetFastCashAmount(gameContext, company);

        //    var companyCost = Economy.CostOf(company, gameContext);

        //    var shares = Companies.GetTotalShares(company);
        //    var futureTotalShares = shares * 10 / 9;

        //    var futureShares = (futureTotalShares - shares) / company.shareholders.Shareholders.Count - 1;

        //    for (var i = 0; i < company.shareholders.Shareholders.Count; i++)
        //    {
        //        var investorId = company.shareholders.Shareholders.Keys.ToArray()[i];
        //        var inv = Companies.GetInvestorById(gameContext, investorId);

        //        bool isCeo = inv.shareholder.InvestorType == InvestorType.Founder;

        //        if (isCeo && !CeoNeedsToCommitToo)
        //            continue;

        //        var proposal = new InvestmentProposal
        //        {
        //            Investment = new Investment(offer, 0, InvestorBonus.None, new InvestmentGoal()),
        //            AdditionalShares = futureShares,

        //            ShareholderId = investorId,
        //            WasAccepted = false
        //        };

        //        Companies.AddInvestmentProposal(company, proposal);
        //        Companies.AcceptInvestmentProposal(gameContext, company, inv);
        //    }
        //}
    }
}
