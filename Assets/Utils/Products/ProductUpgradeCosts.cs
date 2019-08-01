using Assets.Classes;

public enum DevelopmentFocus
{
    Concept,

    // loyalty
    Segments, 
    BugFixes
}

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static TeamResource GetBaseDevelopmentCost(GameEntity niche)
        {
            var costs = NicheUtils.GetNicheCosts(niche);

            var dev = costs.TechCost;

            return new TeamResource(dev, 0, 0, 0, 0);
        }

        public static TeamResource GetProductUpgradeCost(GameEntity product, GameContext gameContext)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            var innovationModifier = IsWillInnovate(product, niche) ? 2 : 1;

            return GetBaseDevelopmentCost(niche) * innovationModifier;
        }
    }
}
