using UnityEngine;

namespace Assets.Core
{
    public static partial class Companies
    {
        public static void NotifyAboutInterest(GameContext gameContext, GameEntity company, GameEntity buyer)
        {
            //if (IsInPlayerSphereOfInterest(company, gameContext))
            //    NotificationUtils.AddPopup(gameContext, new PopupMessageInterestToCompany(companyId, buyerInvestorId));
        }

        public static void NotifyAboutAcquisition(GameContext gameContext, GameEntity buyer, GameEntity target, long bid)
        {
            NotificationUtils.AddNotification(gameContext, new NotificationMessageBuyingCompany(target.company.Id, buyer.shareholder.Id, bid));

            if (IsInPlayerSphereOfInterest(target, gameContext))
                NotificationUtils.AddPopup(gameContext, new PopupMessageAcquisitionOfCompanyInOurSphereOfInfluence(target.company.Id, buyer.shareholder.Id, bid));


            Debug.LogFormat("ACQUISITION: {0} bought {1} for {2}!",
                GetInvestorName(buyer),
                target.company.Name,
                Format.Money(bid));
        }

        public static void NotifyAboutCorporateAcquisition(GameContext gameContext, GameEntity corporation, int targetCompanyId)
        {
            NotificationUtils.AddNotification(gameContext, new NotificationMessageBuyingCompany(targetCompanyId, corporation.shareholder.Id, 0));

            var company = Get(gameContext, targetCompanyId);

            if (IsInPlayerSphereOfInterest(company, gameContext))
                NotificationUtils.AddPopup(gameContext, new PopupMessageAcquisitionOfCompanyInOurSphereOfInfluence(targetCompanyId, corporation.shareholder.Id, 0));


            Debug.LogFormat("CORPORATE ACQUISITION: {0} integrated {1}!",
                GetInvestorName(corporation),
                company.company.Name);
        }
    }
}
