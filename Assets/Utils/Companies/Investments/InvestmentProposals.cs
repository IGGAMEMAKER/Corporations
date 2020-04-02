using System;
using System.Collections.Generic;
using Entitas;

namespace Assets.Core
{
    partial class Companies
    {
        public static List<InvestmentProposal> GetInvestmentProposals(GameContext context, int companyId)
        {
            return Get(context, companyId).investmentProposals.Proposals;
        }

        public static InvestmentProposal GetInvestmentProposal(GameContext context, int companyId, int investorId)
        {
            return GetInvestmentProposals(context, companyId).Find(p => p.ShareholderId == investorId);
        }

        public static int GetInvestmentProposalIndex(GameContext context, int companyId, int investorId)
        {
            return GetInvestmentProposals(context, companyId).FindIndex(p => p.ShareholderId == investorId);
        }

        public static GameEntity[] GetPotentialInvestors(GameContext gameContext, int companyId)
        {
            var investors = gameContext.GetEntities(GameMatcher.Shareholder);

            var c = Get(gameContext, companyId);

            return Array.FindAll(investors, s => Investments.IsInvestorSuitable(s, c));
        }




        public static void AddInvestmentProposal(GameContext gameContext, int companyId, InvestmentProposal proposal)
        {
            var c = Get(gameContext, companyId);

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

        // to remove
        public static void RejectProposal(GameContext gameContext, int companyId, int investorId)
        {
            RemoveProposal(gameContext, companyId, investorId);
        }

        public static void RemoveProposal(GameContext gameContext, int companyId, int investorId)
        {
            var c = Get(gameContext, companyId);

            var proposals = GetInvestmentProposals(gameContext, companyId);

            proposals.RemoveAll(p => p.ShareholderId == investorId);

            c.ReplaceInvestmentProposals(proposals);
        }
    }
}
