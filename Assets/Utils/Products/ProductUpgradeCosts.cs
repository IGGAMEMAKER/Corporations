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
        public static TeamResource GetDevelopmentCost(GameEntity niche)
        {
            var costs = NicheUtils.GetNicheCosts(niche);

            var dev = costs.TechCost;
            var ideas = costs.IdeaCost;

            return new TeamResource(dev, 0, 0, ideas, 0);
        }

        public static TeamResource GetDevelopmentCost(GameEntity e, GameContext context)
        {
            var niche = NicheUtils.GetNicheEntity(context, e.product.Niche);

            return GetDevelopmentCost(niche);
        }

        public static TeamResource GetSegmentImprovementCost(GameEntity e, GameContext gameContext)
        {
            var devCost = GetDevelopmentCost(e, gameContext);

            return new TeamResource(devCost.programmingPoints, 0, 0, 0, 0);
        }

        public static TeamResource GetSegmentUpgradeCost(GameEntity product, GameContext gameContext, UserType userType)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            bool isInnovation = IsWillInnovate(product, niche, userType);

            var innovationModifier = isInnovation ? 4 : 1;

            var costs = NicheUtils.GetNicheCosts(niche);

            return new TeamResource(costs.TechCost * innovationModifier, 0, 0, costs.IdeaCost * innovationModifier, 0);
        }
    }
}
