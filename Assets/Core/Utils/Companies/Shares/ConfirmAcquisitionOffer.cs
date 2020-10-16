using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static void ConfirmAcquisitionOffer(GameContext gameContext, GameEntity company, int buyerInvestorId)
        {
            var offer = GetAcquisitionOffer(gameContext, company, buyerInvestorId);

            BuyCompany(gameContext, company, buyerInvestorId, offer.acquisitionOffer.SellerOffer.Price);
        }

        

        public static void BuyCompany(GameContext gameContext, GameEntity company, int buyerInvestorId, long offer)
        {
            // can afford acquisition
            var inv = Investments.GetInvestor(gameContext, buyerInvestorId);
            if (!IsEnoughResources(inv, offer))
                return;

            var shareholders = GetShareholders(company);
            int[] array = new int[shareholders.Keys.Count];
            shareholders.Keys.CopyTo(array, 0);

            foreach (var shareholderId in array)
                BuyShares(gameContext, company, buyerInvestorId, shareholderId, shareholders[shareholderId].amount, offer, true);

            RemoveAllPartnerships(company, gameContext);

            RemoveAcquisitionOffer(gameContext, company, buyerInvestorId);

            SetIndependence(company, false);

            NotifyAboutAcquisition(gameContext, buyerInvestorId, company.company.Id, offer);

            ScheduleUtils.TweakCampaignStats(gameContext, CampaignStat.Acquisitions);
        }
    }
}
