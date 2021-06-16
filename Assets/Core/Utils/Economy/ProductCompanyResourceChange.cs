using System.Linq;

namespace Assets.Core
{
    static partial class Economy
    {
        public static TeamResource GetProductCompanyResourceChange(GameEntity company, GameContext gameContext)
        {
            long money = GetProfit(gameContext, company);
            var ideas = Products.GetExpertiseGain(company);

            var upgrades = company.team.Teams.Select(TeamInfoqwe).Count();

            return new TeamResource(
                upgrades,
                0,
                0,
                ideas,
                money
                );
        }

        public static int TeamInfoqwe (TeamInfo team)
        {
            if (team.TeamType == TeamType.DevelopmentTeam)
                return 3;

            if (team.TeamType == TeamType.CrossfunctionalTeam)
                return 1;

            return 0;
        }
    }
}
