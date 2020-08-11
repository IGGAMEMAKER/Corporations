namespace Assets.Core
{
    partial class Companies
    {
        public static void AcceptInvestmentProposal(GameContext gameContext, int companyId, int investorId)
        {
            var p = GetInvestmentProposal(gameContext, companyId, investorId);

            //if (p == null)
            //    return;

            long cost = Economy.GetCompanyCost(gameContext, companyId);

            var allShares = (long)GetTotalShares(gameContext, companyId);
            long shares = allShares * p.Investment.Offer / cost;

            var portion = p.Investment.Portion;

            AddShareholder(gameContext, companyId, investorId, (int)shares);

            Economy.IncreaseCompanyBalance(gameContext, companyId, portion);
            Economy.DecreaseInvestmentFunds(gameContext, investorId, portion);

            MarkProposalAsAccepted(gameContext, companyId, investorId);
        }

        static void MarkProposalAsAccepted(GameContext gameContext, int companyId, int investorId)
        {
            var c = Get(gameContext, companyId);

            var proposals = GetInvestmentProposals(gameContext, companyId);

            var index = proposals.FindIndex(p => p.ShareholderId == investorId);

            proposals[index].WasAccepted = true;

            var investments = c.shareholders.Shareholders[investorId].Investments;

            if (investments == null)
                investments = new System.Collections.Generic.List<Investment>();

            investments.Add(proposals[index].Investment);

            c.ReplaceInvestmentProposals(proposals);
        }
    }
}
