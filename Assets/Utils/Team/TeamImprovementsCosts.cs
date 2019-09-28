namespace Assets.Utils
{
    public static partial class TeamUtils
    {
        public static long GetImprovementCost(GameContext gameContext, GameEntity product, TeamUpgrade teamUpgrade)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            switch (teamUpgrade)
            {
                case TeamUpgrade.DevelopmentPrototype:
                case TeamUpgrade.DevelopmentPolishedApp:
                case TeamUpgrade.DevelopmentCrossplatform:
                    return 0;

                case TeamUpgrade.ClientSupport:
                case TeamUpgrade.ImprovedClientSupport:
                    return 0;

                case TeamUpgrade.MarketingBase:
                    return NicheUtils.GetBaseMarketingMaintenance(niche).money;

                case TeamUpgrade.MarketingAggressive:
                    return NicheUtils.GetAggressiveMarketingMaintenance(niche).money;

                case TeamUpgrade.AllPlatformMarketing:
                    return 1000;

                default: return 1;
            }
        }
    }
}
