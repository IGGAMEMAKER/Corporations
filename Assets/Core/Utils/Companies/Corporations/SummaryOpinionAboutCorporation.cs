﻿namespace Assets.Core
{
    public static partial class Companies
    {
        public static bool IsShareholderWillAcceptCorporationOffer(GameEntity company, int shareholderId, GameContext gameContext)
        {
            // costs
            var targetCost = Economy.CostOf(company, gameContext);

            var corporation = Investments.GetCompanyByInvestorId(gameContext, shareholderId);
            var corporationCost = Economy.CostOf(corporation, gameContext);

            // desire to sell
            var baseDesireToSellCompany = GetBaseDesireToSellShares(gameContext, company.company.Id, shareholderId);
            var wantsToSellShares = true || baseDesireToSellCompany == 1;

            var isSmallComparedToCorporation = targetCost * 100 < 15 * corporationCost;

            return wantsToSellShares && isSmallComparedToCorporation;
        }

        //public static long GetSummaryInvestorOpinion(GameContext gameContext, int companyId, int buyerInvestorId, Func<GameEntity, int, int, bool> WillAccept)
        //{
        //    var company = GetCompanyById(gameContext, companyId);

        //    var shareholders = GetShareholders(company);

        //    long blocks = 0;
        //    long desireToSell = 0;

        //    foreach (var s in shareholders)
        //    {
        //        var invId = s.Key;
        //        var block = s.Value;

        //        bool willAcceptOffer = WillAccept();

        //        if (willAcceptOffer)
        //            desireToSell += block.amount;

        //        blocks += block.amount;
        //    }

        //    if (blocks == 0)
        //        return 0;

        //    return desireToSell * 100 / blocks;
        //}

        // sum opinions of all investors
        public static long GetCorporationOfferProgress(GameContext gameContext, GameEntity company, int buyerInvestorId)
        {
            //return GetSummaryInvestorOpinion(gameContext, companyId, buyerInvestorId, IsShareholderWillAcceptCorporationOffer(companyId, invId, gameContext));
            var shareholders = GetShareholders(company);

            long blocks = 0;
            long desireToSell = 0;

            foreach (var s in shareholders)
            {
                var invId = s.Key;
                var block = s.Value;

                bool willAcceptOffer = IsShareholderWillAcceptCorporationOffer(company, invId, gameContext);

                if (willAcceptOffer)
                    desireToSell += block.amount;

                blocks += block.amount;
            }

            if (blocks == 0)
                return 0;

            return desireToSell * 100 / blocks;
        }
    }
}
