using System;
using System.Collections.Generic;
using Entitas;

namespace Assets.Core
{
    partial class Companies
    {
        public static void SpawnProposals(GameContext context, int companyId)
        {
            long cost = Economy.GetCompanyCost(context, companyId);
            var c = Get(context, companyId);

            var potentialInvestors = GetPotentialInvestors(context, companyId);
            var investorsCount = potentialInvestors.Length;

            foreach (var potentialInvestor in potentialInvestors)
            {
                var modifier = (50 + UnityEngine.Random.Range(0, 100));

                long valuation = cost * modifier / 100;

                var max = GetMaxInvestingAmountForInvestorType(potentialInvestor);

                var ShareholderId = potentialInvestor.shareholder.Id;
                var p = new InvestmentProposal
                {
                    Valuation = valuation,
                    Offer = Math.Min(valuation / 20, max),

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

        public static long GetMaxInvestingAmountForInvestorType(GameEntity investor)
        {
            switch (investor.shareholder.InvestorType)
            {
                case InvestorType.FFF: return 10000;

                case InvestorType.Angel: return 150000;

                case InvestorType.VentureInvestor: return 1000000;

                case InvestorType.StockExchange: return 50000000;

                default: return 0;
            }
        }
    }
}
