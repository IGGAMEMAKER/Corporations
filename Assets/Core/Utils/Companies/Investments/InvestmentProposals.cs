using System;
using System.Collections.Generic;
using Entitas;

namespace Assets.Core
{
    partial class Companies
    {
        public static List<InvestmentProposal> GetInvestmentProposals(GameEntity company)
        {
            return company.investmentProposals.Proposals;
        }

        public static InvestmentProposal GetInvestmentProposal(GameEntity company, int investorId)
        {
            return GetInvestmentProposals(company).Find(p => p.ShareholderId == investorId);
        }

        public static int GetInvestmentProposalIndex(GameEntity company, int investorId)
        {
            return GetInvestmentProposals(company).FindIndex(p => p.ShareholderId == investorId);
        }

        public static GameEntity[] GetPotentialInvestors(GameContext gameContext, GameEntity c)
        {
            var investors = gameContext.GetEntities(GameMatcher.Shareholder);

            return Array.FindAll(investors, s => Investments.IsInvestorSuitable(s, c));
        }




        public static void AddInvestmentProposal(GameContext gameContext, GameEntity company, InvestmentProposal proposal)
        {
            int companyId = company.company.Id;

            var proposals = company.investmentProposals.Proposals;

            // TODO REFACTOR
            var curr = GetInvestmentProposal(company, proposal.ShareholderId);

            if (curr == null)
            {
                proposals.Add(proposal);
            }
            else
            {
                var index = GetInvestmentProposalIndex(company, proposal.ShareholderId);
                proposals[index] = proposal;
            }

            company.ReplaceInvestmentProposals(proposals);
        }

        // to remove
        public static void RejectProposal(GameContext gameContext, GameEntity company, int investorId)
        {
            RemoveProposal(gameContext, company, investorId);
        }

        public static void RemoveProposal(GameContext gameContext, GameEntity company, int investorId)
        {
            var proposals = GetInvestmentProposals(company);

            proposals.RemoveAll(p => p.ShareholderId == investorId);

            company.ReplaceInvestmentProposals(proposals);
        }
    }
}
