namespace Assets.Utils
{
    public static partial class TeamUtils
    {
        public static long GetImprovementCost(GameContext gameContext, GameEntity product, TeamUpgrade teamUpgrade)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            var baseMaintenance = NicheUtils.GetBaseMarketingMaintenance(niche).money;

            switch (teamUpgrade)
            {
                case TeamUpgrade.Prototype:
                    return baseMaintenance;
                case TeamUpgrade.Release:
                    return baseMaintenance * 4;
                case TeamUpgrade.Multiplatform:
                    return baseMaintenance * 12;

                //case TeamUpgrade.ClientSupport:
                //case TeamUpgrade.ClientSupportImproved:
                //    return 0;

                //case TeamUpgrade.MarketingBase:
                //    return NicheUtils.GetBaseMarketingMaintenance(niche).money;

                //case TeamUpgrade.MarketingAggressive:
                //    return NicheUtils.GetAggressiveMarketingMaintenance(niche).money;

                //case TeamUpgrade.MarketingAllPlatform:
                //    return 1000;

                default: return 1;
            }
        }
    }
}
