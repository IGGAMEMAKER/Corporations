using Entitas;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static void RejectAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            RemoveAcquisitionOffer(gameContext, companyId, buyerInvestorId);
        }

        public static void RemoveAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            offer.Destroy();
        }

        public static void SendAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            var o = offer.acquisitionOffer;

            offer.ReplaceAcquisitionOffer(companyId, buyerInvestorId,
                o.RemainingTries - 1, o.RemainingDays, AcquisitionTurn.Seller, o.BuyerOffer, o.SellerOffer);
        }
        public static void SendAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId, long bid)
        {
            var offer = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            Debug.Log("SendAcquisitionOffer");
            offer.acquisitionOffer.BuyerOffer.Price = bid;
            offer.acquisitionOffer.BuyerOffer.ByCash = bid;

            SendAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            NotifyAboutInterest(gameContext, companyId, buyerInvestorId);
        }

        public static void NotifyAboutInterest(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var company = GetCompanyById(gameContext, companyId);

            if (IsInPlayerSphereOfInterest(company, gameContext))
                NotificationUtils.AddPopup(gameContext, new PopupMessageInterestToCompany(companyId, buyerInvestorId));
        }

        public static GameEntity CreateAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = gameContext.CreateEntity();

            var cost = EconomyUtils.GetCompanyCost(gameContext, companyId);

            var buyerOffer = new AcquisitionConditions
            {
                Price = cost,
                ByCash = cost,
                ByShares = 0,
                KeepLeaderAsCEO = true
            };

            var sellerOffer = new AcquisitionConditions
            {
                Price = cost * 4,
                ByCash = cost * 4,
                ByShares = 0,
                KeepLeaderAsCEO = true
            };

            offer.AddAcquisitionOffer(companyId, buyerInvestorId, 3, 60, AcquisitionTurn.Seller, buyerOffer, sellerOffer);

            return offer;
        }

        public static GameEntity GetAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = gameContext.GetEntities(GameMatcher.AcquisitionOffer)
                .FirstOrDefault(e => e.acquisitionOffer.CompanyId == companyId && e.acquisitionOffer.BuyerId == buyerInvestorId);

            if (offer == null)
                offer = CreateAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            return offer;
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
