using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static void ConfirmAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            BuyCompany(gameContext, companyId, buyerInvestorId, offer.acquisitionOffer.SellerOffer.Price);
        }

        public static void BuyCompany(GameContext gameContext, int companyId, int buyerInvestorId, long offer)
        {
            // can afford acquisition
            var inv = InvestmentUtils.GetInvestorById(gameContext, buyerInvestorId);
            if (!inv.companyResource.Resources.IsEnoughResources(new Classes.TeamResource(offer)))
                return;

            var c = GetCompanyById(gameContext, companyId);

            var shareholders = c.shareholders.Shareholders;
            int[] array = new int[shareholders.Keys.Count];
            shareholders.Keys.CopyTo(array, 0);

            foreach (var shareholderId in array)
                BuyShares(gameContext, companyId, buyerInvestorId, shareholderId, shareholders[shareholderId].amount, offer, true);



            RemoveAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            c.isIndependentCompany = false;

            NotifyAboutAcquisition(gameContext, buyerInvestorId, companyId, offer);
        }

        public static void NotifyAboutAcquisition(GameContext gameContext, int buyerShareholderId, int targetCompanyId, long bid)
        {
            NotificationUtils.AddNotification(gameContext, new NotificationMessageBuyingCompany(targetCompanyId, buyerShareholderId, bid));

            Debug.LogFormat("ACQUISITION: {0} bought {1} for insane {2}!",
                GetInvestorName(gameContext, buyerShareholderId),
                GetCompanyById(gameContext, targetCompanyId).company.Name,
                Format.Money(bid));
        }
    }
}
