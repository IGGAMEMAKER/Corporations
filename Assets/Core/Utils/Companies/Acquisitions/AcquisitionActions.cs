using UnityEngine;

namespace Assets.Core
{
    public static partial class Companies
    {
        // reject offer
        public static void RemoveAcquisitionOffer(GameContext gameContext, GameEntity company, GameEntity buyer) => RemoveAcquisitionOffer(GetAcquisitionOffer(gameContext, company, buyer));
        public static void RemoveAcquisitionOffer(GameEntity offer)
        {
            offer.Destroy();
        }


        // send offer
        public static void SendAcquisitionOffer(GameContext gameContext, GameEntity company, GameEntity buyer, long bid)
        {
            var offer = GetAcquisitionOffer(gameContext, company, buyer);

            Debug.Log("SendAcquisitionOffer");
            offer.acquisitionOffer.BuyerOffer.Price = bid;
            offer.acquisitionOffer.BuyerOffer.ByCash = bid;

            SendAcquisitionOffer(gameContext, company, buyer);

            NotifyAboutInterest(gameContext, company, buyer);
        }

        public static void SendAcquisitionOffer(GameContext gameContext, GameEntity company, GameEntity buyer)
        {
            var offer = GetAcquisitionOffer(gameContext, company, buyer);

            var o = offer.acquisitionOffer;

            offer.ReplaceAcquisitionOffer(company.company.Id, buyer.shareholder.Id, AcquisitionTurn.Seller, o.BuyerOffer, o.SellerOffer);
        }




        public static void TweakAcquisitionConditions(GameContext gameContext, GameEntity company, GameEntity buyer, AcquisitionConditions newConditions)
        {
            var off = GetAcquisitionOffer(gameContext, company, buyer);

            off.ReplaceAcquisitionOffer(
                company.company.Id,
                buyer.shareholder.Id,
                off.acquisitionOffer.Turn,
                newConditions,
                off.acquisitionOffer.SellerOffer
                );
        }
    }
}
