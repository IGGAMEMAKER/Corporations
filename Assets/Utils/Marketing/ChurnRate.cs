using Assets.Utils.Formatting;
using System;
using System.Text;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static BonusContainer GetChurnBonus(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);
            var state = NicheUtils.GetMarketState(gameContext, c.product.Niche);

            var fromProductLevel = ProductUtils.GetDifferenceBetweenMarketDemandAndAppConcept(c, gameContext);
            var marketIsDying = state == NicheLifecyclePhase.Death;

            return new BonusContainer("Churn rate")
                .RenderTitle()
                .SetDimension("%")
                .Append("Base", 1)
                .Append("Concept", fromProductLevel)
                .AppendAndHideIfZero("Market is DYING", marketIsDying ? 5 : 0);
        }

        public static long GetChurnRate(GameContext gameContext, int companyId)
        {
            return GetChurnBonus(gameContext, companyId).Sum();
        }

        public static long GetChurnClients(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            var churn = GetChurnRate(gameContext, companyId);

            return c.marketing.clients * churn / 100;
        }
    }
}
