﻿namespace Assets.Core
{
    partial class Companies
    {
        public static long GetNewSharesSize(GameContext gameContext, GameEntity company, long offer)
        {
            // calculating new shares size
            long cost = Economy.CostOf(company, gameContext);

            var allShares = (long)GetTotalShares(company);

            long shares = allShares * offer / cost;

            return shares;
        }

        public static void AcceptInvestmentProposal(GameContext gameContext, GameEntity company, GameEntity investor)
        {
            var investorId = investor.shareholder.Id;
            var p = GetInvestmentProposal(company, investorId);


            long shares = p.AdditionalShares; // allShares * p.Investment.Offer / cost;

            // update shareholders list
            AddShares(company, investor, (int)shares);

            var date = ScheduleUtils.GetCurrentDate(gameContext);

            if (Economy.WillPayInvestmentRightNow(p.Investment, date))
            {
                var portion = p.Investment.Portion;
                AddResources(company, portion, "accept investments");
                Pay(investor, portion, "spend on investments");
            }

            MarkProposalAsAccepted(company, investorId);
        }

        static void MarkProposalAsAccepted(GameEntity company, int investorId)
        {
            var proposals = GetInvestmentProposals(company);

            var index = proposals.FindIndex(p => p.ShareholderId == investorId);

            proposals[index].WasAccepted = true;

            var investments = company.shareholders.Shareholders[investorId].Investments;

            if (investments == null)
                investments = new System.Collections.Generic.List<Investment>();

            investments.Add(proposals[index].Investment);

            company.ReplaceInvestmentProposals(proposals);
        }

        public static void AcceptAllInvestmentProposals(GameEntity company, GameContext gameContext)
        {
            foreach (var s in GetInvestmentProposals(company))
            {
                var investorShareholderId = s.ShareholderId;

                AcceptInvestmentProposal(gameContext, company, GetInvestorById(gameContext, investorShareholderId));
            }
        }
    }
}
