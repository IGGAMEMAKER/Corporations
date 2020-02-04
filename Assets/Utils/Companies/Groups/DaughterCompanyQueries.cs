using System.Linq;

namespace Assets.Core
{
    public static partial class Companies
    {
        public static bool IsReleaseableApp(GameEntity product, GameContext gameContext)
        {
            var hasCriticalMassOfUsers = Marketing.GetClients(product) >= 1000;

            return hasCriticalMassOfUsers && !product.isRelease && !Products.IsOutOfMarket(product, gameContext);
        }


        public static GameEntity[] GetDaughterUnhappyCompanies(GameContext gameContext, int companyId)
        {
            return GetDaughterProductCompanies(gameContext, companyId)
            .Where(p => p.team.Morale < 30)
            .ToArray();
        }

        public static GameEntity[] GetDaughterOutdatedCompanies(GameContext gameContext, int companyId)
        {
            return GetDaughterProductCompanies(gameContext, companyId)
            .Where(p => Products.IsOutOfMarket(p, gameContext))
            .ToArray();
        }

        public static GameEntity[] GetDaughterReleaseableCompanies(GameContext gameContext, int companyId)
        {
            return GetDaughterProductCompanies(gameContext, companyId)
            .Where(p => IsReleaseableApp(p, gameContext))
            .ToArray();
        }

        public static GameEntity[] GetDaughterUpgradableCompanies(GameContext gameContext, int companyId)
        {
            return GetDaughterProductCompanies(gameContext, companyId)
            .Where(Products.IsHasAvailableProductImprovements)
            .ToArray();
        }
    }
}
