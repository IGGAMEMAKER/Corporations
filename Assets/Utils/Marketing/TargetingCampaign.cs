using System;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static void EnableTargeting(GameEntity company)
        {
            company.isTargeting = true;
        }

        public static int GetTargetingDuration()
        {
            return 30;
        }
    }
}
