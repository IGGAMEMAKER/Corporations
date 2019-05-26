using Assets.Utils.Formatting;
using System;
using System.Text;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static long GetChurnRateLoyaltyPart(GameContext gameContext, int companyId, UserType userType)
        {
            var loyalty = GetClientLoyalty(gameContext, companyId, userType);

            if (loyalty < -50) loyalty = -50;
            if (loyalty > 50) loyalty = 50;

            return (50 - loyalty) / 10;
        }

        public static BonusContainer GetChurnBonus(GameContext gameContext, int companyId, UserType userType)
        {
            var baseValue = GetUserTypeBaseValue(userType);
            var fromLoyalty = GetChurnRateLoyaltyPart(gameContext, companyId, userType);

            return new BonusContainer("Churn rate")
                .RenderTitle()
                .SetDimension("%")
                .Append($"Base for {EnumUtils.GetFormattedUserType(userType)}", baseValue)
                .Append("From loyalty", fromLoyalty)
                .AppendAndHideIfZero("From negative loyalty", fromLoyalty < 0 ? 15 : 0);
        }

        public static long GetChurnRate(GameContext gameContext, int companyId, UserType userType)
        {
            return GetChurnBonus(gameContext, companyId, userType).Sum();
        }

        public static float GetPromotionRate(GameContext gameContext, int companyId, UserType userType)
        {
            return GetChurnRate(gameContext, companyId, userType) / 10f;
        }

        public static long GetPromotionClients(GameContext gameContext, int companyId, UserType userType)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            long promotionRate = GetChurnRate(gameContext, companyId, userType);

            return c.marketing.Segments[userType] * promotionRate / 10 / 100;
        }

        public static long GetChurnClients(GameContext gameContext, int companyId, UserType userType)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            var churn = GetChurnRate(gameContext, companyId, userType);

            return c.marketing.Segments[userType] * churn / 100;
        }

        internal static int GetUserTypeBaseValue(UserType userType)
        {
            int multiplier = 1;

            if (userType == UserType.Regular)
                multiplier = 8;
            if (userType == UserType.Newbie)
                multiplier = 30;

            return multiplier;
        }
    }
}
