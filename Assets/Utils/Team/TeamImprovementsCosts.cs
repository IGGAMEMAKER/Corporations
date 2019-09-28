using Assets.Classes;

namespace Assets.Utils
{
    public static partial class TeamUtils
    {
        public static long GetImprovementCost(GameContext gameContext, GameEntity product, TeamUpgrade teamUpgrade)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            switch (teamUpgrade)
            {
                case TeamUpgrade.Prototype:
                case TeamUpgrade.OnePlatformPaid:
                case TeamUpgrade.CrossplatformDevelopment:
                    return 0;

                case TeamUpgrade.ClientSupport:
                case TeamUpgrade.ImprovedClientSupport:
                    return 0;

                case TeamUpgrade.AggressiveMarketing:
                    return NicheUtils.GetAggressiveMarketingMaintenance(niche).money;
                case TeamUpgrade.BaseMarketing:
                    return NicheUtils.GetBaseMarketingMaintenance(niche).money;
                case TeamUpgrade.AllPlatformMarketing:
                    return 1000;

                default: return 1;
            }
        }
    }
}
