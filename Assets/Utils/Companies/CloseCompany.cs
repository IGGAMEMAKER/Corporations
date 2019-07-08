using Assets.Classes;
using System.Collections.Generic;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        internal static GameEntity CloseCompany(
            GameContext context,
            GameEntity e)
        {
            // pay to everyone
            PayDividends(context, e, e.companyResource.Resources.money);

            // fire everyone
            foreach (var w in e.team.Workers)
                TeamUtils.FireWorker(e, w.Key, context);

            NotifyAboutProductSupportEnd(e, context);


            foreach (var holding in GetCompanyHoldings(context, e.company.Id, false))
            {
                var c = GetCompanyById(context, holding.companyId);

                DestroyBlockOfShares(context, c, e.shareholder.Id);
            }

            e.isAlive = false;

            return e;
        }

        public static void NotifyAboutProductSupportEnd(GameEntity product, GameContext gameContext)
        {
            if (IsInSphereOfInterest(product, gameContext))
                NotificationUtils.AddNotification(gameContext, new NotificationMessageBankruptcy(product.company.Id));
        }
    }
}
