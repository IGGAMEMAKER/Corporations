using Assets.Utils.Formatting;
using System;
using System.Text;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static long GetChurnRateLoyaltyPart(GameContext gameContext, int companyId)
        {
            var loyalty = GetClientLoyalty(gameContext, companyId);

            if (loyalty < -50) loyalty = -50;
            if (loyalty > 50) loyalty = 50;

            return (50 - loyalty) / 10;
        }

        public static BonusContainer GetChurnBonus(GameContext gameContext, int companyId)
        {
            var baseValue = GetUserTypeBaseValue();
            var fromLoyalty = GetChurnRateLoyaltyPart(gameContext, companyId);

            var c = CompanyUtils.GetCompanyById(gameContext, companyId);
            var state = NicheUtils.GetMarketState(gameContext, c.product.Niche);

            return new BonusContainer("Churn rate")
                .RenderTitle()
                .SetDimension("%")
                .Append($"Base", baseValue)
                .Append("From loyalty", fromLoyalty)
                .AppendAndHideIfZero("Niche is DYING", state == NicheLifecyclePhase.Death ? 5 : 0)
                .AppendAndHideIfZero("From negative loyalty", fromLoyalty < 0 ? 15 : 0);
        }

        public static long GetChurnRate(GameContext gameContext, int companyId)
        {
            return GetChurnBonus(gameContext, companyId).Sum();
        }

        public static float GetPromotionRate(GameContext gameContext, int companyId)
        {
            return GetChurnRate(gameContext, companyId) / 10f;
        }

        public static long GetChurnClients(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            var churn = GetChurnRate(gameContext, companyId);

            return c.marketing.clients * churn / 100;
        }

        internal static int GetUserTypeBaseValue()
        {
            int multiplier = 1;

            return multiplier;
        }
    }
}
