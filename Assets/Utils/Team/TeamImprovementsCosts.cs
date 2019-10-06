namespace Assets.Utils
{
    public static partial class TeamUtils
    {
        public static long GetImprovementCost(GameContext gameContext, GameEntity product, TeamUpgrade teamUpgrade)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            var baseMaintenance = NicheUtils.GetBaseProductMaintenance(niche);

            switch (teamUpgrade)
            {
                case TeamUpgrade.Prototype:
                    return baseMaintenance;
                case TeamUpgrade.Release:
                    return baseMaintenance * 4;
                case TeamUpgrade.Multiplatform:
                    return baseMaintenance * 12;

                default: return 1;
            }
        }
    }
}
