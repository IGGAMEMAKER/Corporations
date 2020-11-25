using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static void ConfirmAcquisitionOffer(GameContext gameContext, GameEntity company, GameEntity buyer)
        {
            try
            {
                var offer = GetAcquisitionOffer(gameContext, company, buyer);

                BuyCompany(gameContext, company, buyer, offer.acquisitionOffer.BuyerOffer.Price);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error in ConfirOffer");
            }
        }

        public static void BuyCompany(GameContext gameContext, GameEntity company, GameEntity buyer, long offer) //  int buyerInvestorId
        {
            // can afford acquisition
            if (!IsEnoughResources(buyer, offer))
                return;

            var shareholders = GetShareholders(company);
            int[] array = new int[shareholders.Keys.Count];
            shareholders.Keys.CopyTo(array, 0);



            foreach (var sellerInvestorId in array)
            {
                BuyShares(gameContext, company, buyer.shareholder.Id, sellerInvestorId, shareholders[sellerInvestorId].amount, offer, true);
            }


            RemoveAllPartnerships(company, gameContext);

            RemoveAcquisitionOffer(gameContext, company, buyer);

            SetIndependence(company, false);

            if (company.hasProduct)
                NotifyAboutAcquisition(gameContext, buyer, company, offer);

            ScheduleUtils.TweakCampaignStats(gameContext, CampaignStat.Acquisitions);

            if (IsGroup(company))
            {
                var daughters = GetDaughters(gameContext, company);

                // transfer all products to buyer
                foreach (var d in daughters)
                {
                    AttachToGroup(gameContext, buyer, d);
                    //TransferCompany(gameContext, d, company);
                }
            }

            if (IsGroup(company))
            {
                // and close group
                NotificationUtils.AddSimplePopup(gameContext, Visuals.Positive("You've bought GROUP company " + company.company.Name), "The group will be destroyed\nAll their products will be in our direct control");

                CloseCompany(gameContext, company);
            }
        }
    }
}
