namespace Assets.Utils
{
    public static partial class Companies
    {
        public static bool IsCompanyWillAcceptAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            return GetOfferProgress(gameContext, companyId, buyerInvestorId) > 75 - GetShareSize(gameContext, companyId, buyerInvestorId);
        }

        public static bool IsShareholderWillAcceptAcquisitionOffer(AcquisitionOfferComponent ackOffer, int shareholderId, GameContext gameContext)
        {
            var cost = Economy.GetCompanyCost(gameContext, ackOffer.CompanyId);

            var company = GetCompany(gameContext, ackOffer.CompanyId);
            var investor = GetInvestorById(gameContext, shareholderId);

            var container = GetInvestorOpinionAboutAcquisitionOffer(ackOffer, investor, company, gameContext);
            bool willAcceptOffer = container.Sum() >= 0; // ackOffer.Offer > cost * modifier;


            bool isBestOffer = true; // when competing with other companies
            var offers = GetAcquisitionOffersToCompany(gameContext, ackOffer.CompanyId);

            var baseDesireToSellCompany = GetBaseDesireToSellShares(gameContext, company, shareholderId, investor.shareholder.InvestorType);
            var wantsToSellShares = true || baseDesireToSellCompany == 1;

            return wantsToSellShares && willAcceptOffer && isBestOffer;
        }

        // sum opinions of all investors
        public static long GetOfferProgress(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var ackOffer = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            var company = GetCompany(gameContext, companyId);

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
