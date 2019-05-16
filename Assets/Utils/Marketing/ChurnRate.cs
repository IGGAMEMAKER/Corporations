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

        public static int GetChurnRate(GameContext gameContext, int companyId, UserType userType)
        {
            int baseValue = GetUserTypeBaseValue(userType);
            int fromLoyalty = GetChurnRateLoyaltyPart(gameContext, companyId, userType);

            return baseValue + fromLoyalty;
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
