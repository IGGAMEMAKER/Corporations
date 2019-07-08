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

            return e;
        }

        public static void NotifyAboutProductSupportEnd(GameEntity product, GameContext gameContext)
        {
            NotificationUtils.AddNotification(gameContext, new NotificationMessageBankruptcy(product.company.Id));
        }
    }
}
