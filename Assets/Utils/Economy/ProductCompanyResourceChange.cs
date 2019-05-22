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

            int performance = TeamUtils.GetPerformance(gameContext, productCompany);

            var ideas = 3 * period * performance / 100;

            long money = GetCompanyIncome(productCompany, gameContext) * period * performance / 100 / 30;

            return new TeamResource(
                team.Programmers * Constants.DEVELOPMENT_PRODUCTION_PROGRAMMER * period * performance / 100,
                team.Managers * Constants.DEVELOPMENT_PRODUCTION_MANAGER * period * performance / 100,
                team.Marketers * Constants.DEVELOPMENT_PRODUCTION_MARKETER * period * performance / 100,
                ideas,
                money
                );
        }
    }
}
