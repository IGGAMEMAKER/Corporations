using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class Companies
    {
        public static GameEntity[] GetDaughterUnhappyCompanies(GameContext gameContext, int companyId)
        {
            return GetDaughterCompanies(gameContext, companyId)
            .Where(p => p.hasProduct && p.team.Morale < 30)
            .ToArray();
        }

        public static GameEntity[] GetDaughterOutdatedCompanies(GameContext gameContext, int companyId)
        {
            return GetDaughterCompanies(gameContext, companyId)
            .Where(p => p.hasProduct && Products.IsOutOfMarket(p, gameContext))
            .ToArray();
        }

        public static bool IsReleaseableApp(GameEntity product, GameContext gameContext)
        {
            var isOutdated = Products.IsOutOfMarket(product, gameContext);

            return !isOutdated && !product.isRelease;
        }

        public static GameEntity[] GetDaughterReleaseableCompanies(GameContext gameContext, int companyId)
        {
            return GetDaughterCompanies(gameContext, companyId)
            .Where(p => p.hasProduct && IsReleaseableApp(p, gameContext))
            .ToArray();
        }
        public static GameEntity[] GetDaughterUpgradableCompanies(GameContext gameContext, int companyId)
        {
            return GetDaughterCompanies(gameContext, companyId)
            .Where(p => p.hasProduct && Products.IsHasAvailableProductImprovements(p))
            .ToArray();
        }
    }
}
