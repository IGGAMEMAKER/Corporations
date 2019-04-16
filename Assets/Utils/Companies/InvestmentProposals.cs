using System;
using System.Collections.Generic;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static List<InvestmentProposal> GetInvestmentProposals(GameContext context, int companyId)
        {
            return GetCompanyById(context, companyId).investmentProposals.Proposals;
        }

        public static InvestmentProposal GetInvestmentProposal(GameContext context, int companyId, int investorId)
        {
            var c = GetCompanyById(context, companyId);

            return GetInvestmentProposals(context, companyId).Find(p => p.ShareholderId == investorId);
        }

        internal static void AddInvestmentProposal(GameContext gameContext, int companyId, InvestmentProposal proposal)
        {
            var c = GetCompanyById(gameContext, companyId);

            var p = c.investmentProposals.Proposals;

            p.Add(proposal);

            c.ReplaceInvestmentProposals(p);
        }


        internal static void RejectProposal(GameContext gameContext, int companyId, int investorId)
        {
            RemoveProposal(gameContext, companyId, investorId);
        }

        static void RemoveProposal(GameContext gameContext, int companyId, int investorId)
        {
            var c = GetCompanyById(gameContext, companyId);

            var proposals = GetInvestmentProposals(gameContext, companyId);

            proposals.RemoveAll(p => p.ShareholderId == investorId);

            c.ReplaceInvestmentProposals(proposals);
        }

        internal static void AcceptProposal(GameContext gameContext, int companyId, int investorId)
        {
            var p = GetInvestmentProposal(gameContext, companyId, investorId);

            long cost = GetCompanyCost(gameContext, companyId);

            int shares = Convert.ToInt32(GetTotalShares(gameContext, companyId) * p.Offer / cost);

            AddShareholder(gameContext, companyId, investorId, shares);

            RemoveProposal(gameContext, companyId, investorId);
        }
    }
}
