using System;
using System.Collections.Generic;
using Entitas;

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

        public static GameEntity[] GetPotentialInvestorsWhoAreReadyToInvest(GameContext gameContext, int companyId)
        {
            var investors = gameContext.GetEntities(GameMatcher.Shareholder);

            var c = GetCompanyById(gameContext, companyId);

            return Array.FindAll(investors,
                s => InvestmentUtils.IsInvestorSuitable(s, c) && InvestmentUtils.GetInvestorOpinion(gameContext, c, s) > 0
                );
        }


        public static GameEntity[] GetPotentialInvestors(GameContext gameContext, int companyId)
        {
            var investors = gameContext.GetEntities(GameMatcher.Shareholder);

            var c = GetCompanyById(gameContext, companyId);

            return Array.FindAll(investors, s => InvestmentUtils.IsInvestorSuitable(s, c));
        }

        public static void SpawnProposals(GameContext context, int companyId)
        {
            long cost = EconomyUtils.GetCompanyCost(context, companyId);
            var c = GetCompanyById(context, companyId);

            foreach (var potentialInvestor in GetPotentialInvestors(context, companyId))
            {
                long valuation = cost * (50 + UnityEngine.Random.Range(0, 100)) / 100;
                var ShareholderId = potentialInvestor.shareholder.Id;

                var p = new InvestmentProposal
                {
                    Valuation = valuation,
                    Offer = valuation / 10,
                    ShareholderId = ShareholderId,
                    InvestorBonus = InvestorBonus.None,
                    WasAccepted = false
                };

                // you cannot invest in yourself!
                if (c.hasShareholder && c.shareholder.Id == ShareholderId)
                    continue;

                AddInvestmentProposal(context, companyId, p);
            }
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
