using Entitas;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        // reject offer
        public static void RejectAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            RemoveAcquisitionOffer(gameContext, companyId, buyerInvestorId);
        }

        public static void RemoveAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            offer.Destroy();
        }


        // send offer
        public static void SendAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId, long bid)
        {
            var offer = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            Debug.Log("SendAcquisitionOffer");
            offer.acquisitionOffer.BuyerOffer.Price = bid;
            offer.acquisitionOffer.BuyerOffer.ByCash = bid;

            SendAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            NotifyAboutInterest(gameContext, companyId, buyerInvestorId);
        }

        public static void SendAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            var o = offer.acquisitionOffer;

            offer.ReplaceAcquisitionOffer(companyId, buyerInvestorId,
                o.RemainingTries - 1, o.RemainingDays, AcquisitionTurn.Seller, o.BuyerOffer, o.SellerOffer);
        }




        public static void TweakAcquisitionConditions(GameContext gameContext, int companyId, int buyerInvestorId, AcquisitionConditions newConditions)
        {
            var off = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            off.ReplaceAcquisitionOffer(
                companyId,
                buyerInvestorId,
                off.acquisitionOffer.RemainingTries,
                off.acquisitionOffer.RemainingDays,
                off.acquisitionOffer.Turn,
                newConditions,
                off.acquisitionOffer.SellerOffer
                );
        }
    }
}
