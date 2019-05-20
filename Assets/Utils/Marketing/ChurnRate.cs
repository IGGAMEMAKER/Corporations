using Assets.Utils.Formatting;
using System;
using System.Text;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static int GetChurnRateLoyaltyPart(GameContext gameContext, int companyId, UserType userType)
        {
            var loyalty = GetClientLoyalty(gameContext, companyId, userType);

            if (loyalty < -50) loyalty = -50;
            if (loyalty > 50) loyalty = 50;

            return (50 - loyalty) / 10;
        }

        public static BonusContainer GetChurnBonus(GameContext gameContext, int companyId, UserType userType)
        {
            int baseValue = GetUserTypeBaseValue(userType);
            int fromLoyalty = GetChurnRateLoyaltyPart(gameContext, companyId, userType);

            return new BonusContainer("Churn rate", true)
                .SetDimension("%")
                .Append($"Base for {EnumUtils.GetFormattedUserType(userType)}", baseValue)
                .Append("From loyalty", fromLoyalty);
        }

        public static int GetChurnRate(GameContext gameContext, int companyId, UserType userType)
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
                multiplier = 4;
            if (userType == UserType.Newbie)
                multiplier = 9;

            return multiplier;
        }
    }
}
