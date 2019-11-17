using System;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static bool IsCompanyWillAcceptCorporationOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            return GetCorporationOfferProgress(gameContext, companyId, buyerInvestorId) > 75 - GetShareSize(gameContext, companyId, buyerInvestorId);
        }

        public static bool IsShareholderWillAcceptCorporationOffer(int companyId, int shareholderId, GameContext gameContext)
        {
            var cost = EconomyUtils.GetCompanyCost(gameContext, companyId);

            var baseDesireToSellCompany = GetBaseDesireToSellShares(gameContext, companyId, shareholderId);
            var wantsToSellShares = true || baseDesireToSellCompany == 1;


            var corporationCost = EconomyUtils.GetCompanyCost(gameContext, companyId);
            var isSmallComparedToCorporation = cost * 100 < 15 * corporationCost;

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
        public static long GetCorporationOfferProgress(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            //return GetSummaryInvestorOpinion(gameContext, companyId, buyerInvestorId, IsShareholderWillAcceptCorporationOffer(companyId, invId, gameContext));
            var company = GetCompanyById(gameContext, companyId);

            var shareholders = GetShareholders(company);

            long blocks = 0;
            long desireToSell = 0;

            foreach (var s in shareholders)
            {
                var invId = s.Key;
                var block = s.Value;

                bool willAcceptOffer = IsShareholderWillAcceptCorporationOffer(companyId, invId, gameContext);

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
