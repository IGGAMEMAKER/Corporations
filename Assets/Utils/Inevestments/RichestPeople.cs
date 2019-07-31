using Entitas;
using System.Linq;

namespace Assets.Utils
{
    public static partial class InvestmentUtils
    {
        public static GameEntity[] GetInfluencialPeople(GameContext gameContext)
        {
            var investors = gameContext
                .GetEntities(GameMatcher.AllOf(GameMatcher.Shareholder, GameMatcher.Human))
                .Where(e => GetInvestorCapitalCost(gameContext, e) > 0)
                .OrderByDescending(e => GetInvestorCapitalCost(gameContext, e))
                .ToArray();

            return investors;
        }
    }
}
