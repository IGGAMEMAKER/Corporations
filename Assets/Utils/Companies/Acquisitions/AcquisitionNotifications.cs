using Entitas;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static void NotifyAboutInterest(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var company = GetCompanyById(gameContext, companyId);

            if (IsInPlayerSphereOfInterest(company, gameContext))
                NotificationUtils.AddPopup(gameContext, new PopupMessageInterestToCompany(companyId, buyerInvestorId));
        }

        public static void NotifyAboutAcquisition(GameContext gameContext, int buyerShareholderId, int targetCompanyId, long bid)
        {
            NotificationUtils.AddNotification(gameContext, new NotificationMessageBuyingCompany(targetCompanyId, buyerShareholderId, bid));

            var company = GetCompanyById(gameContext, targetCompanyId);

            if (IsInPlayerSphereOfInterest(company, gameContext))
                NotificationUtils.AddPopup(gameContext, new PopupMessageAcquisitionOfCompanyInOurSphereOfInfluence(targetCompanyId, buyerShareholderId, bid));

            Debug.LogFormat("ACQUISITION: {0} bought {1} for insane {2}!",
                GetInvestorName(gameContext, buyerShareholderId),
                GetCompanyById(gameContext, targetCompanyId).company.Name,
                Format.Money(bid));
        }
    }
}
