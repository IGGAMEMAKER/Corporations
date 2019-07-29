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
        public static TeamResource GetDevelopmentCost(GameEntity e, GameContext context)
        {
            return new TeamResource(BaseDevCost(e, context), 0, 0, BaseIdeaCost(e, context), 0);
        }

        public static TeamResource GetSegmentImprovementCost(GameEntity e, GameContext gameContext)
        {
            var devCost = GetDevelopmentCost(e, gameContext);

            int multiplier = 3;

            return new TeamResource(
                devCost.programmingPoints / multiplier,
                devCost.programmingPoints / multiplier,
                devCost.managerPoints / multiplier,
                0,
                0);
        }

        public static TeamResource GetSegmentUpgradeCost(GameEntity product, GameContext gameContext, UserType userType)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            bool isInnovation = IsWillInnovate(product, niche, userType);

            var innovationModifier = isInnovation ? 4 : 1;

            var costs = NicheUtils.GetNicheCosts(niche);

            return new TeamResource(costs.TechCost * innovationModifier, 0, 0, costs.IdeaCost * innovationModifier, 0);
        }

        // niche based values
        public static int BaseIdeaCost(GameEntity e, GameContext gameContext)
        {
            var costs = NicheUtils.GetNicheCosts(gameContext, e.product.Niche);

            int baseIdeaCost = costs.IdeaCost;

            return baseIdeaCost;
        }

        public static int BaseDevCost(GameEntity e, GameContext gameContext)
        {
            var costs = NicheUtils.GetNicheCosts(gameContext, e.product.Niche);

            int baseDevCost = costs.TechCost;

            return baseDevCost;
        }
    }
}
