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

            BuyCompany(gameContext, company, buyer, offer.acquisitionOffer.SellerOffer.Price);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error in ConfirOffer");
            }
        }

        

        public static void BuyCompany(GameContext gameContext, GameEntity company, GameEntity investor, long offer) //  int buyerInvestorId
        {
            // can afford acquisition
            if (!IsEnoughResources(investor, offer))
                return;

            var shareholders = GetShareholders(company);
            int[] array = new int[shareholders.Keys.Count];
            shareholders.Keys.CopyTo(array, 0);



            foreach (var sellerInvestorId in array)
                BuyShares(gameContext, company, investor.shareholder.Id, sellerInvestorId, shareholders[sellerInvestorId].amount, offer, true);

            RemoveAllPartnerships(company, gameContext);

            RemoveAcquisitionOffer(gameContext, company, investor);

            SetIndependence(company, false);

            NotifyAboutAcquisition(gameContext, investor, company, offer);

            ScheduleUtils.TweakCampaignStats(gameContext, CampaignStat.Acquisitions);
        }
    }
}
