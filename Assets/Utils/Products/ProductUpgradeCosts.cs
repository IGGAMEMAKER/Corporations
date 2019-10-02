using System;
using Assets.Classes;

public enum ConceptStatus
{
    Leader,
    Relevant,
    Outdated
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

        internal static ConceptStatus GetConceptStatus(GameEntity product, GameContext gameContext)
        {
            var isRelevant = IsInMarket(product, gameContext);
            var isOutdated = IsOutOfMarket(product, gameContext);

            if (isRelevant)
                return ConceptStatus.Relevant;

            if (isOutdated)
                return ConceptStatus.Outdated;

            return ConceptStatus.Leader;
        }
    }
}
