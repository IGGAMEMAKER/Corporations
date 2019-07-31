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

            TeamUtils.DismissTeam(e, context);

            NotifyAboutProductSupportEnd(e, context);


            foreach (var holding in GetDaughterCompanies(context, e.company.Id))
                DestroyBlockOfShares(context, holding, e.shareholder.Id);

            e.ReplaceShareholders(new Dictionary<int, BlockOfShares>());

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
