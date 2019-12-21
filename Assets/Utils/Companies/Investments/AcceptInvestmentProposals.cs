using System;

namespace Assets.Utils
{
    partial class Companies
    {
        internal static void AcceptProposal(GameContext gameContext, int companyId, int investorId)
        {
            var p = GetInvestmentProposal(gameContext, companyId, investorId);

            if (p == null)
                return;

            long cost = Economy.GetCompanyCost(gameContext, companyId);

            var allShares = (long)GetTotalShares(gameContext, companyId);
            long shares = allShares * p.Offer / cost;



            AddShareholder(gameContext, companyId, investorId, (int)shares);

            Economy.IncreaseCompanyBalance(gameContext, companyId, p.Offer);
            Economy.DecreaseInvestmentFunds(gameContext, investorId, p.Offer);

            MarkProposalAsAccepted(gameContext, companyId, investorId);
        }

        static void MarkProposalAsAccepted(GameContext gameContext, int companyId, int investorId)
        {
            var c = GetCompany(gameContext, companyId);

            var proposals = GetInvestmentProposals(gameContext, companyId);

            var index = proposals.FindIndex(p => p.ShareholderId == investorId);

            proposals[index].WasAccepted = true;

            c.ReplaceInvestmentProposals(proposals);
        }
    }
}
