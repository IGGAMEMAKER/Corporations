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

        public static int GetInvestmentProposalIndex(GameContext context, int companyId, int investorId)
        {
            var c = GetCompanyById(context, companyId);

            return GetInvestmentProposals(context, companyId).FindIndex(p => p.ShareholderId == investorId);
        }

        internal static void AddInvestmentProposal(GameContext gameContext, int companyId, InvestmentProposal proposal)
        {
            var c = GetCompanyById(gameContext, companyId);

            var proposals = c.investmentProposals.Proposals;

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

            long cost = CompanyEconomyUtils.GetCompanyCost(gameContext, companyId);

            int shares = Convert.ToInt32(GetTotalShares(gameContext, companyId) * p.Offer / cost);

            AddShareholder(gameContext, companyId, investorId, shares);

            CompanyEconomyUtils.IncreaseCompanyBalance(gameContext, companyId, p.Offer);
            CompanyEconomyUtils.DecreaseInvestmentFunds(gameContext, investorId, p.Offer);

            RemoveProposal(gameContext, companyId, investorId);
        }

        public static int GetInvestmentAttractiveness(GameContext context, int companyId)
        {
            return UnityEngine.Random.Range(1, 5);
        }

        public static void SpawnProposals(GameContext context, int companyId)
        {
            int attractiveness = GetInvestmentAttractiveness(context, companyId);

            int amount = attractiveness;

            long cost = CompanyEconomyUtils.GetCompanyCost(context, companyId);

            for (var i = 0; i < amount; i++)
            {
                long valuation = cost * (50 + UnityEngine.Random.Range(0, 100)) / 100;

                var p = new InvestmentProposal {
                    Valuation = valuation,
                    Offer = valuation / 10,
                    ShareholderId = GetRandomInvestmentFund(context)
                };

                AddInvestmentProposal(context, companyId, p);
            }
        }
    }
}
