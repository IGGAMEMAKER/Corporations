using UnityEngine;

namespace Assets.Core
{
    public static partial class Companies
    {
        // reject offer
        public static void RejectAcquisitionOffer(GameContext gameContext, GameEntity company, int buyerInvestorId)
        {
            RemoveAcquisitionOffer(gameContext, company, buyerInvestorId);
        }

        public static void RemoveAcquisitionOffer(GameContext gameContext, GameEntity company, int buyerInvestorId)
        {
            var offer = GetAcquisitionOffer(gameContext, company, buyerInvestorId);

            offer.Destroy();
        }


        // send offer
        public static void SendAcquisitionOffer(GameContext gameContext, GameEntity company, int buyerInvestorId, long bid)
        {
            var offer = GetAcquisitionOffer(gameContext, company, buyerInvestorId);

            Debug.Log("SendAcquisitionOffer");
            offer.acquisitionOffer.BuyerOffer.Price = bid;
            offer.acquisitionOffer.BuyerOffer.ByCash = bid;

            SendAcquisitionOffer(gameContext, company, buyerInvestorId);

            NotifyAboutInterest(gameContext, company, buyerInvestorId);
        }

        public static void SendAcquisitionOffer(GameContext gameContext, GameEntity company, int buyerInvestorId)
        {
            var offer = GetAcquisitionOffer(gameContext, company, buyerInvestorId);

            var o = offer.acquisitionOffer;

            offer.ReplaceAcquisitionOffer(company.company.Id, buyerInvestorId, AcquisitionTurn.Seller, o.BuyerOffer, o.SellerOffer);
        }




        public static void TweakAcquisitionConditions(GameContext gameContext, GameEntity company, int buyerInvestorId, AcquisitionConditions newConditions)
        {
            var off = GetAcquisitionOffer(gameContext, company, buyerInvestorId);

            off.ReplaceAcquisitionOffer(
                company.company.Id,
                buyerInvestorId,
                off.acquisitionOffer.Turn,
                newConditions,
                off.acquisitionOffer.SellerOffer
                );
        }
    }
}
