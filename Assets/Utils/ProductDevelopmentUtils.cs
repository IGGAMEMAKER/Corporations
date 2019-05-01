using System;
using Assets.Classes;

public enum DevelopmentFocus
{
    Concept,
    Segments,
    BugFixes
}

namespace Assets.Utils
{
    public static class ProductDevelopmentUtils
    {
        public static TeamResource GetDevelopmentCost(GameEntity e, GameContext context)
        {
            int innovationPenalty = IsInnovating(e, context) ? Constants.DEVELOPMENT_INNOVATION_PENALTY : 0;

            int modifier = 100 + innovationPenalty;

            int devCost = BaseDevCost(e) * modifier / 100;
            int ideaCost = BaseIdeaCost(e) * modifier / 100;

            return new TeamResource(devCost, 0, 0, ideaCost, 0);
        }

        public static TeamResource GetSegmentImprovementCost(GameEntity e, GameContext gameContext)
        {
            var devCost = GetDevelopmentCost(e, gameContext);

            int multiplier = 3;

            return new TeamResource(
                devCost.programmingPoints / multiplier,
                devCost.programmingPoints / multiplier,
                devCost.managerPoints / multiplier,
                devCost.ideaPoints / multiplier,
                0);
        }

        internal static void ToggleDevelopment(GameContext gameContext, int companyId, DevelopmentFocus developmentFocus)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            c.ReplaceDevelopmentFocus(developmentFocus);
        }

        public static bool IsCrunching(GameEntity e)
        {
            return false;
        }

        public static int GetMarketRequirementsInNiche(GameContext context, NicheType nicheType)
        {
            return 10;
        }

        public static bool IsInnovating(GameEntity e, GameContext context)
        {
            return e.product.ProductLevel >= GetMarketRequirementsInNiche(context, e.product.Niche);
        }

        public static int BaseDevCost(GameEntity e)
        {
            int baseDevCost = 15;

            return baseDevCost;
        }

        // niche based values
        public static int BaseIdeaCost(GameEntity e)
        {
            int baseIdeaCost = 15;

            return baseIdeaCost;
        }
    }
}
