using System;
using Assets.Classes;

namespace Assets.Utils
{
    public static class ProductDevelopmentUtils
    {
        public static TeamResource GetDevelopmentCost(GameEntity e)
        {
            int innovationPenalty = IsInnovating(e) ? Constants.DEVELOPMENT_INNOVATION_PENALTY : 0;

            int modifier = 100 + innovationPenalty;

            int devCost = BaseDevCost(e) * modifier / 100;
            int ideaCost = BaseIdeaCost(e) * modifier / 100;

            return new TeamResource(devCost, 0, 0, ideaCost, 0);
        }

        public static bool IsCrunching(GameEntity e)
        {
            return false;
        }

        public static bool IsInnovating(GameEntity e)
        {
            return e.product.ProductLevel >= e.product.ExplorationLevel;
        }

        public static int IterationTimeComplexityModifier(GameEntity e)
        {
            return (int)Math.Log(e.product.ProductLevel + 2, 2);
        }

        internal static int GetIterationTime(GameEntity e)
        {
            return BaseIterationTime(e) * IterationTimeComplexityModifier(e);
        }


        // niche based values
        // replace them with e.niche.devCost or e.iteration.devCost
        // and
        // replace them with e.niche.ideaCost

        public static int BaseIterationTime(GameEntity e)
        {
            return 7;
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
