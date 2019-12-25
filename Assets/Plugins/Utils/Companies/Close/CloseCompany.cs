using Assets.Classes;
using System.Collections.Generic;

namespace Assets.Utils
{
    partial class Companies
    {
        public static GameEntity CloseCompany(GameContext context, int CompanyId) => CloseCompany(context, GetCompany(context, CompanyId));
        public static GameEntity CloseCompany(GameContext context, GameEntity e)
        {
            // pay to everyone
            PayDividends(context, e, e.companyResource.Resources.money);

            // fire everyone

            NotifyAboutProductSupportEnd(e, context);

            if (e.hasProduct)
            {
                TeamUtils.DismissTeam(e, context);
                Markets.ReturnUsersWhenCompanyIsClosed(e, context);
            }

            RemoveAllPartnerships(e, context);

            foreach (var holding in GetDaughterCompanies(context, e.company.Id))
                DestroyBlockOfShares(context, holding, e.shareholder.Id);

            e.ReplaceShareholders(new Dictionary<int, BlockOfShares>());


            e.isAlive = false;

            return e;
        }



        public static void NotifyAboutProductSupportEnd(GameEntity company, GameContext gameContext)
        {
            NotificationUtils.AddNotification(gameContext, new NotificationMessageBankruptcy(company.company.Id));

            if (IsInPlayerSphereOfInterest(company, gameContext))
                NotificationUtils.AddPopup(gameContext, new PopupMessageCompanyBankrupt(company.company.Id));
        }
    }
}
