using Assets.Classes;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static TeamResource GetTargetingCost(GameContext gameContext, int companyId)
        {
            long adCost = 700;
            // TODO Calculate proper base value!

            return new TeamResource(0, 0, 0, 0, adCost);
        }

        public static long GetTargetingEffeciency(GameContext gameContext, GameEntity e)
        {
            long baseForNiche = 100;

            long brandModifier = e.marketing.BrandPower / 2;

            return baseForNiche * (100 + brandModifier) / 100;
        }
    }
}
