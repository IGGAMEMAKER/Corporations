using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class Companies
    {
        public static void ConfirmAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            BuyCompany(gameContext, companyId, buyerInvestorId, offer.acquisitionOffer.SellerOffer.Price);
        }

        public static void BuyCompany(GameContext gameContext, int companyId, int buyerInvestorId, long offer)
        {
            // can afford acquisition
            var inv = Investments.GetInvestorById(gameContext, buyerInvestorId);
            if (!IsEnoughResources(inv, offer))
                return;

            var target = GetCompany(gameContext, companyId);

            var shareholders = GetShareholders(target);
            int[] array = new int[shareholders.Keys.Count];
            shareholders.Keys.CopyTo(array, 0);

            foreach (var shareholderId in array)
                BuyShares(gameContext, companyId, buyerInvestorId, shareholderId, shareholders[shareholderId].amount, offer, true);



            RemoveAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            target.isIndependentCompany = false;

            NotifyAboutAcquisition(gameContext, buyerInvestorId, companyId, offer);
        }
    }
}
