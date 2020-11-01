namespace Assets.Core
{
    public static partial class Companies
    {
        public static bool IsCompanyWillAcceptAcquisitionOffer(GameContext gameContext, GameEntity company, GameEntity buyer)
        {
            return GetOfferProgress(gameContext, company, buyer) > 75 - GetShareSize(gameContext, company, buyer.shareholder.Id);
        }

        public static bool IsShareholderWillAcceptAcquisitionOffer(AcquisitionOfferComponent ackOffer, int shareholderId, GameContext gameContext)
        {
            var company = Get(gameContext, ackOffer.CompanyId);

            var cost = Economy.CostOf(company, gameContext);

            var investor = GetInvestorById(gameContext, shareholderId);

            var container = GetInvestorOpinionAboutAcquisitionOffer(ackOffer, investor, company, gameContext);
            bool willAcceptOffer = container.Sum() >= 0; // ackOffer.Offer > cost * modifier;


            bool isBestOffer = true; // when competing with other companies
            var offers = GetAcquisitionOffersToCompany(gameContext, company);

            var baseDesireToSellCompany = GetBaseDesireToSellShares(gameContext, company, shareholderId, investor.shareholder.InvestorType);
            var wantsToSellShares = true || baseDesireToSellCompany == 1;

            return wantsToSellShares && willAcceptOffer && isBestOffer;
        }

        // sum opinions of all investors
        public static long GetOfferProgress(GameContext gameContext, GameEntity company, GameEntity buyer)
        {
            var ackOffer = GetAcquisitionOffer(gameContext, company, buyer);

            var shareholders = company.shareholders.Shareholders;

            long blocks = 0;
            long desireToSell = 0;

            foreach (var s in shareholders)
            {
                var invId = s.Key;
                var block = s.Value;

                bool willAcceptOffer = IsShareholderWillAcceptAcquisitionOffer(ackOffer.acquisitionOffer, invId, gameContext);

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
