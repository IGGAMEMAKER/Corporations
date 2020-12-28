using System.Linq;

namespace Assets.Core
{
    public static partial class Companies
    {
        public static bool IsReleaseableApp(GameEntity product)
        {
            return !product.isRelease && product.companyGoal.Goals.Any(g => g.InvestorGoalType == InvestorGoalType.ProductRelease);
        }


        public static GameEntity[] GetDaughterUnhappyCompanies(GameContext gameContext, GameEntity company)
        {
            return GetDaughterProducts(gameContext, company)
            .Where(p => p.team.Morale < 30)
            .ToArray();
        }

        public static GameEntity[] GetDaughterOutdatedCompanies(GameContext gameContext, GameEntity company)
        {
            return GetDaughterProducts(gameContext, company)
            .Where(p => Products.IsOutOfMarket(p, gameContext))
            .ToArray();
        }

        public static GameEntity[] GetDaughterReleaseableCompanies(GameContext gameContext, GameEntity company)
        {
            return GetDaughterProducts(gameContext, company)
            .Where(p => IsReleaseableApp(p))
            .ToArray();
        }
    }
}
