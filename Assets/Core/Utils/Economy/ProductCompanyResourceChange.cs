using System.Linq;

namespace Assets.Core
{
    static partial class Economy
    {
        public static TeamResource GetProductCompanyResourceChange(GameEntity company, GameContext gameContext)
        {
            long money = GetProfit(gameContext, company);
            var ideas = Products.GetExpertiseGain(company);

            var upgrades = 0;

            var date = ScheduleUtils.GetCurrentDate(gameContext);
            if (ScheduleUtils.IsPeriodicalMonthEnd(date))
            {
                upgrades = company.team.Teams.Select(TeamInfoqwe).Sum() + 1;
                upgrades *= 4;
            }

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
            var slots = Teams.GetTeamFeatureSlots(team);

            if (team.TeamType == TeamType.DevelopmentTeam)
                return slots * 2;

            if (team.TeamType == TeamType.CrossfunctionalTeam)
                return slots;

            return 0;
        }
    }
}
