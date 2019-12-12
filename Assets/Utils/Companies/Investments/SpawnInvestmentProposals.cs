using System;
using System.Collections.Generic;
using Entitas;

namespace Assets.Utils
{
    partial class Companies
    {
        public static void SpawnProposals(GameContext context, int companyId)
        {
            long cost = Economy.GetCompanyCost(context, companyId);
            var c = GetCompany(context, companyId);

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
    }
}
