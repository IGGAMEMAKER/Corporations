using Assets.Classes;
using System;

namespace Assets.Utils
{
    static partial class CompanyEconomyUtils
    {
        // TODO move this to better place!
        public static int GetPeriodDuration()
        {
            return 1;
        }

        public static TeamResource GetResourceChange(GameEntity productCompany, GameContext gameContext)
        {
            var team = productCompany.team;

            int period = GetPeriodDuration();

            var ideas = 3 * period;

            long money = GetCompanyIncome(productCompany, gameContext) * period / 30;

            return new TeamResource(
                team.Programmers * Constants.DEVELOPMENT_PRODUCTION_PROGRAMMER * period,
                team.Managers * Constants.DEVELOPMENT_PRODUCTION_MANAGER * period,
                team.Marketers * Constants.DEVELOPMENT_PRODUCTION_MARKETER * period,
                ideas,
                money
                );
        }
    }
}
