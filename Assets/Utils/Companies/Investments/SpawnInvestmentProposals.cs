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

            var potentialInvestors = GetPotentialInvestors(context, companyId);
            var investorsCount = potentialInvestors.Length;

            foreach (var potentialInvestor in potentialInvestors)
            {
                var modifier = (50 + UnityEngine.Random.Range(0, 100));

                long valuation = cost * modifier / 100;
                var ShareholderId = potentialInvestor.shareholder.Id;

                long offer = valuation / 10;
                var max = GetMaxInvestingAmountForInvestorType(potentialInvestor);

                if (offer > max)
                    offer = max;

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

        public static int GetMaxInvestingAmountForInvestorType(GameEntity investor)
        {
            switch (investor.shareholder.InvestorType)
            {
                case InvestorType.FFF: return 10000;

                case InvestorType.Angel: return 150000;

                case InvestorType.VentureInvestor: return 1000000;

                default: return 0;
            }
        }
    }
}
