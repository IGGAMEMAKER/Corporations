namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static NicheCostsComponent GetNicheCosts(GameEntity niche)
        {
            return niche.nicheCosts;
        }

        public static NicheCostsComponent GetNicheCosts(GameContext context, NicheType nicheType)
        {
            var niche = GetNicheEntity(context, nicheType);

            return GetNicheCosts(niche);
        }

        internal static object GetMaintenanceCost(GameEntity niche)
        {
            var costs = GetNicheCosts(niche);

            var branding = costs.AdCost * 10 * 5;
            var targeting = costs.AdCost;

            var marketingCost = targeting;

            return marketingCost;
        }
    }
}
