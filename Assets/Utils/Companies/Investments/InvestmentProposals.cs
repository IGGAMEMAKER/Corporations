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
            return GetInvestmentProposals(context, companyId).Find(p => p.ShareholderId == investorId);
        }

        public static int GetInvestmentProposalIndex(GameContext context, int companyId, int investorId)
        {
            return GetInvestmentProposals(context, companyId).FindIndex(p => p.ShareholderId == investorId);
        }

        internal static void AddInvestmentProposal(GameContext gameContext, int companyId, InvestmentProposal proposal)
        {
            var c = GetCompanyById(gameContext, companyId);

            var proposals = c.investmentProposals.Proposals;

            // TODO REFACTOR
            var curr = GetInvestmentProposal(gameContext, companyId, proposal.ShareholderId);

            if (curr == null)
            {
                proposals.Add(proposal);
            }
            else
            {
                var index = GetInvestmentProposalIndex(gameContext, companyId, proposal.ShareholderId);
                proposals[index] = proposal;
            }

            c.ReplaceInvestmentProposals(proposals);
        }

        internal static void AcceptProposal(GameContext gameContext, int companyId, int investorId)
        {
            var p = GetInvestmentProposal(gameContext, companyId, investorId);

            if (p == null)
                return;

            long cost = CompanyEconomyUtils.GetCompanyCost(gameContext, companyId);

            int shares = Convert.ToInt32(GetTotalShares(gameContext, companyId) * p.Offer / cost);

            AddShareholder(gameContext, companyId, investorId, shares);

            CompanyEconomyUtils.IncreaseCompanyBalance(gameContext, companyId, p.Offer);
            CompanyEconomyUtils.DecreaseInvestmentFunds(gameContext, investorId, p.Offer);

            MarkProposalAsAccepted(gameContext, companyId, investorId);
        }

        static void MarkProposalAsAccepted(GameContext gameContext, int companyId, int investorId)
        {
            var c = GetCompanyById(gameContext, companyId);

            var proposals = GetInvestmentProposals(gameContext, companyId);

            var index = proposals.FindIndex(p => p.ShareholderId == investorId);

            proposals[index].WasAccepted = true;

            c.ReplaceInvestmentProposals(proposals);
        }


        public static void SpawnProposals(GameContext context, int companyId)
        {
            long cost = CompanyEconomyUtils.GetCompanyCost(context, companyId);

            foreach (var potentialInvestor in GetPotentialInvestors(context, companyId))
            {
                long valuation = cost * (50 + UnityEngine.Random.Range(0, 100)) / 100;

                var p = new InvestmentProposal {
                    Valuation = valuation,
                    Offer = valuation / 10,
                    ShareholderId = potentialInvestor.shareholder.Id,
                    InvestorBonus = InvestorBonus.None,
                    WasAccepted = false
                };

                AddInvestmentProposal(context, companyId, p);
            }
        }


        // to remove
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
    }
}
